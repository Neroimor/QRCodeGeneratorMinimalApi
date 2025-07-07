namespace QRCodeGenerator
{
    public interface IQRGenerator
    {
        public Task<IFormFile> GeneratorQRAsync(string content, string fileName, int width = 300, int height = 300);
    }
}
