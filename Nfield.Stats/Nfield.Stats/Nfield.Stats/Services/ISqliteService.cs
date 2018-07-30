using System.Collections.Generic;

namespace Nfield.Stats.Services
{
    public interface ISqliteService<T>
    {
        void Add(T entity);
        void AddRange(List<T> entities);
        void Delete(T entity);
        void DeleteAll();
        IEnumerable<T> Get();
        void Update(T entity);
        void UpdateRange(List<T> entities);
    }
}
