using Microsoft.EntityFrameworkCore;

namespace TogepiManager.DbManagement
{
    /// <summary>
    /// Code-First planning of the events management database.
    /// </summary>
    public class TogepiContext : DbContext
    {
        #region DbSets
        /// <summary>
        /// The events table representation.
        /// </summary>
        public DbSet<Event> Events { get; set; }

        /// <summary>
        /// The reports table representation.
        /// </summary>
        public DbSet<Report> Reports { get; set; }
        #endregion

        /// <summary>
        /// The controller.
        /// </summary>
        /// <param name="options">Database creation options</param>
        public TogepiContext(DbContextOptions<TogepiContext> options) : base(options) { }

        /// <summary>
        /// The method that runs when creating the SQL tables.
        /// </summary>
        /// <param name="modelBuilder">SQL model builder</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Defining the tables
            modelBuilder.Entity<Event>().Property(e => e.Id).ValueGeneratedNever();
            modelBuilder.Entity<Event>().Ignore(e => e.Location).ToTable("Events");
            modelBuilder.Entity<Report>().ToTable("Reports");
        }
    }
}