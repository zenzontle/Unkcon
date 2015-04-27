using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Unkcon.Models
{
    public class ReplyModels
    {
        [Required]
        [Key]
        public int ID { get; set; }

        public string Reply { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual CommentModel Comment { get; set; }
    }
}