using Azure.Core;
using Domain.Contracts;
using Domain.Entities;
using Domain.Services;
using Infrastructure.Foundation.Services;
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
        List<PropertyWithRoomTypeDto> reservations = await _reservationsService.GetSearchOptions(
            arrivalDate,
            departureTime,
            guestsNumber,
            city );

        return Ok( reservations );
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
                .Select( r => new ReservationResponse(
                    r.Id,
                    r.PropertyId,
                    r.RoomTypeId,
                    r.ArrivalDate,
                    r.DepartureDate,
                    r.ArrivalTime,
                    r.DepartureTime,
                    r.GuestName,
                    r.GuestPhoneNumber,
                    r.Total,
                    r.Currency ) )
                .ToList();

            return Ok( reservationsResponse );
        }
        catch ( Exception ex )
        {
            throw new BadHttpRequestException( $"Error: {ex.Message}" );
        }
    }

    [HttpGet( "reservations/{id:guid}" )]
    public async Task<IActionResult> GetReservationById( [FromRoute] Guid id )
    {
        Reservation? reservation = await _reservationsService.GetReservationByIdAsync( id );

        if ( reservation == null )
        {
            return NotFound( $"Reservation with id '{id}' not found" );
        }

        ReservationResponse reservationResponse = new(
            reservation.Id,
            reservation.PropertyId,
            reservation.RoomTypeId,
            reservation.ArrivalDate,
            reservation.DepartureDate,
            reservation.ArrivalTime,
            reservation.DepartureTime,
            reservation.GuestName,
            reservation.GuestPhoneNumber,
            reservation.Total,
            reservation.Currency );

        return Ok( reservationResponse );
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
            throw new BadHttpRequestException( $"Error: {ex.Message}" );
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
            throw new BadHttpRequestException( $"Error: {ex.Message}" );
        }
    }
}
