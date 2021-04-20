using Microsoft.EntityFrameworkCore;

namespace ck.assecor.assessment_backend.data.EfContext
{
    /// <summary>
    /// DbContext for Ef
    /// </summary>
    public class EfDbContext : DbContext
    {
        public EfDbContext(DbContextOptions<EfDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EfPersonDbo>()
            .HasKey(p => new { p.Id});

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<EfPersonDbo> Persons { get; set; }
    }
}
