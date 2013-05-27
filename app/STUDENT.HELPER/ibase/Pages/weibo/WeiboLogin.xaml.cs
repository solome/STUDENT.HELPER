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

using NetDimension.Weibo;

namespace ibase.Pages.weibo
{
    /// <summary>
    /// Interaction logic for WeiboLogin.xaml
    /// </summary>
    public partial class WeiboLogin : UserControl
    {
        public WeiboLogin()
        {
            InitializeComponent();
            this.tbx_Loginport.Text = "dabbysunshine@qq.com";
            this.pbx_Passwd.Password = "13074036155Dabby";
            this.initLogined();
        }

        private void initLogined()
        {
            if (App.BLogin == true)
            {
                this.spl_Login.Visibility = System.Windows.Visibility.Hidden;
                this.spl_Logined.Visibility = System.Windows.Visibility.Visible;

                this.imgProfile.Source = new BitmapImage(new Uri(App.me.AvatarLarge, UriKind.Absolute));
                this.tbk_NickName.Text = App.me.Name;
                this.tbk_Description.Text = App.me.Description;
                this.tbk_FriendCount.Text = string.Format("_({0})", App.me.FriendsCount);
                this.tbk_FollowersCount.Text = string.Format("_({0})", App.me.FollowersCount);
                this.tbk_StatusesCount.Text = string.Format("_({0})", App.me.StatusesCount);
                this.tbk_Msg.Text = App.me.AvatarLarge + "\n" +
                    App.me.CreatedAt + "\n" +
                    App.me.Domain + "\n" +
                    App.me.Gender + "\n" +
                    App.me.Location + "\n" +
                    App.me.OnlineStatus + "\n" +
                    App.me.Province;
            }

        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            this.tbx_Loginport.Text = "";
            this.pbx_Passwd.Password = "";
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            App.oAuth.ClientLogin(this.tbx_Loginport.Text, this.pbx_Passwd.Password);
            var result = App.oAuth.VerifierAccessToken();
            if (result == NetDimension.Weibo.TokenResult.Success)
            {
                App.BLogin = true;
                App.loginport = this.tbx_Loginport.Text;
                App.password = this.pbx_Passwd.Password;
                App.Sina = new Client(App.oAuth);
                App.me = App.Sina.API.Entity.Users.Show(App.Sina.API.Entity.Account.GetUID());
                this.initLogined();
                MessageBox.Show("登陸成功" + App.me.ProfileUrl);
            }
            else
            {
                MessageBox.Show("error");
            }
        }
    }
}
