using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackendTest2.Models
{
    public class Photo
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(100)]
        public String Name { get; set; }

        [Required]
        [MaxLength(250)]
        public String Description { get; set; }

        public Int32 Likes { get; set; }

        public String Path { get; set; }

        public String UserId { get; set; }

        public ApplicationUser User { get; set; }

        public ICollection<PhotoTag> Tags { get; set; }

        public ICollection<Comment> Comments { get; set; }
    }
}
