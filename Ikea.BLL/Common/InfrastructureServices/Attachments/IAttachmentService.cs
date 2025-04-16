using Microsoft.AspNetCore.Http;

namespace Ikea.BLL.Common.InfrastructureServices.Attachments
{
    public interface IAttachmentService
    {
        Task<String> UploadAsync(IFormFile file, string folderName);
     
        bool DeleteFile(string filePath);

    }
}
