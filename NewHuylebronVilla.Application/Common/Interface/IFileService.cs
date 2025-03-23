using Microsoft . AspNetCore . Http ;

namespace NewHuylebronVilla.Application.Common.Interface ;

public interface IFileService
{
    Task<string> UploadFileAsync(IFormFile file, string folderName);
    bool DeleteFile(string filePath);
}