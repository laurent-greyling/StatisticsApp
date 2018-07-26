using Nfield.Stats.Services;
using SQLite;
using Xamarin.Forms;

namespace Nfield.Stats.Utilities
{
    public class AddLocalData<T>
    {
        readonly SQLiteConnection _table;

        public AddLocalData(T entity)
        {
            _table = DependencyService.Get<ICreateSqliteTable>().Create<T>();
        }

        public bool Add(T entity)
        {
            _table.Insert(entity);
            return true;
        }
    }
}
