using System.Net;
using Bloggie.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyApp.Namespace
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IImageRepository _imageRepository;

        public ImageController(IImageRepository imageRepository)
        {
            this._imageRepository = imageRepository;
        }

        [HttpPost]
        public async Task<IActionResult> uploadAsync(IFormFile formFile)
        {
            var imageUrl = await _imageRepository.uploadAsync(formFile);

            if (imageUrl == null)
            {
                return Problem("Something went wrong while uploading the Image", null, (int)HttpStatusCode.InternalServerError);
            }
            return new JsonResult(new { link = imageUrl });

        }
    }
}
