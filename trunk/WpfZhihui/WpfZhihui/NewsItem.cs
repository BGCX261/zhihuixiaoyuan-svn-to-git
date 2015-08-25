using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WpfZhihui
{
    class NewsItem
    {
        string channelTitle;
        public string ChannelTitle
        {
            get { return channelTitle; }
            set { channelTitle = value; }
        }
        string channellink;
        public string ChannelLink
        {
            get { return channellink; }
            set { channellink = value; }
        }
        string title;
        public string Title
        {
            get { return title; }
            set { title = value; }
        }
        string description;
        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        string link;
        public string Link
        {
            get { return link; }
            set { link = value; }
        }
        string pubDate;
        public string PubDate
        {
            get { return pubDate; }
            set { pubDate = value; }
        }  
    }
}
