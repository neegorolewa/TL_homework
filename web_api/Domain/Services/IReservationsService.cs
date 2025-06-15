using Domain.Contracts;
using Domain.Entities;

namespace Domain.Services;

public interface IReservationsService
{
    Task<Reservation?> GetReservationByIdAsync( Guid id );
    Task<List<Reservation>> GetAllReservationsAsync(
        Guid? propertyId,
        Guid? roomTypeId,
        DateOnly? arrivalDate,
        DateOnly? departureDate,
        string? guestName,
        string? guestPhoneNumber );
    Task<Guid> AddReservationAsync(
        Guid propertyId,
        Guid roomTypeId,
        DateOnly arrivalDate,
        DateOnly departureDate,
        TimeOnly arrivalTime,
        TimeOnly departureTime,
        string guestName,
        string guestPhoneNumber,
        string currency );
    Task<Guid> DeleteReservationAsync( Guid reservationId );
    Task<List<PropertyWithRoomTypeDto>> GetSearchOptions(
        DateOnly? arrivalDate,
        DateOnly? departureTime,
        int? guestsNumber,
        string? city );
}
