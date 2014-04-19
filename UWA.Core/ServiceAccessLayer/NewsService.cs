using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml;

namespace UWA.Core.ServiceAccessLayer
{
    public static class NewsService
    {
        public static List<NewsEntry> GetFeedItems(string url)
        {
            List<NewsEntry> feedItemsList = new List<NewsEntry>();

            try
            {
                WebRequest webRequest = WebRequest.Create(url);
                WebResponse webResponse = webRequest.GetResponse();

                Stream stream = webResponse.GetResponseStream();
                var xmlDocument = new XmlDocument();

                xmlDocument.Load(stream);

                var nsmgr = new XmlNamespaceManager(xmlDocument.NameTable);
                nsmgr.AddNamespace("dc", xmlDocument.DocumentElement.GetNamespaceOfPrefix("dc"));
                nsmgr.AddNamespace("content", xmlDocument.DocumentElement.GetNamespaceOfPrefix("content"));

                XmlNodeList itemNodes = xmlDocument.SelectNodes("rss/channel/item");

                for (int i = 0; i < itemNodes.Count; i++)
                {
                    var feedItem = new NewsEntry();

                    if (itemNodes[i].SelectSingleNode("title") != null)
                    {
                        feedItem.Title = itemNodes[i].SelectSingleNode("title").InnerText;
                    }

                    if (itemNodes[i].SelectSingleNode("link") != null)
                    {
                        feedItem.Url = itemNodes[i].SelectSingleNode("link").InnerText;
                    }

                    if (itemNodes[i].SelectSingleNode("pubDate") != null)
                    {
                        feedItem.Published = Convert.ToDateTime(itemNodes[i].SelectSingleNode("pubDate").InnerText);
                    }


                    if (itemNodes[i].SelectSingleNode("description") != null)
                    {
                        feedItem.Description = itemNodes[i].SelectSingleNode("description").InnerText;
                    }

                    if (itemNodes[i].SelectSingleNode("content:encoded", nsmgr) != null)
                    {
                        feedItem.Content = itemNodes[i].SelectSingleNode("content:encoded", nsmgr).InnerText;
                    }
                    else
                    {
                        feedItem.Content = feedItem.Description;
                    }

                    feedItemsList.Add(feedItem);
                }
            }
            catch (Exception)
            {
                throw;
            }

            return feedItemsList;
        }
    }   
}
