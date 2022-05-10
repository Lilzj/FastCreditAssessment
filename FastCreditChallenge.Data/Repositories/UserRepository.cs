using FastCreditChallenge.Contracts.Repositories;
using FastCreditChallenge.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastCreditChallenge.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        public void AddUser(User user)
        {
            throw new NotImplementedException();
        }

        public void DeleteUser(User user)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUserIdAsync(long userId, bool trackChanges = false)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetUsers(bool trackChanges = false)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUserWithOrdersAsync(long userId, bool trackChanges = false)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> SearchByName(string name, bool trackChanges = false)
        {
            throw new NotImplementedException();
        }

        public void UpdateUser(User user)
        {
            throw new NotImplementedException();
        }
    }
}
