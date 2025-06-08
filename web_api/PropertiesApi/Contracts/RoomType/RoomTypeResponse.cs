namespace PropertiesApi.Contracts.RoomType;

public class RoomTypeResponse
{
    public Guid Id { get; }
    public Guid PropertyId { get; set; }
    public string Name { get; set; }
    public decimal DailyPrice { get; set; }
    public string Currency { get; set; }
    public int MinPersonCount { get; set; }
    public int MaxPersonCount { get; set; }
    public string Services { get; set; }
    public string Amenities { get; set; }
    public int AvailableRooms { get; set; }

    public RoomTypeResponse(
        Guid id,
        Guid propertyId,
        string name,
        decimal dailyPrice,
        string currency,
        int minPersonCount,
        int maxPersonCount,
        string services,
        string amenities,
        int availableRooms )
    {
        Id = id;
        PropertyId = propertyId;
        Name = name;
        DailyPrice = dailyPrice;
        Currency = currency;
        MinPersonCount = minPersonCount;
        MaxPersonCount = maxPersonCount;
        Services = services;
        Amenities = amenities;
        AvailableRooms = availableRooms;
    }

    public static RoomTypeResponse FromEntity( Domain.Entities.RoomType roomType )
    {
        return new RoomTypeResponse(
            roomType.Id,
            roomType.PropertyId,
            roomType.Name,
            roomType.DailyPrice,
            roomType.Currency,
            roomType.MinPersonCount,
            roomType.MaxPersonCount,
            roomType.Services,
            roomType.Amenities,
            roomType.AvailableRooms );
    }
}
