namespace DataAggregator.Model
{
    public class PhotoAlbum
    {
        public int UserId { get; set; }

        public int AlbumId { get; set; }

        public string AlbumTitle { get; set; }

        public int PhotoId { get; set; }

        public string Url { get; set; }

        public string ThumbnailUrl { get; set; }
    }
}
