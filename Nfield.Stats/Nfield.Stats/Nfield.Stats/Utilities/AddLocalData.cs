using Nfield.Stats.Entities;
using SQLite;
using Xamarin.Forms;

namespace Nfield.Stats.Utilities
{
    public class AddLocalData
    {
        private SQLiteConnection _connection;

        public AddLocalData()
        {
            _connection = DependencyService.Get<ISQLite>().GetConnection();
            _connection.CreateTable<Fake>();
        }
    }
}
