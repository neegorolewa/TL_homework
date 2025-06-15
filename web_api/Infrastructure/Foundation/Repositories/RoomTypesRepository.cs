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

    public async Task<Guid> AddAsync( RoomType roomType )
    {
        if ( !await ExistsAsync( roomType.PropertyId ) )
        {
            throw new InvalidOperationException( $"Property with id '{roomType.PropertyId}' not found" );
        }

        await _dbContext.RoomTypes.AddAsync( roomType );
        await _dbContext.SaveChangesAsync();

        return roomType.Id;
    }

    public async Task<Guid> DeleteAsync( Guid id )
    {
        RoomType? roomType = await GetByIdAsync( id );

        if ( roomType is null )
        {
            throw new InvalidOperationException( $"RoomType with id '{id}' doesn't exist" );
        }

        _dbContext.RoomTypes.Remove( roomType );
        await _dbContext.SaveChangesAsync();

        return id;
    }

    public async Task<bool> ExistsAsync( Guid id )
    {
        return await _dbContext.Properties.AnyAsync( rt => rt.Id == id );
    }

    public async Task<RoomType?> GetByIdAsync( Guid id )
    {
        return await _dbContext.RoomTypes
            .FirstOrDefaultAsync( rt => rt.Id == id );
    }

    public async Task<List<RoomType>> GetByPropertyIdAsync( Guid propertyId )
    {
        return await _dbContext.RoomTypes
            .AsNoTracking()
            .Where( rt => rt.PropertyId == propertyId )
            .ToListAsync();
    }

    public async Task<Guid> UpdateAsync( RoomType roomType )
    {
        RoomType? existingRoomType = await GetByIdAsync( roomType.Id );

        if ( existingRoomType is null )
        {
            throw new InvalidOperationException( $"RoomType with id - {roomType.Id} doesn't exist" );
        }

        if ( !await ExistsAsync( roomType.PropertyId ) )
        {
            throw new InvalidOperationException( $"Property with id '{roomType.PropertyId}' not found" );
        }

        existingRoomType.Name = roomType.Name;
        existingRoomType.DailyPrice = roomType.DailyPrice;
        existingRoomType.Currency = roomType.Currency;
        existingRoomType.MinPersonCount = roomType.MinPersonCount;
        existingRoomType.MaxPersonCount = roomType.MaxPersonCount;
        existingRoomType.Services = roomType.Services;
        existingRoomType.Amenities = roomType.Amenities;
        existingRoomType.AvailableRooms = roomType.AvailableRooms;

        await _dbContext.SaveChangesAsync();

        return roomType.Id;
    }
}
