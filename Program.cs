using Microsoft.AspNetCore.HttpLogging;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddOpenApi();

builder.Services.AddHttpLogging(opts =>
opts.LoggingFields = HttpLoggingFields.RequestProperties); 
builder.Logging.AddFilter("Microsoft.AspNetCore.HttpLogging.HttpLoggingMiddleware", LogLevel.Information);


var app = builder.Build();


if (app.Environment.IsDevelopment()) 
{
    app.MapOpenApi(); 

}
app.UseHttpLogging();
app.UseHttpsRedirection();

app.MapGet("/", () => "Hello World!");

app.Run();
