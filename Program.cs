using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using movie_reservation_system.Infrastructure;
using movie_reservation_system.Extension;
using FastEndpoints.Swagger;
using FastEndpoints.Security;


var builder = WebApplication.CreateBuilder(args);


var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

var jwtKey = builder.Configuration["Jwt:Key"];

builder.Services.AddAuthenticationJwtBearer(s => s.SigningKey = jwtKey);
builder.Services.AddAuthorization();

builder.Services.AddOpenApi();
builder.Services.AddFastEndpoints();
builder.Services.AddSwaggerDocument();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(connectionString);
});

builder.Services.AddScoped<IUtils, Utils>();

builder.Services.AddSingleton<IS3Storage, S3Storage>();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();
app.UseFastEndpoints();
app.UseSwaggerGen();


app.Run();

