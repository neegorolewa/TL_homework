using Domain.Entities;

namespace Domain.Repositories;

public interface IRoomTypesRepository
{
    Task<List<RoomType>> GetByPropertyIdAsync( Guid propertyId );
    Task<RoomType?> GetByIdAsync( Guid id );
    Task<Guid> AddAsync( RoomType roomType );
    Task<Guid> UpdateAsync( RoomType roomType );
    Task<bool> ExistsAsync( Guid id );
    Task<Guid> DeleteAsync( Guid id );
}
