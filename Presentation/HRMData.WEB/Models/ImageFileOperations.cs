using SixLabors.ImageSharp.Formats.Jpeg;

namespace HRMData.WEB.Models
{
    public class ImageFileOperations
    {

        public static string? SaveFile(IFormFile expenseDocumentPath,IWebHostEnvironment _env)
        {
            var fileName = Guid.NewGuid().ToString() + Path.GetFileName(expenseDocumentPath.FileName);
            var filePath = Path.Combine(_env.WebRootPath, "files", fileName);

            using (var stream = File.Create(filePath))
            {
                expenseDocumentPath.CopyTo(stream);
            }
            return fileName;
        }
        public static void DeleteFile(string file,IWebHostEnvironment _env)
        {
            if (string.IsNullOrEmpty(file))
                return;

            string filePath = Path.Combine(_env.WebRootPath, "files", file);

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
        public static string? ResizeAddPhoto(IFormFile photo, int width, IWebHostEnvironment _env,string path)
        {
            var photoName = Guid.NewGuid() + Path.GetExtension(photo.FileName);
            var photoPath = Path.Combine(_env.WebRootPath, path, photoName);
            using (var stream = new MemoryStream())
            {
                photo.CopyToAsync(stream).Wait(); // Kopyalama işleminin bitmesini bekleyin.
                stream.Seek(0, SeekOrigin.Begin);
                using var image = Image.Load(stream);
                var newWidth = width;
                var ratio = (double)newWidth / image.Width;
                var newHeight = (int)(image.Height * ratio);

                using var thumbnail = image.Clone(x => x.Resize(newWidth, newHeight));
                // Yeni boyutlu fotoğrafı kaydetmek için kullanılan kod
                thumbnail.Save(photoPath, new JpegEncoder());
            }

            return photoName;
        }
        public static void DeletePhoto(string photo, IWebHostEnvironment _env)
        {
            if (string.IsNullOrEmpty(photo))
                return;

            string filePath = Path.Combine(_env.WebRootPath, "img", photo);

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
    }
}
