using System.Collections.Generic;

namespace Nfield.Stats.Services
{
    public interface IDeleteLocalData<T>
    {
        void Delete(T entity);
    }
}
