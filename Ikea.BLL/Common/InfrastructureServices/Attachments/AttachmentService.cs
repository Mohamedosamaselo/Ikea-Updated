using Microsoft.AspNetCore.Http;

namespace Ikea.BLL.Common.InfrastructureServices.Attachments
{
    public  class AttachmentService : IAttachmentService
    {
        private readonly List<string> _allowedExtensions = new List<string>() { ".png", ".jpg", ".jpeg" };
       
        private const int _allowedMaxSize = 2_097_152;

        /// Upload File 
        // 1. Get Located Folder Path .
        // 2. Get File Name and Make it Unique .
        // 3. Get File Path[Folder Path + FileName] . 
        // 4. Save File As Streams . 
        // 5. Return File Name .

         public async Task<string?> UploadAsync(IFormFile file, string FolderName)
        {

            if (file == null || file.Length == 0)
                return null;

            var extension = Path.GetExtension(file.FileName);

            if (!_allowedExtensions.Contains(extension))// validation on extenstions 
                return null;

            if (file.Length > _allowedMaxSize) // Validations on size of file 
                return null;

            /// 1. Get Located Folder Path .

            //var folderPath = $"{Directory.GetCurrentDirectory()}\\wwwroot\\Files\\{folderName}";
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", FolderName);

            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            // 2. Get File Name and Make it Unique .

            //var FileName = $"{Guid.NewGuid()}{file.FileName}";
            var FileName = $"{Guid.NewGuid()}{extension}"; // Must be unique

            // 3. Get FilePath ( Folder Path + FileName ) . 
            var FilePath = Path.Combine(folderPath, FileName); // File Location Placed

            // 4. Save File As Streams . 
            using var FileStream = new FileStream(FilePath, FileMode.Create); // Streaming => is the Data Per Time
            // using var FileName = file.Create(FilePath)
            
            await file.CopyToAsync(FileStream);
           ///try
           ///      {
           ///      }
           ///finally
           ///      {
           ///fileStream.Dispose();
           ///      }

            // 5. Return File Name .
            return FileName;
        }

        public bool DeleteFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                return true;
            }
            return false;
        }
    }
}
