using UnitsNet;

namespace MadnessEngineTools.IO;

public class GameFormatIO : VehicleReader
{
    public VehicleDetails ReadVechicle(string crdPath)
    {
        var basePath = ExtractBasePath(crdPath);
        var physicsModelName = ReadPhysicsModelName(crdPath);
        return new VehicleDetails {
            PhysicsModel = new VehiclePhysicsModel(Name: physicsModelName) {
                Chassis = ReadVehicleChassis(ChassisPath(basePath, physicsModelName))
            }
        };
    }

    private string ExtractBasePath(string crdPath)
    {
        return "";
    }

    private string ChassisPath(string basePath, string physicsModelId)
    {
        return Path.Combine(basePath, "vehicles", "physics", "chassis", $"{physicsModelId}.cdfbin");
    }

    // Later provide a ReflectionFormatIO<T> to handle CRD-style files
    private string ReadPhysicsModelName(string crdPath)
    {
        return "physics_model_id";
    }

    public VehicleChassis ReadVehicleChassis(string cdfbinPath)
    {
        return new VehicleChassis
        {
            Mass = Mass.FromKilograms(1),
            BodyDragBase = 2.0f,
            GeneralTorqueMult = 3.0f,
            GeneralPowerMult = 4.0f
        };
    }
}
