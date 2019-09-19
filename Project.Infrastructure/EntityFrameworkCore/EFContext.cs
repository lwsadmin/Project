using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Infrastructure.EntityFrameworkCore
{
    public class EFContext : DbContext
    {
        public DbSet<Permissions> Permissions { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<User> User { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;Database=Project;User=sa;Password=123456;");
        }

        public EFContext() {}
        public EFContext(DbContextOptions<EFContext> options) : base(options)
        {

        }

    }
}
