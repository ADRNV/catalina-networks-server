using CatalinaNetworks.API.Profiles;
using CatalinaNetworks.DataBase;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
//Say my name
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<ApiUserMappingProfile>();
    cfg.AddProfile<DBUserMappingProfile>();
});

builder.Services.AddValidatorsFromAssemblies(new[] { Assembly.GetExecutingAssembly() });

// ���� ���� ������ �� �����������, ���������� ���������� ������ ���������
// ��������� ���� ������ �������� ��� ������������ frontend �����
builder.Services.AddDbContext<UsersDbContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection")));

builder.Services.AddSwaggerGen();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.UseAuthorization();

app.MapControllers();

app.Run();
