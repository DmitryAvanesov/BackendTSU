using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Backend6.Models.ViewModels
{
    public class ForumMessageViewModel
    {
        [Required]
        [MaxLength(10000)]
        public String Text { get; set; }
    }
}
