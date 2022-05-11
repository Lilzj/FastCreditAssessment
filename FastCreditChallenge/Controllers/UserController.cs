using FastCreditChallenge.Contracts.Services;
using FastCreditChallenge.Utilities;
using FastCreditChallenge.Utilities.Dtos.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FastCreditChallenge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> AddCustomer(AddUserRequestDto model)
        {
            if (!ModelState.IsValid)
            {
                return ServiceResponse.BadRequest(ModelState);
            }

            return await _userService.AddCustomerAsync(model);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomerAsync(long id, UpdateUserRequestDto model)
        {
            if (!ModelState.IsValid)
            {
                return ServiceResponse.BadRequest(ModelState);
            }

            return await _userService.UpdateCustomerAsync(id, model);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(long id)
        {
            return await _userService.DeleteCustomerAsync(id);
        }

        [HttpGet("{id:long}", Name = nameof(GetCustomerById))]
        public async Task<IActionResult> GetCustomerById([FromRoute] long id)
        {
            return await _userService.GetCustomerWIthOrdersAsync(id);
        }


        [HttpGet]
        public async Task<IActionResult> GetAllCustomers()
            => await _userService.GetCustomersAsync();

        [HttpGet("search")]
        public async Task<IActionResult> GetCustomersByName([FromQuery] string query)
        {
            return await _userService.GetCustomersByName(query);
        }
    }
}
