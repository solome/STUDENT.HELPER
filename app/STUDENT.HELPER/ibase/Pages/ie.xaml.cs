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
using ibase.Utility;
namespace ibase.Pages
{
    /// <summary>
    /// Interaction logic for ie.xaml
    /// </summary>
    public partial class ie : UserControl
    {
        public ie()
        {
            InitializeComponent();
        }

        private void btn_GoNext_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(this.tbx_Uri.Text);
            this.wbs_Ie.Source = new Uri("http://" + this.tbx_Uri.Text);
        }

        private void btn_Home_Click(object sender, RoutedEventArgs e)
        {
            this.tbx_Uri.Text = "http://www.bing.com/";
        }

        private void btn_Left_Click(object sender, RoutedEventArgs e)
        {
            if (this.wbs_Ie.CanGoBack)
            {
                this.wbs_Ie.GoBack();
            }
        }
        private void btn_Right_Click(object sender, RoutedEventArgs e)
        {
            if (this.wbs_Ie.CanGoForward)
            {
                this.wbs_Ie.GoForward();
            }
        }

        private void btn_Refresh_Click(object sender, RoutedEventArgs e)
        {
            this.wbs_Ie.Refresh();
        }

        private void btn_Bing_Click(object sender, RoutedEventArgs e)
        {
            this.tbx_Uri.Text = "http://cn.bing.com/";
        }

        private void btn_Google_Clcik(object sender, RoutedEventArgs e)
        {
            this.tbx_Uri.Text = "http://74.125.128.160/";
        }
    }
}
