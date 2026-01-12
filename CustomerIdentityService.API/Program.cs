using CustomerIdentityService.API.Configurations;
using CustomerIdentityService.API.DependencyInjection;
using CustomerIdentityService.Application.Common.Helpers;
using CustomerIdentityService.Application.DependencyInjection;
using Serilog;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

//logegr

builder.ConfigureSerilog();
//automapper
builder.Services.AddAutoMapperServiceRegistration(builder.Configuration);
// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGenConfiguration(builder.Configuration);
builder.Services.AddApplicationDI(builder.Configuration);
//logger
builder.Host.UseSerilog();
//cấu hình xác thực
builder.Services.AddAuthenticationIdentityServer(builder.Configuration);
builder.Services.AddAuthentication(builder.Configuration);
builder.Services.AddJwtAuthentication(builder.Configuration);

var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options => options.DisplayRequestDuration());
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseGlobalException();
app.MapControllers();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");
app.Run();
