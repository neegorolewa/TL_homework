using Domain.Contracts;
using Domain.Entities;
using Domain.Repositories;
using Domain.Services;

namespace Infrastructure.Foundation.Services;

public class ReservationsService : IReservationsService
{
    private readonly IReservationsRepository _reservationsRepository;

    public ReservationsService( IReservationsRepository reservationsRepository )
    {
        _reservationsRepository = reservationsRepository;
    }

    public async Task<Guid> AddReservationAsync( Guid propertyId, Guid roomTypeId, DateOnly arrivalDate, DateOnly departureDate, TimeOnly arrivalTime, TimeOnly departureTime, string guestName, string guestPhoneNumber, string currency )
    {
        try
        {
            decimal dailyPrice = await _reservationsRepository.GetRoomTypeDailyPriceAsync( roomTypeId );
            decimal total = CalculateTotal( dailyPrice, arrivalDate, departureDate, arrivalTime, departureTime );

            Reservation reservation = new(
                propertyId,
                roomTypeId,
                arrivalDate,
                departureDate,
                arrivalTime,
                departureTime,
                guestName,
                guestPhoneNumber,
                total,
                currency );

            return await _reservationsRepository.AddAsync( reservation );

        }
        catch ( Exception ex )
        {
            throw new InvalidOperationException( $"Error: {ex.Message}" );
        }
    }

    public async Task<Guid> DeleteReservationAsync( Guid reservationId )
    {
        return await _reservationsRepository.DeleteAsync( reservationId );
    }

    public async Task<List<Reservation>> GetAllReservationsAsync( Guid? propertyId, Guid? roomTypeId, DateOnly? arrivalDate, DateOnly? departureDate, string? guestName, string? guestPhoneNumber )
    {
        return await _reservationsRepository.GetAllAsync(
            propertyId,
            roomTypeId,
            arrivalDate,
            departureDate,
            guestName,
            guestPhoneNumber );
    }

    public async Task<Reservation?> GetReservationByIdAsync( Guid id )
    {
        return await _reservationsRepository.GetByIdAsync( id );
    }

    public async Task<List<PropertyWithRoomTypeDto>> GetSearchOptions( DateOnly? arrivalDate, DateOnly? departureTime, int? guestsNumber, string? city )
    {
        return await _reservationsRepository.SearchOptions(
            arrivalDate,
            departureTime,
            guestsNumber,
            city );
    }

    private decimal CalculateTotal(
        decimal dailyPrice,
        DateOnly arrivalDate,
        DateOnly departureDate,
        TimeOnly arrivalTime,
        TimeOnly departureTime )
    {
        DateTime arrival = arrivalDate.ToDateTime( arrivalTime );
        DateTime departure = departureDate.ToDateTime( departureTime );

        int countNights = ( departure - arrival ).Days;

        decimal total = countNights * dailyPrice;

        return total;
    }
}
