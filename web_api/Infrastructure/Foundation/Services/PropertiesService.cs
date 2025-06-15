using Domain.Entities;
using Domain.Repositories;
using Domain.Services;

namespace Infrastructure.Foundation.Services;

public class PropertiesService : IPropertiesService
{
    private readonly IPropertiesRepository _propertiesRepository;

    public PropertiesService( IPropertiesRepository propertiesRepository )
    {
        _propertiesRepository = propertiesRepository;
    }

    public async Task<Guid> AddPropertyAsync( string name, string country, string city, string address, decimal latitude, decimal longitude )
    {
        try
        {
            Property property = new(
                name,
                country,
                city,
                address,
                latitude,
                longitude );

            return await _propertiesRepository.AddAsync( property );
        }
        catch ( Exception ex )
        {
            throw new InvalidOperationException( $"Error: {ex.Message}" );
        }
    }

    public async Task<Guid> DeletePropertyAsync( Guid id )
    {
        return await _propertiesRepository.DeleteAsync( id );
    }

    public async Task<List<Property>> GetAllPropertiesAsync()
    {
        List<Property> properties = await _propertiesRepository.GetAllAsync();

        return properties;
    }

    public async Task<Property?> GetPropertyByIdAsync( Guid id )
    {
        Property? property = await _propertiesRepository.GetByIdAsync( id );

        return property;
    }

    public async Task<Guid> UpdatePropertyAsync( Guid id, string name, string country, string city, string address, decimal latitude, decimal longitude )
    {
        try
        {
            Property property = new(
                id,
                name,
                country,
                city,
                address,
                latitude,
                longitude );

            return await _propertiesRepository.UpdateAsync( property );
        }
        catch ( Exception ex )
        {
            throw new InvalidOperationException( $"Error: {ex.Message}");
        }
    }
}
