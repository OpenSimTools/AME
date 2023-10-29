using MadnessEngineTools;
using MadnessEngineTools.IO;
using ReactiveUI;
using System.Reactive;
using System.Reactive.Linq;

namespace AME.GUI.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    #region Interactions

    public Interaction<Unit, string?> ChooseFile { get; } = new();

    #endregion

    #region Properties

    private string? mass;
    public string? Mass
    {
        get => mass;
        set => this.RaiseAndSetIfChanged(ref mass, value);
    }

    private string? bodyDragBase;
    public string? BodyDragBase
    {
        get => bodyDragBase;
        set => this.RaiseAndSetIfChanged(ref bodyDragBase, value);
    }

    private string? generalTorqueMult;
    public string? GeneralTorqueMult
    {
        get => generalTorqueMult;
        set => this.RaiseAndSetIfChanged(ref generalTorqueMult, value);
    }

    private string? generalPowerMult;
    public string? GeneralPowerMult
    {
        get => generalPowerMult;
        set => this.RaiseAndSetIfChanged(ref generalPowerMult, value);
    }

    private string? maxForceAtSteeringRack;
    public string? MaxForceAtSteeringRack
    {
        get => maxForceAtSteeringRack;
        set => this.RaiseAndSetIfChanged(ref maxForceAtSteeringRack, value);
    }

    private string? pneumaticTrail;
    public string? PneumaticTrail
    {
        get => pneumaticTrail;
        set => this.RaiseAndSetIfChanged(ref pneumaticTrail, value);
    }

    private string? pneumaticTrailGripFractPower;
    private readonly IVehicleReader vehicleReader;

    public string? PneumaticTrailGripFractPower
    {
        get => pneumaticTrailGripFractPower;
        set => this.RaiseAndSetIfChanged(ref pneumaticTrailGripFractPower, value);
    }

    #endregion

    #region Commands

    public ReactiveCommand<Unit, Unit> OpenCommand { get; }
    public ReactiveCommand<Unit, Unit> SaveCommand { get; }

    #endregion

    public MainWindowViewModel(IVehicleReader vehicleReader)
    {
        this.vehicleReader = vehicleReader;
        OpenCommand = ReactiveCommand.Create(Open);
        SaveCommand = ReactiveCommand.Create(Save);
    }

    public async void Open()
    {
        var path = await ChooseFile.Handle(Unit.Default);
        System.Diagnostics.Debug.WriteLine($"Open Command {path}");
        var vehicleChassis = vehicleReader.ReadVehicleChassis(path);
        ReadModel(vehicleChassis);
    }

    private void ReadModel(VehicleChassis vehicleChassis)
    {
        Mass = vehicleChassis.Mass?.Kilograms.ToString();
        BodyDragBase = vehicleChassis.BodyDragBase?.ToString();
        GeneralTorqueMult = vehicleChassis.GeneralTorqueMult?.ToString();
        GeneralPowerMult = vehicleChassis.GeneralPowerMult?.ToString();
        MaxForceAtSteeringRack = vehicleChassis.MaxForceAtSteeringRack?.ToString();
        PneumaticTrail = vehicleChassis.PneumaticTrail?.ToString();
        PneumaticTrailGripFractPower = vehicleChassis.PneumaticTrailGripFractPower?.ToString();
    }

    public void Save()
    {
        System.Diagnostics.Debug.WriteLine($"Save Command");
    }
}
