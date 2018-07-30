using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;

namespace Nfield.Stats.Droid
{
    [Activity(Label = "Nfield Statistics", Icon = "@drawable/barchart", Theme = "@style/splashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, LaunchMode = LaunchMode.SingleTop)]
    public class Splash : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.Window.RequestFeature(WindowFeatures.ActionBar);
            base.SetTheme(Resource.Style.splashTheme);

            base.OnCreate(savedInstanceState);

            StartActivity(typeof(MainActivity));
        }
    }
}