using Domain.Entities;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using PropertiesApi.Contracts.Property;

namespace PropertiesApi.Controllers;

[ApiController]
[Route( "api/properties" )]
public class PropertiesController : ControllerBase
{
    private readonly IPropertiesService _propertiesService;

    public PropertiesController( IPropertiesService propertyService )
    {
        _propertiesService = propertyService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllProperties()
    {
        List<Property> properties = await _propertiesService.GetAllPropertiesAsync();

        List<PropertyResponse> propertyResponses = properties
            .Select( p => new PropertyResponse(
                p.Id,
                p.Name,
                p.Country,
                p.City,
                p.Address,
                p.Latitude,
                p.Longitude ) )
            .ToList();

        return Ok( propertyResponses );
    }

    [HttpGet( "{id:Guid}" )]
    public async Task<IActionResult> GetPropertyById( [FromRoute] Guid id )
    {
        Property? property = await _propertiesService.GetPropertyByIdAsync( id );

        if ( property == null )
        {
            return NotFound( $"Property with ID {id} not found" );
        }

        PropertyResponse propertyResponse = new(
            property.Id,
            property.Name,
            property.Country,
            property.City,
            property.Address,
            property.Latitude,
            property.Longitude );

        return Ok( propertyResponse );
    }

    [HttpPost]
    public async Task<IActionResult> CreateProperty( [FromBody] CreatePropertyRequest propertyRequest )
    {
        Guid property = await _propertiesService.AddPropertyAsync(
          propertyRequest.Name,
          propertyRequest.Country,
          propertyRequest.City,
          propertyRequest.Address,
          propertyRequest.Latitude,
          propertyRequest.Longitude );

        return Ok( property );
    }

    [HttpPut( "{id:Guid}" )]
    public async Task<IActionResult> UpdateProperty( [FromRoute] Guid id, [FromBody] UpdatePropertyRequest updatePropertyRequest )
    {
        try
        {
            Guid updateProperty = await _propertiesService.UpdatePropertyAsync(
            id,
            updatePropertyRequest.Name,
            updatePropertyRequest.Country,
            updatePropertyRequest.City,
            updatePropertyRequest.Address,
            updatePropertyRequest.Latitude,
            updatePropertyRequest.Longitude
            );

            return Ok( updateProperty );
        }
        catch ( Exception ex )
        {
            throw new BadHttpRequestException( $"Error: {ex.Message}" );
        }
    }

    [HttpDelete( "{id:Guid}" )]
    public async Task<IActionResult> DeleteProperty( [FromRoute] Guid id )
    {
        try
        {
            Guid deletedProperty = await _propertiesService.DeletePropertyAsync( id );

            return Ok( deletedProperty );
        }
        catch ( Exception ex )
        {
            throw new BadHttpRequestException( $"Error: {ex.Message}" );
        }
    }
}
