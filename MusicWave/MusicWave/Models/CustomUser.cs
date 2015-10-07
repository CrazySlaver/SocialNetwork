using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MusicWave.Models
{
    public class CustomUser
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Type your first name")]
        [MinLength(2)]
        [MaxLength(20)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Type your last name")]
        [MinLength(2)]
        [MaxLength(20)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Enter your age")]
        //[DataType(DataType.DateTime)]
        public DateTime Age { get; set; }
        
        [Required]
        public bool Sex { get; set; }

        [Required(ErrorMessage = "Type your city")]
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
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Image should not be empty")]
        
        public string ImageBase64 { get; set; }
        [Required(ErrorMessage = "Image should not be empty")]
        public string ImageContentType { get; set; }

       
    }
}