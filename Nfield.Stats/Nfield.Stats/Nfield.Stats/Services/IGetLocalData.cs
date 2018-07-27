using System.Collections.Generic;

namespace Nfield.Stats.Services
{
    public interface IGetLocalData<T>
    {
        IEnumerable<T> Get(T entity);
    }
}
