using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackendTest1.Models;

namespace BackendTest1.Services
{
    public class VictoryTypeDataProviderService : DataProviderService<VictoryType>, IVictoryTypeDataProviderService
    {
        public VictoryTypeDataProviderService()
        {
            this.AddWithProbability(new VictoryType(1, "Time"), 0.75);
            this.AddWithProbability(new VictoryType(2, "Science"), 0.75);
            this.AddWithProbability(new VictoryType(3, "Domination"), 0.75);
            this.AddWithProbability(new VictoryType(4, "Cultural"), 0.75);
            this.AddWithProbability(new VictoryType(5, "Diplomatic"), 0.75);
        }
    }
}
