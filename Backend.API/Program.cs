using System.Net;
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

builder.Services.AddAuthorization();
builder.Services.AddControllers();

builder.WebHost.UseUrls("https://0.0.0.0:8080")
    .UseKestrel(options =>
    {
        options.Listen(IPAddress.Any, 8080, listenOptions =>
        {
            listenOptions.UseHttps("/usr/local/share/ca-certificates/aspnet/https.crt", "qwerfdsazxcv");
        });
    });

builder.Services.AddCors(options => 
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => {
            builder.WithOrigins("http://localhost:3000", "https://my-finances-app.vercel.app")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
                
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.


app.UseCors("AllowSpecificOrigin");
  
app.UseRouting();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();
app.UseAuthorization();


app.Map("/Backend/Backend.API", app =>
{
    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
    });
});


app.Run();