using System;
using FastEndpoints;

namespace movie_reservation_system.Features.Showtime.UpdateShowtime;

public class RequestModel
{
    [BindFrom("id")]
    public int Id {get; set;}
    public int? MovieId {get; set;}
    public int? RoomId {get; set;}
    public DateTime? StartTime {get; set;}
}
