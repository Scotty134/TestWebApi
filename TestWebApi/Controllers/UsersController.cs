﻿using Infrastructure.Dtos;
using Infrastructure.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Abstraction.Services;
using TestWebApi.Extensions;

namespace TestWebApi.Controllers
{
    [Authorize]
    public class UsersController : BaseController
    {
        private IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        //[HttpGet]
        //public ActionResult<IEnumerable<MemberDto>> GetUsers()
        //{            
        //    var users = _userService.GetUsers();
        //    return users.ToList();
        //}

        [HttpGet]
        public async Task<ActionResult<PagedList<MemberDto>>> GetUsers([FromQuery]UserParams userParams)
        {
            var currentUser = _userService.GetUserByName(User.GetUserName());
            userParams.CurrentUsername = currentUser.UserName;

            if (string.IsNullOrEmpty(userParams.Gender))
            {
                userParams.Gender = currentUser.Gender == "male"? "female" : "male";
            }

            var result = await _userService.GetUsersAsync(userParams);
            Response.AddPaginationHeader(new PaginationHeader(result.CurrentPage, result.PageSize, result.TotalCount, result.TotalPages));
            return Ok(result);
        }

        [HttpGet("id/{id}")]
        public ActionResult<MemberDto> GetUserById(int id)
        {
            var user = _userService.GetUserById(id);
            return user;
        }

        [HttpGet("{name}")]
        public ActionResult<MemberDto> GetUserByName(string name)
        {
            var user = _userService.GetUserByName(name);
            return user;
        }

        [HttpPut]
        public ActionResult UpdateUser (MemberUpdateDto model)
        {
            var userName = User.GetUserName();
            if(userName == null) return NotFound();
            try
            {
                _userService.UpdateUser(userName, model);
                return NoContent();
            }
            catch
            {
                return BadRequest("Failed to update the user.");
            }            
        }
    }
}
