using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PriceTag.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PriceTag.DAL
{
    public class AccountDBContext : IdentityDbContext<User>
    {
        public AccountDBContext()
            : base("AccountDBContext")
        {
        }

        static AccountDBContext()
        {  
            // Set the database intializer which is run once during application start
            // This seeds the database with admin user credentials and admin role
            Database.SetInitializer<AccountDBContext>(new ApplicationDBInitializer());
            
        }

        public DbSet<UserProfile> UserProfile { get; set; }

        public static AccountDBContext Create()
        {
            return new AccountDBContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Customize Identity Table Name
            modelBuilder.Entity<IdentityRole>().HasKey<string>(r => r.Id);
            modelBuilder.Entity<IdentityUserRole>().HasKey(r => new { r.RoleId, r.UserId });
            modelBuilder.Entity<User>().ToTable("Users", "dbo");
            modelBuilder.Entity<IdentityUserRole>().ToTable("UserRole", "dbo");
            modelBuilder.Entity<IdentityRole>().ToTable("Role", "dbo");
            modelBuilder.Entity<IdentityUserLogin>().ToTable("UserLogin", "dbo");
            modelBuilder.Entity<IdentityUserClaim>().ToTable("UserClaim", "dbo");

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }


}