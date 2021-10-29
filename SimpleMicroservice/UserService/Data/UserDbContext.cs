using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserService.Data.model;

namespace UserService.Data
{
    public class UserDbContext : DbContext
    {
        private readonly string _connectionString;

        private UserDbContext()
        {

        }

        public UserDbContext(DbContextOptions<UserDbContext> options)
          : base(options)
        {
        }

        private UserDbContext(DbContextOptions<UserDbContext> options, IConfiguration configuration)
            : base(options)
        {
            _connectionString = configuration.GetConnectionString("Microservice").ToString();
        }


        public DbSet<User> Users { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_connectionString,
                        sqlServerOptionsAction: sqlOptions =>
                        {
                            sqlOptions.EnableRetryOnFailure(
                            maxRetryCount: 10,
                            maxRetryDelay: TimeSpan.FromSeconds(30),
                            errorNumbersToAdd: null);
                            sqlOptions.CommandTimeout((int)TimeSpan.FromMinutes(10).TotalSeconds);
                        });
                base.OnConfiguring(optionsBuilder);
            }


        }

    }
} 
