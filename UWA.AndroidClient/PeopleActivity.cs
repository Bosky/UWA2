using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Util;
using Android.Widget;
using UWA.AndroidClient.Adapters;
using UWA.Core.BusinessLayer.Contracts;

namespace UWA.AndroidClient
{
    [Activity(Label = "People")]
    public class PeopleActivity : Activity
    {      
        private IList<Person> _peopleList;
        private ListView _peopleListView;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.People);

            this._peopleListView = FindViewById<ListView>(Resource.Id.peopleListView);

            Log.Info("PeopleActivity", "Requesting people from Repository...");
            _peopleList = UwaApplication.DataManager.GetPeople();

            
            PopulateListView();
        }

        private void PopulateListView()
        {
            Log.Info("PeopleActivity", "Starting to populate listview");

            var adapter = new PeopleListAdapter(this, _peopleList);
            _peopleListView.Adapter = adapter;
        }
    }
}