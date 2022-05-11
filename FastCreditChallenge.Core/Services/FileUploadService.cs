using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using FastCreditChallenge.Contracts.Services;
using FastCreditChallenge.Data.Settings;
using FastCreditChallenge.Utilities.Dtos.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastCreditChallenge.Core.Services
{
    public class FileUploadService : IFIleUploadService
    {
        private readonly Cloudinary _cloudinary;
        private readonly CloudinaryConfig _config;

        public FileUploadService(IOptions<CloudinaryConfig> configuration)
        {
            _config = configuration.Value;

            Account account = new Account(
                _config.CloudName,
                _config.ApiKey,
                _config.ApiSecret
                );

            _cloudinary = new Cloudinary(account);
        }

         public async Task<ImageResponse> UploadFile(IFormFile file)
        {
            var uploadResults = new ImageUploadResult();

            using (var fs = file.OpenReadStream())
            {
                var imageUploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(file.FileName, fs),
                    Transformation = new Transformation().Width(300).Height(300)
                                                         .Crop("fill").Gravity("face")
                };

                uploadResults = await _cloudinary.UploadAsync(imageUploadParams);

            }
            var res = new ImageResponse
            {
                PublicId = uploadResults.PublicId,
                ImageUrl = uploadResults.Url.ToString(),
            };

            return res;
        }
    }
  
}
