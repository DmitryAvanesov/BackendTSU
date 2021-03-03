using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendTest1.Models
{
    public class Data
    {
        public String ChosenName { get; set; }

        public Int32? ChosenCivilization { get; set; }

        public IList<Int32?> ChosenOpponents { get; set; }

        public Int32 ChosenMapType { get; set; }

        public Int32 ChosenMapSize { get; set; }

        public Int32 ChosenDifficulty { get; set; }

        public IList<Boolean> ChosenVictoryType { get; set; }

        public IList<Boolean> ChosenAdvancedGameOptions { get; set; }

        public IList<String> AdvancedGameOptions { get; set; }
    }
}
