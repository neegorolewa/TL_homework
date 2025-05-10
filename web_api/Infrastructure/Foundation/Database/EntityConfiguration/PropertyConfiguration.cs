using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Foundation.Database.EntityConfiguration;

internal class PropertyConfiguration : IEntityTypeConfiguration<Property>
{
    public void Configure( EntityTypeBuilder<Property> builder )
    {
        builder.ToTable( nameof( Property ) )
            .HasKey( p => p.Id );

        builder.Property( p => p.Name )
            .HasMaxLength( 100 )
            .IsRequired();

        builder.Property( p => p.Country )
            .HasMaxLength( 50 )
            .IsRequired();

        builder.Property( p => p.City )
            .HasMaxLength( 50 )
            .IsRequired();

        builder.Property( p => p.Address )
            .HasMaxLength( 200 )
            .IsRequired();

        builder.Property( p => p.Latitude )
            .HasPrecision( 18, 6 )
            .IsRequired();

        builder.Property( p => p.Longitude )
            .HasPrecision( 18, 6 )
            .IsRequired();

        builder.HasMany( p => p.RoomTypes )
            .WithOne()
            .HasForeignKey( rt => rt.PropertyId );

        builder.HasMany( p => p.Reservations )
            .WithOne()
            .HasForeignKey( r => r.PropertyId );
    }
}
