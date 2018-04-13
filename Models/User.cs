using System.ComponentModel.DataAnnotations;
using System;

namespace ShareThoughts.Models
{
    public class User : BaseEntity
    {
        [Display(Name = "First Name")]
        [Required(ErrorMessage = "Firstname should be more than 4 characters")]
        [MinLength(2)]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Lastname should be more than 4 characters")]
        [MinLength(2)]
        public string LastName { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email address is invalid")]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Password should be more than 8 characters")]
        [MinLength(8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [CompareAttribute(nameof(Password), ErrorMessage = "Password is not matching")]
        public string ConfirmedPW { get; set; }
    }
}