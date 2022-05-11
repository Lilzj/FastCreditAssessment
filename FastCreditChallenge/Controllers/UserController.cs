using FastCreditChallenge.Contracts.Services;
using FastCreditChallenge.Data.Settings;
using FastCreditChallenge.Entities;
using FastCreditChallenge.Utilities;
using FastCreditChallenge.Utilities.Dtos.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace FastCreditChallenge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        //Injected Services

        private readonly IOptions<JWTData> _JWTData;
        private readonly IUserService _userService;
        private readonly SignInManager<User> _signInManager;

        public UserController(IUserService userService,
                              IOptions<JWTData> JWTData,
                              SignInManager<User> signInManager)
        {
            _JWTData = JWTData;
            _userService = userService;
            _signInManager = signInManager;
        }


        [HttpPost("register")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddUser(AddUserRequestDto model)
        {
            if (!ModelState.IsValid)
                return ServiceResponse.BadRequest(ModelState);

            return await _userService.AddUserAsync(model);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestDto model)
        {
            if (!ModelState.IsValid)
                return ServiceResponse.BadRequest(ModelState);

            return await _userService.Login(model);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateUserAsync([FromRoute]string id, UpdateUserRequestDto model)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return ServiceResponse.BadRequest("No id entered");
            }

            if (!ModelState.IsValid)
            {
                return ServiceResponse.BadRequest(ModelState);
            }

            return await _userService.UpdateUserAsync(id, model);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return ServiceResponse.BadRequest("No id entered");
            }

            return await _userService.DeleteUserAsync(id);
        }

        [HttpGet("{id)", Name = nameof(GetUserById))]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetUserById([FromRoute] string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return ServiceResponse.BadRequest("No id entered");
            }

            return await _userService.GetUserAsync(id);
        }


        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllUsers()
            => await _userService.GetUsersAsync();

        [HttpGet("search")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetUsersByName([FromQuery] string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return ServiceResponse.BadRequest("No name entered");
            }
            return await _userService.Search(name);
        }

        [HttpPost]
        [Route("Logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            HttpContext.Session.Clear();
            return Ok();

        }
    }
}
