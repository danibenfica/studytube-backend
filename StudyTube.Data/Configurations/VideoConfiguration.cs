using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudyTube.Domain.Entities;
using StudyTube.Domain.ValueObjects;

namespace StudyTube.Data.Configurations;

public class VideoConfiguration : IEntityTypeConfiguration<Video>
{
    public void Configure(EntityTypeBuilder<Video> builder)
    {
        builder.ToTable("Videos");
        builder.HasKey(v => v.Id);

        builder.Property(v => v.YouTubeId)
            .HasConversion(id => id.Value, value => YouTubeVideoId.Create(value))
            .HasMaxLength(11)
            .IsRequired();
        builder.HasIndex(v => v.YouTubeId).IsUnique();

        builder.Property(v => v.Title).HasMaxLength(300).IsRequired();
        builder.Property(v => v.ChannelTitle).HasMaxLength(200);
        builder.Property(v => v.ThumbnailUrl).HasMaxLength(500);
    }
}