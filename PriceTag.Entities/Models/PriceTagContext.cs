using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Repository.Pattern.Ef6;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace PriceTag.Entities.Models
{
    public partial class PriceTagContext : DataContext
    {
        static PriceTagContext()
        {
            Database.SetInitializer<PriceTagContext>(null);
        }

        public PriceTagContext()
            : base("Name=PriceTagContext")
        {
        }

        public DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
