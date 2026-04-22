using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using movie_reservation_system.Dto;


namespace movie_reservation_system.Infrastructure.BackgroundService;

public class SeatCleanupService : Microsoft.Extensions.Hosting.BackgroundService
{
    private readonly IServiceProvider? _serviceProvider;
    private readonly ILogger? _logger;

    public SeatCleanupService (IServiceProvider serviceProvider, ILogger<SeatCleanupService> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken ct)
    {
        _logger!.LogInformation("Seat cleanup is running..");

        while (!ct.IsCancellationRequested)
        {
            try
            {
                await CleanupReservation(ct);
            }
            catch (System.Exception ex)
            {
                _logger!.LogError($"Error occured when cleaning up reservation: {ex}");
            }

            await Task.Delay(TimeSpan.FromMinutes(1), ct);
        }
    }

    public async Task CleanupReservation (CancellationToken ct)
    {
        var scope = _serviceProvider!.CreateScope();

        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        var expiredReservation = await db.Reservations
        .Where(t => t.Status == ReservationStatus.Pending
        && t.ExpiredAt < DateTime.UtcNow)
        .ToListAsync(ct);

        if (expiredReservation.Count == 0)
        {
            return;
        }

        foreach (var reservation in expiredReservation)
        {
            reservation.Status = ReservationStatus.Canceled;

            _logger!.LogInformation("Seat cleaned successfully");
        }

        await db.SaveChangesAsync(ct);
    }
}
