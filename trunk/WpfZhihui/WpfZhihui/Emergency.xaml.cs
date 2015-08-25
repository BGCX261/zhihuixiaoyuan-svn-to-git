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
using System.Windows.Shapes;
using System.Web;
using System.Runtime.Serialization;
using System.IO;
using System.Data;
using System.Runtime.Serialization.Json;

namespace WpfZhihui
{
    /// <summary>
    /// Emergency.xaml 的交互逻辑
    /// </summary>
    public partial class Emergency : Window
    {
        public Emergency()
        {
            InitializeComponent();
            //string path=Directory.GetParent(System.Environment.CurrentDirectory + @"/html.htm").Parent.Parent.ToString();
            //webBrowser1.Navigate(
            //    new Uri(System.Environment.CurrentDirectory + @"/html.htm",
            //        UriKind.RelativeOrAbsolute));//获取根目录的日历文件  
            DirectoryInfo di = new DirectoryInfo(System.Environment.CurrentDirectory);
            string path = di.Parent.Parent.FullName;
            ds = new EmergencyBasic();
            webBrowser1.Navigate(new Uri(path + @"/html/Emergency.htm", UriKind.RelativeOrAbsolute));//获取根目录的日历文件
            webBrowser1.ObjectForScripting = ds;//该对象可由显示在WebBrowser控件中的网页所包含的脚本代码访问

        }
        public EmergencyBasic ds;
        [System.Runtime.InteropServices.ComVisibleAttribute(true)]//将该类设置为com可访问
        public class EmergencyBasic
        {
            public static string name;
            public string Name
            {
                get { return name; }
                set { name = value; }
            }
            public void ClickEvent(string strlocation)
            {
                int ijudge = 0;
                if (strlocation == "火灾")
                {
                    
                    ijudge = 1;
                }
                if (strlocation == "突发事件")
                {
                    ijudge = 2;
                }
                DataTable dt = new DataTable();
                SQLHelper.getPointsByCategory(ijudge, out dt);
                List<Location> m_list = new List<Location>();

                for (int ix = 0; ix < dt.Rows.Count; ix++)
                {
                    Location new_Location = new Location();
                    new_Location.Name = dt.Rows[ix]["Name"].ToString();
                    new_Location.Description = dt.Rows[ix]["Description"].ToString();
                    new_Location.Longitude = (double)dt.Rows[ix]["ilng"];
                    new_Location.Latitude = (double)dt.Rows[ix]["ilat"];

                    m_list.Add(new_Location);
                }
                strlocation = ToJsJson(m_list);
                this.Name = strlocation;
            }
            public static string ToJsJson(object item)
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(item.GetType());
                using (MemoryStream ms = new MemoryStream())
                {
                    serializer.WriteObject(ms, item);
                    StringBuilder sb = new StringBuilder();
                    sb.Append(Encoding.UTF8.GetString(ms.ToArray()));
                    return sb.ToString();
                }
            }
        }
        /// <summary>
        /// 数据格式，定义好数据的序列化细节
        /// </summary>
        [DataContract]
        public class Location
        {
            [DataMember(Order = 1)]
            public double Longitude { get; set; }
            [DataMember(Order = 2)]
            public double Latitude { get; set; }
            [DataMember(Order = 3)]
            public string Name { get; set; }
            [DataMember(Order = 4)]
            public string Description { get; set; }

        }
    }
}