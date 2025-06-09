using System.ComponentModel.DataAnnotations;
using Domain.Helpers;

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

    public List<RoomType> RoomTypes { get; private set; } = [];

    public List<Reservation> Reservations { get; private set; } = [];


    public Property(
        string name,
        string country,
        string city,
        string address,
        decimal latitude,
        decimal longitude )
    {
        DomainValidator.NullOrEmpty( name, nameof( name ) );
        DomainValidator.NullOrEmpty( country, nameof( country ) );
        DomainValidator.NullOrEmpty( city, nameof( city ) );
        DomainValidator.NullOrEmpty( address, nameof( address ) );

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
        DomainValidator.NullOrEmpty( name, nameof( name ) );
        DomainValidator.NullOrEmpty( country, nameof( country ) );
        DomainValidator.NullOrEmpty( city, nameof( city ) );
        DomainValidator.NullOrEmpty( address, nameof( address ) );

        Id = id;
        Name = name;
        Country = country;
        City = city;
        Address = address;
        Latitude = latitude;
        Longitude = longitude;
    }
}
