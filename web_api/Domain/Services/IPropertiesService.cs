using Domain.Entities;

namespace Domain.Services;

public interface IPropertiesService
{
    Task<List<Property>> GetAllPropertiesAsync();
    Task<Property?> GetPropertyByIdAsync( Guid id );
    Task<Guid> AddPropertyAsync(
        string name,
        string country,
        string city,
        string address,
        decimal latitude,
        decimal longitude );

    Task<Guid> UpdatePropertyAsync(
        Guid id,
        string name,
        string country,
        string city,
        string address,
        decimal latitude,
        decimal longitude );

    Task<Guid> DeletePropertyAsync( Guid id );
}
