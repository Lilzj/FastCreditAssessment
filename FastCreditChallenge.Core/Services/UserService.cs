using FastCreditChallenge.Contracts.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastCreditChallenge.Core.Services
{
    public class UserService : IUserService
    {
        public Task<ObjectResult> AddCustomerAsync(CustomerToAddDto model)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> DeleteCustomerAsync(long customerId)
        {
            throw new NotImplementedException();
        }

        public Task<ObjectResult> GetCustomersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ObjectResult> GetCustomersByName(string name)
        {
            throw new NotImplementedException();
        }

        public Task<ObjectResult> GetCustomerWIthOrdersAsync(long customerId)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> UpdateCustomerAsync(long customerId, CustomerToUpdateDto model)
        {
            throw new NotImplementedException();
        }
    }
}
