using System;

namespace movie_reservation_system.Dto;

public class SeatDto
{
    public int Id {get; set;}
    public required string Row {get; set;}
    public int Number {get; set;}
    public required SeatsType Type {get; set;}
}
