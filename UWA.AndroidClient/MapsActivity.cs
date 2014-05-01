using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Gms.Maps;
using Android.GoogleMaps;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace UWA.AndroidClient
{
    [MetaData("android.app.default_searchable", Value = "uwa.androidclient.MapSearchActivity")]
    [IntentFilter(new string[] { "android.intent.action.SEARCH" })]
    [Activity(Label = "Campus Map")]
    public class MapsActivity : Android.GoogleMaps.MapActivity
    {
        private MapFragment _mapFragment;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Maps);

            // Test data connection
            Log.Info("MapsActivity", UwaApplication.DataManager.GetLocations().FirstOrDefault().Category);
            //var map = new MapView(this, "AIzaSyD7A1rcCWODSLEX64tBh4TiQYg1-pSSk1w");

            _mapFragment = new MapFragment();
            FragmentTransaction fragmentTx = this.FragmentManager.BeginTransaction();
            fragmentTx.Add(Resource.Id.fragmentContainer, _mapFragment);
            fragmentTx.Commit();

            SetupMapPosition();
        }

        private void SetupMapPosition()
        {

            GoogleMap map = _mapFragment.Map;

            _mapFragment.Controller.SetZoom(16);
            _mapFragment.Controller.SetCenter(
                   new GeoPoint((int)40.8270449E6, (int)-73.9279148E6));
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.MapMenu, menu);

            Log.Info("HA", "Menu items built.");

            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.ItemId == Resource.Id.action_search)
            {
                OnSearchRequested();
            }

            if (item.ItemId == Resource.Id.action_places)
            {
                string toast = string.Format("Opening location list navigation...maybe..");
                Toast.MakeText(this, toast, ToastLength.Long).Show();
            }

            return true;
        }

        protected override bool IsRouteDisplayed
        {
            get
            {
                return false;
            }
        }
    }
}