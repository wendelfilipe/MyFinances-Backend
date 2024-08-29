using System.Net;
using Backend.Domain.Account;
using Backend.Domain.Interfaces.AssetsInterface;
using Backend.Domain.Interfaces.UserInterface;
using Backend.Domain.Interfaces.WalletInterface;
using Backend.Infra.Data.Context;
using Backend.Infra.Data.EnititesConfiguration;
using Backend.Infra.Ioc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient();

DependencyInjection.AddInfrastruture(builder.Services, builder.Configuration);

DependencyInjectionJWT.AddInfrastructureJWT(builder.Services, builder.Configuration);

builder.Services.AddCors(options => 
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => {
            builder.WithOrigins()
                .AllowAnyOrigin()
            // builder.WithOrigins("https://myfinancesapp.vercel.app", "http://localhost:3000", "http://127.0.0.1:45853" )
                .AllowAnyHeader()
                .AllowAnyMethod();

        });
});
builder.Services.AddControllers();

var port = builder.Configuration["PORT"]?? "5001";
builder.WebHost.UseUrls($"http://0.0.0.0:{port}");


var app = builder.Build();

// Configure the HTTP request pipeline.


app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseCors("AllowSpecificOrigin");

app.MapControllers();


app.Run();