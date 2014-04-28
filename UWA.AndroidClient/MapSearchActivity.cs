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
using UWA.Core.BusinessLayer.Managers;

namespace UWA.AndroidClient
{
 

    [Activity(Label = "Campus Map", LaunchMode = Android.Content.PM.LaunchMode.SingleTop)]
    [IntentFilter(new string[] { "android.intent.action.SEARCH" })]
    [MetaData("android.app.searchable", Resource = "@xml/searchable")]
    public class MapSearchActivity : Activity
    {
        private IList<MapLocation> _results; 

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Map);

            HandleIntent(Intent);
            // Create your application here
        }

        protected override void OnNewIntent(Intent intent)
        {
            HandleIntent(intent);
        }

        void HandleIntent(Intent intent)
        {

            if (Intent.ActionSearch.Equals(intent.Action))
            {
                string query = intent.GetStringExtra(SearchManager.Query);
                ShowLocations(query);
            }
        }

        void ShowLocations(string query)
        {
            IMapLocationManager mapManager = new MapLocationManager();
            _results = mapManager.Search(query);
            PopulateResults();
        }

        private void PopulateResults()
        {
            Log.Info("MapSearchActivity", _results.FirstOrDefault().ToString());
        }
    }
}