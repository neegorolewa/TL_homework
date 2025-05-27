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

    public IReadOnlyList<Reservation> Reservations { get; private set; } = [];

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
        if ( propertyId == Guid.Empty )
        {
            throw new ArgumentException( $"'{nameof( propertyId )} can't be empty'", nameof( propertyId ) );
        }

        CheckStringIfEmpty( name, nameof( name ) );
        CheckStringIfEmpty( currency, nameof( currency ) );
        CheckStringIfEmpty( services, nameof( services ) );
        CheckStringIfEmpty( amenities, nameof( amenities ) );


        if ( dailyPrice <= 0 )
        {
            throw new ArgumentOutOfRangeException( $"'{nameof( DailyPrice )} must be grater than 0'" );
        }

        if ( minPersonCount <= 0 || maxPersonCount <= 0 || maxPersonCount < minPersonCount )
        {
            throw new ArgumentOutOfRangeException( "Invalid person count range." );
        }

        if ( availableRooms < 1 )
        {
            throw new ArgumentOutOfRangeException( $"{nameof( availableRooms )} must be grater than 1" );
        }

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
        if ( id == Guid.Empty )
        {
            throw new ArgumentException( $"'{nameof( id )} can't be empty'", nameof( id ) );
        }

        if ( propertyId == Guid.Empty )
        {
            throw new ArgumentException( $"'{nameof( propertyId )} can't be empty'", nameof( propertyId ) );
        }

        CheckStringIfEmpty( name, nameof( name ) );
        CheckStringIfEmpty( currency, nameof( currency ) );
        CheckStringIfEmpty( services, nameof( services ) );
        CheckStringIfEmpty( amenities, nameof( amenities ) );


        if ( dailyPrice <= 0 )
        {
            throw new ArgumentOutOfRangeException( $"'{nameof( DailyPrice )} must be grater than 0'" );
        }

        if ( minPersonCount <= 0 || maxPersonCount <= 0 || maxPersonCount < minPersonCount )
        {
            throw new ArgumentOutOfRangeException( "Invalid person count range." );
        }

        if ( availableRooms < 1 )
        {
            throw new ArgumentOutOfRangeException( $"{nameof( availableRooms )} must be grater than 1" );
        }

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

    private void CheckStringIfEmpty( string value, string nameOfValue )
    {
        if ( string.IsNullOrEmpty( value ) )
        {
            throw new ArgumentException( $"'{nameOfValue}' can't be null or empty", nameOfValue );

        }
    }
}
