#region

using System;
using Windows.System;
using Windows.UI.ApplicationSettings;

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