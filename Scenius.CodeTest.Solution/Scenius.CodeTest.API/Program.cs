using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Scenius.CodeTest.API.Helpers;
using Scenius.CodeTest.API.Repositories;
using Scenius.CodeTest.API.Services;

var builder = WebApplication.CreateBuilder(args);

var AllowSpecificOrigins = "_allowSpecificOrigins";

// CORS
builder.Services.AddCors(options =>
{
	options.AddPolicy(name: AllowSpecificOrigins, policy =>
	{
		policy.WithOrigins("http://localhost:4200")
		.AllowAnyHeader()
		.AllowAnyMethod()
		.Build();
	});
});

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
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataContext>(options =>
{
options.UseSqlServer(builder.Configuration.GetConnectionString("WebApiDatabase"));
});
builder.Services.AddScoped<IIngestRepository, IngestRepository>();
builder.Services.AddScoped<IIngestService, IngestService>();
builder.Services.AddScoped<IProducer, Producer>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseCors(AllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();