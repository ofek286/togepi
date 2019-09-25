using Microsoft.EntityFrameworkCore;

namespace TogepiManager.DbManagement {
    public class TogepiContext : DbContext {
        #region DbSets
        public DbSet<Event> Events { get; set; }

        public DbSet<Report> Reports { get; set; }
        #endregion

        public TogepiContext(DbContextOptions<TogepiContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Event>().Ignore(e => e.Location).ToTable("Events");
            modelBuilder.Entity<Report>().ToTable("Reports");
        }
    }
}