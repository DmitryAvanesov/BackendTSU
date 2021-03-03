using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackendTest1.Models;

namespace BackendTest1.Services
{
    public interface IMapTypeDataProviderService
    {
        IReadOnlyList<MapType> GetEntities();

        MapType FindEntity(Int32 key);
    }
}
