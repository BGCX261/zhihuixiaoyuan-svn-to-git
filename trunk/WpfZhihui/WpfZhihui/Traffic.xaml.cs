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

namespace WpfZhihui
{
    /// <summary>
    /// Traffic.xaml 的交互逻辑
    /// </summary>
    public partial class Traffic : Window
    {
        public Traffic()
        {
            InitializeComponent();
            DirectoryInfo di = new DirectoryInfo(System.Environment.CurrentDirectory);
            string path = di.Parent.Parent.FullName;
            webBrowser1.Navigate(new Uri(path+@"/html/Traffic.htm", UriKind.RelativeOrAbsolute));
        }
    }
}
