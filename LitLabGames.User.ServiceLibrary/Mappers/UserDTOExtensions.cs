using LitLabGames.User.ServiceLibrary.DTOs;

namespace LitLabGames.User.ServiceLibrary.Mappers
{
    public static class UserDTOExtensions
    {
        public static UserDTO ToUserDTO(this DataAccess.Entities.User source)
        {
            if (source == null) return default;

            UserDTO result = new UserDTO()
            {
                Id = source.Id,
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
