using System.IO;
using Nfield.Stats.Droid.Services;
using Nfield.Stats.Utilities;
using SQLite;
using Xamarin.Forms;

[assembly: Dependency(typeof(Sqlite_Droid))]
namespace Nfield.Stats.Droid.Services
{
    public class Sqlite_Droid : ISQLite
    {
        public SQLiteConnection GetConnection()
        {
            var fileName = "NfieldStats.db3";
            var documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var path = Path.Combine(documentsPath, fileName);

            var connection = new SQLiteConnection(path);

            return connection;
        }
    }
}