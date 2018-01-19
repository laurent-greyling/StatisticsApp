using System.Threading.Tasks;

namespace StatisticsApp.Interfaces
{
    public interface IScreenshotManager
    {
        byte[] CaptureAsync();
    }
}
