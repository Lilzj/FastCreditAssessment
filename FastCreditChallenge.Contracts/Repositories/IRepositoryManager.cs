using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastCreditChallenge.Contracts.Repositories
{
    public interface IRepositoryManager
    {
        IUserRepository Customers { get; }
        Task<bool> Save();
    }
}
