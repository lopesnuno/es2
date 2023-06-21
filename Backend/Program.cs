using System.Text.Json.Serialization;
using BusinessLogic.Context;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

var  MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy  =>
        {
            // Didn't know how to do it 🤷‍♂️
            policy.WithOrigins("https://localhost:7275", "http://localhost:5211").AllowAnyMethod().AllowAnyHeader();
        });
});

// Add services to the container.

//TODO: remove json options - we shouldn't ignore circular dependencies
builder.Services.AddControllers(
        options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true)
    .AddJsonOptions(o => o.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add default connection string for the Web API controllers
builder.Services.AddDbContext<ES2DbContext>(options => 
    options.UseNpgsql(builder.Configuration.GetConnectionString("DatabaseConnection"))
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();