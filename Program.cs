using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using movie_reservation_system.Infrastructure;
using movie_reservation_system.Extension;
using FastEndpoints.Swagger;
using FastEndpoints.Security;
using movie_reservation_system.Exception;


//Allow Postgres to use its old TimestampBehavior
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

var builder = WebApplication.CreateBuilder(args);


var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

var jwtKey = builder.Configuration["Jwt:Key"];

builder.Services.AddAuthenticationJwtBearer(s => s.SigningKey = jwtKey);
builder.Services.AddAuthorization();

builder.Services.AddFastEndpoints();
builder.Services.SwaggerDocument(t =>
{
    t.EnableJWTBearerAuth = true;
});

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(connectionString);
});


builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddScoped<IUtils, Utils>();

builder.Services.AddSingleton<IS3Storage, S3Storage>();


var app = builder.Build();


app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();
app.UseFastEndpoints();
app.UseSwaggerGen();


using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    try
    {
        await DataSeeder.ExecuteAsync(services);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Some error popup while data seeding");
    }
}


app.Run();

