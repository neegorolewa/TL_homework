using Domain.Entities;
using Domain.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
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
        try
        {
            List<Property> properties = await _propertiesService.GetAllPropertiesAsync();

            List<PropertyResponse> propertyResponses = properties
                .Select( p => PropertyResponse.FromEntity( p ) )
                .ToList();

            return Ok( propertyResponses );
        }
        catch ( Exception ex )
        {
            return BadRequest( $"Error: {ex.Message}" );
        }
    }

    [HttpGet( "{id:Guid}" )]
    public async Task<IActionResult> GetPropertyById( [FromRoute] Guid id )
    {
        try
        {
            Property? property = await _propertiesService.GetPropertyByIdAsync( id );

            if ( property == null )
            {
                return NotFound( $"Property with ID {id} not found" );
            }

            PropertyResponse propertyResponse = PropertyResponse.FromEntity( property );

            return Ok( propertyResponse );
        }
        catch ( Exception ex )
        {
            return BadRequest( $"Error: {ex.Message}" );
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateProperty( [FromBody] CreatePropertyRequest propertyRequest )
    {
        try
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
        catch ( Exception ex )
        {
            return BadRequest( $"Error: {ex.Message}" );
        }
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
            return BadRequest( $"Error: {ex.Message}" );
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
            return BadRequest( $"Error: {ex.Message}" );
        }
    }
}
