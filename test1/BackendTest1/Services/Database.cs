using BackendTest1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendTest1.Services
{
    public class Database : IDatabase
    {
        private readonly List<Data> _data = new List<Data>();

        public IReadOnlyList<Data> GetData()
        {
            return _data;
        }

        public void PushData(Data data)
        {
            _data.Add(data);
        }
    }
}
