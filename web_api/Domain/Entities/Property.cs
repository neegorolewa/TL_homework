namespace Domain.Entities;

public class Property
{
    public Guid Id { get; }
    public string Name { get; private set; }
    public string Country { get; private set; }
    public string City { get; private set; }
    public string Address { get; private set; }
    public decimal Latitude { get; private set; }
    public decimal Longitude { get; private set; }

    public IReadOnlyList<RoomType> RoomTypes { get; private set; } = new List<RoomType>();

    public IReadOnlyList<Reservation> Reservations { get; private set; } = new List<Reservation>();

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
