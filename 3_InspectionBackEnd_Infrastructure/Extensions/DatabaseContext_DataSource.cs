// Ignore Spelling: sql

using System.Data;
using _3_InspectionBackEnd_Infrastructure.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Npgsql;

namespace Equipment_Infrastructure.DataSources
{
    public class DatabaseContextDataSource : DbContext
    {
        protected string _dapperConstring = "";
        private readonly Infrastructure_Setting _config;
        private IDbConnection? _dbConnectionDappper;


        public DatabaseContextDataSource(DbContextOptions options,
            IOptions<Infrastructure_Setting> config
            ) : base(options)
        {
            _config = config.Value;
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
        {
            return base.SaveChangesAsync(cancellationToken);
        }

        public void Reset()
        {
            var entries = base.ChangeTracker
                                 .Entries()
                                 .Where(e => e.State != EntityState.Unchanged)
                                 .ToArray();

            foreach (var entry in entries)
            {
                switch (entry.State)
                {
                    case EntityState.Modified:
                        entry.State = EntityState.Unchanged;
                        break;
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                    case EntityState.Deleted:
                        entry.Reload();
                        break;
                }
            }
        }
    }
}
