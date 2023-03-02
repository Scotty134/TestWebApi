using CloudinaryDotNet.Actions;

namespace TestWebApi.Interfaces
{
    public interface ICloudPhotoService
    {
        public Task<ImageUploadResult> AddPhotoAsync(IFormFile photo);
        public Task<DeletionResult> DeletePhotoAsync(string publicId);
    }
}
