using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using NetDimension.Weibo;
namespace ibase
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public const string AppKey = "1864717315";
        public const string AppSecret = "dba4267b37bb29ae9b711e31371e7398";
        public const string CallbackUrl = "http://www.iliyang.cn/";
        //public string AccessToken = "";
        public static bool BLogin { get; set; }
        public static  OAuth oAuth { get; set; }
        public static Client Sina { get; set; }
        public static string loginport { get; set; }
        public static string password { get; set; }
        public static NetDimension.Weibo.Entities.user.Entity me { get; set; }
        public App()
        {
            oAuth = new OAuth(AppKey, AppSecret, CallbackUrl);
            BLogin = false;
        }

    }
}
