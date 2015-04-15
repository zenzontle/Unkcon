using System.ComponentModel.DataAnnotations;

namespace Unkcon.Models
{
    public class CommentModels
    {
        [Required]
        [Display(Name = "Comment")]
        public string Comment { get; set; }

    }
}