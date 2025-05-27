using Domain.Entities;
using Domain.Repositories;
using Domain.Services;

namespace Infrastructure.Foundation.Services;

public class RoomTypesService : IRoomTypesService
{
    private readonly IRoomTypesRepository _roomTypesRepository;

    public RoomTypesService( IRoomTypesRepository roomTypesRepository )
    {
        _roomTypesRepository = roomTypesRepository;
    }

    public async Task<Guid> AddRoomTypeAsync( Guid propertyId, string name, decimal dailyPrice, string currency, int minPersonCount, int maxPersonCount, string services, string amenities, int availableRooms )
    {
        try
        {
            RoomType roomType = new(
                propertyId,
                name,
                dailyPrice,
                currency,
                minPersonCount,
                maxPersonCount,
                services,
                amenities,
                availableRooms );

            return await _roomTypesRepository.AddAsync( roomType );
        }
        catch ( Exception ex )
        {
            throw new InvalidOperationException( $"Error: {ex.Message}" );
        }
    }

    public async Task<Guid> DeleteRoomTypeAsync( Guid id )
    {
        return await _roomTypesRepository.DeleteAsync( id );
    }

    public async Task<List<RoomType>> GetAllRoomTypesByPropertyIdAsync( Guid propertyId )
    {
        return await _roomTypesRepository.GetByPropertyIdAsync( propertyId );
    }

    public async Task<RoomType?> GetRoomTypeByIdAsync( Guid id )
    {
        return await _roomTypesRepository.GetByIdAsync( id );
    }

    public async Task<Guid> UpdateRoomTypeByIdAsync( Guid id, Guid propertyId, string name, decimal dailyPrice, string currency, int minPersonCount, int maxPersonCount, string services, string amenities, int availableRooms )
    {
        try
        {
            RoomType roomType = new(
                id,
                propertyId,
                name,
                dailyPrice,
                currency,
                minPersonCount,
                maxPersonCount,
                services,
                amenities,
                availableRooms );

            return await _roomTypesRepository.UpdateAsync( roomType );
        }
        catch ( Exception ex )
        {
            throw new InvalidOperationException( $"Error: {ex.Message}" );
        }
    }
}
