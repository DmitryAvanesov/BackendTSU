using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Backend5.Models
{
    public class Diagnosis
    {
        public Int32 Id { get; set; }

        public Int32 PatientId { get; set; }

        public Patient Patient { get; set; }

        [Required]
        [MaxLength(200)]
        public String Type { get; set; }

        [MaxLength(300)]
        public String Complications { get; set; }

        [MaxLength(400)]
        public String Details { get; set; }

    }
}
