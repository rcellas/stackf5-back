using Microsoft.EntityFrameworkCore;
using StackF5.Entity;

namespace StackF5.Data;

public class ApplicationDbContext(DbContextOptions options): DbContext(options)
{
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<InicidenceTag>()
            .HasKey(x => new {x.IncidenceId, x.TagId});
        modelBuilder.Entity<Incidence>()
            .HasMany(i => i.Comments)
            .WithOne(c => c.Incidence)
            .HasForeignKey(c => c.IncidenceId);
    }
    
    public DbSet<Incidence> Incidences { get; set; } = null!;
    public DbSet<Comment> Comments { get; set; } = null!;
    public DbSet<Tag> Tags { get; set; } = null!;
    public DbSet<InicidenceTag> InicidenceTags { get; set; } = null!;
    public DbSet<Image> Images { get; set; } = null!;
}