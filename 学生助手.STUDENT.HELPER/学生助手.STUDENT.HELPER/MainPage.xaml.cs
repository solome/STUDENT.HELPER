using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

using Windows.Storage;
using 学生助手.STUDENT.HELPER.Data;
using Windows.UI.Notifications;
using Windows.UI.Xaml.Media.Imaging;

namespace 学生助手.STUDENT.HELPER
{
    public sealed partial class MainPage : 学生助手.STUDENT.HELPER.Common.LayoutAwarePage
    {
        public static SampleDataSource solomeData;
        public MainPage()
        {
            this.InitializeComponent();

            TileUpdateManager.CreateTileUpdaterForApplication().EnableNotificationQueue(true);
            InitTiles();

            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
            //初始化，判断是否是第一次加载程序
            if (!localSettings.Values.ContainsKey("db"))
            {
                this.CreateDataBase();
                localSettings.Values["db"] = "sqlite";
            }

            {
                string path = Windows.Storage.ApplicationData.Current.LocalFolder.Path;
                var dbPath = Path.Combine(path, "db.sqlite");

                App.db = new SQLite.SQLiteConnection(dbPath);
            }
            solomeData = new SampleDataSource();
            AppSettingsPane.Load();

        }

        private void InitTiles()
        {
            Random random = new Random();
            for (int i = 0; i < 13; i++)
            {
                string tileXmlString = "<tile>" + "<visual>"
                + "<binding template='TileWideImage' branding='None'>"
                + "<image id='1' src='ms-appx:///Assets/" + random.Next(0,30) +".png' alt='alt text'/>"
                + "</binding>"
                + "<binding template='TileSquareImage' branding='None'>"
                + "<image id='1' src='ms-appx:///Assets/" + random.Next(0, 30) + ".png' alt='alt text'/>"
                + "</binding>"
                + "</visual>"
                + "</tile>";
                Windows.Data.Xml.Dom.XmlDocument tileDOM = new Windows.Data.Xml.Dom.XmlDocument();
                tileDOM.LoadXml(tileXmlString);
                TileNotification tile = new TileNotification(tileDOM);
                TileUpdateManager.CreateTileUpdaterForApplication().Update(tile);
            }
        }
        protected override void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
        {
            Random random = new Random();
            Uri uri = new Uri("ms-appx:///Assets/background_" + random.Next(1,7) +".jpg", UriKind.RelativeOrAbsolute);
            ImageBrush background = new ImageBrush();
            background.ImageSource = new BitmapImage(uri);
            this.MainPage_Grid.Background = background;
            var sampleDataGroups = SampleDataSource.GetGroups("AllGroups");
            this.DefaultViewModel["Items"] = sampleDataGroups;

        }

        private void ItemView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var groupId = ((SampleDataGroup)e.ClickedItem).UniqueId;
            this.Frame.Navigate(typeof(SplitPage), groupId);
        }



