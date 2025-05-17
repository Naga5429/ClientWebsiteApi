using System.ComponentModel.DataAnnotations;

namespace ClientWebsiteAPI.Model
{
    public class UserTokenLogins
    {
        [Required]
        public string? username { get; set; }
        [Required]
        public string? password { get; set; }
        [Required]
        public int? interfaceID { get; set; }

        
    }
}
