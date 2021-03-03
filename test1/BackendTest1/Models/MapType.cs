using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendTest1.Models
{
    public class MapType : IEntity
    {
        public MapType()
        {
        }

        public MapType(Int32 id, String name)
        {
            this.Id = id;
            this.Name = name;
        }

        public Int32 Id { get; set; }

        public String Name { get; set; }
    }
}
