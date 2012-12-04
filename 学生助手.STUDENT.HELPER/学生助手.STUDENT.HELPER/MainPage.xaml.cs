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

using 学生助手.STUDENT.HELPER.Data;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上有介绍

namespace 学生助手.STUDENT.HELPER
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : 学生助手.STUDENT.HELPER.Common.LayoutAwarePage
    {
        public MainPage()
        {
            this.InitializeComponent();
            AppSettingsPane.Load(); 
        }

        protected override void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
        {
            var sampleDataGroups = SampleDataSource.GetGroups("AllGroups");
            this.DefaultViewModel["Items"] = sampleDataGroups;
        }

        private void ItemView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var groupId = ((SampleDataGroup)e.ClickedItem).UniqueId;
            this.Frame.Navigate(typeof(SplitPage), groupId);
        }

        private void AppBarEditButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(UpdateDataPage));
        }
    }
}
