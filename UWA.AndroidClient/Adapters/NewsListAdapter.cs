using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Views;
using Android.Widget;
using UWA.Core.ServiceAccessLayer;

namespace UWA.AndroidClient.Adapters
{
// ReSharper disable once InconsistentNaming
    public class NewsListAdapter : BaseAdapter<NewsEntry>
    {
        protected Activity context = null;
        protected List<NewsEntry> feedsList = new List<NewsEntry>();

        public NewsListAdapter(Activity context, List<NewsEntry> feedsList)
            : base()
        {
            this.context = context;
            this.feedsList = feedsList;
        }

        public override NewsEntry this[int position]
        {
            get { return this.feedsList[position]; }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override int Count
        {
            get { return this.feedsList.Count; }
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var NewsEntry = this.feedsList[position];

            var view = (convertView ?? context.LayoutInflater.Inflate(Resource.Layout.NewsListItem, parent, false)) as LinearLayout;

            view.FindViewById<TextView>(Resource.Id.title).Text = NewsEntry.Title.Length < 51 ? NewsEntry.Title : NewsEntry.Title.Substring(0, 50) + "...";
            view.FindViewById<TextView>(Resource.Id.pubDate).Text = NewsEntry.Published.ToString("dd/MM/yyyy HH:mm");
            return view;
        }
    }
}
