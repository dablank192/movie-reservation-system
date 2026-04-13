using System;

namespace movie_reservation_system.Features.Seats.BookSeat;

public class RequestModel
{
    public int ShowtimeId {get; set;}
    public List<int>? SeatId {get; set;}
}
