using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Backend6.Models.ViewModels
{
    public class ForumMessageAttachmentViewModel
    {
        [Required]
        [MaxLength(100)]
        public String FileName { get; set; }

        public IFormFile File { get; set; }
    }
}
