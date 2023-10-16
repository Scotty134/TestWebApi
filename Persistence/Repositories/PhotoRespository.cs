using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Abstraction.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class PhotoRespository : IPhotoRepository
    {
        private readonly DataContext _context;

        public PhotoRespository(DataContext context)
        {
            _context = context;
        }

        public Photo Add(Photo photo, int userId)
        {
            var user = _context.Users
                .Include(p => p.Photos)
                .FirstOrDefault(u => u.Id == userId);
            
            if (user == null) return null;

            user.Photos.Add(photo);
            _context.SaveChanges();

            return photo;
        }

        public void Delete(int photoId, int userId)
        {
            var user = _context.Users
                .Include(p => p.Photos)
                .FirstOrDefault(u => u.Id == userId);

            if (user == null) return;
            var photo = user.Photos.FirstOrDefault(p => p.Id == photoId);
            if (!photo.IsMain)
            {
                user.Photos.Remove(photo);
                _context.SaveChanges();
            }
        }

        public async Task<Photo> GetPhotoByIdAsync(int photoId, int userId)
        {
            var user = await _context.Users
                .Include(p => p.Photos)
                .Where(u => u.Id == userId)
                .SingleOrDefaultAsync();

            if (user == null) return null;
            var photo = user.Photos.FirstOrDefault(p => p.Id == photoId);
            return photo;
        }

        public async Task<Photo> SetMainPhotoAsync(int photoId, int userId)
        {
            var user = _context.Users
                .Include(p => p.Photos)
                .FirstOrDefault(u => u.Id == userId);
            if (user == null) return null;
            var photo = user.Photos.FirstOrDefault(photo=> photo.Id == photoId);
            if (photo == null || photo.IsMain) return null;
            var currentMainPhoto = user.Photos.FirstOrDefault(p => p.IsMain == true);
            if (currentMainPhoto != null) currentMainPhoto.IsMain = false;
            photo.IsMain = true;
            await _context.SaveChangesAsync();
            return photo;
        }
    }
}
