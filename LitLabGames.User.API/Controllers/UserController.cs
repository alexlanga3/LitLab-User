using LitLabGames.User.API.Extensions;
using LitLabGames.User.API.Models;
using LitLabGames.User.ServiceLibrary.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace LitLabGames.User.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;

        private readonly IUserService _userService;

        public UserController(ILogger<UserController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [HttpGet("Name")]
        public IActionResult Get(string name)
        {
            if (String.IsNullOrEmpty(name))
            {
                _logger.LogError($"The user couldn't be retrieved.");
                return BadRequest();
            }

            _logger.LogDebug($"Getting User named {name}.");

            return Ok(_userService.GetUserByName(name).ToUserNameValidationDTO());
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserViewModel model)
        {
            if (model == null)
            {
                _logger.LogError($"The user couldn't be saved.");
                return BadRequest();
            }

            _logger.LogDebug($"Saving User with name {model.Name}.");

            var userDTO = model.ToUserDTO();

            var validationResult = _userService.DoExtraValidationOnUser(userDTO);
            if (!validationResult)
            {
                _logger.LogError($"Error. The User {model.Name} has validation errors");
                return BadRequest();
            }

            var result = await _userService.SaveAsync(userDTO);
            if (result)
            {
                _logger.LogInformation($"The User {model.Name} saved.");
                return Ok(result);
            }

            _logger.LogError($"The User {model.Name} couldn't be saved.");
            return BadRequest(result);
        }

        [HttpDelete("DeleteUser")]
        public async Task<IActionResult> Delete(string name)
        {
            _logger.LogDebug($"Entering in Delete User named {name}.");

            if (String.IsNullOrEmpty(name))
            {
                _logger.LogError($"The user couldn't be removed.");
                return BadRequest();
            }

            var result = await _userService.DeleteAsync(name);

            if (result)
            {
                _logger.LogInformation("User deleted.");
                return Ok(result);
            }

            _logger.LogError($"The user {name} couldn't be deleted.");
            return BadRequest(result);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UserViewModel model)
        {
            if (model == null)
            {
                _logger.LogError($"The user couldn't be updated.");
                return BadRequest();
            }

            _logger.LogDebug($"UPdating User with name {model.Name}.");

            var userDTO = model.ToUserDTO();

            var validationResult = _userService.DoExtraValidationOnUser(userDTO);
            if (!validationResult)
            {
                _logger.LogError($"Error. The User {model.Name} has validation errors");
                return BadRequest();
            }

            var result = await _userService.UpdateAsync(userDTO);
            if (result)
            {
                _logger.LogInformation($"The User {model.Name} updated.");
                return Ok(result);
            }

            _logger.LogError($"The User {model.Name} couldn't be updated.");
            return BadRequest(result);
        }

    }
}
