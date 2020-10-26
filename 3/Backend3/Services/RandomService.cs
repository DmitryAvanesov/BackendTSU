using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend1.Services
{
    public class RandomService : IRandomService
    {
        private readonly Random _random = new Random();
        private readonly Int32 _min = 0;
        private readonly Int32 _max = 1000;
        private readonly IList<String> _operators = new List<String> { "+", "-", "*", "/" };

        public Int32 RandInt()
        {
            return this._random.Next(this._min, this._max);
        }

        public String RandOperator()
        {
            return this._operators[this._random.Next(this._operators.Count)];
        }
    }
}