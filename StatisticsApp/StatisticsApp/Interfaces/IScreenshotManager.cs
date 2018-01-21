using System.Threading.Tasks;
using Xamarin.Forms;

namespace StatisticsApp.Interfaces
{
    public interface IScreenshotManager
    {
        Task<ImageSource> CaptureAsync();
    }
}
