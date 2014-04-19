using System;
using System.Collections.Generic;
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
    [Activity(Label = "UWA.AndroidClient", MainLauncher = true, Icon = "@drawable/icon")]
    public class HomeActivity : Activity
    {
        private MapLocationRepository _repository;
        private IList<MapLocation> _mapLocations;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            //Class1 textService = new Class1();

            //var textView = FindViewById<TextView>(Resource.Id.HelloText);
            //textView.Text = textService.GetGreeting();

            // Get our button from the layout resource,
            // and attach an event to it
            var button = FindViewById<Button>(Resource.Id.MapsButton);

            button.Click += MapsClicked;
        }

        private void MapsClicked(object sender, EventArgs e)
        {
            //var intent = new Intent(this, typeof(MapsActivity));
            //StartActivity(intent);
            _repository = new MapLocationRepository();

            _mapLocations = _repository.GetLocations();

            Log.Info("Test1", "Locations fetched.");
        }
    }
}





