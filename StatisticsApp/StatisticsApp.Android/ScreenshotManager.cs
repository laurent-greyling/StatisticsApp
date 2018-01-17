using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using StatisticsApp.Droid;
using StatisticsApp.Interfaces;
using Xamarin.Forms;

[assembly: Dependency(typeof(ScreenshotManager))]
namespace StatisticsApp.Droid
{
    public class ScreenshotManager : IScreenshotManager
    {
        public static Activity Activity { get; set; }

        public async Task<byte[]> CaptureAsync()
        {
            try
            {
                if (Activity == null)
                {
                    throw new Exception("You have to set ScreenshotManager.Activity in your Android project");
                }

                var view = Activity.Window.DecorView;
                view.DrawingCacheEnabled = true;

                Bitmap bitmap = view.GetDrawingCache(true);

                byte[] bitmapData;

                using (var stream = new MemoryStream())
                {
                    bitmap.Compress(Bitmap.CompressFormat.Png, 0, stream);
                    bitmapData = stream.ToArray();
                }

                return bitmapData;
            }
            catch (Exception e)
            {
                var t = e;
                throw;
            }
            
        }
        
    }
}