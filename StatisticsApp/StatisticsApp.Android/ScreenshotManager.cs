using System;
using System.IO;
using Android.App;
using Android.Graphics;
using StatisticsApp.Droid;
using StatisticsApp.Interfaces;
using Xamarin.Forms;
using Plugin.Screenshot;
using System.Threading.Tasks;
using Plugin.CurrentActivity;

[assembly: Dependency(typeof(ScreenshotManager))]
namespace StatisticsApp.Droid
{
    public class ScreenshotManager : IScreenshotManager
    {
        public static Activity Activity { get; set; }

        public async Task<ImageSource> CaptureAsync()
        {
            try
            {
                if (Activity == null)
                {
                    throw new Exception("You have to set ScreenshotManager.Activity in your Android project");
                }

                CrossCurrentActivity.Current.Activity = Activity;
                
                var stream = new MemoryStream(await CrossScreenshot.Current.CaptureAsync());

                return ImageSource.FromStream(() => stream);
                //var view = Activity.Window.DecorView;
                //view.DrawingCacheEnabled = true;

                //Bitmap bitmap = view.GetDrawingCache(true);

                //byte[] bitmapData;

                //using (var stream = new MemoryStream())
                //{
                //    bitmap.Compress(Bitmap.CompressFormat.Png, 0, stream);
                //    bitmapData = stream.ToArray();
                //}

                //return bitmapData;
            }
            catch (Exception e)
            {
                var t = e;
                throw;
            }
            
        }
        
    }
}