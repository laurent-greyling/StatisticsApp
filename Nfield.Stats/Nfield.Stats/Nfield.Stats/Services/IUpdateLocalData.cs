using System.Collections.Generic;

namespace Nfield.Stats.Services
{
    public interface IUpdateLocalData<T>
    {
        void Update(T entity);

        void UpdateRange(List<T> entities);
    }
}
