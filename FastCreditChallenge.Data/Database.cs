using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastCreditChallenge.Data
{
    public class Database
    {
        public static void Initialize(IApplicationBuilder app)
        {
            var context = app.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<FastCreditContext>();

            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.MigrateAsync().Wait();
            }
        }
    }
}
