using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment.WebAppMvc.Models
{
    public class ApplicationUser : IdentityUser
    {
        [PersonalData]
        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string FirstName { get; set; }
        [PersonalData]
        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string LastName { get; set; }

        [PersonalData]
        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string StreetName { get; set; }
        [PersonalData]
        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string PostalCode { get; set; }
        [PersonalData]
        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string City { get; set; }

    }
}
