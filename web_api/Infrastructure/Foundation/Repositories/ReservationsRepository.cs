using Domain.Contracts;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Foundation.Repositories;

public class ReservationsRepository : IReservationsRepository
{
    private readonly AppDbContext _dbContext;

    public ReservationsRepository( AppDbContext dbContext )
    {
        _dbContext = dbContext;
    }

    public async Task<Guid> AddAsync( Reservation reservation )
    {
        if ( !await ExistsPropertyAsync( reservation.PropertyId ) )
        {
            throw new InvalidOperationException( $"Property with id '{reservation.PropertyId}' not found" );
        }

        if ( !await ExistsRoomTypeAsync( reservation.RoomTypeId ) )
        {
            throw new InvalidOperationException( $"RooMType with id '{reservation.RoomTypeId}' not found" );
        }

        await _dbContext.Reservations.AddAsync( reservation );
        await _dbContext.SaveChangesAsync();

        return reservation.Id;
    }

    public async Task<Guid> DeleteAsync( Guid id )
    {
        Reservation? reservation = await GetByIdAsync( id );

        if ( reservation is null )
        {
            throw new InvalidOperationException( $"Reservation with id - {id} doesn't exist" );
        }

        _dbContext.Reservations.Remove( reservation );
        await _dbContext.SaveChangesAsync();

        return id;
    }

    public async Task<bool> ExistsPropertyAsync( Guid propertyId )
    {
        return await _dbContext.Properties.AnyAsync( p => p.Id == propertyId );
    }

    public async Task<bool> ExistsRoomTypeAsync( Guid roomTypeId )
    {
        return await _dbContext.RoomTypes.AnyAsync( rt => rt.Id == roomTypeId );
    }

    public async Task<List<Reservation>> GetAllAsync( Guid? propertyId, Guid? roomTypeId, DateOnly? arrivalDate, DateOnly? departureDate, string? guestName, string? guestPhoneNumber )
    {
        IQueryable<Reservation> reservations = _dbContext.Reservations
            .AsNoTracking();

        if ( propertyId.HasValue )
        {
            reservations = reservations
                .Where( r => r.PropertyId == propertyId );
        }

        if ( roomTypeId.HasValue )
        {
            reservations = reservations
                .Where( r => r.RoomTypeId == roomTypeId );
        }

        if ( arrivalDate.HasValue )
        {
            reservations = reservations
                .Where( r => r.ArrivalDate == arrivalDate );
        }

        if ( departureDate.HasValue )
        {
            reservations = reservations
                .Where( r => r.DepartureDate == departureDate );
        }

        if ( !string.IsNullOrEmpty( guestName ) )
        {
            reservations = reservations
                .Where( r => r.GuestName == guestName );
        }

        if ( !string.IsNullOrEmpty( guestPhoneNumber ) )
        {
            reservations = reservations
                .Where( r => r.GuestPhoneNumber == guestPhoneNumber );
        }

        return await reservations.ToListAsync();
    }

    public async Task<Reservation?> GetByIdAsync( Guid id )
    {
        return await _dbContext.Reservations
            .FirstOrDefaultAsync( r => r.Id == id );
    }

    public async Task<decimal> GetRoomTypeDailyPriceAsync( Guid roomTypeId )
    {
        RoomType? roomType = await _dbContext.RoomTypes.FirstOrDefaultAsync( rt => rt.Id == roomTypeId );

        if ( roomType is null )
        {
            throw new InvalidOperationException( $"RoomType with id - {roomTypeId} doesn't exist" );
        }

        return roomType.DailyPrice;
    }

    public async Task<List<PropertyWithRoomTypeDto>> SearchOptions( DateOnly? arrivalDate, DateOnly? departureDate, int? guestsNumber, string? city )
    {
        if ( ( arrivalDate.HasValue && !departureDate.HasValue ) ||
            ( !arrivalDate.HasValue && departureDate.HasValue ) )
        {
            throw new ArgumentException( "Filter by date must have 2 dates" );
        }

        IQueryable<Property> query = _dbContext.Properties
            .AsNoTracking()
            .Include( p => p.RoomTypes )
                .ThenInclude( rt => rt.Reservations )
            .AsQueryable();

        if ( !string.IsNullOrWhiteSpace( city ) )
        {
            query = query.Where( p => p.City == city );
        }

        List<PropertyWithRoomTypeDto> filteredProperties = await query
         .Select( p => new PropertyWithRoomTypeDto
         {
             Property = PropertyDto.FromEntity( p ),
             RoomTypes = p.RoomTypes
                 .Where( rt =>
                     ( !guestsNumber.HasValue || rt.MaxPersonCount >= guestsNumber ) && rt.AvailableRooms > 0 &&
                     !rt.Reservations.Any( r =>
                         r.DepartureDate > arrivalDate &&
                         r.ArrivalDate < departureDate )
                 )
                 .Select( rt => RoomTypeDto.FromEntity( rt ) )
                 .ToList()
         } )
         .ToListAsync();

        return filteredProperties
            .Where( p => p.RoomTypes.Any() )
            .ToList();
    }
}
