using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TCT.FitApp.BL;
using TCT.FitApp.BL.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TCT.FitApp.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Return a list of users
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> Get()
        {
            try
            {
                return Ok(await UserManager.Load());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Get a user by username
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        [HttpGet("{username}")]
        public async Task<ActionResult<User>> Get(string username)
        {
            try
            {
                return Ok(await UserManager.LoadByUsername(username));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Get a user by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<User>> Get(Guid id)
        {
            try
            {
                return Ok(await UserManager.LoadById(id));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Get a user by sessionKey
        /// </summary>
        /// <param name="sessionKey"></param>
        /// <returns></returns>
        [HttpPost("LoadBySessionKey/{sessionKey:Guid}")]
        public async Task<ActionResult<User>> LoadBySessionKey(Guid sessionKey)
        {
            try
            {
                return Ok(await UserManager.LoadBySessionKey(sessionKey));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Insert a new user
        /// </summary>
        /// <param name="User"></param>
        /// <param name="rollback"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] User User, bool rollback = false)
        {
            try
            {
                return Ok(await UserManager.Insert(User, rollback));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Returns a session key if login is successful
        /// </summary>
        /// <param name="user"></param>
        /// <param name="logoutOtherDevices"></param>
        /// <param name="rollback"></param>
        /// <returns></returns>
        [HttpPost("Login")]
        public async Task<IActionResult> Login(User user, bool logoutOtherDevices = false, bool rollback = false)
        {
            try
            {
                var sessionKey = await UserManager.Login(user, logoutOtherDevices, rollback);
                if (sessionKey != Guid.Empty)
                    return Ok(sessionKey);
                else
                    throw new Exception("Username or password is incorrect");
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        /// <summary>
        /// Update a user
        /// </summary>
        /// <param name="id"></param>
        /// <param name="User"></param>
        /// <param name="rollback"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] User User, bool rollback = false)
        {
            try
            {
                return Ok(await UserManager.Update(User, rollback));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Delete a user
        /// </summary>
        /// <param name="id"></param>
        /// <param name="rollback"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id, bool rollback = false)
        {
            try
            {
                return Ok(await UserManager.Delete(id, rollback));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
