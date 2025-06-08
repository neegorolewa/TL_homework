using Domain.Entities;

namespace Domain.Contracts;

public class RoomTypeDto
{
    public Guid Id { get; }
    public string Name { get; set; }
    public decimal DailyPrice { get; set; }
    public string Currency { get; set; }
    public int MinPersonCount { get; set; }
    public int MaxPersonCount { get; set; }
    public string Services { get; set; }
    public string Amenities { get; set; }
    public int AvailableRooms { get; set; }

    public RoomTypeDto(
        Guid id,
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
        Name = name;
        DailyPrice = dailyPrice;
        Currency = currency;
        MinPersonCount = minPersonCount;
        MaxPersonCount = maxPersonCount;
        Services = services;
        Amenities = amenities;
        AvailableRooms = availableRooms;
    }

    public static RoomTypeDto FromEntity( RoomType roomType )
    {
        return new RoomTypeDto(
            roomType.Id,
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
