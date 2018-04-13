using System.ComponentModel.DataAnnotations;
using System;

namespace ShareThoughts.Models
{
    public class Messages:BaseEntity
    {

        [Required]
        public int UserID { get; set; }


        [Display(Name = "Message")]
        [Required]
        public string Message { get; set; }

        // [Display(Name = "Created At")]
        // [Required]
        // [DataType(DataType.DateTime)]
        // public DateTime Created_at { get; set; }

        // [Display(Name = "Updated At")]
        // [DataType(DataType.DateTime)]
        // public DateTime Updated_at { get; set; }
    }
}