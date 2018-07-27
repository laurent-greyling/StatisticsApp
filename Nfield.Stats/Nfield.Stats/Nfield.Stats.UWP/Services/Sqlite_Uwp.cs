using Nfield.Stats.Services;
using Nfield.Stats.UWP.Services;
using SQLite;
using System.IO;
using Windows.Storage;
using Xamarin.Forms;

[assembly: Dependency(typeof(Sqlite_Uwp))]
namespace Nfield.Stats.UWP.Services
{
    public class Sqlite_Uwp : ISQLite
    {
        public SQLiteConnection GetConnection()
        {
            var fileName = "NfieldStats.db3";
            var path = Path.Combine(ApplicationData.Current.LocalFolder.Path, fileName);
            return new SQLiteConnection(path);
        }
    }
}
