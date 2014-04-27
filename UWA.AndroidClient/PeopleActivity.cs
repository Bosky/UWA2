using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Widget;
using UWA.AndroidClient.Adapters;
using UWA.Core.BusinessLayer.Contracts;

namespace UWA.AndroidClient
{
    [Activity(Label = "People")]
    public class PeopleActivity : Activity
    {
        private ListView _peopleList;
        private IList<Person> _people;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.People);

            _peopleList = FindViewById<ListView>(Resource.Id.PeopleListView);

            //_people = UwaApplication.Repository.GetPeople();

            //_people = new List<People> { new People { ID = 1, LastName = "Voihan nena" } };

            _peopleList.Adapter = new PeopleListAdapter(this, _people);
        }
    }
}