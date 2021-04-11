using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackendTest2.Models
{
    public class Comment
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public DateTime Created { get; set; }

        [Required]
        [MaxLength(1000)]
        public String Text { get; set; }

        public String UserId { get; set; }

        public ApplicationUser User { get; set; }
    }
}
