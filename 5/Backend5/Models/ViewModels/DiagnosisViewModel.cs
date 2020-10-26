using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Backend5.Models.ViewModels
{
    public class DiagnosisViewModel
    {
        [Required]
        [MaxLength(200)]
        public String Type { get; set; }

        [MaxLength(300)]
        public String Complications { get; set; }

        [MaxLength(400)]
        public String Details { get; set; }
    }
}
