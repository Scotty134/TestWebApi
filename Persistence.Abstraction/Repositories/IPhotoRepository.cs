using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Abstraction.Repositories
{
    public interface IPhotoRepository
    {
        public Photo Add(Photo photo, int userId);
        public Task<Photo> SetMainPhotoAsync(int photoId, int userId);
        public Task<Photo> GetPhotoByIdAsync(int photoId, int userId);
        public void Delete(int photoId, int userId);
    }
}
