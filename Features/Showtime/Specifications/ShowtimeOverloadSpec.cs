using System;
using Ardalis.Specification;


namespace movie_reservation_system.Features.Showtime.Specifications;

public class ShowtimeOverloadSpec : Specification<Model.Showtime>
{
    public ShowtimeOverloadSpec(int roomId, DateTime movieStartTime, DateTime movieEndTime, int? excludeShowtimeId = null)
    {
        Query.Where(t => t.RoomId == roomId);

        Query.Where(t => t.StartTime < movieStartTime && t.EndTime > movieEndTime);

        if (excludeShowtimeId.HasValue)
        {
            Query.Where(t => t.Id != excludeShowtimeId.Value);
        }
    }
}
