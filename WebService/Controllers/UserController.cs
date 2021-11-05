using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebService.Models;

namespace WebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public UserController(ApplicationDbContext context)
        {
            this.context = context;
        }
        [HttpPost]
        [Route("/api/User/Register")]
        public IActionResult Register(User user)
        {
            try
            {
                context.Users.Add(user);
                context.SaveChanges();
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        [Route("/api/User/Login")]
        public IActionResult Login(User user)
        {
            var _user = from us in context.Users
                        where us.Email == user.Email && us.Password == user.Password
                        select us;
            if (_user.Any())
            {
                return Ok(_user.FirstOrDefault());
            }
            return BadRequest("Login Failed !");
        }
    }
}
