using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MusicWave.Models
{
    public class CustomUser 
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "First Name should not be empty")]
        [MinLength(2)]
        [MaxLength(20)]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name should not be empty")]
        [MinLength(2)]
        [MaxLength(20)]
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Birthday")]
        public DateTime Age { get; set; }
        
        public string Sex { get; set; }

        [MinLength(2)]
        [MaxLength(20)]
        public string City { get; set; }

        [MaxLength(200)]
        [DataType(DataType.MultilineText)]
        [Display(Name = "About: ")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Email should not be empty")]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Wrong email format. Try new one")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email adress:")]
        public string Email { get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        [MinLength(5)]
        [MaxLength(200)]
        [Display(Name = "Password: ")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        [Display(Name = "Confirm password")]
        public string ConfirmPassword { get; set; }

        public string ImageBase64 { get; set; }

        public string ImageContentType { get; set; }

       
    }
}