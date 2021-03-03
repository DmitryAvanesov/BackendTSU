using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendTest1.Models
{
    public class SetUpGameViewModel : Data
    {
        public SetUpGameViewModel()
        {
        }

        public IReadOnlyList<MapType> MapType { get; set; }

        public IReadOnlyList<MapSize> MapSize { get; set; }

        public IReadOnlyList<Difficulty> Difficulty { get; set; }

        public IReadOnlyList<VictoryType> VictoryType { get; set; }

        public IReadOnlyList<Civilization> Civilization { get; set; }


        public String ChosenTab { get; set; }
    }
}
