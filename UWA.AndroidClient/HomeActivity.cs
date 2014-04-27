using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.ComponentModel;
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
            var button = FindViewById<Button>(Resource.Id.MapsButton);
            _locationTextView = FindViewById<TextView>(Resource.Id.FetchedLocations);

            var mapsbutton = FindViewById<Button>(Resource.Id.MapsButton);
            var newsbutton = FindViewById<Button>(Resource.Id.NewsButton);


            mapsbutton.Click += MapsClicked;
            newsbutton.Click += NewsClicked;
        }

        private void NewsClicked(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(EventActivity));
            StartActivity(intent);
        }

        private void MapsClicked(object sender, EventArgs e)
        {
            //var intent = new Intent(this, typeof(MapsActivity));
            //StartActivity(intent);
            

            _repository = new OrmMapLocationRepository();

            _mapLocations = _repository.GetLocations();

            Log.Info("Test1", "Locations fetched.");
            try
            {
                _locationTextView.Text = _mapLocations.FirstOrDefault().ID.ToString();
            }
            catch (Exception)
            {
                Log.Info("Test1", "No Locations in _mapLocations");
            }
      
            
        }
    }
}





