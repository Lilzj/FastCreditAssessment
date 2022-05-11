using FastCreditChallenge.Utilities.Dtos.Request;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastCreditChallenge.Contracts.Services
{
    public interface IUserService
    {
        Task<ObjectResult> GetUsersAsync();
        Task<ObjectResult> AddUserAsync(AddUserRequestDto model);
        Task<ObjectResult> Login(LoginRequestDto model);
        Task<ObjectResult> GetUserAsync(string userId);
        Task<IActionResult> UpdateUserAsync(string id, UpdateUserRequestDto model);
        Task<IActionResult> DeleteUserAsync(string Id);
        Task<ObjectResult> Search(string name);
    }
}
