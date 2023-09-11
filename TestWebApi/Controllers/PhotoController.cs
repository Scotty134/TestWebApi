using Infrastructure.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Abstraction.Services;
using TestWebApi.Extensions;
using TestWebApi.Interfaces;

namespace TestWebApi.Controllers
{
    [Authorize]
    public class PhotoController : BaseController
    {
        private ICloudPhotoService _cloudPhotoService;
        private IUserService _userService;
        private IPhotoService _photoService;

        public PhotoController(ICloudPhotoService cloudPhotoService, 
            IUserService userService, 
            IPhotoService photoService)
        {
            _cloudPhotoService = cloudPhotoService;
            _userService = userService;
            _photoService = photoService;
        }

        [HttpPost("add-photo")]
        public async Task<ActionResult<PhotoDto>> AddPhoto(IFormFile file)
        {
            var userName = User.GetUserName();
            var user = _userService.GetUserByName(userName);
            if (user == null)
                return NotFound();
            var result = await _cloudPhotoService.AddPhotoAsync(file);

            if (result.Error != null) return BadRequest(result.Error.Message);

            var photo = new PhotoDto
            {
                Url = result.SecureUri.AbsoluteUri,
                PublicId= result.PublicId
            };

            if ( user.Photos.Count == 0 ) photo.IsMain = true;

            photo = _photoService.AddPhoto(photo, user.Id);

            return photo;
        }

        [HttpPut("set-main/{photoId}")]
        public async Task<ActionResult> SetMainPhoto(int photoId)
        {
            var userName = User.GetUserName();
            var user = _userService.GetUserByName(userName);
            if (user == null)
                return NotFound();
            var photo = await _photoService.SetMainPhotoAsync(photoId, user.Id);
            if (photo == null) return BadRequest();
            return Ok(photo);
        }

        [HttpDelete("delete-photo/{photoId}")]
        public async Task<ActionResult> DeletePhoto(int photoId)
        {
            var userName = User.GetUserName();
            var user = _userService.GetUserByName(userName);
            if (user == null)
                return NotFound();
            var photo = await _photoService.GetPhotoByIdAsync(photoId, user.Id);
            if (photo.IsMain) return BadRequest();

            if (photo.PublicId != null)
            {
                var result = await _cloudPhotoService.DeletePhotoAsync(photo.PublicId);
                if (result.Error != null) return BadRequest(result.Error.Message);
            }

            _photoService.DeletePhoto(photoId, user.Id);

            return Ok();
        }
    }
}
