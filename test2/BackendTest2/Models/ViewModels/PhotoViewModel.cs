using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace BackendTest2.Models.ViewModels
{
    public class PhotoViewModel
    {
        [Required]
        [MaxLength(100)]
        public String Name { get; set; }

        [Required]
        [MaxLength(250)]
        public String Description { get; set; }

        public String Tags { get; set; }

        public IFormFile File { get; set; }
    }
}
