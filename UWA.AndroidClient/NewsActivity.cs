using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using UWA.AndroidClient.Adapters;
using UWA.Core.ServiceAccessLayer;

namespace UWA.AndroidClient
{
    [Activity(Label = "My Activity")]
    public class NewsActivity : Activity
    {
        const string sourceURI = "http://news.google.com/news?q=mobile%20world%20congress&output=rss";

        private List<NewsEntry> _newsList;
        private ListView _newsItemsListView;
        private ProgressDialog _progressDialog;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            this._newsItemsListView = this.FindViewById<ListView>(Resource.Id.newsItemsListView);

            this._progressDialog = new ProgressDialog(this);
            this._progressDialog.SetMessage("Fetching news...");

            this.GetFeedItemsList();
        }

        private void GetFeedItemsList()
        {
            this._progressDialog.Show();

            Task<List<NewsEntry>> task1 = Task.Factory.StartNew(() =>
            {
                return NewsService.GetFeedItems(sourceURI);
            }
            );

            Task task2 = task1.ContinueWith((antecedent) =>
            {
                try
                {
                    this._progressDialog.Dismiss();
                    this._newsList = antecedent.Result;
                    this.PopulateListView(this._newsList);
                }
                catch (AggregateException aex)
                {
                    Toast.MakeText(this, aex.InnerException.Message, ToastLength.Short).Show();
                }
            }, TaskScheduler.FromCurrentSynchronizationContext()
            );
        }

        private void PopulateListView(List<NewsEntry> feedItemsList)
        {
            var adapter = new NewsListAdapter(this, _newsList);
            this._newsItemsListView.Adapter = adapter;
            this._newsItemsListView.ItemClick += OnListViewItemClick;
        }

        protected void OnListViewItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            // Qui la logica per iniziare un'altra schermata con i detagli....
            var t = _newsList[e.Position];
            Android.Widget.Toast.MakeText(this, t.Url, Android.Widget.ToastLength.Short).Show();
        }
    }
}