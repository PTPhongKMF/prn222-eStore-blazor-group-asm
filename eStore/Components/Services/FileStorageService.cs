using Microsoft.AspNetCore.Components.Forms;

namespace eStore.Components.Services
{
    public interface IFileStorageService
    {
        Task<(bool Success, string Message, string? FilePath)> UploadProductImageAsync(IBrowserFile file);
        Task<bool> DeleteFileAsync(string? filePath);
        string GetImageUrl(string? imagePath);
        Task<bool> EnsureDirectoryExistsAsync();
    }

    public class FileStorageService : IFileStorageService
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _environment;
        private readonly ILogger<FileStorageService> _logger;

        public FileStorageService(
            IConfiguration configuration, 
            IWebHostEnvironment environment,
            ILogger<FileStorageService> logger)
        {
            _configuration = configuration;
            _environment = environment;
            _logger = logger;
        }

        public async Task<(bool Success, string Message, string? FilePath)> UploadProductImageAsync(IBrowserFile file)
        {
            try
            {
                // Get configuration
                var config = _configuration.GetSection("FileStorage:ProductImages");
                var maxSizeInMB = config.GetValue<int>("MaxSizeInMB");
                var maxSizeInBytes = maxSizeInMB * 1024 * 1024;
                var allowedExtensions = config.GetSection("AllowedExtensions").Get<string[]>();

                // Validate inputs
                if (file == null)
                {
                    return (false, "Kh�ng c� file ???c ch?n", null);
                }

                // Validate file size
                if (file.Size > maxSizeInBytes)
                {
                    return (false, $"K�ch th??c file kh�ng ???c v??t qu� {maxSizeInMB}MB. File hi?n t?i: {file.Size / (1024 * 1024):F1}MB", null);
                }

                if (file.Size == 0)
                {
                    return (false, "File kh�ng c� d? li?u", null);
                }

                // Validate extension
                var extension = Path.GetExtension(file.Name).ToLowerInvariant();
                if (string.IsNullOrEmpty(extension))
                {
                    return (false, "File kh�ng c� ph?n m? r?ng", null);
                }

                if (allowedExtensions == null || !allowedExtensions.Contains(extension))
                {
                    return (false, $"??nh d?ng file kh�ng ???c h? tr?: {extension}. Ch? ch?p nh?n: {string.Join(", ", allowedExtensions ?? [])}", null);
                }

                // Ensure directory exists
                var directoryCreated = await EnsureDirectoryExistsAsync();
                if (!directoryCreated)
                {
                    return (false, "Kh�ng th? t?o th? m?c upload", null);
                }

                // Generate unique filename
                var fileName = $"{DateTime.Now:yyyyMMdd_HHmmss}_{Guid.NewGuid():N}{extension}";
                var uploadDirectory = Path.Combine(_environment.WebRootPath, "uploads", "products");
                var fullPath = Path.Combine(uploadDirectory, fileName);

                // Check if directory is writable
                if (!Directory.Exists(uploadDirectory))
                {
                    return (false, "Th? m?c upload kh�ng t?n t?i", null);
                }

                // Save file with proper error handling
                try
                {
                    using var stream = new FileStream(fullPath, FileMode.Create, FileAccess.Write);
                    using var fileStream = file.OpenReadStream(maxSizeInBytes);
                    await fileStream.CopyToAsync(stream);
                    await stream.FlushAsync();
                }
                catch (UnauthorizedAccessException)
                {
                    return (false, "Kh�ng c� quy?n ghi file v�o th? m?c upload", null);
                }
                catch (DirectoryNotFoundException)
                {
                    return (false, "Th? m?c upload kh�ng t?n t?i", null);
                }
                catch (IOException ioEx)
                {
                    return (false, $"L?i I/O khi l?u file: {ioEx.Message}", null);
                }

                // Verify file was saved
                if (!File.Exists(fullPath))
                {
                    return (false, "File kh�ng ???c l?u th�nh c�ng", null);
                }

                var fileInfo = new FileInfo(fullPath);
                if (fileInfo.Length == 0)
                {
                    File.Delete(fullPath); // Clean up empty file
                    return (false, "File ???c l?u nh?ng kh�ng c� d? li?u", null);
                }

                var baseUrl = config.GetValue<string>("BaseUrl");
                var relativePath = $"{baseUrl}/{fileName}";
                
                _logger.LogInformation("Successfully uploaded image: {FileName} ({Size} bytes) to {Path}", 
                    fileName, fileInfo.Length, relativePath);
                
                return (true, "Upload th�nh c�ng", relativePath);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error uploading file: {FileName}", file?.Name ?? "unknown");
                return (false, $"L?i kh�ng mong mu?n: {ex.Message}", null);
            }
        }

        public async Task<bool> DeleteFileAsync(string? filePath)
        {
            try
            {
                if (string.IsNullOrEmpty(filePath) || filePath.StartsWith("http")) 
                    return true; // External URL or null, nothing to delete

                var config = _configuration.GetSection("FileStorage:ProductImages");
                var baseUrl = config.GetValue<string>("BaseUrl");
                
                if (!filePath.StartsWith(baseUrl!))
                    return true; // Not our file

                // Extract filename from path
                var fileName = Path.GetFileName(filePath);
                if (string.IsNullOrEmpty(fileName))
                    return true;

                var fullPath = Path.Combine(_environment.WebRootPath, "uploads", "products", fileName);

                if (File.Exists(fullPath))
                {
                    File.Delete(fullPath);
                    _logger.LogInformation("Successfully deleted file: {FilePath}", fullPath);
                }

                await Task.CompletedTask;
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting file: {FilePath}", filePath);
                return false;
            }
        }

        public string GetImageUrl(string? imagePath)
        {
            if (string.IsNullOrEmpty(imagePath))
            {
                var config = _configuration.GetSection("FileStorage:ProductImages");
                return config.GetValue<string>("DefaultImage") ?? "/images/no-product-image.png";
            }
            
            return imagePath.StartsWith("http") ? imagePath : imagePath;
        }

        public async Task<bool> EnsureDirectoryExistsAsync()
        {
            try
            {
                var fullPath = Path.Combine(_environment.WebRootPath, "uploads", "products");
                
                if (!Directory.Exists(fullPath))
                {
                    Directory.CreateDirectory(fullPath);
                    
                    // Test write permission
                    var testFile = Path.Combine(fullPath, "test_write.tmp");
                    try
                    {
                        await File.WriteAllTextAsync(testFile, "test");
                        File.Delete(testFile);
                    }
                    catch
                    {
                        _logger.LogError("Upload directory created but not writable: {Path}", fullPath);
                        return false;
                    }
                    
                    _logger.LogInformation("Created upload directory: {Path}", fullPath);
                }

                await Task.CompletedTask;
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating upload directory");
                return false;
            }
        }
    }
}