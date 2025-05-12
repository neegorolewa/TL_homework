using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Foundation.Repositories;

public class ReservationsRepository : IReservationsRepository
{
    private readonly AppDbContext _dbContext;

    public ReservationsRepository( AppDbContext dbContext )
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync( Reservation reservation )
    {
        await _dbContext.Reservations.AddAsync( reservation );
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync( Guid id )
    {
        Reservation? reservation = await GetByIdAsync( id );

        if ( reservation is null )
        {
            throw new InvalidOperationException( $"Reservation with id - {id} doesn't exist" );
        }

        _dbContext.Reservations.Remove( reservation );
        await _dbContext.SaveChangesAsync();
    }

    public async Task<List<Reservation>> GetAllAsync()
    {
        return await _dbContext.Reservations
             .ToListAsync();
    }

    public async Task<Reservation?> GetByIdAsync( Guid id )
    {
        return await _dbContext.Reservations
            .AsNoTracking()
            .FirstOrDefaultAsync( r => r.Id == id );
    }
}
