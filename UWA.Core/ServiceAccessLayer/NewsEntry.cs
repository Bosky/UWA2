using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UWA.Core.BusinessLayer.Contracts;

namespace UWA.Core.ServiceAccessLayer
{


    /// <summary>
    /// The ServiceAccessLayer contains the code that accesses remote servers to download data for 
    /// news RSS and Twitter feed. Three classes – RSSParser and TwitterParser – 
    /// exist to convert the external data format (which could be XML, JSON or any custom format) 
    /// into objects that can be saved to the database. The Service Abstraction Layer exists to 
    /// encapsulate this code and ensure that the rest of the application only has to deal with objects.
    /// </summary>
    public class NewsEntry : BusinessEntityBase
    {

        public NewsEntry()
        {
        }

        public string Title
        {
            get;
            set;
        }

        public string Content
        {
            get;
            set;
        }

        public DateTime Published
        {
            get;
            set;
        }

        public string Description { get; set; }

        public string Url
        {
            get;
            set;
        }
    }
}
