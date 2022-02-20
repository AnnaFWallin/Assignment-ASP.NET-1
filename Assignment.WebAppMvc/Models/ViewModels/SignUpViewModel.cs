using System.ComponentModel.DataAnnotations;

namespace Assignment.WebAppMvc.Models.ViewModels
{
    public class SignUpViewModel
    {
        [Display(Name = "Förnamn")]
        [Required(ErrorMessage = "Du måste ange ett förnamn.")]        
        [StringLength(50, ErrorMessage = "Förnamnet måste bestå av minst 2 tecken.", MinimumLength = 2)]
        public string FirstName { get; set; }
        [Display(Name = "Efternamn")]
        [Required(ErrorMessage = "Du måste ange ett efternamn.")]
        [StringLength(50, ErrorMessage = "Efternamnet måste bestå av minst 2 tecken.", MinimumLength = 2)]
        public string LastName { get; set; }

        [Display(Name = "Emailadress")]
        [EmailAddress(ErrorMessage = "Du måste ange en giltig emailadress.")]
        [StringLength(100, ErrorMessage = "Emailadressen måste vara en giltig emailadress.", MinimumLength = 6)]
        public string Email { get; set; }

        [Display(Name = "Lösenord")]
        [Required(ErrorMessage = "Du måste ange ett lösenord.")]
        [DataType(DataType.Password)]
        [StringLength(50, ErrorMessage = "Lösenordet måste bestå av minst 8 tecken.", MinimumLength = 8)]
        public string Password { get; set; }

        [Display(Name = "Bekräfta lösenord")]
        [Required(ErrorMessage = "Du måste bekräfta lösenordet")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Lösenorden matchar inte.")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Gatuadress")]
        [Required(ErrorMessage = "Du måste ange ett gatuadress.")]
        [StringLength(50, ErrorMessage = "Gatuadressen måste bestå av minst 2 tecken.", MinimumLength = 2)]
        public string StreetName { get; set; }

        [Display(Name = "Postnummer")]
        [Required(ErrorMessage = "Du måste ange ett förnamn.")]
        [StringLength(50, ErrorMessage = "Postnumret måste bestå av minst 5 siffror.", MinimumLength = 5)]
        public string PostalCode { get; set; }

        [Display(Name = "Stad")]
        [Required(ErrorMessage = "Du måste ange en stad.")]
        [StringLength(50, ErrorMessage = "Staden måste bestå av minst 2 tecken.", MinimumLength = 2)]
        public string City { get; set; }
             
    }
}
