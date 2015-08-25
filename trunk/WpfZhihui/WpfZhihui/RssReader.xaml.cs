using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using System.Collections;
using System.Diagnostics;

namespace WpfZhihui
{
    /// <summary>
    /// RssReader.xaml 的交互逻辑
    /// </summary>
    public partial class RssReader : UserControl
    {
        private List<NewsItem> items = new List<NewsItem>();
        

        public RssReader()
        {
            InitializeComponent();
            LoadRss();

        }
        public void LoadRss()
        {
            //string channeltitle = "网易头条";
            //string  channellink ="http://news.163.com/";
            //string rsspath = "http://news.163.com/special/00011K6L/rss_newstop.xml";//RSS地址
            string[] rsspath ={ "http://news.163.com/special/00011K6L/rss_newstop.xml",
                                   "http://www.xinhuanet.com/politics/news_politics.xml",
                                   "http://news.baidu.com/n?cmd=1&class=civilnews&tn=rss&sub=0",
                                   "http://www.shanghai.gov.cn/shanghai/node27041/node27044/index.xml",
                                   "http://news.online.sh.cn/news/gb/special/news.xml",
                                   "http://rich.online.sh.cn/rich/gb/special/rich.xml"};//RSS地址
            string[] channeltitle ={"网易头条",
                                   "新华时政",
                                   "百度国内焦点",
                                   "中国上海",
                                   "上海新闻热线",
                                   "上海财经热线"};
            string[] channellink ={"http://news.163.com/",
                                   "http://www.xinhuanet.com/politics/xw.htm",
                                   "http://news.baidu.com",
                                   "http://www.shanghai.gov.cn/shanghai/node2314/index.html",
                                   "http://news.online.sh.cn/news/gb/node/news_default.htm",
                                   "http://rich.online.sh.cn/rich/gb/node/node_54714.htm"};
            for (int cid = 0; cid < 6; cid++)
            {
                XmlDocument doc = new XmlDocument();//创建文档对象
                try
                {
                    doc.Load(rsspath[cid]);//加载XML 包括HTTP：// 和本地
                }
                catch (Exception)
                {
                    //异常处理
                }
                //初始化Rss 
                XmlNodeList list = doc.GetElementsByTagName("item"); //获得项   
                XmlNode node = list.Item(1);//
                NewsItem item = new NewsItem();
                item = getItem((XmlElement)node);
                item.ChannelLink = channellink[cid];
                item.ChannelTitle = channeltitle[cid];
                //加入list
                items.Add(item);
            }
           
            //添加绑定操作
            this.Rsslist.ItemsSource = items;
        }
        private NewsItem getItem(XmlElement ele)
        {
            NewsItem item = new NewsItem();
            item.Title = ele.GetElementsByTagName("title")[0].InnerText;//获得标题
            item.Link = ele.GetElementsByTagName("link")[0].InnerText;//获得联接
            string des = ele.GetElementsByTagName("description")[0].InnerText;//获得简介
            if (des.Length > 80) {
                des = des.Substring(0, 80)+"……";
            }
            item.Description = des;
            //item.PubDate = ele.GetElementsByTagName("pubDate")[0].InnerText;//获得发布日期
            return item;   
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }



    }
}
