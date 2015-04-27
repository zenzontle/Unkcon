using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Unkcon.Models
{
    public class CommentModel
    {
        public CommentModel()
        {
            Replies = new HashSet<CommentModel>();
            Parent = new HashSet<CommentModel>();
        }

        [Required]
        [Key]
        [Display(Name = "Comment")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        public string Comment { get; set; }

        public virtual ICollection<CommentModel> Replies { get; set; }

        public virtual ICollection<CommentModel> Parent { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}