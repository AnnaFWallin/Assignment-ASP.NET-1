using System.ComponentModel.DataAnnotations;

namespace Assignment.WebAppMvc.Models.ViewModels
{
    public class SignInViewModel
    {
        [Display(Name = "Emailadress")]
        [EmailAddress(ErrorMessage = "Du måste ange en giltig emailadress.")]
        [StringLength(100, ErrorMessage = "Emailadressen måste vara en giltig emailadress.", MinimumLength = 6)]
        public string Email { get; set; }

        [Display(Name = "Lösenord")]
        [Required(ErrorMessage = "Du måste ange ett lösenord.")]
        [DataType(DataType.Password)]
        [StringLength(50, ErrorMessage = "Lösenordet måste bestå av minst 8 tecken.", MinimumLength = 8)]
        public string Password { get; set; }

        public string ReturnUrl { get; set; }
    }
}
