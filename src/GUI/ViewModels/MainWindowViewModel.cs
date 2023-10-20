namespace AME.GUI.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    public string? Mass { get; set; }
    public string? BodyDragBase { get; set; }
    public string? GeneralTorqueMult { get; set; }
    public string? GeneralPowerMult { get; set; }
    public string? MaxForceAtSteeringRack { get; set; }
    public string? PneumaticTrail { get; set; }
    public string? PneumaticTrailGripFractPower { get; set; }
}
