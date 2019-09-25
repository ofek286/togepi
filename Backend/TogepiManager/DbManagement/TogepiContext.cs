using Microsoft.EntityFrameworkCore;

namespace TogepiManager.DbManagement
{
    public class TogepiContext : DbContext
    {
        public TogepiContext(DbContextOptions<TogepiContext> options) : base(options) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            // TODO
        }
    }
}