namespace MadnessEngineTools.IO;

public interface IVehicleReader
{
    VehicleChassis ReadVehicleChassis(string cdfbinPath);
}
