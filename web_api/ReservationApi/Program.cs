using Domain.Repositories;
using Domain.Services;
using Infrastructure;
using Infrastructure.Foundation.Repositories;
using Infrastructure.Foundation.Services;
using Microsoft.EntityFrameworkCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder( args );
ConfigurationManager configuration = builder.Configuration;
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(
    options =>
    {
        options.UseSqlServer( configuration.GetConnectionString( "DefaultConnection" ) );
    } );

builder.Services.AddScoped<IReservationsService, ReservationsService>();
builder.Services.AddScoped<IReservationsRepository, ReservationsRepository>();

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if ( app.Environment.IsDevelopment() )
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
