using Domain.Entities;

namespace Domain.Repositories;

public interface IRoomTypesRepository
{
    Task<List<RoomType>> GetByPropertyIdAsync( Guid propertyId );
    Task<RoomType?> GetByIdAsync( Guid id );
    Task AddAsync( RoomType roomType );
    Task UpdateAsync( RoomType roomType );
    Task<bool> ExistsAsync( Guid id );
    Task DeleteAsync( Guid id );
}
