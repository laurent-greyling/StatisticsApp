using SQLite;
using Xamarin.Forms;

namespace Nfield.Stats.Services
{
    public class DeleteLocalData<T> : IDeleteLocalData<T>
    {
        readonly SQLiteConnection _table;

        public DeleteLocalData()
        {
            _table = DependencyService.Get<ICreateSqliteTable>().Create<T>();
        }

        public void Delete(T entity)
        {
            _table.Delete(entity);
        }

        public void DeleteAll()
        {
            _table.DeleteAll<T>();
        }
    }
}
