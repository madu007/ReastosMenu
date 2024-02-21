using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using ReastosMenu.Data;
using ReastosMenu.Entities;

namespace ReastosMenu.Services
{
    public interface IMenuImageService
    {
        Task<string> UploadAsync(IFormFile file);
    }
    public class MenuImageService : IMenuImageService
    {
        private readonly IConfiguration _configuration;
        private readonly Account _account;
        public MenuImageService(IConfiguration configuration)
        {
            _configuration=configuration;
            _account = new Account(
                _configuration.GetSection("Cloudinary")["CloudName"],
                _configuration.GetSection("Cloudinary")["ApiKey"],
                _configuration.GetSection("Cloudinary")["ApiSecret"]
                );
        }
        public async Task<string> UploadAsync(IFormFile file)
        {
            var client = new Cloudinary(_account);

            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(file.FileName, file.OpenReadStream()),
                DisplayName = file.FileName,
            };
            var uploadResult = await client.UploadAsync(uploadParams);

            if (uploadResult != null && uploadResult.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return uploadResult.SecureUrl.ToString();
            }

            return null;
        }











        //public MenuImage GetMenuImages()
        //{
        //    var menuImageList = _context.MenuImages
        //        .OrderBy(m => m.Id);

        //    return (MenuImage)menuImageList;
        //}
    }
}
