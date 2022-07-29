using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterAPI.DatabaseRepository.Interface
{
    public interface IDatabaseRepository<TEntity> where TEntity : class
    {
        TEntity Get(int id);
        Task<IEnumerable<TEntity>> GetAll();

        Task<TEntity> Insert(TEntity entity);
        void Update(TEntity entity);

        void Remove(TEntity entity, bool saveNow = true);

    }
}
