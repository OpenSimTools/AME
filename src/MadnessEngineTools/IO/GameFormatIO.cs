using UnitsNet;

namespace MadnessEngineTools.IO;

public class GameFormatIO : IVehicleReader
{
    public VehicleChassis ReadVehicleChassis(string cdfbinPath)
    {
        return new VehicleChassis
        {
            Mass = Mass.FromKilograms(1),
            BodyDragBase = 2f,
            GeneralTorqueMult = 3f,
            GeneralPowerMult = 4f,
            MaxForceAtSteeringRack = 5,
            PneumaticTrail = 6f,
            PneumaticTrailGripFractPower = 7f
        };
    }
}
