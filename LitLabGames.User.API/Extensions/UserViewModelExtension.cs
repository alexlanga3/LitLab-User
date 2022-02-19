using LitLabGames.User.API.Models;
using LitLabGames.User.ServiceLibrary.DTOs;

namespace LitLabGames.User.API.Extensions
{
    public static class UserViewModelExtension
    {
        public static UserViewModel ToUserNameValidationDTO(this UserDTO source)
        {
            if (source == null) return default;

            return new UserViewModel()
            {
                Name = source.Name,
                LastName = source.LastName,
                Direction = source.Direction,
                Email = source.Email,
                Nick = source.Nick,
                PhoneNumber = source.PhoneNumber
            };
        }


        public static UserDTO ToUserDTO(this UserViewModel source)
        {
            if (source == null) return default;

            return new UserDTO()
            {
                Name = source.Name,
                LastName = source.LastName,
                Direction = source.Direction,
                Email = source.Email,
                Nick = source.Nick,
                PhoneNumber = source.PhoneNumber
            };
        }
    }
}
