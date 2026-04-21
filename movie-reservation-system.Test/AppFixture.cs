using System;
using FastEndpoints.Testing;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using movie_reservation_system.Infrastructure;


namespace movie_reservation_system.Test;

public class AppFixture : AppFixture<Program>
{
    private SqliteConnection? _connection;
    protected override void ConfigureApp(IWebHostBuilder b)
    {
        _connection = new SqliteConnection("DataSource=:memory:");
        _connection.Open();

        b.ConfigureServices(service =>
        {
            var descriptor = service.SingleOrDefault(t => t.ServiceType == typeof(DbContextOptions<AppDbContext>));
            if (descriptor != null)
            {
                service.Remove(descriptor);
            }

            service.AddDbContext<AppDbContext>(option =>
            {
                option.UseSqlite(_connection);
            });
        });
    }

    protected override async ValueTask SetupAsync ()
    {
        using var scope = Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        await db.Database.EnsureCreatedAsync();
    }

    protected override async ValueTask TearDownAsync ()
    {
        if (_connection != null)
        {
            await _connection.CloseAsync();
            _connection.Dispose();
        }
    }

    public async Task SeedData (Action<AppDbContext> seedDb)
    {
        using var scope = Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        seedDb(db);
        await db.SaveChangesAsync();
    }
}
