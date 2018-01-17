using System.Threading.Tasks;

namespace StatisticsApp.Interfaces
{
    public interface IScreenshotManager
    {
        Task<byte[]> CaptureAsync();
    }
}
