using System.ComponentModel.DataAnnotations;

namespace ClientWebsiteAPI.Model
{
    public class Student
    {
        [Required]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string phone { get; set; }
    }
}
