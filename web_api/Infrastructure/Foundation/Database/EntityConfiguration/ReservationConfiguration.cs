﻿using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Foundation.Database.EntityConfiguration;

internal class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
{
    public void Configure( EntityTypeBuilder<Reservation> builder )
    {
        builder.ToTable( nameof( Reservation ) )
            .HasKey( r => r.Id );

        builder.Property( r => r.ArrivalDate )
            .IsRequired();

        builder.Property( r => r.DepartureDate )
            .IsRequired();

        builder.Property( r => r.ArrivalTime )
            .IsRequired();

        builder.Property( r => r.DepartureTime )
            .IsRequired();

        builder.Property( r => r.GuestName )
            .HasMaxLength( 250 )
            .IsRequired();

        builder.Property( r => r.GuestPhoneNumber )
            .HasMaxLength( 16 )
            .IsRequired();

        builder.Property( r => r.Total )
            .HasPrecision( 18, 2 )
            .IsRequired();

        builder.Property( r => r.Currency )
            .HasMaxLength( 5 )
            .IsRequired();
    }
}
