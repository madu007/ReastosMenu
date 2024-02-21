using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReastosMenu.Services;
using System.Net;

namespace ReastosMenu.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IMenuImageService _menuImageService;

        public ImageController(IMenuImageService menuImageService)
        {
            _menuImageService=menuImageService;
        }
        [HttpPost]
        public async Task<IActionResult> UploadAsync(IFormFile file)
        {
            var imageURL = await _menuImageService.UploadAsync(file);

            if (imageURL == null)
            {
                return Problem("Something went wrong!", null, (int)HttpStatusCode.InternalServerError);
            }

            return new JsonResult(new { link = imageURL });
        }
    }
}
