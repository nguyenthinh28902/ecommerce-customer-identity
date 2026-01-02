using CustomerIdentityService.API.Configurations;
using CustomerIdentityService.API.DependencyInjection;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

//loger

builder.ConfigureSerilog();
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplicationDI(builder.Configuration);
var app = builder.Build();

builder.Host.UseSerilog();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseGlobalException();
app.MapControllers();

app.Run();
