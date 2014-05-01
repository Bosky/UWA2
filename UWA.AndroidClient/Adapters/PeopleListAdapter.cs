using System;
using System.Collections.Generic;
using System.Net.Mime;
using Android.App;
using Android.Provider;
using Android.Util;
using Android.Views;
using Android.Widget;
using UWA.Core.BusinessLayer;

namespace UWA.AndroidClient.Adapters
{
    class PeopleListAdapter : BaseAdapter<Person>
    {
        protected Activity context;
        protected IList<Person> personList = new List<Person>(); 

        public PeopleListAdapter(Activity context, IList<Person> people)
            : base()
        {
            this.context = context;
            this.personList = people;
        }

        public override long GetItemId(int position)
        {
            return personList[position].ID;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            

            var person = this.personList[position];

            Log.Info("PeopleListAdapter", person.FirstName.ToString());

            var view = (convertView ?? context.LayoutInflater.Inflate(Resource.Layout.PeopleListItem, parent, false)) as LinearLayout;

            view.FindViewById<TextView>(Resource.Id.peoplelist_name).Text = person.ToString();

            view.FindViewById<TextView>(Resource.Id.peoplelist_title).Text = person.Title;
            return view;
        }

        public override Person this[int position]
        {
            get { return this.personList[position]; }
        }

        public override int Count
        {
            get { return this.personList.Count; }
        }
    }
}
