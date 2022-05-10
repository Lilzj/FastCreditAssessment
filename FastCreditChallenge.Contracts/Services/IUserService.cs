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
        Task<ObjectResult> GetCustomersAsync();
        Task<ObjectResult> AddCustomerAsync(CustomerToAddDto model);
        Task<ObjectResult> GetCustomerWIthOrdersAsync(long customerId);
        Task<IActionResult> UpdateCustomerAsync(long customerId, CustomerToUpdateDto model);
        Task<IActionResult> DeleteCustomerAsync(long customerId);
        Task<ObjectResult> GetCustomersByName(string name);
    }
}
