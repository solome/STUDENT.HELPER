using FirstFloor.ModernUI.Windows.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net;
namespace ibase.Utility
{
    public static class WebBrowserUtility
    {
        public static readonly DependencyProperty BindableSourceProperty =
            DependencyProperty.RegisterAttached("BindableSource", typeof(string), typeof(WebBrowserUtility), new UIPropertyMetadata(null, BindableSourcePropertyChanged));

        public static string GetBindableSource(DependencyObject obj)
        {
            return (string)obj.GetValue(BindableSourceProperty);
        }

        public static void SetBindableSource(DependencyObject obj, string value)
        {
            obj.SetValue(BindableSourceProperty, value);
        }

        public static HashSet<string> URI_LIST = new HashSet<string>()
        {
            "http://www.google.com/",
            "http://www.bing.com/",
            "http://www.windows.com/",
            "http://www.iliyang.cn/",
            "http://j2ee.iliyang.cn/",
            "http://dolphinux.iliyang.cn/",
            "http://www.apple.com/",
            "http://www.baidu.com/",
            "http://74.125.128.160/",
            "http://cn.bing.com/"
        };

        public static void BindableSourcePropertyChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            WebBrowser browser = o as WebBrowser;
            if (browser != null)
            {
                string uri = e.NewValue as string;
                if (URI_LIST.Contains(uri))
                {
                    browser.Source = uri != null ? new Uri(uri) : null;
                }
            }
        }
    }
}
