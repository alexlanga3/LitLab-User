using LitLabGames.User.Domain.Interfaces;
using LitLabGames.User.ServiceLibrary.DTOs;
using LitLabGames.User.ServiceLibrary.Interfaces;
using LitLabGames.User.ServiceLibrary.Mappers;
using Microsoft.Extensions.Logging;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LitLabGames.User.ServiceLibrary.Implementations
{
    public class UserService : IUserService
    {
        public const string phoneNumberRegex = @"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{3})$";

        private readonly ILogger<UserService> _logger;

        /// <summary>
        /// The user domain service init (readonly).
        /// </summary>
        private readonly IUserDomainService _userDomainService;

        public UserService(ILogger<UserService> logger, IUserDomainService userDomainService)
        {
            _logger = logger;
            _userDomainService = userDomainService;
        }

        public UserDTO GetUserByName(string name)
        {
            _logger.LogDebug($"UserService. Getting User {name}");

            try
            {
                return _userDomainService.GetUserByName(name).ToUserDTO();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while getting the User {name}", ex);
                throw;
            }
        }

        public async Task<bool> SaveAsync(UserDTO userDTO)
        {
            _logger.LogDebug($"UserService. Saving User {userDTO.Name}");

            try
            {
                _userDomainService.AddUser(userDTO.ToUser());

                if (await _userDomainService.SaveChangesAsync() > 0)
                {
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while saving the User {userDTO.Name}", ex);
                throw;
            }            
        }        

        public async Task<bool> DeleteAsync(string name)
        {
            _logger.LogDebug($"UserService. Deleting User {name}");
            try
            {
                DataAccess.Entities.User entityToDelete = _userDomainService.GetUserByName(name);
                if (entityToDelete == null)
                {
                    return false;
                }

                _userDomainService.DeleteUserByName(entityToDelete);

                if (await _userDomainService.SaveChangesAsync() > 0)
                {
                    return true;
                }

                return false;

            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while deleting the User {name}", ex);
                throw;
            }
        }

        public async Task<bool> UpdateAsync(UserDTO userDTO)
        {
            _logger.LogDebug($"UserService. Updating User {userDTO.Name}");
            try
            {
                DataAccess.Entities.User entityToUpdate = _userDomainService.GetUserByName(userDTO.Name);
                if (entityToUpdate == null)
                {
                    return false;
                }

                entityToUpdate.LastName = userDTO.LastName;
                entityToUpdate.Nick = userDTO.Nick;
                entityToUpdate.PhoneNumber = userDTO.PhoneNumber;
                entityToUpdate.Direction = userDTO.Direction;
                entityToUpdate.Email = userDTO.Email;

                _userDomainService.UpdateUser(entityToUpdate);

                if (await _userDomainService.SaveChangesAsync() > 0)
                {
                    return true;
                }

                return false;

            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while updating the User {userDTO.Name}", ex);
                throw;
            }
        }

        public bool DoExtraValidationOnUser(UserDTO userDTO)
        {
            if (userDTO == null)
            {
                throw new ArgumentNullException(nameof(userDTO));
            }

            _logger.LogDebug($"Doing fields extra Validation");

            if (userDTO.Email != null)
            {
                var trimmedEmail = userDTO.Email.Trim();

                if (trimmedEmail.EndsWith("."))
                {
                    _logger.LogInformation($"The user email validation is not success | email: {userDTO.Email}.");
                    return false;
                }
                try
                {
                    var addr = new System.Net.Mail.MailAddress(userDTO.Email);
                    if (addr.Address != trimmedEmail)
                    {
                        _logger.LogInformation($"The user email validation is not success | email: {userDTO.Email}.");
                        return false;
                    }
                }
                catch
                {
                    return false;
                }

            }

            if (!String.IsNullOrEmpty(userDTO.PhoneNumber))
            {
                if (!Regex.IsMatch(userDTO.PhoneNumber, phoneNumberRegex))
                {
                    _logger.LogInformation($"The Phone number validation is not success | phoneNumber: {userDTO.PhoneNumber}.");
                    return false;
                }
            }

            return true;
        }
    }
}
