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
using System.Threading;
using System.IO;

namespace ibase.Pages.weibo
{
    /// <summary>
    /// Interaction logic for WeiboMessages.xaml
    /// </summary>
    public partial class WeiboMessages : UserControl
    {
        System.Collections.ObjectModel.ObservableCollection<string> msgList = new System.Collections.ObjectModel.ObservableCollection<string>();
        public WeiboMessages()
        {
            InitializeComponent();
            this.lbx_WeiboMsg.ItemsSource = msgList;
            this.refresh();
        }

        private delegate void addMsgEvent(string msg);
        private void addMsg(string msg)
        {
            msgList.Add(msg);
        }

        private void refresh()
        {    
            ThreadPool.QueueUserWorkItem(delegate
            {
                if (App.BLogin == true)
                {
                    var json = App.Sina.API.Entity.Statuses.FriendsTimeline(count: 20);
                    if (json.Statuses != null)
                    {
                        foreach (var status in json.Statuses)
                        {
                            if (status.User == null)
                                continue;

                            if (status.RetweetedStatus != null && status.RetweetedStatus.User != null)
                            {
                                Application.Current.Dispatcher.Invoke(new addMsgEvent(this.addMsg), status.Text);
                            }
                        }
                    }
                }
            });
            
       
            
        }
        private void btn_Refresh_Click(object sender, RoutedEventArgs e)
        {
            this.refresh();
        }
    }
}
