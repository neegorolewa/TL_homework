using System.Diagnostics;

namespace Domain.Entities;

public class Reservation
{
    public Guid Id { get; }
    public Guid PropertyId { get; set; }
    public Guid RoomTypeId { get; set; }
    public DateOnly ArrivalDate { get; set; }
    public DateOnly DepartureDate { get; set; }
    public TimeOnly ArrivalTime { get; set; }
    public TimeOnly DepartureTime { get; set; }
    public string GuestName { get; set; }
    public string GuestPhoneNumber { get; set; }
    public decimal Total { get; set; }
    public string Currency { get; set; }
    public Property? Property { get; set; }
    public RoomType? RoomType { get; set; }

    public Reservation(
        Guid propertyId,
        Guid roomTypeId,
        DateOnly arrivalDate,
        DateOnly departureDate,
        TimeOnly arrivalTime,
        TimeOnly departureTime,
        string guestName,
        string guestPhoneNumber,
        decimal total,
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

        if ( arrivalDate.ToDateTime( arrivalTime ) >= departureDate.ToDateTime( departureTime ) )
        {
            throw new ArgumentOutOfRangeException( "Departure date and time must be after arrival date and time" );
        }

        CheckStringIfEmpty( guestName, nameof( guestName ) );
        CheckStringIfEmpty( guestPhoneNumber, nameof( guestPhoneNumber ) );
        CheckStringIfEmpty( currency, nameof( currency ) );

        Id = Guid.NewGuid();
        PropertyId = propertyId;
        RoomTypeId = roomTypeId;
        ArrivalDate = arrivalDate;
        DepartureDate = departureDate;
        ArrivalTime = arrivalTime;
        DepartureTime = departureTime;
        GuestName = guestName;
        GuestPhoneNumber = guestPhoneNumber;
        Total = total;
        Currency = currency;
    }

    private void CheckStringIfEmpty( string value, string nameOfValue )
    {
        if ( string.IsNullOrEmpty( value ) )
        {
            throw new ArgumentException( $"'{nameOfValue}' can't be null or empty", nameOfValue );

        }
    }
}
