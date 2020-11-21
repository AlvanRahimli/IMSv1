using System.ComponentModel.DataAnnotations;

namespace IMSv1.Models.Dtos
{
    public class UserLoginDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Password { get; set; }
    }
}