
using System.Collections.Generic;

namespace UWA.Core.ServiceAccessLayer
{
    /// <summary>
    /// RSSModel Contains a set of POCO objects that
    /// function as Data Transfer Objects (DTOs).
    /// In order for the RestSharp default deserealiser to work, 
    /// these objects must equal the XML schema naming format.
    /// </summary> 
    public class EventFeed
    {
        public string version { get; set; }
        public EventChannel channel { get; set; }
    }

    public class EventChannel
    {
        public string title { get; set; }
        public string link { get; set; }
        public string description { get; set; }
        public EventItems item { get; set; }
    }

    public class EventItems : List<item> { }

    public class item
    {
        public string title { get; set; }
        public string pubDate { get; set; }
        public string link { get; set; }
        public string category { get; set; }
        public string description { get; set; }              
    }

}
