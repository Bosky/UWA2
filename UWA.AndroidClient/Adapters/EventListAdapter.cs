using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Views;
using Android.Widget;
using UWA.Core.BusinessLayer;
using UWA.Core.BusinessLayer.Contracts;
using UWA.Core.ServiceAccessLayer;

namespace UWA.AndroidClient.Adapters
{
    // ReSharper disable once InconsistentNaming
    public class EventListAdapter : BaseAdapter<RSSEvent>
    {
        protected Activity context = null;
        protected IList<RSSEvent> eventList = new List<RSSEvent>();

        public EventListAdapter(Activity context, IList<RSSEvent> feed)
            : base()
        {
            this.context = context;
            this.eventList = feed;
        }

        public override RSSEvent this[int position]
        {
            get { return this.eventList[position]; }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override int Count
        {
            get { return this.eventList.Count; }
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var eventItem = this.eventList[position];

            var view = (convertView ?? context.LayoutInflater.Inflate(Resource.Layout.EventsListItem, parent, false)) as LinearLayout;

            view.FindViewById<TextView>(Resource.Id.description).Text = eventItem.Description.Length < 51 ? eventItem.Description : eventItem.Description.Substring(0, 50) + "...";
            view.FindViewById<TextView>(Resource.Id.pubDate).Text = eventItem.Published;
            return view;
        }
    }
}