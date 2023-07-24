using System.Reflection;
using Antique_Store_API.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Antique Store REST API",
        Description = $"Welcome to the Interactive Antique Store API documentation. " +
                      $"This API allows you to interact with various endpoints to manage antique store products. " +
                      $"The Antique Store API provides a range of functionalities for accessing and manipulating product data, " +
                      $"making it easy for clients to integrate with the antique store's backend systems. " +
                      $"You can use these endpoints to view detailed product information, add new products to the store, " +
                      $"modify existing product details, and remove products from the inventory. " +
                      $"The API follows RESTful principles and uses HTTP methods such as GET, POST, PUT, and DELETE for " +
                      $"different operations. Please refer to the individual endpoint documentation for specific details on how to use them. ", 
        Version = "v1"
    });
    
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
});
Console.WriteLine(Directory.GetCurrentDirectory() + "/antiquestoreapi.db");
builder.Services.AddScoped<IDbOperation, DbOperation>();
builder.Services.AddDbContext<MainContext>(options =>
{
    //var conStr = $"Data Source={Directory.GetCurrentDirectory()}";
    options.UseSqlite(ConStr.GetConStr());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Your API Name v1");
    options.DocumentTitle = "Antique Store REST API";
});

app.UseAuthorization();

app.MapControllers();

app.Run();
