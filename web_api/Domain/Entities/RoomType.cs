using Domain.Helpers;

namespace Domain.Entities;

public class RoomType
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
    public Property? Property { get; set; }

    public List<Reservation> Reservations { get; private set; } = [];

    public RoomType(
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
        DomainValidator.EmptyGuid( propertyId, nameof( propertyId ) );

        DomainValidator.NullOrEmpty( name, nameof( name ) );
        DomainValidator.NullOrEmpty( currency, nameof( currency ) );
        DomainValidator.NullOrEmpty( services, nameof( services ) );
        DomainValidator.NullOrEmpty( amenities, nameof( amenities ) );

        DomainValidator.InvalidDailyPrice( dailyPrice, nameof( dailyPrice ) );
        DomainValidator.InvalidPersonCount( minPersonCount, maxPersonCount );
        DomainValidator.InvalidAvailableRooms( availableRooms, nameof( availableRooms ) );

        Id = Guid.NewGuid();
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

    public RoomType(
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
        DomainValidator.EmptyGuid( id, nameof( id ) );
        DomainValidator.EmptyGuid( propertyId, nameof( propertyId ) );

        DomainValidator.NullOrEmpty( name, nameof( name ) );
        DomainValidator.NullOrEmpty( currency, nameof( currency ) );
        DomainValidator.NullOrEmpty( services, nameof( services ) );
        DomainValidator.NullOrEmpty( amenities, nameof( amenities ) );

        DomainValidator.InvalidDailyPrice( dailyPrice, nameof( dailyPrice ) );
        DomainValidator.InvalidPersonCount( minPersonCount, maxPersonCount );
        DomainValidator.InvalidAvailableRooms( availableRooms, nameof( availableRooms ) );

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
}
