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
using Android.Webkit;
using Android.Widget;
using UWA.AndroidClient.Adapters;
using UWA.Core.BusinessLayer;
using UWA.Core.BusinessLayer.Contracts;
using UWA.Core.ServiceAccessLayer;

namespace UWA.AndroidClient
{
    [Activity(Label = "My Activity")]
    public class EventActivity : Activity
    {
        private IList<RSSEvent> _eventsList;
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
                    this._eventsList = antecedent.Result;
                    Log.Info("News", "Started populating list.");
                    this.PopulateListView(this._eventsList);
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
            var adapter = new EventListAdapter(this, _eventsList);
            this._eventListView.Adapter = adapter;
            this._eventListView.ItemClick += OnListViewItemClick;
            Log.Info("News", "Finished populating list.");
        }

        protected void OnListViewItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var selectedEvent = _eventsList[e.Position];

            // Check for category of event to determine display format.
            switch (selectedEvent.Category)
            {
                case("Warning"):
                    DisplayWarning(selectedEvent);
                    break;
                case("Article"):
                    DisplayArticle(selectedEvent);
                    break;
                default:
                    Toast.MakeText(this, "Unkown event type :(", Android.Widget.ToastLength.Short).Show();
                    break;
            }

        }

        private void DisplayArticle(RSSEvent selectedEvent)
        {
            var intent = new Intent(this, typeof(EventWebViewActivity));
            intent.PutExtra("Url", selectedEvent.Link);
            StartActivity(intent);
        }

        private void DisplayWarning(RSSEvent selectedEvent)
        {
            AlertDialog.Builder builder = new AlertDialog.Builder(this);
            AlertDialog dialog = builder.Create();

            dialog.SetTitle("Notice");
            dialog.SetMessage(selectedEvent.Description);
            dialog.SetButton("Ok", (s, ev) => dialog.Dismiss());
            dialog.Show();
        }
    }
}