using Microsoft.EntityFrameworkCore;
using RuneSharper.Shared.Entities;
using System.Reflection;

namespace RuneSharper.Data {
    public class RuneSharperContext : DbContext {
        public RuneSharperContext(DbContextOptions options) : base(options) {
        }

        public DbSet<Account> Accounts { get; set; } = default!;
        public DbSet<Snapshot> Snapshots { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            base.OnConfiguring(optionsBuilder);
        }
    }
}
