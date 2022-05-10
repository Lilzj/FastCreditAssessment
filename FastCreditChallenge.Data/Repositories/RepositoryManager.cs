using FastCreditChallenge.Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastCreditChallenge.Data.Repositories
{
    public class RepositoryManager : IRepositoryManager
    {
        public IUserRepository Customers => throw new NotImplementedException();

        public Task<bool> Save()
        {
            throw new NotImplementedException();
        }
    }
}
