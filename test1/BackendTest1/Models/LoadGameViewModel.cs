using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendTest1.Models
{
    public class LoadGameViewModel
    {
        public String Name { get; set; }

        public Civilization Civilization { get; set; }

        public IList<Civilization> Opponents { get; set; }

        public MapType MapType { get; set; }

        public MapSize MapSize { get; set; }

        public Difficulty Difficulty { get; set; }

        public IList<VictoryType> VictoryTypes { get; set; }

        public IList<String> AdvancedGameOptions { get; set; }

        public Int32 NumberOfGames { get; set; }

        public Int32? ChosenGame { get; set; }
    }
}
