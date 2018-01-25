using System.IO;
using SQLite;
using StatisticsApp.Droid;
using StatisticsApp.Interfaces;
using Xamarin.Forms;

[assembly: Dependency(typeof(Fav_Survey))]
namespace StatisticsApp.Droid
{
    public class Fav_Survey : ISQLite
    {
        public SQLiteConnection GetConnection()
        {
            var fileName = "FavSurvey.db3";
            var documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var path = Path.Combine(documentsPath, fileName);

            var connection = new SQLiteConnection(path);

            return connection;
        }
    }
}