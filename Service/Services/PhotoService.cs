using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Domain.Entities;
using Infrastructure.Dtos;
using Infrastructure.Mapping;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Persistence.Abstraction.Repositories;
using Service.Abstraction.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly IMapper _mapper;
        private readonly IPhotoRepository _photoRepository;

        public PhotoService(IPhotoRepository photoRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AllowNullDestinationValues = true;
                cfg.AddProfile<DefaultProfile>();
            });
            _mapper = new Mapper(config);
            _photoRepository = photoRepository;
        }

        public PhotoDto AddPhoto(PhotoDto photo, int userId)
        {
            var model = _mapper.Map<Photo>(photo);
            model = _photoRepository.Add(model, userId);
            photo = _mapper.Map<PhotoDto>(model);
            return photo;
        }

        public void DeletePhoto(int photoId, int userId)
        {
            _photoRepository.Delete(photoId, userId);
        }

        public async Task<PhotoDto> GetPhotoByIdAsync(int photoId, int userId)
        {
            var model = await _photoRepository.GetPhotoByIdAsync(photoId, userId);
            var photo = _mapper.Map<PhotoDto>(model);
            return photo;
        }

        public async Task<PhotoDto> SetMainPhotoAsync(int photoId, int userId)
        {
            var model = await _photoRepository.SetMainPhotoAsync(photoId, userId);
            var photo = _mapper.Map<PhotoDto>(model);
            return photo;
        }
    }
}
