using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Backend6.Models
{
    public class ForumTopic
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public DateTime Created { get; set; }

        [Required]
        [MaxLength(100)]
        public String Name { get; set; }

        public Guid ForumId { get; set; }

        public Forum Forum { get; set; }

        public String UserId { get; set; }

        public ApplicationUser User { get; set; }

        public ICollection<ForumMessage> Messages { get; set; }
    }
}
