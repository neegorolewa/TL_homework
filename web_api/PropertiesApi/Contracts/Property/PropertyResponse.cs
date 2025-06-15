﻿namespace PropertiesApi.Contracts.Property;

public class PropertyResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
    public string Address { get; set; }
    public decimal Latitude { get; set; }
    public decimal Longitude { get; set; }

    public PropertyResponse(
        Guid id,
        string name,
        string country,
        string city,
        string address,
        decimal latitude,
        decimal longitude )
    {
        Id = id;
        Name = name;
        Country = country;
        City = city;
        Address = address;
        Latitude = latitude;
        Longitude = longitude;
    }

    public static PropertyResponse FromEntity( Domain.Entities.Property property )
    {
        return new PropertyResponse(
            property.Id,
            property.Name,
            property.Country,
            property.City,
            property.Address,
            property.Latitude,
            property.Longitude );
    }
}
