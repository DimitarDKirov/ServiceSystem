using System;
using System.Data.Entity;
using System.Linq;
using Microsoft.AspNet.Identity.EntityFramework;
using ServiceSystem.Data.Common.Contracts;
using ServiceSystem.Data.Common.Models;
using ServiceSystem.Data.Models;
using ServiceSystem.Infrastructure.DateProvider;

namespace ServiceSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>, IEfDbRepositorySaveChanges
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public virtual IDbSet<Order> Orders { get; set; }

        public virtual IDbSet<Brand> Brand { get; set; }

        public virtual IDbSet<Category> Category { get; set; }

        public virtual IDbSet<Customer> Customer { get; set; }

        public virtual IDbSet<Note> Note { get; set; }

        public virtual IDbSet<Part> Parts { get; set; }

        public virtual IDbSet<PartsInOrder> PartsInOrders { get; set; }

        public virtual IDbSet<Unit> Units { get; set; }

        // idendtiity usage
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public override int SaveChanges()
        {
            this.ApplyAuditInfoRules();
            return base.SaveChanges();
        }

        private void ApplyAuditInfoRules()
        {
            // Approach via @julielerman: http://bit.ly/123661P
            foreach (var entry in
                this.ChangeTracker.Entries()
                    .Where(
                        e =>
                        e.Entity is IAuditInfo && ((e.State == EntityState.Added) || (e.State == EntityState.Modified))))
            {
                var entity = (IAuditInfo)entry.Entity;
                if (entry.State == EntityState.Added && entity.CreatedOn == default(DateTime))
                {
                    entity.CreatedOn = DateTimeProvider.Current.UtcNow;
                }
                else
                {
                    entity.ModifiedOn = DateTimeProvider.Current.UtcNow;
                }
            }
        }
    }
}
