using FastCreditChallenge.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastCreditChallenge.Data
{
    public class FastCreditContext : IdentityDbContext<User>
    {
        public FastCreditContext(DbContextOptions<FastCreditContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}
