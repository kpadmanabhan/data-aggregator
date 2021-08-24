using System.Threading.Tasks;
using DataAggregator.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DataAggregator.Controllers
{
    [ApiController]
    public class PhototAlbumController : ControllerBase
    {
        private readonly ILogger<PhototAlbumController> _logger;
        private readonly PhotoAlbumService photoAlbumService;

        public PhototAlbumController(ILogger<PhototAlbumController> logger, PhotoAlbumService photoAlbumService)
        {
            _logger = logger;
            this.photoAlbumService = photoAlbumService;
        }

        // GET list of all photo albums for all users
        // TODO: Introduce searching and pagination with offset as this is a heavy operation depending on the data available
        [HttpGet]
        [Route("PhotoAlbum")]
        public async Task<ActionResult> GetPhotoAlbums()
        {
            return Ok(await photoAlbumService.GetPhotoAlbums(-1));
        }

        // GET list of photo albums for the specific user id
        [HttpGet]
        [Route("PhotoAlbum/{userId:int}")]
        public async Task<ActionResult> GetPhotoAlbumsForUser(int userId)
        {
            return Ok(await photoAlbumService.GetPhotoAlbums(userId));
        }
    }
}
