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
using UWA.Core.BusinessLayer.Contracts;
using UWA.Core.DataAccessLayer;

namespace UWA.AndroidClient
{
    [Activity(Label = "Map")]
    public class MapActivity : Activity
    {
        private MapLocationRepository _repository;
        private IList<MapLocation> _mapLocations;

        protected override void OnCreate(Bundle bundle)
        {
            Log.Info("MAPS", "Start OnCreate");
            base.OnCreate(bundle);

            _repository = new MapLocationRepository();

            _mapLocations = _repository.GetLocations();

            Log.Info("Test1", "Locations fetched.");

            SetContentView(Resource.Layout.Map);
            // Create your application here
            Log.Info("MAPS", "End OnCreate");
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
                string toast = string.Format("Searching.. searching... not.");
                Toast.MakeText(this, toast, ToastLength.Long).Show();
            }

            if (item.ItemId == Resource.Id.action_places)
            {
                return base.OnOptionsItemSelected(item);
                //var intent = new Intent(this, typeof(PlaceCategoriesActivity));
                //StartActivity(intent);
            }

            return base.OnOptionsItemSelected(item);
        }

        //protected override bool IsRouteDisplayed
        //{
        //    get { return false; }
        //}
    }
}