using SQLite;

namespace Nfield.Stats.Services
{
    public interface ICreateSqliteTable
    {
        SQLiteConnection Create<T>();
    }
}
