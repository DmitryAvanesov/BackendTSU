using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackendTest1.Models;

namespace BackendTest1.Services
{
    public class MapTypeDataProviderService : DataProviderService<MapType>, IMapTypeDataProviderService
    {
        public MapTypeDataProviderService()
        {
            this.AddWithProbability(new MapType(1, "Archipelago"), 0.75);
            this.AddWithProbability(new MapType(2, "Continents"), 0.75);
            this.AddWithProbability(new MapType(3, "Earth"), 0.75);
            this.AddWithProbability(new MapType(4, "Hemispheres"), 0.75);
            this.AddWithProbability(new MapType(5, "Islands"), 0.75);
            this.AddWithProbability(new MapType(6, "Lakes"), 0.75);
            this.AddWithProbability(new MapType(7, "Pangaea"), 0.75);
        }
    }
}
