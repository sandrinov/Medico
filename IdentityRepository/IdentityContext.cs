using IdentityServerRepository.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityServerRepository
{
    public class IdentityContext : DbContext
    {
        public IdentityContext() : base("name=IdentityContext")
        {
        }
        public virtual DbSet<CustomUser> CustomUsers { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CustomUser>()
                .Property(x => x.Subject)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }
}
