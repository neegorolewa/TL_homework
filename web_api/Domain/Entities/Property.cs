using System.Net;

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


    public Property(
        string name,
        string country,
        string city,
        string address,
        decimal latitude,
        decimal longitude )
    {
        CheckStringIfEmpty( name, nameof( name ) );
        CheckStringIfEmpty( country, nameof( country ) );
        CheckStringIfEmpty( city, nameof( city ) );
        CheckStringIfEmpty( address, nameof( address ) );

        Id = Guid.NewGuid();
        Name = name;
        Country = country;
        City = city;
        Address = address;
        Latitude = latitude;
        Longitude = longitude;
    }

    public Property(
        Guid id,
        string name,
        string country,
        string city,
        string address,
        decimal latitude,
        decimal longitude )
    {
        CheckStringIfEmpty( name, nameof( name ) );
        CheckStringIfEmpty( country, nameof( country ) );
        CheckStringIfEmpty( city, nameof( city ) );
        CheckStringIfEmpty( address, nameof( address ) );

        Id = id;
        Name = name;
        Country = country;
        City = city;
        Address = address;
        Latitude = latitude;
        Longitude = longitude;
    }

    private void CheckStringIfEmpty( string value, string nameOfValue )
    {
        if ( string.IsNullOrEmpty( value ) )
        {
            throw new ArgumentException( $"'{nameOfValue}' can't be null or empty", nameOfValue );

        }
    }
}
