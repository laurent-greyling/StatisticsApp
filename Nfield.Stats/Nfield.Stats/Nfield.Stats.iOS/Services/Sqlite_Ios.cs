using Nfield.Stats.iOS.Services;
using Nfield.Stats.Services;
using SQLite;
using System.IO;
using Xamarin.Forms;

[assembly: Dependency(typeof(Sqlite_Ios))]
namespace Nfield.Stats.iOS.Services
{
    class Sqlite_Ios : ISQLite
    {
        public SQLiteConnection GetConnection()
        {
            var fileName = "NfieldStats.db3";
            var documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            string libraryFolder = Path.Combine(documentsPath, "..", "Library");
            var path = Path.Combine(libraryFolder, fileName);
            return new SQLiteConnection(path);
        }
    }
}