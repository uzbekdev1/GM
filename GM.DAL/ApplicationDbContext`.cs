using System;
using System.Threading;
using System.Threading.Tasks;
using GM.DAL.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace GM.DAL
{
    public partial class ApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions options)
            : base(options)
        {    
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            OnBeforeSaving();

            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            OnBeforeSaving();

            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void OnBeforeSaving()
        {
            var entries = ChangeTracker.Entries();

            foreach (var entry in entries)
            {
                if (entry.Entity is AuditEntity)
                {
                    var now = DateTime.Now;

                    switch (entry.State)
                    {
                        case EntityState.Modified:
                        {
                            entry.CurrentValues["Modified"] = now;
                        }
                            break;

                        case EntityState.Added:
                        {
                            entry.CurrentValues["Added"] = now;
                            entry.CurrentValues["Modified"] = now;
                        }
                            break;
                    }
                }
            }
        }

    }
}
