using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using movie_reservation_system.Infrastructure;


var builder = WebApplication.CreateBuilder(args);


var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");


builder.Services.AddOpenApi();

builder.Services.AddFastEndpoints();

builder.Services.AddSwaggerDocument();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();


app.Run();

