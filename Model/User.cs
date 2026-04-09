using System;
using movie_reservation_system.Dto;


namespace movie_reservation_system.Model;

public class User
{
    public int Id {get; set;}
    public required string Email {get; set;}
    public required string Username {get; set;}
    public required string HashPassword {get; set;}
    public UserRoles Roles {get; set;} = UserRoles.User;

    public List<Reservations> Reservations {get; set;}
}
