using FastCreditChallenge.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastCreditChallenge.Contracts.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetUserIdAsync(long userId, bool trackChanges = false);
        Task<User> GetUserWithOrdersAsync(long userId, bool trackChanges = false);
        Task<IEnumerable<User>> GetUsers(bool trackChanges = false);
        Task<IEnumerable<User>> SearchByName(string name, bool trackChanges = false);
        void AddUser(User user);
        void UpdateUser(User user);
        void DeleteUser(User user);
    }
}
