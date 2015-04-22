using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Unkcon.Models
{
    public class CommentModel
    {
        [Required]
        [Display(Name = "Comment")]
        public int ID { get; set; }

        [Required]
        public string Comment { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}