using Microsoft . AspNetCore . Hosting ;
using Microsoft . AspNetCore . Http ;
using NewHuylebronVilla . Application . Common . Interface ;

namespace NewHuylebronVilla.Application.Services.Implementation ;

public class FileService : IFileService
{
     private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly string _uploadsBasePath;
    private readonly long _maxFileSize = 5 * 1024 * 1024; // 5MB
    private readonly string[] _allowedExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".webp" };

    public FileService(IWebHostEnvironment webHostEnvironment)
    {
        _webHostEnvironment = webHostEnvironment;
        _uploadsBasePath = Path.Combine(_webHostEnvironment.WebRootPath, "images");
    }

    public async Task<string> UploadFileAsync(IFormFile file, string folderName)
    {
        if (file == null || file.Length == 0)
        {
            throw new ArgumentException("Tệp không được để trống.");
        }

        if (file.Length > _maxFileSize)
        {
            throw new ArgumentException($"Kích thước tệp không được vượt quá {_maxFileSize / 1024 / 1024}MB.");
        }

        var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();
        if (!_allowedExtensions.Contains(fileExtension))
        {
            throw new ArgumentException($"Loại tệp không được hỗ trợ. Các loại được hỗ trợ: {string.Join(", ", _allowedExtensions)}");
        }

        string fileName = Guid.NewGuid().ToString() + fileExtension;
        string folderPath = Path.Combine(_uploadsBasePath, folderName);
        string filePath = Path.Combine(folderPath, fileName);
        string relativePath = Path.Combine("\\images", folderName, fileName);

        // Đảm bảo thư mục tồn tại
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        try
        {
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
            return relativePath;
        }
        catch (Exception ex)
        {
            throw new IOException($"Lỗi khi lưu tệp: {ex.Message}", ex);
        }
    }

    public bool DeleteFile(string filePath)
    {
        if (string.IsNullOrEmpty(filePath))
        {
            return false;
        }

        try
        {
            var fullPath = Path.Combine(_webHostEnvironment.WebRootPath, filePath.TrimStart('\\'));
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
                return true;
            }
            return false;
        }
        catch (Exception)
        {
            return false;
        }
    }
}