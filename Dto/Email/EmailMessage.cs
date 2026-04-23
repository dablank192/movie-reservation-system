using System;

namespace movie_reservation_system.Dto.Email;

public class EmailMessage
{
    public string? ToEmail {get; set;}
    public string? Subject {get; set;}
    public string? Body {get; set;}
}
