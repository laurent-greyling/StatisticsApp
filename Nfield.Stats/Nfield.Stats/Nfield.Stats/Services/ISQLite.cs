using SQLite;

namespace Nfield.Stats.Services
{
    interface ISQLite
    {
        SQLiteConnection GetConnection();
    }
}
