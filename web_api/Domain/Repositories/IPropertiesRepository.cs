using Domain.Entities;

namespace Domain.Repositories;

public interface IPropertiesRepository
{
    Task<List<Property>> GetAllAsync();
    Task<Property?> GetByIdAsync( Guid id );
    Task<Guid> AddAsync( Property property );
    Task<Guid> UpdateAsync( Property property );
    Task<Guid> DeleteAsync( Guid id );
}
