using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
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
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Map);

            // Test data connection
            Log.Info("MapsActivity", UwaApplication.DataManager.GetLocations().FirstOrDefault().Category);

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