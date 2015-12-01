using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MusicWave.Models
{
    public class LogInUser
    {
        [Required(ErrorMessage = "Email should not be empty")]
        [RegularExpression(@"^\S+@\S+$", ErrorMessage = "Wrong email format. Try new one")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email adress:")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password should not be empty")]
        [DataType(DataType.Password)]
        [MinLength(5)]
        [MaxLength(100)]
        [Display(Name = "Password: ")]
        public string Password { get; set; }
    }
}