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

    private int? mass;
    public int? Mass
    {
        get => mass;
        set => this.RaiseAndSetIfChanged(ref mass, value);
    }

    private float? bodyDragBase;
    public float? BodyDragBase
    {
        get => bodyDragBase;
        set => this.RaiseAndSetIfChanged(ref bodyDragBase, value);
    }

    private float? generalTorqueMult;
    public float? GeneralTorqueMult
    {
        get => generalTorqueMult;
        set => this.RaiseAndSetIfChanged(ref generalTorqueMult, value);
    }

    private float? generalPowerMult;
    public float? GeneralPowerMult
    {
        get => generalPowerMult;
        set => this.RaiseAndSetIfChanged(ref generalPowerMult, value);
    }

    private int? maxForceAtSteeringRack;
    public int? MaxForceAtSteeringRack
    {
        get => maxForceAtSteeringRack;
        set => this.RaiseAndSetIfChanged(ref maxForceAtSteeringRack, value);
    }

    private float? pneumaticTrail;
    public float? PneumaticTrail
    {
        get => pneumaticTrail;
        set => this.RaiseAndSetIfChanged(ref pneumaticTrail, value);
    }

    private float? pneumaticTrailGripFractPower;
    public float? PneumaticTrailGripFractPower
    {
        get => pneumaticTrailGripFractPower;
        set => this.RaiseAndSetIfChanged(ref pneumaticTrailGripFractPower, value);
    }

    #endregion

    #region Commands

    public ReactiveCommand<Unit, Unit> OpenCommand { get; }
    public ReactiveCommand<Unit, Unit> SaveCommand { get; }

    #endregion

    private readonly IVehicleIO vehicleIO;
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
            Mass = vehicleChassis.Mass;
            BodyDragBase = vehicleChassis.BodyDragBase;
            GeneralTorqueMult = vehicleChassis.GeneralTorqueMult;
            GeneralPowerMult = vehicleChassis.GeneralPowerMult;
            MaxForceAtSteeringRack = vehicleChassis.MaxForceAtSteeringRack;
            PneumaticTrail = vehicleChassis.PneumaticTrail;
            PneumaticTrailGripFractPower = vehicleChassis.PneumaticTrailGripFractPower;
        }
    }

    public void Save()
    {
        if (path is not null)
        {
            var vehicleChassis = new VehicleChassis();
            vehicleChassis.Mass = Mass;
            vehicleChassis.BodyDragBase = BodyDragBase;
            vehicleChassis.GeneralTorqueMult = GeneralTorqueMult;
            vehicleChassis.GeneralPowerMult = GeneralPowerMult;
            vehicleChassis.MaxForceAtSteeringRack = MaxForceAtSteeringRack;
            vehicleChassis.PneumaticTrail = PneumaticTrail;
            vehicleChassis.PneumaticTrailGripFractPower = PneumaticTrailGripFractPower;
            vehicleIO.WriteVehicleChassis(path, vehicleChassis);
        }
    }
}
