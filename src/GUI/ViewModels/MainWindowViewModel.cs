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
    private readonly IVehicleIO vehicleIO;

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

    private string? path;

    public MainWindowViewModel(IVehicleIO vehicleIO)
    {
        this.vehicleIO = vehicleIO;
        OpenCommand = ReactiveCommand.Create(Open);
        SaveCommand = ReactiveCommand.Create(Save);
    }

    public async void Open()
    {
        path = await ChooseFile.Handle(Unit.Default);
        if (path is not null)
        {
            var vehicleChassis = vehicleIO.ReadVehicleChassis(path);
            Mass = vehicleChassis.Mass?.ToString();
            BodyDragBase = vehicleChassis.BodyDragBase?.ToString();
            GeneralTorqueMult = vehicleChassis.GeneralTorqueMult?.ToString();
            GeneralPowerMult = vehicleChassis.GeneralPowerMult?.ToString();
            MaxForceAtSteeringRack = vehicleChassis.MaxForceAtSteeringRack?.ToString();
            PneumaticTrail = vehicleChassis.PneumaticTrail?.ToString();
            PneumaticTrailGripFractPower = vehicleChassis.PneumaticTrailGripFractPower?.ToString();
        }
    }

    public void Save()
    {
        if (path is not null)
        {
            var vehicleChassis = new VehicleChassis();
            if (!String.IsNullOrEmpty(Mass))
            {
                vehicleChassis.Mass = Int32.Parse(Mass);
            }
            if (!String.IsNullOrEmpty(BodyDragBase))
            {
                vehicleChassis.BodyDragBase = Single.Parse(BodyDragBase);
            }
            if (!String.IsNullOrEmpty(GeneralTorqueMult))
            {
                vehicleChassis.GeneralTorqueMult = Single.Parse(GeneralTorqueMult);
            }
            if (!String.IsNullOrEmpty(GeneralPowerMult))
            {
                vehicleChassis.GeneralPowerMult = Single.Parse(GeneralPowerMult);
            }
            if (!String.IsNullOrEmpty(MaxForceAtSteeringRack))
            {
                vehicleChassis.MaxForceAtSteeringRack = Int32.Parse(MaxForceAtSteeringRack);
            }
            if (!String.IsNullOrEmpty(PneumaticTrail))
            {
                vehicleChassis.PneumaticTrail = Single.Parse(PneumaticTrail);
            }
            if (!String.IsNullOrEmpty(PneumaticTrailGripFractPower))
            {
                vehicleChassis.PneumaticTrailGripFractPower = Single.Parse(PneumaticTrailGripFractPower);
            }
            vehicleIO.WriteVehicleChassis(path, vehicleChassis);
        }
    }
}
