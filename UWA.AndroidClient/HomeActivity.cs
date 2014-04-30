using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Android.OS;
using UWA.Core.BusinessLayer.Contracts;
using UWA.Core.DataAccessLayer;
using UWA.Core.MonoAndroid;

namespace UWA.AndroidClient
{
    [Activity(Label = "Worcester Mobile", MainLauncher = true, Icon = "@drawable/Icon")]
    public class HomeActivity : Activity
    {        
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Home);

            var mButton = FindViewById<Button>(Resource.Id.mapButton);
            var pButton = FindViewById<Button>(Resource.Id.peopleButton);
            var eButton = FindViewById<Button>(Resource.Id.eventsButton);

            mButton.Click += MapsClicked;
            pButton.Click += PeopleClicked;
            eButton.Click += NewsClicked;
        }

        private void PeopleClicked(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(PeopleActivity));
            StartActivity(intent);
        }

        private void MapsClicked(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(MapActivity));
            StartActivity(intent);            
        }

        private void NewsClicked(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(EventActivity));
            StartActivity(intent);
        }
    }
}





