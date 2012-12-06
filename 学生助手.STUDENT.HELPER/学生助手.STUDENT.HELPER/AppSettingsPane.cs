#region

using System;
using Windows.System;
using Windows.UI.ApplicationSettings;

using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

#endregion

public class AppSettingsPane
{
    private AppSettingsPane()
    {
        SettingsPane.GetForCurrentView().CommandsRequested += App_CommandsRequested;
    }

    public static void Load()
    {
        new AppSettingsPane();
    }


    private void App_CommandsRequested(SettingsPane sender, SettingsPaneCommandsRequestedEventArgs args)
    {

         var privateCommand = new SettingsCommand("Privacy",
                                                 "隐私声明",
                                                  delegate { Launcher.LaunchUriAsync(new Uri("http://blog.iliyang.cn/", UriKind.RelativeOrAbsolute)); });

          args.Request.ApplicationCommands.Add(privateCommand);
   }
}