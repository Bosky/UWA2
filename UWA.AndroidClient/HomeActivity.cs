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
        private OrmMapLocationRepository _repository;
        private IList<MapLocation> _mapLocations;

        private TextView _mapTextView;
        private TextView _locationTextView;

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
            var mButton = FindViewById<Button>(Resource.Id.MapsButton);
            _locationTextView = FindViewById<TextView>(Resource.Id.FetchedLocations);

            var pButton = FindViewById<Button>(Resource.Id.PeopleButton);

            mButton.Click += MapsClicked;
            pButton.Click += PeopleClicked;
        }

        private void PeopleClicked(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(PeopleActivity));
            StartActivity(intent);
        }

        private void MapsClicked(object sender, EventArgs e)
        {
            //var intent = new Intent(this, typeof(MapsActivity));
            //StartActivity(intent);
                  
            _mapLocations = UwaApplication.Repository.GetLocations();

            Log.Info("Test1", "Locations fetched.");
            try
            {
                _locationTextView.Text = _mapLocations.Count.ToString();
            }
            catch (Exception)
            {
                Log.Info("Test1", "No Locations in _mapLocations");
            }
      
            
        }
    }
}





