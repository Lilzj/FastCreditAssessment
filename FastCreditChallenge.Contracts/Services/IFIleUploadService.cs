using FastCreditChallenge.Utilities.Dtos.Response;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastCreditChallenge.Contracts.Services
{
    public interface IFIleUploadService
    {
        Task<ImageResponse> UploadFile(IFormFile file);
    }
}
