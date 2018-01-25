
using Android.App;
using Android.Content.PM;
using Android.OS;
using Lottie.Forms.Droid;

namespace StatisticsApp.Droid
{
    [Activity(Label = "Nfield Statistics", Icon = "@drawable/barchart", Theme = "@style/MainTheme", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);
            //ScreenshotManager.Activity = this;
            global::Xamarin.Forms.Forms.Init(this, bundle);

            AnimationViewRenderer.Init();
            
            LoadApplication(new App());
        }
    }
}

