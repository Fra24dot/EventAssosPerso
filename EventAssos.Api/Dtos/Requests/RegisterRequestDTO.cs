using System.ComponentModel.DataAnnotations;

namespace EventAssos.Api.Dtos.Requests
{
    public class RegisterRequestDTO
    {
        [Required(ErrorMessage = "L'email est requis.")]//Validation
        [EmailAddress(ErrorMessage = "Le format est incorrect.")]
        public string Email { get; set; } = null!;

    }
}
