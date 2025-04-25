using Accommodations.Models;

namespace Accommodations;

public class BookingService : IBookingService
{
    private List<Booking> _bookings = [];

    private readonly IReadOnlyList<RoomCategory> _categories =
    [
        new RoomCategory { Name = "Standard", BaseRate = 100, AvailableRooms = 10 },
        new RoomCategory { Name = "Deluxe", BaseRate = 200, AvailableRooms = 5 }
    ];

    private readonly IReadOnlyList<User> _users =
    [
        new User { Id = 1, Name = "Alice Johnson" },
        new User { Id = 2, Name = "Bob Smith" },
        new User { Id = 3, Name = "Charlie Brown" },
        new User { Id = 4, Name = "Diana Prince" },
        new User { Id = 5, Name = "Evan Wright" }
    ];

    public Booking Book( int userId, string categoryName, DateTime startDate, DateTime endDate, Currency currency )
    {
        //check booking if start date earlier than now date, else throw ArgumentException
        if ( startDate < DateTime.Now )
        {
            throw new ArgumentException( "Start date can't be earlier than now date" );
        }

        //Add check startDate = endDate
        if ( endDate <= startDate )
        {
            throw new ArgumentException( "End date cannot be earlier than start date and in the same date" );
        }

        //add lower category name
        RoomCategory? selectedCategory = _categories.FirstOrDefault( c => c.Name.ToLower() == categoryName.ToLower() );
        if ( selectedCategory == null )
        {
            throw new ArgumentException( "Category not found" );
        }

        if ( selectedCategory.AvailableRooms <= 0 )
        {
            throw new ArgumentException( "No available rooms" );
        }

        User? user = _users.FirstOrDefault( u => u.Id == userId );
        if ( user == null )
        {
            throw new ArgumentException( "User not found" );
        }

        int days = ( endDate - startDate ).Days;
        decimal currencyRate = GetCurrencyRate( currency );
        decimal totalCost = CalculateBookingCost( selectedCategory.BaseRate, days, userId, currencyRate );

        //delete null
        Booking booking = new()
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            StartDate = startDate,
            EndDate = endDate,
            RoomCategory = selectedCategory,
            Cost = totalCost,
            Currency = currency
        };

        _bookings.Add( booking );
        selectedCategory.AvailableRooms--;

        return booking;
    }

    public void CancelBooking( Guid bookingId )
    {
        Booking? booking = _bookings.FirstOrDefault( b => b.Id == bookingId );

        if ( booking == null )
        {
            throw new ArgumentException( $"Booking with id: '{bookingId}' does not exist" );
        }

        if ( booking.StartDate <= DateTime.Now )
        {
            throw new ArgumentException( "Start date cannot be earlier than now date" );
        }

        Console.WriteLine( $"Refund of {booking.Cost} {booking.Currency}" );
        _bookings.Remove( booking );
        //changed FirstOrDefault -> First, remove ? in RoomCategory?
        RoomCategory category = _categories.First( c => c.Name == booking.RoomCategory.Name );
        category.AvailableRooms++;
    }

    private static decimal CalculateDiscount( int userId )
    {
        return 0.1m;
    }

    public Booking? FindBookingById( Guid bookingId )
    {
        return _bookings.FirstOrDefault( b => b.Id == bookingId );
    }

    public IEnumerable<Booking> SearchBookings( DateTime startDate, DateTime endDate, string categoryName )
    {
        IQueryable<Booking> query = _bookings.AsQueryable();

        query = query.Where( b => b.StartDate >= startDate );

        //booking dates must be included in the filter search
        query = query.Where( b => b.EndDate <= endDate );

        if ( !string.IsNullOrEmpty( categoryName ) )
        {
            //add ToLower() to avoid users errors 
            query = query.Where( b => b.RoomCategory.Name.ToLower() == categoryName.ToLower() );
        }

        return query.ToList();
    }

    public decimal CalculateCancellationPenaltyAmount( Booking booking )
    {
        if ( booking.StartDate <= DateTime.Now )
        {
            throw new ArgumentException( "Start date cannot be earlier than now date" );
        }

        //error, must be start date - now date
        int daysBeforeArrival = ( booking.StartDate - DateTime.Now ).Days;

        //fixed divide by 0
        decimal currencyRate = GetCurrencyRate( booking.Currency );

        return daysBeforeArrival == 0
            ? ( 5000.0m / currencyRate )
            : ( 5000.0m ) / ( daysBeforeArrival * currencyRate );
    }

    private static decimal GetCurrencyRate( Currency currency )
    {
        //currencyRate = 1m - it's useless
        decimal currencyRate = currency switch
        {
            Currency.Usd => ( decimal )( new Random().NextDouble() * 100 ) + 1,
            Currency.Cny => ( decimal )( new Random().NextDouble() * 12 ) + 1,
            Currency.Rub => 1m,
            _ => throw new ArgumentOutOfRangeException( nameof( currency ), currency, null )
        };

        return currencyRate;
    }

    private static decimal CalculateBookingCost( decimal baseRate, int days, int userId, decimal currencyRate )
    {
        //fixed currency convertion
        decimal cost = ( baseRate * days ) / currencyRate;
        decimal discount = CalculateDiscount( userId );
        decimal totalCost = cost * ( 1 - discount );
        return totalCost;
    }
}
