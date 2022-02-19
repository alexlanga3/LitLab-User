using System.ComponentModel.DataAnnotations;

namespace LitLabGames.User.API.Models
{
    public class UserViewModel
    {
        [StringLength(20)]
        public string Nick { get; set; }

        [Required]
        [StringLength(20)]
        public string Name { get; set; }

        [Required]
        [StringLength(25)]
        public string LastName { get; set; }

        [StringLength(30)]
        public string Email { get; set; }

        [StringLength(50)]
        public string Direction { get; set; }

        [StringLength(12)]
        public string PhoneNumber { get; set; }
    }
}
