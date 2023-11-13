namespace MadnessEngineTools.IO;

public interface IVehicleIO
{
    VehicleChassis ReadVehicleChassis(string cdfbinPath);
    void WriteVehicleChassis(string cdfbinPath, VehicleChassis vehicleChassis);
}
