using SQLite;

namespace StatisticsApp.Interfaces
{
    public interface ISQLite
    {
        SQLiteConnection GetConnection();
    }
}
