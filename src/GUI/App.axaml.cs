using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;

using AME.GUI.ViewModels;
using AME.GUI.Views;
using MadnessEngineTools.IO;

namespace AME.GUI;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        var vehicleReader = new GameFormatIO();

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                DataContext = new MainWindowViewModel(vehicleReader)
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
}
