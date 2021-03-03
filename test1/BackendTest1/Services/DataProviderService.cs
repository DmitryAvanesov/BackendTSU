using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackendTest1.Models;

namespace BackendTest1.Services
{
    public class DataProviderService<T>
        where T : IEntity
    {
        private readonly List<T> entities = new List<T>();
        private readonly Random random;

        public DataProviderService()
        {
            this.random = new Random();
        }

        public IReadOnlyList<T> GetEntities()
        {
            return this.entities;
        }

        public T FindEntity(Int32 key)
        {
            return this.entities.SingleOrDefault(x => x.Id == key);
        }

        protected void AddWithProbability(T entity, Double probability)
        {
            if (this.random.NextDouble() < probability)
            {
                this.entities.Add(entity);
            }
        }
    }
}
