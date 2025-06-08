using Domain.Entities;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;
using PropertiesApi.Contracts.RoomType;

namespace PropertiesApi.Controllers;

[ApiController]
[Route( "api" )]
public class RoomTypesController : ControllerBase
{
    private readonly IRoomTypesService _roomTypesService;
    public RoomTypesController( IRoomTypesService roomTypesService )
    {
        _roomTypesService = roomTypesService;
    }

    [HttpGet( "properties/{propertyId:guid}/roomtypes" )]
    public async Task<IActionResult> GetAllRoomTypesByPropertyId( [FromRoute] Guid propertyId )
    {
        try
        {
            List<RoomType> roomTypes = await _roomTypesService.GetAllRoomTypesByPropertyIdAsync( propertyId );

            List<RoomTypeResponse> roomTypesResponse = roomTypes
                .Select( rt => RoomTypeResponse.FromEntity( rt ) )
                .ToList();

            return Ok( roomTypesResponse );
        }
        catch ( Exception ex )
        {
            return BadRequest( $"Error: {ex.Message}" );
        }
    }

    [HttpGet( "roomtypes/{id:guid}" )]
    public async Task<IActionResult> GetRoomTypeById( [FromRoute] Guid id )
    {
        try
        {
            RoomType? roomType = await _roomTypesService.GetRoomTypeByIdAsync( id );

            if ( roomType == null )
            {
                return NotFound( $"RoomType with ID '{id}' not found" );
            }

            RoomTypeResponse roomTypeResponse = RoomTypeResponse.FromEntity( roomType );

            return Ok( roomTypeResponse );
        }
        catch ( Exception ex )
        {
            return BadRequest( $"Error: {ex.Message}" );
        }
    }

    [HttpPost( "properties/{propertyId:guid}/roomtypes" )]
    public async Task<IActionResult> CreateRoomTypeByPropertyId( [FromRoute] Guid propertyId, [FromBody] CreateRoomTypeRequest roomTypeRequest )
    {
        try
        {
            Guid roomType = await _roomTypesService.AddRoomTypeAsync(
                propertyId,
                roomTypeRequest.Name,
                roomTypeRequest.DailyPrice,
                roomTypeRequest.Currency,
                roomTypeRequest.MinPersonCount,
                roomTypeRequest.MaxPersonCount,
                roomTypeRequest.Services,
                roomTypeRequest.Amenities,
                roomTypeRequest.AvailableRooms );

            return Ok( roomType );
        }
        catch ( Exception ex )
        {
            return BadRequest( $"Error: {ex.Message}" );
        }
    }

    [HttpPut( "roomtypes/{id:guid}" )]
    public async Task<IActionResult> UpdateRoomTypeById( [FromRoute] Guid id, [FromBody] UpdateRoomTypeRequest roomTypeRequest )
    {
        try
        {
            Guid updateRoomType = await _roomTypesService.UpdateRoomTypeByIdAsync(
                id,
                roomTypeRequest.PropertyId,
                roomTypeRequest.Name,
                roomTypeRequest.DailyPrice,
                roomTypeRequest.Currency,
                roomTypeRequest.MinPersonCount,
                roomTypeRequest.MaxPersonCount,
                roomTypeRequest.Services,
                roomTypeRequest.Amenities,
                roomTypeRequest.AvailableRooms );

            return Ok( updateRoomType );
        }
        catch ( Exception ex )
        {
            return BadRequest( $"Error: {ex.Message}" );
        }
    }

    [HttpDelete( "roomtypes/{id:guid}" )]
    public async Task<IActionResult> DeleteRoomTypeById( [FromRoute] Guid id )
    {
        try
        {
            Guid deletedRoomType = await _roomTypesService.DeleteRoomTypeAsync( id );

            return Ok( deletedRoomType );
        }
        catch ( Exception ex )
        {
            return BadRequest( $"Error: {ex.Message}" );
        }
    }
}
