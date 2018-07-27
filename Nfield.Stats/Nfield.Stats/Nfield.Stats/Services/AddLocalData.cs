using System.Collections.Generic;
using SQLite;
using Xamarin.Forms;

namespace Nfield.Stats.Services
{
    public class AddLocalData<T>: IAddLocalData<T>
    {
        readonly SQLiteConnection _table;

        public AddLocalData()
        {
            _table = DependencyService.Get<ICreateSqliteTable>().Create<T>();
        }

        public void Add(T entity)
        {            
            _table.Insert(entity);
        }

        public void AddRange(List<T> entities)
        {
            _table.InsertAll(entities);
        }
    }
}
