using Nfield.Stats.Utilities;
using SQLite;
using Xamarin.Forms;

namespace Nfield.Stats.Services
{
    public class CreateSqliteTable : ICreateSqliteTable
    {
        private SQLiteConnection _connection;

        public CreateSqliteTable()
        {
            _connection = DependencyService.Get<ISQLite>().GetConnection();
        }

        public SQLiteConnection Create<T>()
        {            
            _connection.CreateTable<T>();            
            return _connection;
        }
    }
}
