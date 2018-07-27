using System.Collections.Generic;

namespace Nfield.Stats.Services
{
    public interface IAddLocalData<T>
    {
        void Add(T entity);
        void AddRange(List<T> entities);
    }
}
