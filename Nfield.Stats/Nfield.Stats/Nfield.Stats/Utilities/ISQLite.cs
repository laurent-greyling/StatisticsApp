using SQLite;

namespace Nfield.Stats.Utilities
{
    interface ISQLite
    {
        SQLiteConnection GetConnection();
    }
}
