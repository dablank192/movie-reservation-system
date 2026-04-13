using System;
using FastEndpoints;
using movie_reservation_system.Dto;


namespace movie_reservation_system.Features.Seats.ListSeat;

public class ResponseModel
{
    public int Id {get; set;}
    public int RoomId {get; set;}
    public string? Row {get; set;}
    public int Number {get; set;}
    public required SeatsType Type {get; set;}
    public bool IsBooked {get; set;}
}
