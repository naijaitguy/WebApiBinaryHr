using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiBinaryHr.Entities
{
    public class BinaryHrDbContext:IdentityDbContext<User>
    {
        public BinaryHrDbContext(DbContextOptions <BinaryHrDbContext> options)
          : base(options)
        {

        }



        public virtual DbSet<Profile> Profiles { get; set; }
        public virtual DbSet<Academic> Academics { get; set; }
        public virtual DbSet<Experience> Experiences { get; set; }
        public virtual DbSet<Application> Applications { get; set; }
        public virtual DbSet<Job> Jobs { get; set; }


    }
}
