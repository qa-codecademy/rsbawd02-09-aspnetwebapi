﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SEDC.NotesApp.Dtos.Users;
using SEDC.NotesApp.Services.Interfaces;
using SEDC.NotesApp.Shared.CustomExceptions;

namespace SEDC.NotesApp.Controllers
{
    [Authorize] 
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous] //no token needed (we can not be logged in before registration)
        [HttpPost("register")] 
        public IActionResult Register([FromBody] RegisterUserDto registerUserDto)
        {
            try
            {
                _userService.RegisterUser(registerUserDto);
                return StatusCode(StatusCodes.Status201Created, "User created!");
            }
            catch(UserDataException e)
            {
                return BadRequest(e.Message);
            }
            catch(Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred!");
            }
        }


        [AllowAnonymous] //no token needed (we can not be logged in before login)
        [HttpPost("login")]
        public IActionResult LoginUser([FromBody] LoginUserDto loginDto)
        {
            try
            {
                string token = _userService.LoginUser(loginDto);
                return Ok(token);
            }
            catch(UserDataException ude)
            {
                return BadRequest("No user with this id");
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred!");
            }
        }


        [HttpGet("info")]
        public IActionResult GetUserInfo()
        {
            int userId = Int32.Parse(User.FindFirst("UserId")?.Value);

            var response = _userService.Info(userId);

            return Ok(response);
        }
    }
}
