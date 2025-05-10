using System.Diagnostics;

namespace Domain.Entities;

public class Reservation
{
    public Guid Id { get; }
    public Guid PropertyId { get; private set; }
    public Guid RoomTypeId { get; private set; }
    public DateOnly ArrivalDate { get; private set; }
    public DateOnly DepartureDate { get; private set; }
    public TimeOnly ArrivalTime { get; private set; }
    public TimeOnly DepartureTime { get; private set; }
    public string GuestName { get; private set; }
    public string GuestPhoneNumber { get; private set; }
    public decimal Total { get; private set; }
    public string Currency { get; private set; }

    public Reservation(
        Guid propertyId,
        Guid roomTypeId,
        DateOnly arrivalDate,
        DateOnly departureDate,
        TimeOnly arrivalTime,
        TimeOnly departureTime,
        string guestName,
        string guestPhoneNumber,
        decimal dailyPrice,
        string currency
        )
    {
        if ( propertyId == Guid.Empty )
        {
            throw new ArgumentNullException( "PropertyId can't be empty" );
        }

        if ( roomTypeId == Guid.Empty )
        {
            throw new ArgumentNullException( "RoomTypeId can't be empty" );
        }

        if ( arrivalDate >= departureDate )
        {
            throw new ArgumentOutOfRangeException( "Departure date must be after arrival date" );
        }

        if ( string.IsNullOrEmpty( guestName ) )
        {
            throw new ArgumentException( $"'{nameof( guestName )}' can't be null or empty", nameof( guestName ) );
        }

        if ( string.IsNullOrEmpty( guestPhoneNumber ) )
        {
            throw new ArgumentException( $"'{nameof( guestPhoneNumber )}' can't be null or empty", nameof( guestPhoneNumber ) );
        }

        if ( string.IsNullOrEmpty( currency ) )
        {
            throw new ArgumentException( $"'{nameof( currency )}' can't be null or empty", nameof( currency ) );
        }

        if ( dailyPrice <= 0 )
        {
            throw new ArgumentOutOfRangeException( "Daily price must be positive" );
        }

        Id = Guid.NewGuid();
        PropertyId = propertyId;
        RoomTypeId = roomTypeId;
        ArrivalDate = arrivalDate;
        DepartureDate = departureDate;
        ArrivalTime = arrivalTime;
        DepartureTime = departureTime;
        GuestName = guestName;
        GuestPhoneNumber = guestPhoneNumber;
        Currency = currency;
        Total = CalculateTotal( dailyPrice, arrivalDate, departureDate );
    }


    private decimal CalculateTotal( decimal dailyPrice, DateOnly arrivalDate, DateOnly departureDate )
    {
        int countNights = departureDate.DayNumber - arrivalDate.DayNumber;
        decimal total = dailyPrice * countNights;

        return total;
    }
}
