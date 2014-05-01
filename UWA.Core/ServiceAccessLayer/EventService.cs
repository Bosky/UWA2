
using System.Collections.Generic;
using System.Diagnostics;
using RestSharp;
using UWA.Core.BusinessLayer;

namespace UWA.Core.ServiceAccessLayer
{
    public static class EventService
    {
        public const string FeedUrl = "http://olli.worcestercomputing.com/rss/eventfeed.rss";

        private static RestClient _client;

        //static EventService()
        //{
        //    _client = new RestClient
        //    {
        //        BaseUrl = FeedUrl
        //    };
        //}

        public static List<RSSEvent> GetFeed()
        {
            _client = new RestClient {BaseUrl = FeedUrl};

            // var request = new RestRequest {RequestFormat = DataFormat.Xml };

            //var response = _client.Execute<EventItems>(request);

            //Debug.Write(response.Data.Count.ToString());
            //return response.Data;

            var request = new RestRequest {RequestFormat = DataFormat.Xml};
            _client.ExecuteAsync(request, response => Debug.WriteLine(response.Content));

            var response2 = _client.Execute<EventFeed>(request);
            Debug.Write(response2.Data.channel.item.Count.ToString());

            var eventList = new List<RSSEvent>();
            foreach (var item in response2.Data.channel.item)
                eventList.Add(new RSSEvent(item));

            return eventList;
        }
    }
}
