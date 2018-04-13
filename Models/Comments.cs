using System.ComponentModel.DataAnnotations;
using System;

namespace ShareThoughts.Models
{
    public class Comments:BaseEntity
    {

        [Required]
        public int UserID { get; set; }

        [Required]
        public int MessageID { get; set; }

        [Required]
        public int CommentID { get; set; }

        // [Display(Name = "Created At")]
        // [Required]
        // [DataType(DataType.DateTime)]
        // public DateTime Created_at { get; set; }

        // [Display(Name = "Updated At")]
        // [DataType(DataType.DateTime)]
        // public DateTime Updated_at { get; set; }
    }
}