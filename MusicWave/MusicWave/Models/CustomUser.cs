using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MusicWave.Models
{
    public class CustomUser
    {
        public Guid Id { get; set; }

        [MinLength(2)]
        [MaxLength(20)]
        public string FirstName { get; set; }

        [MinLength(2)]
        [MaxLength(20)]
        public string LastName { get; set; }

        public DateTime Age { get; set; }
        
        public bool Sex { get; set; }

        [MinLength(2)]
        [MaxLength(20)]
        public string City { get; set; }

        [MinLength(10)]
        [MaxLength(200)]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Required(ErrorMessage = "Email should not be empty")]
        [RegularExpression(@"^\S+@\S+$", ErrorMessage = "Wrong email format. Try new one")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Email { get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        [MinLength(5)]
        [MaxLength(100)]
        [Display(Name = "Password")]
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