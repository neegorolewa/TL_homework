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

    public decimal CalculateTotal( decimal dailyPrice, DateOnly arrivalDate, DateOnly departureDate )
    {
        int countNights = departureDate.DayNumber - arrivalDate.DayNumber;
        decimal total = dailyPrice * countNights;

        return total;
    }
}
