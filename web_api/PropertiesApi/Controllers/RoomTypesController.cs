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
        List<RoomType> roomTypes = await _roomTypesService.GetAllRoomTypesByPropertyIdAsync( propertyId );

        List<RoomTypeResponse> roomTypesResponse = roomTypes
            .Select( rt => new RoomTypeResponse(
                rt.Id,
                rt.PropertyId,
                rt.Name,
                rt.DailyPrice,
                rt.Currency,
                rt.MinPersonCount,
                rt.MaxPersonCount,
                rt.Services,
                rt.Amenities,
                rt.AvailableRooms ) )
            .ToList();

        return Ok( roomTypesResponse );
    }

    [HttpGet( "roomtypes/{id:guid}" )]
    public async Task<IActionResult> GetRoomTypeById( [FromRoute] Guid id )
    {
        RoomType? roomType = await _roomTypesService.GetRoomTypeByIdAsync( id );

        if ( roomType == null )
        {
            return NotFound( $"RoomType with ID '{id}' not found" );
        }

        RoomTypeResponse roomTypeResponse = new(
            roomType.Id,
            roomType.PropertyId,
            roomType.Name,
            roomType.DailyPrice,
            roomType.Currency,
            roomType.MinPersonCount,
            roomType.MaxPersonCount,
            roomType.Services,
            roomType.Amenities,
            roomType.AvailableRooms );

        return Ok( roomTypeResponse );
    }

    [HttpPost( "properties/{propertyId:guid}/roomtypes" )]
    public async Task<IActionResult> CreateRoomTypeByPropertyId( [FromRoute] Guid propertyId, [FromBody] CreateRoomTypeRequest roomTypeRequest )
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
            throw new InvalidOperationException( $"Error: {ex.Message}" );
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
            throw new BadHttpRequestException( $"Error: {ex.Message}" );
        }
    }
}
