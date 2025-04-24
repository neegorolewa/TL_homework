//�������� �������� ����������
using Domain.Repositories;
using Infrastructure.Repositories;

WebApplicationBuilder builder = WebApplication.CreateBuilder( args );

builder.Services.AddScoped<IPropertiesRepository, PropertiesRepository>();

//���������� �������� � DI-���������
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//������������ ��������� ����������
var app = builder.Build();
if ( app.Environment.IsDevelopment() )
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
