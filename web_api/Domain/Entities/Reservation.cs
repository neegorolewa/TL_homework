using Domain.Helpers;

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
        Guard.AgainstEmptyGuid( propertyId, nameof( propertyId ) );
        Guard.AgainstEmptyGuid( roomTypeId, nameof( roomTypeId ) );

        Guard.AgainstInvalidDateRange( arrivalDate, departureDate, arrivalTime, departureTime );

        Guard.AgainstNullOrEmpty( guestName, nameof( guestName ) );
        Guard.AgainstNullOrEmpty( guestPhoneNumber, nameof( guestPhoneNumber ) );
        Guard.AgainstNullOrEmpty( currency, nameof( currency ) );

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
}
