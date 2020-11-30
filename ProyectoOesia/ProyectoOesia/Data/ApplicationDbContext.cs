using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Data
{
    public partial class ApplicationDbContext : IdentityDbContext<User, IdentityRole, string>
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Models.Activity> Activity { get; set; }

        //public virtual DbSet<Activity> Activities { get; set; }
        //public virtual DbSet<Company> Companies { get; set; }
        //public virtual DbSet<Contact> Contacts { get; set; }
        //public virtual DbSet<SocialMedia> SocialMedias { get; set; }
        //public virtual DbSet<User> Users { get; set; }


    }
}
