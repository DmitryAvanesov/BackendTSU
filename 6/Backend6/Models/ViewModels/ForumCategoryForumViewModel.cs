using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend6.Models.ViewModels
{
    public class ForumCategoryForumViewModel
    {
        public ICollection<ForumCategory> Categories { get; set; }
        public ICollection<Forum> Forums { get; set; }
    }
}
