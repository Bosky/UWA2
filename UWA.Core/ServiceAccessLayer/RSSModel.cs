
using System.Collections.Generic;

namespace UWA.Core.ServiceAccessLayer
{
    /// <summary>
    ///  RSSModel Contains a set of POCO objects that
    ///  functions as Data Transfer Objects (DTOs).
    /// </summary>
    ///   
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
        //public image Image { get; set; }
        public EventItems item { get; set; }
    }

    //public class image
    //{
    //    public string Title { get; set; }
    //    public string Url { get; set; }
    //    public string Link { get; set; }
    //    public string Width { get; set; }
    //    public string Height { get; set; }
    //}

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
