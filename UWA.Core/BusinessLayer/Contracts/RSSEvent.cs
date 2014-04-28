using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using SQLite;
using UWA.Core.ServiceAccessLayer;

namespace UWA.Core.BusinessLayer.Contracts
{
    /// <summary>
    /// Maybe use enumeration later to specify the format in which the Event is displayed on screen.
    /// In example, Warning events would not display url and would have an appropriate image thumbnail.
    /// </summary>
    public enum Category { ARTICLE, ANNOUNCEMENT, WARNING, }

    /// <summary>
    /// Business model presentation of DTO data deserealised by the Eventservice.
    /// </summary>
    public class RSSEvent : IBusinessEntity
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Published { get; set; }
        public string Link { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }

        public RSSEvent() {}

        public RSSEvent(ServiceAccessLayer.item inputEvent)
        {
            Published = inputEvent.pubDate;
            Link = inputEvent.link;
            Category = inputEvent.category;
            Description = inputEvent.description;
        }

        public override string ToString()
        {
            return Description;
        }
    }
}
