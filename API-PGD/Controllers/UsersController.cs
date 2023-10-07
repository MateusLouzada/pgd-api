using API_PGD.Models;
using API_PGD.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;


namespace API_PGD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserRepositorie _userRepositorie;

        public UsersController(UserRepositorie userRepositorie)
        {
            _userRepositorie = userRepositorie;
        }

        [HttpGet("{id}")]
        public ActionResult GetUserId(Guid id)
        {
            try
            {
                List<User> lstUsers = _userRepositorie.GetUserId(id);
                return Ok(lstUsers);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpGet]
        public ActionResult GetAllUsers()
        {
            try
            {
                List<User> lstUsers = _userRepositorie.GetAllUsers();
                return Ok(lstUsers);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }

        }

        [HttpPost]
        public ActionResult RegisterUser([FromBody] User user)
        {
            try
            {
                object result = _userRepositorie.InsertUser(user);
                return Ok(result);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpPut]
        public ActionResult UpdateUser([FromBody] User user)
        {
            try
            {
                string result = _userRepositorie.UpdateUser(user);
                return Ok(result);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteUser(Guid id)
        {
            try
            {
                _userRepositorie.DeleteUser(id);
                return Ok();
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }
    }
}
