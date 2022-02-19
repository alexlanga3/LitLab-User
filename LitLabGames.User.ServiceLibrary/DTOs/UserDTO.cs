using System;
using System.Collections.Generic;
using System.Text;

namespace LitLabGames.User.ServiceLibrary.DTOs
{
    public class UserDTO
    {
        public Guid Id { get; set; }
        public string Nick { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Direction { get; set; }
        public string PhoneNumber { get; set; }
    }
}
