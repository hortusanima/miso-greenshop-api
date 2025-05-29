using miso_greenshop_api.Domain.Validations;
using System.ComponentModel.DataAnnotations;

namespace miso_greenshop_api.Dtos.Users
{
    public class GetUserDto
    {
        [Required]
        [EmailIsValid]
        public string? UserEmail { get; set; }
        [Required]
        [UsernameIsValid]
        public string? UserName { get; set; }
        [Required]
        public bool IsSubscribed { get; set; }
    }
}
