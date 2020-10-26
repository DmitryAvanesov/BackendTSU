using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend1.Services
{
    public class RandomIntService : IRandomIntService
    {
        private readonly Random _random = new Random();
        private readonly Int32 _min = 0;
        private readonly Int32 _max = 10000;

        public Int32 RandInt()
        {
            return this._random.Next(this._min, this._max);
        }
    }
}
