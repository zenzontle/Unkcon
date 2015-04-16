using System.ComponentModel.DataAnnotations;

namespace Unkcon.Models
{
    public class CommentModel
    {
        [Required]
        [Display(Name = "Comment")]
        public string Comment { get; set; }

    }
}