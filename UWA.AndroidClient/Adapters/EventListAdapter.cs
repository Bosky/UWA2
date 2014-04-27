using System;
using System.Collections.Generic;
using Android.App;
using Android.Provider;
using Android.Views;
using Android.Widget;
using UWA.Core.BusinessLayer.Contracts;

namespace UWA.AndroidClient.Adapters
{
    class PeopleListAdapter : BaseAdapter<Person>
    {
        private readonly Activity _context;
        private readonly IList<Person> _people;

        public PeopleListAdapter(Activity context, IList<Person> people)
        {
            _context = context;
            _people = people;
        }

        public override long GetItemId(int position)
        {
            return _people[position].ID;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView
                ?? _context.LayoutInflater.Inflate(Resource.Layout.PeopleListItem, null);

            view.FindViewById<TextView>(Resource.Id.LastName).Text = _people[position].ID.ToString();

            return view;
        }

        public override Person this[int position]
        {
            get { return _people[position]; }
        }

        public override int Count
        {
            get { return _people.Count; }
        }
    }
}
