using Domain.Entities;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;
using PropertiesApi.Contracts.Property;

namespace PropertiesApi.Controllers;

[ApiController]
[Route( "api/properties" )]
public class PropertiesController : ControllerBase
{
    private readonly IPropertyService _propertiesService;

    public PropertiesController( IPropertyService propertyService )
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

    [HttpPost]
    public async Task<IActionResult> CreateProperty( [FromBody] CreatePropertyRequest propertyRequest )
    {
        var property = await _propertiesService.AddPropertyAsync(
          propertyRequest.Name,
          propertyRequest.Country,
          propertyRequest.City,
          propertyRequest.Address,
          propertyRequest.Latitude,
          propertyRequest.Longitude );

        return Ok( property );
    }
}
