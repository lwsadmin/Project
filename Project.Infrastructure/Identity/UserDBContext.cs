
using Domain.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Identity.Entity
{
    public class UserDBContext : IdentityDbContext<User>
    {
        public UserDBContext() { }

        public UserDBContext(string con = "Server=.;Database=Project;User=sa;Password=123456;")
            : base(con)
        {

        }
        public DbSet<User> TBUsers { get; set; }
    }
}
