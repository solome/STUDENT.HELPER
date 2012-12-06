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
using Windows.UI.Xaml.Media.Imaging;
 
using SQLite;

using Windows.UI.Popups;

namespace 学生助手.STUDENT.HELPER
{

    public sealed partial class UpdateDataPage : 学生助手.STUDENT.HELPER.Common.LayoutAwarePage
    {
        public UpdateDataPage()
        {
            this.InitializeComponent();
        }

        protected override void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
        {
            Random random = new Random();
            Uri uri = new Uri("ms-appx:///Assets/background_" + random.Next(1,7) +".jpg", UriKind.RelativeOrAbsolute);
            ImageBrush background = new ImageBrush();
            background.ImageSource = new BitmapImage(uri);
            this.UpdateDataPage_Main_Grid.Background = background;
        }

        protected override void SaveState(Dictionary<String, Object> pageState)
        {
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Week_ComboBox.SelectedIndex = 0;
           
            this.Title_TextBox.Text = "";
            this.SubTitle_TextBox.Text = "";
            this.Teacher_TextBox.Text = "";
            this.TextBook_TextBox.Text = "";


             this.Start_Hour_ComboBox.SelectedIndex = 0;
             this.Start_Minute_ComboBox.SelectedIndex = 0;
             this.End_Hour_ComboBox.SelectedIndex = 0;
             this.End_Minute_ComboBox.SelectedIndex = 0;


             this.Description.Text = "";
        }

        private string weekTrans(string val)
        {
            string backData = "0";
            switch (val)
            {
                case "星期一":
                    {
                        backData = "1";
                    }
                    break;
                case "星期二":
                    {
                        backData = "2";
                    }
                    break;
                case "星期三":
                    {
                        backData = "3";
                    }
                    break;
                case "星期四":
                    {
                        backData = "4";
                    }
                    break;
                case "星期五":
                    {
                        backData = "5";
                    }
                    break;
                case "星期六":
                    {
                        backData = "6";
                    }
                    break;
                default:
                    break;
            }
            return backData;
        }

        private async void OKButton_Click(object sender, RoutedEventArgs e)
        {
            Messages NewData = new Messages();
            NewData.Week = weekTrans(this.Week_ComboBox.SelectionBoxItem as string);
            NewData.Title = this.Title_TextBox.Text;
            NewData.Subtitle = this.SubTitle_TextBox.Text;
            NewData.Teacher = this.Teacher_TextBox.Text;
            NewData.TextBook = this.TextBook_TextBox.Text;


            DateTime start = new DateTime(1991, 2, 28, Int32.Parse(this.Start_Hour_ComboBox.SelectionBoxItem as string),
                Int32.Parse(Start_Minute_ComboBox.SelectionBoxItem as string), 0);
            DateTime end = new DateTime(1991, 2, 28, Int32.Parse(this.End_Hour_ComboBox.SelectionBoxItem as string),
                Int32.Parse(End_Minute_ComboBox.SelectionBoxItem as string), 0);

            NewData.Start = start.ToString();
            NewData.End = end.ToString();
            NewData.Description = this.Description.Text;

            App.db.Insert(NewData);
            MessageDialog dialog = new MessageDialog("您已成功插入一条新的课程信息，程序下次重启时见效！", "温馨提示");
            
            await dialog.ShowAsync();
            this.Frame.Navigate(typeof(MainPage));
        }

        private void AppBarHomeButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }
        private void Help_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(HelpBasicPage));
        }
    }
}
