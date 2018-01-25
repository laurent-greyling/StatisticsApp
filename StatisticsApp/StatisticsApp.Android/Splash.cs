using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using StatisticsApp.Services;

namespace StatisticsApp.Droid
{
    [Activity(Label = "Nfield Statistics", Icon = "@drawable/barchart", Theme = "@style/splashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, LaunchMode = LaunchMode.SingleTop)]
    public class Splash :Activity
    {

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.Window.RequestFeature(WindowFeatures.ActionBar);
            base.SetTheme(Resource.Style.splashTheme);

            base.OnCreate(savedInstanceState);

            // Create your application here


            StartActivity(typeof(MainActivity));
        }
    }
}