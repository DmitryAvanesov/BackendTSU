using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackendTest1.Models;

namespace BackendTest1.Services
{
    public class CivilizationDataProviderService : DataProviderService<Civilization>, ICivilizationDataProviderService
    {
        public CivilizationDataProviderService()
        {
            this.AddWithProbability(new Civilization(1, "American", "Washington"), 0.75);
            this.AddWithProbability(new Civilization(2, "Arabian", "Harun al-Rashid"), 0.75);
            this.AddWithProbability(new Civilization(3, "Aztec", "Montezuma"), 0.75);
            this.AddWithProbability(new Civilization(4, "Chinese", "Wu Zetian"), 0.75);
            this.AddWithProbability(new Civilization(5, "Egyptian", "Ramesses II"), 0.75);
            this.AddWithProbability(new Civilization(6, "English", "Elizabeth"), 0.75);
            this.AddWithProbability(new Civilization(7, "French", "Napoleon"), 0.75);
            this.AddWithProbability(new Civilization(8, "German", "Bismarck"), 0.75);
            this.AddWithProbability(new Civilization(9, "Greek", "Alexander"), 0.75);
            this.AddWithProbability(new Civilization(10, "Indian", "Gandhi"), 0.75);
            this.AddWithProbability(new Civilization(11, "Iroquois", "Hiawatha"), 0.75);
            this.AddWithProbability(new Civilization(12, "Japanese", "Oda Nobunaga"), 0.75);
            this.AddWithProbability(new Civilization(13, "Ottoman", "Suleiman"), 0.75);
            this.AddWithProbability(new Civilization(14, "Persian", "Darius I"), 0.75);
            this.AddWithProbability(new Civilization(15, "Roman", "Augustus Caesar"), 0.75);
            this.AddWithProbability(new Civilization(16, "Russian", "Catherine"), 0.75);
            this.AddWithProbability(new Civilization(17, "Siamese", "Ramkhamhaeng"), 0.75);
            this.AddWithProbability(new Civilization(18, "Songhai", "Askia"), 0.75);
        }
    }
}
