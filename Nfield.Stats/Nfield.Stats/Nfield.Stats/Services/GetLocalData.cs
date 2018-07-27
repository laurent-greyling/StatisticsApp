using System.Collections.Generic;
using SQLite;
using Xamarin.Forms;
using System.Linq;

namespace Nfield.Stats.Services
{
    public class GetLocalData<T>: IGetLocalData<T> where T: new()
    {
        readonly SQLiteConnection _table;

        public GetLocalData()
        {
            _table = DependencyService.Get<ICreateSqliteTable>().Create<T>();
        }
        
        public IEnumerable<T> Get(T entity)
        {
            return (from tbl in _table.Table<T>() select tbl).ToList();
        }
    }
}
