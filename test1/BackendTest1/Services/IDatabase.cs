using BackendTest1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendTest1.Services
{
    public interface IDatabase
    {
        IReadOnlyList<Data> GetData();

        void PushData(Data data);
    }
}
