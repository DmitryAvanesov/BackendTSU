﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Backend6.Models.ViewModels
{
    public class ForumViewModel
    {
        [Required]
        [MaxLength(100)]
        public String Name { get; set; }

        [Required]
        [MaxLength(200)]
        public String Description { get; set; }
    }
}
