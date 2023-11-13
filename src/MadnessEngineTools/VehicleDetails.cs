namespace MadnessEngineTools;

public class VehicleDetails
{
    public VehiclePhysicsModel? PhysicsModel { get; set; }
}

public record VehiclePhysicsModel(string Name)
{
    public VehicleChassis? Chassis { get; set; }
}

public class VehicleChassis
{
    public int? Mass { get; set; }

    // Body Aero
    public float? BodyDragBase { get; set; }

    // Engine
    public float? GeneralTorqueMult { get; set; }
    public float? GeneralPowerMult { get; set; }

    // Controls
    public int? MaxForceAtSteeringRack { get; set; }
    public float? PneumaticTrail {  get; set; }
    public float? PneumaticTrailGripFractPower { get; set; }
}
