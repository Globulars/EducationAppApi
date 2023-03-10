using FluentAssertions.Common;
using Microsoft.EntityFrameworkCore;
using Web.Data.Data;
using Web.Data.Generic_Repository;
using Web.Data.Interfaces;
using Web.Services.Interfaces;
using Web.Services.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataContext>();
string connectionString = builder.Configuration.GetConnectionString("Database")!;

builder.Services.AddDbContext<DataContext>(
    (sp, optionsBuilder) =>
    {
        optionsBuilder.UseSqlServer(connectionString,
            b => b.MigrationsAssembly("Web.Data"));
    });

builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddTransient(typeof(IUserAuthenticationService), typeof(UserAuthenticationService));
var app = builder.Build();

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
