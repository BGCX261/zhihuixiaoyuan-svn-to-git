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
using System.IO;
using System.Windows.Controls.DataVisualization.Charting;
using System.Diagnostics;
using System.Net;

namespace WpfZhihui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool RemoteFileExists(string fileUri)
        {
            bool result = false;//下载结果

            WebResponse response = null;
            try
            {
                WebRequest req = WebRequest.Create(fileUri);

                response = req.GetResponse();

                result = response == null ? false : true;

            }
            catch (Exception ex)
            {
                result = false;
            }
            finally
            {
                if (response != null)
                {
                    response.Close();
                }
            }

            return result;
        }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            //string path=Directory.GetParent(System.Environment.CurrentDirectory + @"/html.htm").Parent.Parent.ToString();
            //webBrowser1.Navigate(
            //    new Uri(System.Environment.CurrentDirectory + @"/html.htm",
            //        UriKind.RelativeOrAbsolute));//获取根目录的日历文件  
            DirectoryInfo di = new DirectoryInfo(System.Environment.CurrentDirectory);
            string path = di.Parent.Parent.FullName;
            ds = new Basic();
            webBrowser1.Navigate(new Uri(path + @"/aaa.html", UriKind.RelativeOrAbsolute));//获取根目录的日历文件
            webBrowser1.ObjectForScripting = ds;//该对象可由显示在WebBrowser控件中的网页所包含的脚本代码访问


            System.Net.WebClient wc = new System.Net.WebClient();

            DateTime nowtime = DateTime.Now;
            string timeStr = nowtime.ToString("yyyyMMddHHmm");
            string minute = DateTime.Now.Minute.ToString();
            int minuteInt = Convert.ToInt32(minute);
            int model = minuteInt - minuteInt % 6 + 2;
            string modelstr = model.ToString("D2");
            timeStr = timeStr.Substring(0, 10) + modelstr;
            string imgPath = @"http://www.soweather.com/PicFiles/wd" + timeStr + @".png";

            while (!RemoteFileExists(imgPath))
            {
                nowtime = nowtime.AddMinutes(-6);
                timeStr = nowtime.ToString("yyyyMMddHHmm");
                minute = nowtime.Minute.ToString();
                minuteInt = Convert.ToInt32(minute);
                model = minuteInt - minuteInt % 6 + 2;
                modelstr = model.ToString("D2");
                timeStr = timeStr.Substring(0, 10) + modelstr;
                imgPath = @"http://www.soweather.com/PicFiles/wd" + timeStr + @".png";
            }
            /*
            wc.DownloadFile(new Uri(imgPath), timeStr + @".png");
            FileInfo fileInfo = new FileInfo(timeStr + @".png");

            DirectoryInfo diDebug = new DirectoryInfo(System.Environment.CurrentDirectory);
            string strPath = diDebug.FullName;
            image1.Source = new BitmapImage(new Uri(diDebug+@"\"+timeStr+@".png", UriKind.RelativeOrAbsolute));
            */
            
            Uri uri = new Uri(imgPath, UriKind.Absolute);
            BitmapImage bmp = new BitmapImage(uri);
            image1.Source = bmp;
            //wc.DownloadFile(@"http://www.soweather.com/PicFiles/wd201208041516.png", @"d:\a.jpg");


        }
        public Basic ds;

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            label1.Content = ds.Name;
            ((LineSeries)mcChart.Series[0]).ItemsSource = new KeyValuePair<int, int>[]
            {
                new KeyValuePair<int, int>(1, 100),
                new KeyValuePair<int, int>(2, 130),
                new KeyValuePair<int, int>(3, 150),
                new KeyValuePair<int, int>(4, 125)
            };


        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }
    }

    [System.Runtime.InteropServices.ComVisibleAttribute(true)]//将该类设置为com可访问
    public class Basic
    {
        public static string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public void ClickEvent(string str)
        {
            this.Name = str + "hello";
        }
    }



}
