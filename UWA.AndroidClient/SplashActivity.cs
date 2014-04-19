using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace UWA.AndroidClient
{
    [Activity(Label = "Welcome", MainLauncher = true, Theme = "@style/Theme.SplashGreeting", 
              NoHistory = true, Icon = "@drawable/Icon")]
    public class SplashActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            System.Threading.Thread.Sleep(1000);
            // Start our real activity
            StartActivity(typeof(HomeActivity));
        }
    }
}