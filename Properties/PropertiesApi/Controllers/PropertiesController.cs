﻿using Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using PropertiesApi.Contracts;
namespace PropertiesApi.Controllers;
/*
 * Create property:
 * POST     /api/properties + body
 * GET      /api/properties
 * GET      /api/properties/{propertyId}
 * PUT      /api/properties/{propertyId} + body
 * DELETE   /api/properties/{propertyId} 
 * Почитать PATCH
 * 
 */

[ApiController]
[Route( "api/properties" )]
public class PropertiesController : ControllerBase
{
    private readonly IPropertiesRepository _propertiesRepository;
    public PropertiesController( IPropertiesRepository propertiesRepository )
    {
        _propertiesRepository = propertiesRepository;
    }

    [HttpPost]
    public IActionResult Create( [FromBody] CreatePropertyRequest createPropertyRequest )
    {
        Domain.Entities.Property property = new( createPropertyRequest.Name );
        _propertiesRepository.Add( property );

        return Created( "", property.Id );
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        IEnumerable<Domain.Entities.Property> props = _propertiesRepository.List();
        IEnumerable<Contracts.Property> propertiesResponse = props.Select( p => new Property( p.Id, p.Name ) );

        return Ok( propertiesResponse );
    }

    [HttpGet( "{propertyId:guid}" )]
    public IActionResult Get( [FromRoute] Guid propertyId )
    {
        Domain.Entities.Property? property = _propertiesRepository.GetById( propertyId );

        if ( property is null )
        {
            return NotFound();
        }

        Contracts.Property propertyResponse = new Property( property.Id, property.Name );

        return Ok( propertyResponse );
    }

    //[HttpPut( "{propertyId:guid}" )]
    //public IActionResult Update( )
    //{
    //}

    //[HttpDelete( "{propertyId:guid}" )]
    //public IActionResult Delete(s )
    //{
    //}
}
