using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendTest1.Models
{
    public class MapSize : IEntity
    {
        public MapSize()
        {
        }

        public MapSize(Int32 id, String name, Int32 maxCivilizations)
        {
            this.Id = id;
            this.Name = name;
            this.MaxCivilizations = maxCivilizations;
        }

        public Int32 Id { get; set; }

        public String Name { get; set; }

        public Int32 MaxCivilizations { get; set; }
    }
}
