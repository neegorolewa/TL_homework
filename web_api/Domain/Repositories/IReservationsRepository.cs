using Domain.Entities;

namespace Domain.Repositories;

public interface IReservationsRepository
{
    //еще подумать какие методы нужны
    Task<Reservation?> GetByIdAsync( Guid id );
    Task<List<Reservation>> GetAllAsync();
    Task AddAsync( Reservation reservation );
    Task DeleteAsync( Guid id );
}
