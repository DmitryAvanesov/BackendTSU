using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Backend6.Models
{
    public class ForumMessage
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public DateTime Created { get; set; }

        [Required]
        public DateTime Modified { get; set; }

        [Required]
        [MaxLength(10000)]
        public String Text { get; set; }

        public Guid TopicId { get; set; }

        public ForumTopic Topic { get; set; }

        public String UserId { get; set; }

        public ApplicationUser User { get; set; }

        public ICollection<ForumMessageAttachment> Attachments { get; set; }
    }
}
