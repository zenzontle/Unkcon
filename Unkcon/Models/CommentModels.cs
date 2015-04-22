using System;
using System.ComponentModel.DataAnnotations;

namespace Unkcon.Models
{
    public class CommentModel
    {
        [Required]
        [Display(Name = "Comment")]
        public int ID { get; set; }

        [Required]
        public string Comment { get; set; }
    }
}