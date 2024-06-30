using System.IO;
using System.Threading.Tasks;
using Microsoft.Maui.Storage;

namespace SweepSenseApp.Services
{
    public class ImageService
    {
        private readonly string _uploadsPath;

        public ImageService()
        {
            _uploadsPath = Path.Combine(FileSystem.AppDataDirectory, "Uploads");
            if (!Directory.Exists(_uploadsPath))
            {
                Directory.CreateDirectory(_uploadsPath);
            }
        }

        public async Task<string> SaveImageAsync(Stream imageStream, string fileName)
        {
            try
            {
                var filePath = Path.Combine(_uploadsPath, fileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                {
                    await imageStream.CopyToAsync(fileStream);
                }

                return filePath;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"An error occurred while saving the image: {ex.Message}");
                throw;
            }
        }

        public string GetImagePath(string fileName)
        {
            return Path.Combine(_uploadsPath, fileName);
        }
    }
}
