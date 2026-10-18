using Microsoft.EntityFrameworkCore;
using StudyTube.Domain.Entities;

namespace StudyTube.Data;

public class StudyTubeDbContext(DbContextOptions<StudyTubeDbContext> options) : DbContext(options)
{
    public DbSet<Video> Videos => Set<Video>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(StudyTubeDbContext).Assembly);
    }
}