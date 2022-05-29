using CatalinaNetworks.DataBase;
using Microsoft.EntityFrameworkCore;
//Say my name
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// ���� ���� ������ �� �����������, ���������� ���������� ������ ���������
// ��������� ���� ������ �������� ��� ������������ frontend �����
builder.Services.AddDbContext<UsersDbContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection"))); // TODO : �� ������������� DI

builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
