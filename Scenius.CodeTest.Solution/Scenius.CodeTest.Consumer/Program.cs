// See https://aka.ms/new-console-template for more information

using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Scenius.CodeTest.API.Models;
using Scenius.CodeTest.Consumer.Consumers;
using Scenius.CodeTest.Consumer.Helpers;
using Scenius.CodeTest.Consumer.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseUrls("http://*:5001");
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("WebApiDatabase"));
});
builder.Services.AddScoped<IIngestRepository, IngestRepository>();
builder.Services.AddMassTransit(x =>
{
    x.SetKebabCaseEndpointNameFormatter();
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });

        cfg.ConfigureEndpoints(context);
    });
    x.AddConsumer<PostCalculationConsumer>();
});
EndpointConvention.Map<Calculation>(new Uri("queue:post-calculation?durable=true"));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();