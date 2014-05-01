using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

using Android.App;
using Android.Content;
using Android.Gms.Ads.Identifier;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.GoogleMaps;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Java.Lang;
using UWA.Core.BusinessLayer;
using UWA.Core.BusinessLayer.Contracts;

namespace UWA.AndroidClient
{
    [MetaData("android.app.default_searchable", Value = "uwa.androidclient.MapSearchActivity")]
    [IntentFilter(new string[] { "android.intent.action.SEARCH" })]
    [Activity(Label = "Campus Map")]
    public class MapsActivity : Android.GoogleMaps.MapActivity
    {
        private MapFragment _mapFragment;
        private GoogleMap _map;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Maps);

            // Test data connection
            Log.Info("MapsActivity", UwaApplication.DataManager.GetLocations().FirstOrDefault().Category);
            InitMapFragment();
            SetupMapPosition();
        }

        protected override void OnResume()
        {
            base.OnResume();
            SetupMapPosition();
        }

        private void InitMapFragment()
        {
            _mapFragment = FragmentManager.FindFragmentByTag("map") as MapFragment;

            if (_mapFragment == null)
            {
                GoogleMapOptions mapOptions = new GoogleMapOptions()
                    .InvokeMapType(GoogleMap.MapTypeNormal)
                    .InvokeZoomControlsEnabled(false)
                    .InvokeCompassEnabled(true);

                FragmentTransaction fragTx = FragmentManager.BeginTransaction();
                _mapFragment = MapFragment.NewInstance(mapOptions);
                fragTx.Add(Resource.Id.map, _mapFragment, "map");
                fragTx.Commit();
            }

        }

        private void SetupMapPosition()
        {

            if (_map == null)
            {
                _map = _mapFragment.Map;

                if (_map != null)
                {
                    
                    // try-catch statement tries to use Extra's as argument
                    // if the there is no intent, the setup will continue as default.

                    try
                    {
                        FocusOnLocation(Intent.GetStringArrayExtra("RequestedLocation"));
                    }
                    catch (NullPointerException exception)
                    {

                        Log.Info("MapsActivity", "No given intent extras.. resuming.");
                    }

                    MapLocation startLocation = UwaApplication.DataManager.GetLocations().FirstOrDefault();
                    LatLng locationInfo = new LatLng(startLocation.Longitude, startLocation.Latitude);

                    MarkerOptions marker1 = new MarkerOptions();
                    marker1.SetPosition(locationInfo);
                    marker1.SetTitle(startLocation.Name);
                    _map.AddMarker(marker1);

                    // We create an instance of CameraUpdate, and move the map to it.
                    CameraUpdate cameraUpdate = CameraUpdateFactory.NewLatLngZoom(locationInfo, 15);
                    _map.MoveCamera(cameraUpdate);
                }
            }
        }

        private void FocusOnLocation(string[] requestedLocation)
        {
            //IConvertible 
            //var Longitude = requestedLocation[1].ToDouble();
            // LatLng locationInfo = new LatLng(requestedLocation[1].to, requestedLocation[2]);
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
                string toast = string.Format("The search function is currently unvailable, my apologies.");
                Toast.MakeText(this, toast, ToastLength.Long).Show();
            }

            if (item.ItemId == Resource.Id.action_places)
            {
                Log.Info("MapsActivity", "Starting places by categories event");
                var intent = new Intent(this, typeof(MapLocationCategoriesActivity));
                StartActivity(intent); 
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