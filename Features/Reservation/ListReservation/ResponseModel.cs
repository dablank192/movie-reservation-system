using System;
using movie_reservation_system.Dto;
using movie_reservation_system.Dto.Reservations;


namespace movie_reservation_system.Features.Reservation.ListReservation;

public class ResponseModel
{
    public int Id {get; set;}
    public int UserId {get; set;}
    public decimal TotalAmount {get; set;}
    public ReservationStatus Status {get; set;}
    public List<ReservationDto>? AllReservations {get; set;}
}
