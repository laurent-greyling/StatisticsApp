using SQLite;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Nfield.Stats.Services
{
    public class UpdateLocalData<T> : IUpdateLocalData<T>
    {
        readonly SQLiteConnection _table;

        public UpdateLocalData()
        {
            _table = DependencyService.Get<ICreateSqliteTable>().Create<T>();
        }

        public void Update(T entity)
        {
            _table.InsertOrReplace(entity);
        }

        public void UpdateRange(List<T> entities)
        {
            _table.UpdateAll(entities);
        }
    }
}
