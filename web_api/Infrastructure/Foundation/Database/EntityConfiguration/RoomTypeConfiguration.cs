using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Foundation.Database.EntityConfiguration;

internal class RoomTypeConfiguration : IEntityTypeConfiguration<RoomType>
{
    public void Configure( EntityTypeBuilder<RoomType> builder )
    {
        builder.ToTable( nameof( RoomType ) )
            .HasKey( rt => rt.Id );

        builder.Property( rt => rt.Name )
            .HasMaxLength( 100 )
            .IsRequired();

        builder.Property( rt => rt.DailyPrice )
            .HasPrecision( 18, 2 )
            .IsRequired();

        builder.Property( rt => rt.Currency )
            .HasMaxLength( 5 )
            .IsRequired();

        builder.Property( rt => rt.MinPersonCount )
            .IsRequired();

        builder.Property( rt => rt.MaxPersonCount )
            .IsRequired();

        builder.Property( rt => rt.Services )
            .HasMaxLength( 1000 )
            .IsRequired();

        builder.Property( rt => rt.Amenities )
            .HasMaxLength( 1000 )
            .IsRequired();

        builder.HasMany( rt => rt.Reservations )
            .WithOne( r => r.RoomType )
            .HasForeignKey( r => r.RoomTypeId )
            .OnDelete( DeleteBehavior.Cascade );
    }
}
