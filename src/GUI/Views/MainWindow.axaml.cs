using System.Threading.Tasks;
using MEME.GUI.ViewModels;
using Avalonia.Platform.Storage;
using ReactiveUI;

namespace MEME.GUI.Views;

public partial class MainWindow : ReactiveWindow<MainWindowViewModel>
{
    public MainWindow()
    {
        this.WhenActivated(d =>
        {
            d(ViewModel!.ChooseFile.RegisterHandler(async interaction =>
            {
                var path = await ChooseFileHandler();
                interaction.SetOutput(path);
            }));
        });
        InitializeComponent();
    }

    private async Task<string?> ChooseFileHandler()
    {
        var files = await StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
        {
            Title = "Open Chassis File",
            AllowMultiple = false,
            FileTypeFilter = new[] { new FilePickerFileType("cdfbin")             {
                Patterns = new[] { "*.cdfbin" }
            }}
        });
        return files?.Select(_ => _.TryGetLocalPath()).FirstOrDefault();
    }
}
