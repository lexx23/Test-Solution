using Box.Dal.Entity;
using Microsoft.EntityFrameworkCore;

namespace Box.Dal.MsSql;

public class BoxContext : DbContext
{
    public DbSet<Entity.Box> Boxes { get; set; }
    
    public BoxContext(DbContextOptions<BoxContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new BoxConfiguration());
    }
}