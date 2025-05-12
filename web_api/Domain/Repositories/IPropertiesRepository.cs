using Domain.Entities;

namespace Domain.Repositories;

public interface IPropertiesRepository
{
    Task<List<Property>> GetAllAsync();
    Task<Property?> GetByIdAsync( Guid id );
    Task AddAsync( Property property );
    Task UpdateAsync( Property property );
    Task DeleteAsync( Guid id );
}
