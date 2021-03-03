using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackendTest1.Models;

namespace BackendTest1.Services
{
    public interface IVictoryTypeDataProviderService
    {
        IReadOnlyList<VictoryType> GetEntities();

        VictoryType FindEntity(Int32 key);
    }
}
