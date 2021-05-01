using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniversityAPI.Models;
using UniversityAPI.Models.DTO;
using UniversityAPI.Repository.IRepository;

namespace UniversityAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _repo;
        public UserController(IUserRepository repo)
        {
            _repo = repo;
        }

        [AllowAnonymous]  //Cho phep vuot qua Authorize cua Controller
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] User model)
        {
            var user = _repo.Authenticate(model.UserName, model.Password);
            if (user == null)
            {
                return BadRequest(new { message = "Username or Password is incorrect" });
            }
            return Ok(user);
        }
    }
}
