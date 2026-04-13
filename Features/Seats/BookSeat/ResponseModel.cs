using System;
using movie_reservation_system.Dto;


namespace movie_reservation_system.Features.Seats.BookSeat;

public class ResponseModel
{
    public int MovieId {get; set;}
    public int RoomId {get; set;}
    public DateTime StartTime {get; set;}
    public List<SeatDto>? Seats {get; set;}
}
