using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using DataAggregator.Model;
using Microsoft.Extensions.Logging;

namespace DataAggregator.Services
{
    public class PhotoAlbumService
    {
        private readonly ILogger<PhotoAlbumService> _logger;

        public PhotoAlbumService(ILogger<PhotoAlbumService> logger)
        {
            _logger = logger;
        }

        public async Task<IList<PhotoAlbum>> GetPhotoAlbums(int userId)
        {
            IList<PhotoAlbum> photoAlbums = new List<PhotoAlbum>();

            using var albumClient = new HttpClient();

            StringBuilder albumsUrl = new StringBuilder();
            albumsUrl.Append("https://jsonplaceholder.typicode.com/albums");
            if (userId != -1)
            {
                albumsUrl.Append("?userId=");
                albumsUrl.Append(userId);
            }

            IList<Album> albums =
                await Get<Album>(albumsUrl.ToString(), albumClient);
            albums = albums.Where(x => x != null).ToList();

            IList<Photo> photos = new List<Photo>();
            using var photosClient = new HttpClient();
            StringBuilder photosUrl = new StringBuilder();
            photosUrl.Append("https://jsonplaceholder.typicode.com/photos");
            if (albums.Any())
            {
                photosUrl.Append("?");
                foreach (var album in albums)
                {
                    photosUrl.Append("&albumId=");
                    photosUrl.Append(album.Id);
                }

                photos =
                    await Get<Photo>(photosUrl.ToString(), photosClient);
                photos = photos.Where(x => x != null).ToList();
            }

            photoAlbums = albums.Join(photos,
                arg => arg.Id,
                arg => arg.AlbumId,
                (albums, photos) => new PhotoAlbum {
                    AlbumId = albums.Id,
                    AlbumTitle = albums.Title,
                    UserId = albums.UserId,
                    PhotoId = photos.Id,
                    ThumbnailUrl = photos.ThumbnailUrl,
                    Url = photos.Url
                }).ToList();

            return photoAlbums;
        }

        private async Task<IList<T>> Get<T>(string url, HttpClient client)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            using (var response = await client.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    string data = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<List<T>>(data);
                }

                _logger.LogError("Internal server error");
            }
            return null;
        }
    }
}
