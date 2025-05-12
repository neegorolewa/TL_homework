using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Foundation.Repositories;

public class RoomTypesRepository : IRoomTypesRepository
{
    private readonly AppDbContext _dbContext;

    public RoomTypesRepository( AppDbContext dbContext )
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync( RoomType roomType )
    {
        await _dbContext.RoomTypes.AddAsync( roomType );
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync( Guid id )
    {
        RoomType? roomType = await GetByIdAsync( id );

        if ( roomType is null )
        {
            throw new InvalidOperationException( $"RoomType with id {id} doesn't exist" );
        }

        _dbContext.RoomTypes.Remove( roomType );
        await _dbContext.SaveChangesAsync();
    }

    public async Task<bool> ExistsAsync( Guid id )
    {
        return await _dbContext.RoomTypes.AnyAsync( rt => rt.Id == id );
    }

    public async Task<RoomType?> GetByIdAsync( Guid id )
    {
        return await _dbContext.RoomTypes
            .AsNoTracking()
            .FirstOrDefaultAsync( rt => rt.Id == id );
    }

    public async Task<List<RoomType>> GetByPropertyIdAsync( Guid propertyId )
    {
        return await _dbContext.RoomTypes
            .Where( rt => rt.PropertyId == propertyId )
            .ToListAsync();
    }

    public async Task UpdateAsync( RoomType roomType )
    {
        RoomType? existingRoomType = await GetByIdAsync( roomType.Id );

        if ( existingRoomType is null )
        {
            throw new InvalidOperationException( $"RoomType with id {roomType.Id} doesn't exist" );
        }

        existingRoomType.Name = roomType.Name;
        existingRoomType.DailyPrice = roomType.DailyPrice;
        existingRoomType.Currency = roomType.Currency;
        existingRoomType.MinPersonCount = roomType.MinPersonCount;
        existingRoomType.MaxPersonCount = roomType.MaxPersonCount;
        existingRoomType.Services = roomType.Services;
        existingRoomType.Amenities = roomType.Amenities;

        await _dbContext.SaveChangesAsync();
    }
}
