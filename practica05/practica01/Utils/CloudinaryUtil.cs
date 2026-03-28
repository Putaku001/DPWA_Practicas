using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace practica01.Utils
{
    public class CloudinaryUtil
    {
        public static string? UploadImage(IFormFile? photoFile, IConfiguration config)
        {
            if (photoFile == null || photoFile.Length == 0)
            {
                return null;
            }

            string cloudName = config["Cloudinary:CloudName"] ?? string.Empty;
            string apiKey = config["Cloudinary:ApiKey"] ?? string.Empty;
            string apiSecret = config["Cloudinary:ApiSecret"] ?? string.Empty;

            Account account = new Account(cloudName, apiKey, apiSecret);
            Cloudinary cloudinary = new Cloudinary(account);
            cloudinary.Api.Secure = true;

            using (var stream = photoFile.OpenReadStream())
            {
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(photoFile.FileName, stream)
                };

                var uploadResult = cloudinary.Upload(uploadParams);

                if (uploadResult.Error != null)
                {
                    throw new Exception($"Cloudinary upload error: {uploadResult.Error.Message}");
                }

                return uploadResult.SecureUrl?.ToString();
            }
        }
    }
}
