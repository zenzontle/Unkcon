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

        [Required]
        public DateTime PostedDate { get; set; }

        [Required]
        public virtual int UserID { get; set; }

        public virtual int ParentCommentID { get; set; }
    }
}