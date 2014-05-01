using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Util;
using Android.Widget;
using UWA.AndroidClient.Adapters;
using UWA.Core.BusinessLayer;

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

            _peopleListView.ItemClick += (sender, args) =>
            {
                var selectedPerson = _peopleList[args.Position];

                var intent = new Intent(this, typeof (ViewPersonActivity));
                intent.PutExtra("Name", selectedPerson.ToString());
                intent.PutExtra("Title", selectedPerson.Title);
                intent.PutExtra("Office", selectedPerson.Office);
                intent.PutExtra("Phone", selectedPerson.Phone);
                intent.PutExtra("Email", selectedPerson.Email);

                StartActivity(intent);
            };
        }

        private void PopulateListView()
        {
            Log.Info("PeopleActivity", "Starting to populate listview");

            var adapter = new PeopleListAdapter(this, _peopleList);
            _peopleListView.Adapter = adapter;
        }
    }
}