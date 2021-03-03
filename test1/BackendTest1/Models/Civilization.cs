using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendTest1.Models
{
    public class Civilization : IEntity
    {
        public Civilization()
        {
        }

        public Civilization(Int32 id, String name, String leader)
        {
            this.Id = id;
            this.Name = name;
            this.Leader = leader;
        }

        public Int32 Id { get; set; }

        public String Name { get; set; }

        public String Leader { get; set; }
    }
}
