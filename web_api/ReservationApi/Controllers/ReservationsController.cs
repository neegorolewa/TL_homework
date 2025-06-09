using Domain.Contracts;
using Domain.Entities;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;
using ReservationApi.Contracts;

namespace PropertiesApi.Controllers;

[ApiController]
[Route( "api" )]
public class ReservationsController : ControllerBase
{
    private readonly IReservationsService _reservationsService;

    public ReservationsController( IReservationsService reservationsService )
    {
        _reservationsService = reservationsService;
    }

    [HttpGet( "search" )]
    public async Task<IActionResult> SearchOptionsReservations(
        [FromQuery] DateOnly? arrivalDate,
        [FromQuery] DateOnly? departureTime,
        [FromQuery] int? guestsNumber,
        [FromQuery] string? city )
    {
        try
        {
            List<PropertyWithRoomTypeDto> reservations = await _reservationsService.GetSearchOptions(
            arrivalDate,
            departureTime,
            guestsNumber,
            city );

            return Ok( reservations );
        }
        catch ( Exception ex )
        {
            return BadRequest( $"Eror: {ex.Message}" );
        }
    }

    [HttpGet( "reservations/" )]
    public async Task<IActionResult> GetAllReservations(
        [FromQuery] Guid? propertyId,
        [FromQuery] Guid? roomTypeId,
        [FromQuery] DateOnly? arrivalDate,
        [FromQuery] DateOnly? departureDate,
        [FromQuery] string? guestName,
        [FromQuery] string? guestPhoneNumber )
    {
        try
        {
            List<Reservation> reservations = await _reservationsService.GetAllReservationsAsync(
            propertyId,
            roomTypeId,
            arrivalDate,
            departureDate,
            guestName,
            guestPhoneNumber );

            List<ReservationResponse> reservationsResponse = reservations
                .ConvertAll( r => ReservationResponse.FromEntity( r ) );

            return Ok( reservationsResponse );
        }
        catch ( Exception ex )
        {
            return BadRequest( $"Error: {ex.Message}" );
        }
    }

    [HttpGet( "reservations/{id:guid}" )]
    public async Task<IActionResult> GetReservationById( [FromRoute] Guid id )
    {
        try
        {
            Reservation? reservation = await _reservationsService.GetReservationByIdAsync( id );

            if ( reservation == null )
            {
                return NotFound( $"Reservation with id '{id}' not found" );
            }

            ReservationResponse reservationResponse = ReservationResponse.FromEntity( reservation );

            return Ok( reservationResponse );
        }
        catch ( Exception ex )
        {
            return BadRequest( $"Eror: {ex.Message}" );
        }
    }

    [HttpPost( "reservations" )]
    public async Task<IActionResult> CreateReservation( [FromBody] CreateReservationRequest createReservationRequest )
    {
        try
        {
            Guid reservation = await _reservationsService.AddReservationAsync(
                createReservationRequest.PropertyId,
                createReservationRequest.RoomTypeId,
                createReservationRequest.ArrivalDate,
                createReservationRequest.DepartureDate,
                createReservationRequest.ArrivalTime,
                createReservationRequest.DepartureTime,
                createReservationRequest.GuestName,
                createReservationRequest.GuestPhoneNumber,
                createReservationRequest.Currency );

            return Ok( reservation );
        }
        catch ( Exception ex )
        {
            return BadRequest( $"Error: {ex.Message}" );
        }
    }

    [HttpDelete( "reservations/{id:guid}" )]
    public async Task<IActionResult> DeleteReservationsbyId( [FromRoute] Guid id )
    {
        try
        {
            Guid deletedReservation = await _reservationsService.DeleteReservationAsync( id );

            return Ok( deletedReservation );
        }
        catch ( Exception ex )
        {
            return BadRequest( $"Error: {ex.Message}" );
        }
    }
}
