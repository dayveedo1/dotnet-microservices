using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserService.Data;
using UserService.Data.model;

namespace UserService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserDbContext db;
        public UserController(UserDbContext db)
        {
            this.db = db;         
        }


        [HttpGet]
        public IEnumerable<User> GetUser()
        {
            return db.Users.ToList();
        }

        [HttpGet("{id}")]
        public User GetUserSingle(int id)
        {

            return db.Users.Find(id);
           
        }

        [HttpPost]
        public IActionResult SaveUser([FromBody] User user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //var generateUserId = System.Guid.NewGuid();
                    //user.UserId = generateUserId;

                    db.Users.Add(user);
                    db.SaveChanges();

                    return StatusCode(StatusCodes.Status201Created);
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest);
                }
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
    }
}
