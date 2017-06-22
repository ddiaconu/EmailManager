using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmailManager.Data
{
    public class DbContextFactory : IDbContextFactory<DatabaseContext>
    {
        public DatabaseContext Create(DbContextFactoryOptions options)
        {
            return Create(AppSettings.Get<string>("DbContextConnection"));
        }

        public DatabaseContext Create(string connectionString)
        {
            var builder = new DbContextOptionsBuilder<DatabaseContext>();
            builder.UseSqlServer(connectionString);
            return new DatabaseContext(builder.Options);
        }
    }
}
