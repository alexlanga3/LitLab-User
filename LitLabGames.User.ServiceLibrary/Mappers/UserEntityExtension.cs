using LitLabGames.User.ServiceLibrary.DTOs;
using UserEntityRepo = LitLabGames.User.DataAccess.Entities.User;

namespace LitLabGames.User.ServiceLibrary.Mappers
{
    public static class UserEntityExtension
    {
        /// <summary>
        /// Map userDTO to User .
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>The <see cref="User"/>.</returns>
        public static UserEntityRepo ToUser(this UserDTO source)
        {
            if (source == null) return default;
            UserEntityRepo result = new UserEntityRepo
            {
                Id = new System.Guid(),
                Name = source.Name,
                LastName = source.LastName,
                Direction = source.Direction,
                Email = source.Email,
                Nick = source.Nick,
                PhoneNumber = source.PhoneNumber
            };
            return result;
        }
    }
}
