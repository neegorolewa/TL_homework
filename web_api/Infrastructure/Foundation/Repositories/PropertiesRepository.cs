using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Foundation.Repositories;

public class PropertiesRepository : IPropertiesRepository
{
    private readonly AppDbContext _dbContext;

    public PropertiesRepository( AppDbContext dbContext )
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync( Property property )
    {
        await _dbContext.Properties.AddAsync( property );
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync( Guid id )
    {
        Property? property = await GetByIdAsync( id );

        if ( property is null )
        {
            throw new InvalidOperationException( $"Property with id - {id} doesn't exist" );
        }

        _dbContext.Properties.Remove( property );
        await _dbContext.SaveChangesAsync();
    }

    public async Task<List<Property>> GetAllAsync()
    {
        return await _dbContext.Properties
            .ToListAsync();
    }

    public async Task<Property?> GetByIdAsync( Guid id )
    {
        return await _dbContext.Properties
            .FirstOrDefaultAsync( p => p.Id == id );
    }

    public async Task UpdateAsync( Property property )
    {
        Property? existingProperty = await GetByIdAsync( property.Id );

        if ( existingProperty is null )
        {
            throw new InvalidOperationException( $"Property with id - {property.Id} doesn't exist" );
        }

        existingProperty.Name = property.Name;
        existingProperty.City = property.City;
        existingProperty.Country = property.Country;
        existingProperty.Address = property.Address;
        existingProperty.Latitude = property.Latitude;
        existingProperty.Longitude = property.Longitude;

        await _dbContext.SaveChangesAsync();

    }
}
