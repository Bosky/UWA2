using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using UWA.AndroidClient.Adapters;
using UWA.Core.BusinessLayer.Contracts;
using UWA.Core.ServiceAccessLayer;

namespace UWA.AndroidClient
{
    [Activity(Label = "My Activity")]
    public class EventActivity : Activity
    {
        const string sourceURI = "http://news.google.com/news?q=mobile%20world%20congress&output=rss";

        private IList<RSSEvent> _newsList;
        private ListView _eventListView;
        private ProgressDialog _progressDialog;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Events);

            this._eventListView = this.FindViewById<ListView>(Resource.Id.eventListView);

            this._progressDialog = new ProgressDialog(this);
            this._progressDialog.SetMessage("Fetching events...");

            this.GetEventFeed();
        }

        //private void GetEventsFeed()
        //{        
            //Log.Info("News", "Startet method GetEventsFeed.");
            //_newsList = EventService.GetFeed();
            //Log.Info("News", "First feeditem: " + _newsList.FirstOrDefault().Description);
        //}

        private void GetEventFeed()
        {
            this._progressDialog.Show();

            Task<List<RSSEvent>> task1 = Task.Factory.StartNew(() =>
            {
                Log.Info("News", "Started Task GetFeedItems.");
                return EventService.GetFeed();
            }
            );

            Task task2 = task1.ContinueWith((antecedent) =>
            {
                try
                {
                    this._progressDialog.Dismiss();
                    this._newsList = antecedent.Result;
                    Log.Info("News", "Started populating list.");
                    this.PopulateListView(this._newsList);
                }
                catch (AggregateException aex)
                {
                    Toast.MakeText(this, aex.InnerException.Message, ToastLength.Short).Show();
                }
            }, TaskScheduler.FromCurrentSynchronizationContext()
            );
        }

        private void PopulateListView(IList<RSSEvent> eventList)
        {
            var adapter = new EventListAdapter(this, _newsList);
            this._eventListView.Adapter = adapter;
            this._eventListView.ItemClick += OnListViewItemClick;
            Log.Info("News", "Finished populating list.");
        }

        protected void OnListViewItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var t = _newsList[e.Position];
            Android.Widget.Toast.MakeText(this, t.Link, Android.Widget.ToastLength.Short).Show();
        }
    }
}