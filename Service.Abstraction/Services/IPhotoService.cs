using CloudinaryDotNet.Actions;
using Infrastructure.Dtos;
using Microsoft.AspNetCore.Http;

namespace Service.Abstraction.Services
{
    public interface IPhotoService
    {
        public PhotoDto AddPhoto(PhotoDto photo, int userId);
        public Task<PhotoDto> SetMainPhotoAsync(int photoId, int userId);
        public Task<PhotoDto> GetPhotoByIdAsync(int photoId, int userId);
        public void DeletePhoto(int photoId, int userId);
    }
}
