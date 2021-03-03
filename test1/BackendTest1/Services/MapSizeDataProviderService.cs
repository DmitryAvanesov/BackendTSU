using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackendTest1.Models;

namespace BackendTest1.Services
{
    public class MapSizeDataProviderService : DataProviderService<MapSize>, IMapSizeDataProviderService
    {
        public MapSizeDataProviderService()
        {
            this.AddWithProbability(new MapSize(1, "Duel", 2), 0.75);
            this.AddWithProbability(new MapSize(2, "Tiny", 4), 0.75);
            this.AddWithProbability(new MapSize(3, "Small", 6), 0.75);
            this.AddWithProbability(new MapSize(4, "Standard", 8), 0.75);
            this.AddWithProbability(new MapSize(5, "Large", 10), 0.75);
            this.AddWithProbability(new MapSize(6, "Huge", 12), 0.75);
        }
    }
}
