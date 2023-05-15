using HelloJobBackEnd.Entities;
using HelloJobBackEnd.Utilities.Comparer;
using Microsoft.EntityFrameworkCore;
using System.Drawing;

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


        public static List<Cv> RelatedByBusinessArea(IQueryable<Cv> queryable, Cv cv, int id)
        {
            List<Cv> relatedCvs = new();
            if (cv.BusinessAreaId != 0)
            {
                List<Cv> relatedByBusinessArea = queryable
                    .Include(v => v.BusinessArea)
                    .Include(e => e.Education)
                    .Include(ex => ex.Experience)
                    .Include(c => c.City)
                    .Include(o => o.OperatingMode)
                    .Include(u => u.User)
                    .AsEnumerable()
                    .Where(p =>
                        p.BusinessAreaId == cv.BusinessAreaId &&
                        p.Id != id &&
                        !relatedCvs.Contains(p, new CvComparer())
                    )
                    .Take(6).ToList();

                relatedCvs.AddRange(relatedByBusinessArea);
            }

            return relatedCvs;
        }

    }
}
