using LitLabGames.User.Crosscutting.Entities;

namespace LitLabGames.User.DataAccess.Entities
{
    public class User : BaseEntity
    {        
        public string Nick { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Direction { get; set; }
        public string PhoneNumber { get; set; }
    }
}
