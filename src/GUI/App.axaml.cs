using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;

using MEME.GUI.ViewModels;
using MEME.GUI.Views;
using MadnessEngineTools.IO;

namespace MEME.GUI;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        var vehicleIO = new GameFormatIO();

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                DataContext = new MainWindowViewModel(vehicleIO)
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
}
