using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackendTest1.Models;

namespace BackendTest1.Services
{
    public interface IMapSizeDataProviderService
    {
        IReadOnlyList<MapSize> GetEntities();

        MapSize FindEntity(Int32 key);
    }
}
