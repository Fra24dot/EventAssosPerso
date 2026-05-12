using System.ComponentModel.DataAnnotations;

namespace EventAssos.Api.Dtos.Requests
{
    public class LoginRequestDto
    {
        [Required(ErrorMessage = "The email is required.")]
        public required string Email { get; set; } = null!;

        [Required(ErrorMessage = "The password is required.")]
        public required string Password { get; set; } = null!;
    }
}
