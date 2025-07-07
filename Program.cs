using Microsoft.AspNetCore.HttpLogging;
using QRCodeGenerator;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddOpenApi();
builder.Services.AddScoped<IQRGenerator, QRGenerate>();

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

app.MapGet("/help", () => "Hello World!");

app.MapGet("/qr", async (string content, IQRGenerator qrGenerator) =>
{
    var fileName = $"{Guid.NewGuid()}.png";
    var qrCodeFile = await qrGenerator.GeneratorQRAsync(content, fileName);
    return Results.File(qrCodeFile.OpenReadStream(), qrCodeFile.ContentType, fileName);
});


app.Run();
