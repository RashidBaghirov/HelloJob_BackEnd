using HelloJobBackEnd.Entities;
using Microsoft.EntityFrameworkCore;

namespace HelloJobBackEnd.Utilities.Extension
{
    public static class ExtensionMethods
    {

        public static async Task<string> CreateImage(this IFormFile file, string imagepath, string folder)
        {
            var destinationpath = Path.Combine(imagepath, folder);
            Random r = new();
            int random = r.Next(0, 1000);
            var filename = string.Concat(random, file.FileName);
            var path = Path.Combine(destinationpath, filename);
            using (FileStream stream = new(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return filename;
        }

        public static bool DeleteImage(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
                return true;
            }
            return false;
        }
        public static bool IsValidLength(this IFormFile file, double size)
        {
            return (double)file.Length / 1024 / 1024 <= size;
        }

        public static bool IsValidFile(this IFormFile file, string type)
        {
            return file.ContentType.Contains(type);
        }
    }
}
