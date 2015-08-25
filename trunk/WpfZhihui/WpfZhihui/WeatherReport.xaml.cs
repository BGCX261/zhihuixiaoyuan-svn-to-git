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
using System.IO;
using System.Net;

namespace WpfZhihui
{
    /// <summary>
    /// Interaction logic for WeatherReport.xaml
    /// </summary>
    public partial class WeatherReport : Window
    {
        public WeatherReport()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            WeatherWebService.WeatherWebServiceSoapClient ws = new WeatherWebService.WeatherWebServiceSoapClient();
            string[] strWeather = ws.getWeatherbyCityName("上海");
            DirectoryInfo di = new DirectoryInfo(System.Environment.CurrentDirectory);
            string strPath = di.Parent.Parent.FullName;
            BitmapImage image = new BitmapImage(new Uri(strPath + @"/weatherlogo/a_" + strWeather[8], UriKind.RelativeOrAbsolute));
            image1.Source = image;
            label1.Content = strWeather[6];
            string str = (strWeather[10].Split('；'))[0];
            label2.Content = (str.Split('：'))[2];
            label3.Content = "上次监测：" + (strWeather[4].Split(' '))[1];
            label4.Content = (strWeather[10].Split('；'))[2];
            label5.Content = (strWeather[10].Split('；'))[1];
            label6.Content = (strWeather[10].Split('；'))[3];
            label7.Content = (strWeather[10].Split('；'))[4];

            label8.Content = (strWeather[6].Split(' '))[0];
            label9.Content = (strWeather[13].Split(' '))[0];
            label10.Content = (strWeather[18].Split(' '))[0];
            image = new BitmapImage(new Uri(strPath + @"/weatherlogo/b_" + strWeather[8], UriKind.RelativeOrAbsolute));
            image2.Source = image;
            image = new BitmapImage(new Uri(strPath + @"/weatherlogo/b_" + strWeather[15], UriKind.RelativeOrAbsolute));
            image3.Source = image;
            image = new BitmapImage(new Uri(strPath + @"/weatherlogo/b_" + strWeather[20], UriKind.RelativeOrAbsolute));
            image4.Source = image;
            label11.Content = strWeather[5];
            label12.Content = strWeather[12];
            label13.Content = strWeather[17];

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
            imageWeather.Source = bmp;
        }

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
    }
}
