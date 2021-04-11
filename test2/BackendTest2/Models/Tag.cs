using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackendTest2.Models
{
    public class Tag
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(100)]
        public String Text { get; set; }

        public ICollection<PhotoTag> Photos { get; set; }
    }
}
