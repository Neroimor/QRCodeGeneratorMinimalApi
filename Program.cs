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

app.MapGet("/help", () =>
    """
    ƒл€ получени€ QR-кода введите в URL браузера, Postman или на frontend адрес такого формата:
    https://localhost:7138/qr?content=https://www.youtube.com/watch?v=dQw4w9WgXcQ

    √де:
    - https://localhost:7138/qr Ч конечна€ точка
    - content Ч query-параметр, содержащий текст или ссылку, которую вы хотите закодировать
    """);


app.MapGet("/qr", async (string content, IQRGenerator qrGenerator) =>
{
    var fileName = $"{Guid.NewGuid()}.png";
    var qrCodeFile = await qrGenerator.GeneratorQRAsync(content, fileName);
    return Results.File(qrCodeFile.OpenReadStream(), qrCodeFile.ContentType);

});


app.Run();
