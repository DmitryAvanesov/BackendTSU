using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Backend6.Models
{
    public class Forum
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(100)]
        public String Name { get; set; }

        [Required]
        [MaxLength(200)]
        public String Description { get; set; }

        public Guid CategoryId { get; set; }

        public ForumCategory Category { get; set; }

        public ICollection<ForumTopic> Topics { get; set; }
    }
}
