using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Unkcon.Models
{
    public class PotentialMatchViewModel
    {
        [Required]
        public string Comment { get; set; }

        [Required]
        public string Reply { get; set; }
    }
}