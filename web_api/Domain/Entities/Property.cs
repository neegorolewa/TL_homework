namespace Domain.Entities;

public class Property
{
    public Guid Id { get; }
    public string Name { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
    public string Address { get; set; }
    public decimal Latitude { get; set; }
    public decimal Longitude { get; set; }

    public IReadOnlyList<RoomType> RoomTypes { get; private set; } = [];

    public IReadOnlyList<Reservation> Reservations { get; private set; } = [];

    protected Property()
    {
    }

    public Property(
        string name,
        string country,
        string city,
        string address,
        decimal latitude,
        decimal longitude )
    {
        if ( string.IsNullOrEmpty( name ) )
        {
            throw new ArgumentException( $"'{nameof( name )}' can't be null or empty", nameof( name ) );
        }

        if ( string.IsNullOrEmpty( country ) )
        {
            throw new ArgumentException( $"'{nameof( country )}' can't be null or empty", nameof( country ) );
        }

        if ( string.IsNullOrEmpty( city ) )
        {
            throw new ArgumentException( $"'{nameof( city )}' can't be null or empty", nameof( city ) );
        }

        if ( string.IsNullOrEmpty( address ) )
        {
            throw new ArgumentException( $"'{nameof( address )}' can't be null or empty", nameof( address ) );
        }

        Id = Guid.NewGuid();
        Name = name;
        Country = country;
        City = city;
        Address = address;
        Latitude = latitude;
        Longitude = longitude;
    }


}
