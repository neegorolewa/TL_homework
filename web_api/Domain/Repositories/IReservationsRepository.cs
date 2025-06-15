using Domain.Contracts;
using Domain.Entities;

namespace Domain.Repositories;

public interface IReservationsRepository
{
    Task<Reservation?> GetByIdAsync( Guid id );
    Task<List<Reservation>> GetAllAsync(
        Guid? propertyId,
        Guid? roomTypeId,
        DateOnly? arrivalDate,
        DateOnly? departureDate,
        string? guestName,
        string? guestPhoneNumber );
    Task<Guid> AddAsync( Reservation reservation );
    Task<Guid> DeleteAsync( Guid id );
    Task<bool> ExistsPropertyAsync( Guid propertyId );
    Task<bool> ExistsRoomTypeAsync( Guid roomTypeId );
    Task<decimal> GetRoomTypeDailyPriceAsync( Guid roomTypeId );
    Task<List<PropertyWithRoomTypeDto>> SearchOptions(
        DateOnly? arrivalDate,
        DateOnly? departureTime,
        int? guestsNumber,
        string? city
        );
}
