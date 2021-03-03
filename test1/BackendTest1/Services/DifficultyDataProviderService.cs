using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackendTest1.Models;

namespace BackendTest1.Services
{
    public class DifficultyDataProviderService : DataProviderService<Difficulty>, IDifficultyDataProviderService
    {
        public DifficultyDataProviderService()
        {
            this.AddWithProbability(new Difficulty(1, "Settler", "Easiest"), 0.75);
            this.AddWithProbability(new Difficulty(2, "Chieftain", "Very easy"), 0.75);
            this.AddWithProbability(new Difficulty(3, "Warlord", "Easy"), 0.75);
            this.AddWithProbability(new Difficulty(4, "Prince", "Normal"), 0.75);
            this.AddWithProbability(new Difficulty(5, "King", "Hard"), 0.75);
            this.AddWithProbability(new Difficulty(6, "Emperor", "Very hard"), 0.75);
            this.AddWithProbability(new Difficulty(7, "Immortal", "Extremely hard"), 0.75);
            this.AddWithProbability(new Difficulty(8, "Deity", "Hardest"), 0.75);
        }
    }
}
