using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackendTest1.Models;

namespace BackendTest1.Services
{
    public interface IDifficultyDataProviderService
    {
        IReadOnlyList<Difficulty> GetEntities();

        Difficulty FindEntity(Int32 key);
    }
}
