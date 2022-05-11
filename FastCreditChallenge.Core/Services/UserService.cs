using AutoMapper;
using FastCreditChallenge.Contracts.Services;
using FastCreditChallenge.Core.Security;
using FastCreditChallenge.Data.Settings;
using FastCreditChallenge.Entities;
using FastCreditChallenge.Utilities;
using FastCreditChallenge.Utilities.Dtos.Request;
using FastCreditChallenge.Utilities.Dtos.Response;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace FastCreditChallenge.Core.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IOptions<JWTData> _JWTData;
        private readonly IMapper _mapper;
        private readonly IFIleUploadService _fileUpload;

        public UserService(IMapper mapper, UserManager<User> userManager,
                              SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager, IOptions<JWTData> JWTData, IFIleUploadService fIleUpload)
        {
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _JWTData = JWTData;
            _fileUpload = fIleUpload;
        }

        public async Task<ObjectResult> AddUserAsync(AddUserRequestDto model)
        {
            //check if user already exist
            if (await _userManager.FindByEmailAsync(model.Email) != null)
                return ServiceResponse.BadRequest("Email already exists");

            var imageUrl = await _fileUpload.UploadFile(model.Photo);

            if (string.IsNullOrWhiteSpace(imageUrl.ImageUrl))
                return ServiceResponse.BadRequest("Image upload failed");

            var user = _mapper.Map<User>(model);

            user.Photo = imageUrl.ImageUrl;
            user.UserName = user.Email;
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                //Check if role exists and create if not
                var role = await _roleManager.FindByNameAsync("Customer");
                if (role == null)
                    await _roleManager.CreateAsync(new IdentityRole("Customer"));

                await _userManager.AddToRoleAsync(user, role.Name);

                var userToReturn = _mapper.Map<UserResponse>(user);

                return ServiceResponse.Created("GetUserById", new { id = userToReturn.Id }, userToReturn);
            }
            else
            {
                return ServiceResponse.BadRequest("Error in creating new user");
            }


        }

        public async Task<ObjectResult> Login(LoginRequestDto model)
        {
            //Get user from Database
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
                return ServiceResponse.NotFound("User Not Found");

            //signin user
            var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);

            if (!result.Succeeded)
            {
                return ServiceResponse.BadRequest("invalid credentials");
            }

            try
            {
                //Get user roles
                var roles = await _userManager.GetRolesAsync(user);
                var token = JWTService.GenerateToken(user, roles, _JWTData);//Generate token

                var userToReturn = new LoginResponseDto
                {
                    UserId = user.Id,
                    Role = string.Join(",", await _userManager.GetRolesAsync(user)),
                    Token = token.ToString()
                };

                return ServiceResponse.Ok(userToReturn);
            }
            catch (Exception)
            {
                throw new Exception("Something went wrong generating token");
            }
        }

        public async Task<ObjectResult> GetUserAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                return ServiceResponse.NotFound($"User with {userId} not found");

            var response = _mapper.Map<UserResponse>(user);

            return ServiceResponse.Ok(response);
        }

        public async Task<IActionResult> UpdateUserAsync(string id, UpdateUserRequestDto model)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
                return ServiceResponse.NotFound("User was not found");

            var res = _mapper.Map(model, user);

            var result = await _userManager.UpdateAsync(res);

            return ServiceResponse.Ok("Updated successfully");
        }

        public async Task<IActionResult> DeleteUserAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return ServiceResponse.NotFound("User not found");

            await _userManager.DeleteAsync(user);

            return ServiceResponse.NoContent();
        }

        public async Task<ObjectResult> GetUsersAsync()
        {
            var users = _userManager.Users;
            if (users == null)
                return ServiceResponse.NotFound("No user in the database");

            var usersToReturn = _mapper.Map<IEnumerable<UserResponse>>(users);

            return ServiceResponse.Ok(usersToReturn);
        }

        public async Task<ObjectResult> Search(string name)
        {
            var users = await _userManager.Users.Where(x => x.FirstName == name).ToListAsync();
            if (users == null)
                return ServiceResponse.NotFound("No users in the database");

            var usersToReturn = _mapper.Map<IEnumerable<UserResponse>>(users);
            return ServiceResponse.Ok(usersToReturn);
        }
    }
}
