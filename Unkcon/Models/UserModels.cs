using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Unkcon.Models
{
    public class UserModel
    {
        [Required]
        public int ID { get; set; }

        [Required]
        public string UserName { get; set; }
    }
}