        /// <summary>
        /// 应用程序第一次启动时创建数据库
        /// </summary>
        private void CreateDataBase()
        {
            string path = Windows.Storage.ApplicationData.Current.LocalFolder.Path;
            var dbPath = Path.Combine(path, "db.sqlite");

            using (App.db = new SQLite.SQLiteConnection(dbPath))
            {
                DateTime TempTime = new DateTime(2012, 12, 12, 12, 12, 12);
                App.db.CreateTable<Messages>();
                App.db.Insert(new Messages()
                {
                    Week = "0",
                    Title = "课程名",
                    Subtitle = "上课地点",
                    Description = "Good Good Study,Day Day Up!Good Good Study,Day Day Up!Good Good Study,Day Day Up!Good Good Study,Day Day Up!Good Good Study,Day Day Up!Good Good Study,Day Day Up!Good Good Study,Day Day Up!",
                    Teacher = "教师名",
                    TextBook = "（教材类型）课程名",
                    Start = TempTime.ToString(),
                    End = TempTime.ToString()
                });
                App.db.Insert(new Messages()
                {
                    Week = "0",
                    Title = "课程名",
                    Subtitle = "上课地点",
                    Description = "Good Good Study,Day Day Up!Good Good Study,Day Day Up!Good Good Study,Day Day Up!Good Good Study,Day Day Up!Good Good Study,Day Day Up!Good Good Study,Day Day Up!Good Good Study,Day Day Up!",
                    Teacher = "教师名",
                    TextBook = "（教材类型）课程名",
                    Start = TempTime.ToString(),
                    End = TempTime.ToString()
                });

                App.db.Insert(new Messages()
                {
                    Week = "1",
                    Title = "课程名",
                    Subtitle = "上课地点",
                    Description = "Good Good Study,Day Day Up!Good Good Study,Day Day Up!Good Good Study,Day Day Up!Good Good Study,Day Day Up!Good Good Study,Day Day Up!Good Good Study,Day Day Up!Good Good Study,Day Day Up!",
                    Teacher = "教师名",
                    TextBook = "（教材类型）课程名",
                    Start = TempTime.ToString(),
                    End = TempTime.ToString()
                }); App.db.Insert(new Messages()
                {
                    Week = "1",
                    Title = "课程名",
                    Subtitle = "上课地点",
                    Description = "Good Good Study,Day Day Up!Good Good Study,Day Day Up!Good Good Study,Day Day Up!Good Good Study,Day Day Up!Good Good Study,Day Day Up!Good Good Study,Day Day Up!Good Good Study,Day Day Up!",
                    Teacher = "教师名",
                    TextBook = "（教材类型）课程名",
                    Start = TempTime.ToString(),
                    End = TempTime.ToString()
                }); App.db.Insert(new Messages()
                {
                    Week = "1",
                    Title = "课程名",
                    Subtitle = "上课地点",
                    Description = "Good Good Study,Day Day Up!Good Good Study,Day Day Up!Good Good Study,Day Day Up!Good Good Study,Day Day Up!Good Good Study,Day Day Up!Good Good Study,Day Day Up!Good Good Study,Day Day Up!",
                    Teacher = "教师名",
                    TextBook = "（教材类型）课程名",
                    Start = TempTime.ToString(),
                    End = TempTime.ToString()
                }); App.db.Insert(new Messages()
                {
                    Week = "1",
                    Title = "课程名",
                    Subtitle = "上课地点",
                    Description = "Good Good Study,Day Day Up!Good Good Study,Day Day Up!Good Good Study,Day Day Up!Good Good Study,Day Day Up!Good Good Study,Day Day Up!Good Good Study,Day Day Up!Good Good Study,Day Day Up!",
                    Teacher = "教师名",
                    TextBook = "（教材类型）课程名",
                    Start = TempTime.ToString(),
                    End = TempTime.ToString()
                });
                App.db.Insert(new Messages()
                {
                    Week = "2",
                    Title = "课程名",
                    Subtitle = "上课地点",
                    Description = "Good Good Study,Day Day Up!Good Good Study,Day Day Up!Good Good Study,Day Day Up!Good Good Study,Day Day Up!Good Good Study,Day Day Up!Good Good Study,Day Day Up!Good Good Study,Day Day Up!",
                    Teacher = "教师名",
                    TextBook = "（教材类型）课程名",
                    Start = TempTime.ToString(),
                    End = TempTime.ToString()
                });
                App.db.Insert(new Messages()
                {
                    Week = "2",
                    Title = "课程名",
                    Subtitle = "上课地点",
                    Description = "Good Good Study,Day Day Up!Good Good Study,Day Day Up!Good Good Study,Day Day Up!Good Good Study,Day Day Up!Good Good Study,Day Day Up!Good Good Study,Day Day Up!Good Good Study,Day Day Up!",
                    Teacher = "教师名",
                    TextBook = "（教材类型）课程名",
                    Start = TempTime.ToString(),
                    End = TempTime.ToString()
                }); App.db.Insert(new Messages()
                {
                    Week = "2",
                    Title = "课程名",
                    Subtitle = "上课地点",
                    Description = "Good Good Study,Day Day Up!Good Good Study,Day Day Up!Good Good Study,Day Day Up!Good Good Study,Day Day Up!Good Good Study,Day Day Up!Good Good Study,Day Day Up!Good Good Study,Day Day Up!",
                    Teacher = "教师名",
                    TextBook = "（教材类型）课程名",
                    Start = TempTime.ToString(),
                    End = TempTime.ToString()
                }); App.db.Insert(new Messages()
                {
                    Week = "2",
                    Title = "课程名",
                    Subtitle = "上课地点",
                    Description = "Good Good Study,Day Day Up!Good Good Study,Day Day Up!Good Good Study,Day Day Up!Good Good Study,Day Day Up!Good Good Study,Day Day Up!Good Good Study,Day Day Up!Good Good Study,Day Day Up!",
                    Teacher = "教师名",
                    TextBook = "（教材类型）课程名",
                    Start = TempTime.ToString(),
                    End = TempTime.ToString()
                });
                App.db.Insert(new Messages()
                {
                    Week = "3",
                    Title = "课程名",
                    Subtitle = "上课地点",
                    Description = "Good Good Study,Day Day Up!Good Good Study,Day Day Up!Good Good Study,Day Day Up!Good Good Study,Day Day Up!Good Good Study,Day Day Up!Good Good Study,Day Day Up!Good Good Study,Day Day Up!",
                    Teacher = "教师名",
                    TextBook = "（教材类型）课程名",
                    Start = TempTime.ToString(),
                    End = TempTime.ToString()
                });
                App.db.Insert(new Messages()
                {
                    Week = "3",
                    Title = "课程名",
                    Subtitle = "上课地点",
                    Description = "Good Good Study,Day Day Up!Good Good Study,Day Day Up!Good Good Study,Day Day Up!Good Good Study,Day Day Up!Good Good Study,Day Day Up!Good Good Study,Day Day Up!Good Good Study,Day Day Up!",
                    Teacher = "教师名",
                    TextBook = "（教材类型）课程名",
                    Start = TempTime.ToString(),
                    End = TempTime.ToString()
                }); App.db.Insert(new Messages()
                {
                    Week = "3",
                    Title = "课程名",
                    Subtitle = "上课地点",
                    Description = "Good Good Study,Day Day Up!Good Good Study,Day Day Up!Good Good Study,Day Day Up!Good Good Study,Day Day Up!Good Good Study,Day Day Up!Good Good Study,Day Day Up!Good Good Study,Day Day Up!",
                    Teacher = "教师名",
                    TextBook = "（教材类型）课程名",
                    Start = TempTime.ToString(),
                    End = TempTime.ToString()
                }); App.db.Insert(new Messages()
                {
                    Week = "3",
                    Title = "课程名",
                    Subtitle = "上课地点",
                    Description = "Good Good Study,Day Day Up!Good Good Study,Day Day Up!Good Good Study,Day Day Up!Good Good Study,Day Day Up!Good Good Study,Day Day Up!Good Good Study,Day Day Up!Good Good Study,Day Day Up!",
                    Teacher = "教师名",
                    TextBook = "（教材类型）课程名",
                    Start = TempTime.ToString(),
                    End = TempTime.ToString()
                }); App.db.Insert(new Messages()
                {
                    Week = "3",
                    Title = "课程名",
                    Subtitle = "上课地点",
                    Description = "Good Good Study,Day Day Up!Good Good Study,Day Day Up!Good Good Study,Day Day Up!Good Good Study,Day Day Up!Good Good Study,Day Day Up!Good Good Study,Day Day Up!Good Good Study,Day Day Up!",
                    Teacher = "教师名",
                    TextBook = "（教材类型）课程名",
                    Start = TempTime.ToString(),
                    End = TempTime.ToString()
                });
                App.db.Insert(new Messages()
                {
                    Week = "4",
                    Title = "课程名",
                    Subtitle = "上课地点",
                    Description = "Good Good Study,Day Day Up!Good Good Study,Day Day Up!Good Good Study,Day Day Up!Good Good Study,Day Day Up!Good Good Study,Day Day Up!Good Good Study,Day Day Up!Good Good Study,Day Day Up!",
                    Teacher = "教师名",
                    TextBook = "（教材类型）课程名",
                    Start = TempTime.ToString(),
                    End = TempTime.ToString()
                });
                App.db.Insert(new Messages()
                {
                    Week = "4",
                    Title = "课程名",
                    Subtitle = "上课地点",
                    Description = "Good Good Study,Day Day Up!Good Good Study,Day Day Up!Good Good Study,Day Day Up!Good Good Study,Day Day Up!Good Good Study,Day Day Up!Good Good Study,Day Day Up!Good Good Study,Day Day Up!",
                    Teacher = "教师名",
                    TextBook = "（教材类型）课程名",
                    Start = TempTime.ToString(),
                    End = TempTime.ToString()
                }); App.db.Insert(new Messages()
                {
                    Week = "4",
                    Title = "课程名",
                    Subtitle = "上课地点",
                    Description = "Good Good Study,Day Day Up!Good Good Study,Day Day Up!Good Good Study,Day Day Up!Good Good Study,Day Day Up!Good Good Study,Day Day Up!Good Good Study,Day Day Up!Good Good Study,Day Day Up!",
                    Teacher = "教师名",
                    TextBook = "（教材类型）课程名",
                    Start = TempTime.ToString(),
                    End = TempTime.ToString()
                }); App.db.Insert(new Messages()
                {
                    Week = "4",
                    Title = "课程名",
                    Subtitle = "上课地点",
                    Description = "Good Good Study,Day Day Up!Good Good Study,Day Day Up!Good Good Study,Day Day Up!Good Good Study,Day Day Up!Good Good Study,Day Day Up!Good Good Study,Day Day Up!Good Good Study,Day Day Up!",
                    Teacher = "教师名",
                    TextBook = "（教材类型）课程名",
                    Start = TempTime.ToString(),
                    End = TempTime.ToString()
                }); App.db.Insert(new Messages()
                {
                    Week = "4",
                    Title = "课程名",
                    Subtitle = "上课地点",
                    Description = "Good Good Study,Day Day Up!Good Good Study,Day Day Up!Good Good Study,Day Day Up!Good Good Study,Day Day Up!Good Good Study,Day Day Up!Good Good Study,Day Day Up!Good Good Study,Day Day Up!",
                    Teacher = "教师名",
                    TextBook = "（教材类型）课程名",
                    Start = TempTime.ToString(),
                    End = TempTime.ToString()
                }); App.db.Insert(new Messages()
                {
                    Week = "4",
                    Title = "课程名",
                    Subtitle = "上课地点",
                    Description = "Good Good Study,Day Day Up!Good Good Study,Day Day Up!Good Good Study,Day Day Up!Good Good Study,Day Day Up!Good Good Study,Day Day Up!Good Good Study,Day Day Up!Good Good Study,Day Day Up!",
                    Teacher = "教师名",
                    TextBook = "（教材类型）课程名",
                    Start = TempTime.ToString(),
                    End = TempTime.ToString()
                });
                App.db.Insert(new Messages()
                {
                    Week = "5",
                    Title = "课程名",
                    Subtitle = "上课地点",
                    Description = "Good Good Study,Day Day Up!Good Good Study,Day Day Up!Good Good Study,Day Day Up!Good Good Study,Day Day Up!Good Good Study,Day Day Up!Good Good Study,Day Day Up!Good Good Study,Day Day Up!",
                    Teacher = "教师名",
                    TextBook = "（教材类型）课程名",
                    Start = TempTime.ToString(),
                    End = TempTime.ToString()
                });
                App.db.Insert(new Messages()
                {
                    Week = "5",
                    Title = "课程名",
                    Subtitle = "上课地点",
                    Description = "Good Good Study,Day Day Up!Good Good Study,Day Day Up!Good Good Study,Day Day Up!Good Good Study,Day Day Up!Good Good Study,Day Day Up!Good Good Study,Day Day Up!Good Good Study,Day Day Up!",
                    Teacher = "教师名",
                    TextBook = "（教材类型）课程名",
                    Start = TempTime.ToString(),
                    End = TempTime.ToString()
                }); App.db.Insert(new Messages()
                {
                    Week = "5",
                    Title = "课程名",
                    Subtitle = "上课地点",
                    Description = "Good Good Study,Day Day Up!Good Good Study,Day Day Up!Good Good Study,Day Day Up!Good Good Study,Day Day Up!Good Good Study,Day Day Up!Good Good Study,Day Day Up!Good Good Study,Day Day Up!",
                    Teacher = "教师名",
                    TextBook = "（教材类型）课程名",
                    Start = TempTime.ToString(),
                    End = TempTime.ToString()
                });
                App.db.Insert(new Messages()
                {
                    Week = "5",
                    Title = "课程名",
                    Subtitle = "上课地点",
                    Description = "Good Good Study,Day Day Up!Good Good Study,Day Day Up!Good Good Study,Day Day Up!Good Good Study,Day Day Up!Good Good Study,Day Day Up!Good Good Study,Day Day Up!Good Good Study,Day Day Up!",
                    Teacher = "教师名",
                    TextBook = "（教材类型）课程名",
                    Start = TempTime.ToString(),
                    End = TempTime.ToString()
                });
                App.db.Insert(new Messages()
                {
                    Week = "6",
                    Title = "课程名",
                    Subtitle = "上课地点",
                    Description = "Good Good Study,Day Day Up!Good Good Study,Day Day Up!Good Good Study,Day Day Up!Good Good Study,Day Day Up!Good Good Study,Day Day Up!Good Good Study,Day Day Up!Good Good Study,Day Day Up!",
                    Teacher = "教师名",
                    TextBook = "（教材类型）课程名",
                    Start = TempTime.ToString(),
                    End = TempTime.ToString()
                });
                App.db.Insert(new Messages()
                {
                    Week = "6",
                    Title = "课程名",
                    Subtitle = "上课地点",
                    Description = "Good Good Study,Day Day Up!Good Good Study,Day Day Up!Good Good Study,Day Day Up!Good Good Study,Day Day Up!Good Good Study,Day Day Up!Good Good Study,Day Day Up!Good Good Study,Day Day Up!",
                    Teacher = "教师名",
                    TextBook = "（教材类型）课程名",
                    Start = TempTime.ToString(),
                    End = TempTime.ToString()
                });
            }

        }

        private void AppBarAddButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(UpdateDataPage));
        }
        private void AppBarEditButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(UpdatePage));
        }

        private void Help_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(HelpBasicPage));
        }
    }
}
