using Domain.Entities;

namespace Domain.Services;

public interface IRoomTypesService
{
    Task<List<RoomType>> GetAllRoomTypesByPropertyIdAsync( Guid propertyId );
    Task<RoomType?> GetRoomTypeByIdAsync( Guid id );
    Task<Guid> AddRoomTypeAsync(
        Guid propertyId,
        string name,
        decimal dailyPrice,
        string currency,
        int minPersonCount,
        int maxPersonCount,
        string services,
        string amenities,
        int availableRooms );
    Task<Guid> UpdateRoomTypeByIdAsync(
        Guid id,
        Guid propertyId,
        string name,
        decimal dailyPrice,
        string currency,
        int minPersonCount,
        int maxPersonCount,
        string services,
        string amenities,
        int availableRooms );
    Task<Guid> DeleteRoomTypeAsync( Guid id );
}
