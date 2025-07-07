
using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System.Drawing;

using System.Runtime.InteropServices;
using ZXing;
using ZXing.QrCode;
using ZXing.Rendering;


namespace QRCodeGenerator
{
    public class QRGenerate : IQRGenerator
    {
        public Task<IFormFile> GeneratorQRAsync(string content, string fileName="file", int width = 300, int height = 300)
        {
            content = content ?? "https://www.youtube.com/watch?v=dQw4w9WgXcQ";

            var writer = new BarcodeWriterPixelData
            {
                Format = BarcodeFormat.QR_CODE,
                Options = new QrCodeEncodingOptions
                {
                    Width = width,
                    Height = height,
                    Margin = 1,
                    ErrorCorrection = ZXing.QrCode.Internal.ErrorCorrectionLevel.Q
                }
            };

            PixelData pixelData = writer.Write(content);
            byte[] bytes = ConvertingToBitmapAsync(pixelData).GetAwaiter().GetResult();
            return ConvertToIFormFileAsync(bytes, fileName);

        }

        private async Task<byte[]> ConvertingToBitmapAsync(PixelData pixelData)
        {
            using var image = Image.LoadPixelData<Bgra32>(pixelData.Pixels, pixelData.Width, pixelData.Height);
            using var ms = new MemoryStream();
            await image.SaveAsPngAsync(ms);
            return ms.ToArray();
        }

        private async Task<IFormFile> ConvertToIFormFileAsync(byte[] imageBytes, string fileName)
        {
            using var stream = new MemoryStream(imageBytes);
            var formFile = new FormFile(stream, 0, stream.Length, "file", fileName)
            {
                Headers = new HeaderDictionary(),
                ContentType = "image/png"
            };
            return await Task.FromResult(formFile);
        }
    }
}
