using CustomerIdentityService.API.Configurations;
using CustomerIdentityService.API.DependencyInjection;
using CustomerIdentityService.Application.DependencyInjection;
using Microsoft.AspNetCore.DataProtection;
using Serilog;
using System.IdentityModel.Tokens.Jwt;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddEnvironmentVariables();
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
// Xóa bản đồ ánh xạ mặc định để giữ nguyên tên "sub" từ Token
JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();


var storagePath = @"/home/app/.aspnet/DataProtection-Keys";
if (!Directory.Exists(storagePath)) Directory.CreateDirectory(storagePath);

builder.Services.AddDataProtection()
    .PersistKeysToFileSystem(new DirectoryInfo(storagePath))
    .SetApplicationName("EcommerceApp");

var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options => options.DisplayRequestDuration());
}


app.UseAuthentication();
app.UseAuthorization();
app.UseGlobalException();
app.MapControllers();

app.Run();
