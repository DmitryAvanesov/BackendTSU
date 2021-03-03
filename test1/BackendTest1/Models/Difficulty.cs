using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendTest1.Models
{
    public class Difficulty : IEntity
    {
        public Difficulty()
        {
        }

        public Difficulty(Int32 id, String name, String type)
        {
            this.Id = id;
            this.Name = name;
            this.Type = type;
        }

        public Int32 Id { get; set; }

        public String Name { get; set; }

        public String Type { get; set; }
    }
}
