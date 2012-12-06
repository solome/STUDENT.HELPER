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

    public sealed partial class UpdatePage : 学生助手.STUDENT.HELPER.Common.LayoutAwarePage
    {

        class ItemShow : 学生助手.STUDENT.HELPER.Common.BindableBase
        {
            private static Uri _baseUri = new Uri("ms-appx:///");
            private ImageSource _image;
            private string _imagePath;
            private string _title;
            private string _subtitle;

            private DateTime _start;
            private DateTime _end;

            private string _teacher;
            private string _textbook;
            public string Start
            {
                get
                {
                    return _start.ToString("HH:mm");
                }
            }

            public string End
            {
                get
                {
                    return _end.ToString("HH:mm");
                }
            }
            public ItemShow(string path, string Title, string Subtitle,string Teacher,string Textbook,
                DateTime Start = new DateTime(), DateTime End = new DateTime())
            {
                this._imagePath = path;
                this._title = Title;
                this._subtitle = Subtitle;
                this._teacher = Teacher;
                this._textbook = Textbook;
                this._start = Start;
                this._end = End;
            }

            public string Title
            {
                get { return this._title; }
                set { this.SetProperty(ref this._title, value); }
            }

            public string Subtitle
            {
                get { return this._subtitle; }
                set { this.SetProperty(ref this._subtitle, value); }
            }

            public string Teacher
            {
                get { return this._teacher; }
                set { this.SetProperty(ref this._teacher, value); }
            }

            public string Textbook
            {
                get { return this._textbook; }
                set { this.SetProperty(ref this._textbook, value); }
            }

            public ImageSource Image
            {
                get
                {
                    if (this._image == null && this._imagePath != null)
                    {
                        this._image = new BitmapImage(new Uri(ItemShow._baseUri, this._imagePath));
                    }
                    return this._image;
                }

                set
                {
                    this._imagePath = null;
                    this.SetProperty(ref this._image, value);
                }
            }

        }

        public UpdatePage()
        {
            this.InitializeComponent();
        }

        protected override void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
        {
            Random random = new Random();
            Uri uri = new Uri("ms-appx:///Assets/background_" + random.Next(1,7) +".jpg", UriKind.RelativeOrAbsolute);
            ImageBrush background = new ImageBrush();
            background.ImageSource = new BitmapImage(uri);
            this.UpdatePage_Main_Grid.Background = background;
            updateData("6");
        }

        protected override void SaveState(Dictionary<String, Object> pageState)
        {
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

        private void OKButton_click(object sender, RoutedEventArgs e)
        {
            updateData(weekTrans(this.Week_ComboBox.SelectionBoxItem as string));
        }


        private void updateData(string Week)
        {
            List<ItemShow> _list = new List<ItemShow>();
           
            string sql = "select * From messages where week = '" + Week + "'";
            List<object> list = App.db.Query(new TableMapping(typeof(Messages)), sql);
            Messages messages = null;
            Random rand = new Random();
            for (int i = 0; i < list.Count; ++i)
            {
                if (list[i] is Messages)
                {
                    messages = (Messages)list[i];
                    _list.Add(new ItemShow("Assets/" + rand.Next(0, 30) + ".png", messages.Title,
                        messages.Subtitle,messages.Teacher,messages.TextBook,
                        DateTime.Parse(messages.Start), DateTime.Parse(messages.End)));
                }

            }
            this.ShowItem_ListView.ItemsSource = _list;
        }

        private async void DeleteButton_Clcik(object sender, RoutedEventArgs e)
        {
            string Week = weekTrans(this.Week_ComboBox.SelectionBoxItem as string);
            int index = this.ShowItem_ListView.SelectedIndex;
            if (index == -1)
            {
                MessageDialog dialog = new MessageDialog("您未选择欲删除的课程项！", "温馨提示");
                await dialog.ShowAsync();
                return;
            }
            List<ItemShow> _list = new List<ItemShow>();

            string sql = "select * From messages where week = '" + Week + "'";
            List<object> list = App.db.Query(new TableMapping(typeof(Messages)), sql);
            Messages messages = null;
            messages = list[index] as Messages;

            {

            }

            App.db.Delete(messages);
            list.RemoveAt(index);
            Random rand = new Random();
            for (int i = 0; i < list.Count; ++i)
            {
                if (list[i] is Messages)
                {
                    messages = (Messages)list[i];
                    _list.Add(new ItemShow("Assets/" + rand.Next(0, 30) + ".png", messages.Title,
                        messages.Subtitle, messages.Teacher, messages.TextBook,
                        DateTime.Parse(messages.Start), DateTime.Parse(messages.End)));
                }

            }

            this.ShowItem_ListView.ItemsSource = _list;
        }

        private void AppBarAddButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(UpdateDataPage));
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
