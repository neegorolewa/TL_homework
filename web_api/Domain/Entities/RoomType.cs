namespace Domain.Entities;

public class RoomType
{
    public Guid Id { get; }
    public Guid PropertyId { get; private set; }
    public string Name { get; private set; }
    public decimal DailyPrice { get; private set; }
    public string Currency { get; private set; }
    public int MinPersonCount { get; private set; }
    public int MaxPersonCount { get; private set; }
    public string Services { get; private set; }
    public string Amenities { get; private set; }

    public IReadOnlyList<Reservation> Reservations { get; private set; } = new List<Reservation>();

    public RoomType(
        Guid propertyId,
        string name,
        decimal dailyPrice,
        string currency,
        int minPersonCount,
        int maxPersonCount,
        string services,
        string amenities )
    {
        if ( propertyId == Guid.Empty )
        {
            throw new ArgumentException( $"'{nameof( propertyId )} can't be empty'", nameof( propertyId ) );
        }

        if ( string.IsNullOrEmpty( name ) )
        {
            throw new ArgumentException( $"'{nameof( name )}' can't be null or empty", nameof( name ) );
        }

        if ( string.IsNullOrEmpty( currency ) )
        {
            throw new ArgumentException( $"'{nameof( currency )}' can't be null or empty", nameof( currency ) );
        }

        if ( string.IsNullOrEmpty( services ) )
        {
            throw new ArgumentException( $"'{nameof( services )}' can't be null or empty", nameof( services ) );
        }

        if ( string.IsNullOrEmpty( amenities ) )
        {
            throw new ArgumentException( $"'{nameof( amenities )}' can't be null or empty", nameof( amenities ) );
        }

        if ( dailyPrice <= 0 )
        {
            throw new ArgumentOutOfRangeException( $"'{nameof( DailyPrice )} must be grater than 0'" );
        }

        if ( minPersonCount <= 0 || maxPersonCount <= 0 || maxPersonCount < minPersonCount )
        {
            throw new ArgumentOutOfRangeException( "Invalid person count range." );
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
    }

}
