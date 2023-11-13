namespace MadnessEngineTools.IO;

public class GameFormatIO : IVehicleIO
{
    private static readonly byte[] MassKey = { 0x21, 0x67, 0x0B, 0x57, 0xAB };
    private static readonly byte[] BodyDragBaseKey = { 0x24, 0x33, 0x63, 0xED, 0xFD, 0x21 };
    private static readonly byte[] GeneralTorqueMultKey = { 0x22, 0xA3, 0xBF, 0x1E, 0x60 };
    private static readonly byte[] GeneralPowerMultKey = { 0x22, 0xC3, 0x66, 0x7D, 0x2E };
    private static readonly byte[] MaxForceAtSteeringRackKey = { 0x21, 0x01, 0xC1, 0x8B, 0x82 };
    private static readonly byte[] PneumaticTrailKey = { 0x22, 0x3E, 0x53, 0xB5, 0xCF };
    private static readonly byte[] PneumaticTrailGripFractPowerKey = { 0x22, 0x00, 0xAB, 0x3E, 0x9B };

    public VehicleChassis ReadVehicleChassis(string cdfbinPath)
    {
        var contents = File.ReadAllBytes(cdfbinPath);
        return new VehicleChassis
        {
            Mass = ReadI32(contents, MassKey),
            BodyDragBase = ReadF32(contents, BodyDragBaseKey),
            GeneralTorqueMult = ReadF32(contents, GeneralTorqueMultKey),
            GeneralPowerMult = ReadF32(contents, GeneralPowerMultKey),
            MaxForceAtSteeringRack = ReadI32(contents, MaxForceAtSteeringRackKey),
            PneumaticTrail = ReadF32(contents, PneumaticTrailKey),
            PneumaticTrailGripFractPower = ReadF32(contents, PneumaticTrailGripFractPowerKey)
        };
    }

    private static int? ReadI32(byte[] contents, byte[] key)
    {
        var keyIndex = contents.AsSpan().IndexOf(key);
        if (keyIndex < 0)
        {
            return null;
        }
        var valueIndex = keyIndex + key.Length;
        var valueBytes = contents.AsSpan(valueIndex, sizeof(int));
        return BitConverter.ToInt32(valueBytes);
    }

    private static float? ReadF32(byte[] contents, byte[] key)
    {
        var keyIndex = contents.AsSpan().IndexOf(key);
        if (keyIndex < 0)
        {
            return null;
        }
        var valueIndex = keyIndex + key.Length;
        var valueBytes = contents.AsSpan(valueIndex, sizeof(float));
        return BitConverter.ToSingle(valueBytes);
    }

    public void WriteVehicleChassis(string cdfbinPath, VehicleChassis vehicleChassis)
    {
        var contents = File.ReadAllBytes(cdfbinPath);
        WriteI32(contents, MassKey, vehicleChassis.Mass);
        WriteF32(contents, BodyDragBaseKey, vehicleChassis.BodyDragBase);
        WriteF32(contents, GeneralTorqueMultKey, vehicleChassis.GeneralTorqueMult);
        WriteF32(contents, GeneralPowerMultKey, vehicleChassis.GeneralPowerMult);
        WriteI32(contents, MaxForceAtSteeringRackKey, vehicleChassis.MaxForceAtSteeringRack);
        WriteF32(contents, PneumaticTrailKey, vehicleChassis.PneumaticTrail);
        WriteF32(contents, PneumaticTrailGripFractPowerKey, vehicleChassis.PneumaticTrailGripFractPower);
        File.WriteAllBytes(cdfbinPath, contents);
    }

    private static void WriteI32(byte[] contents, byte[] key, int? value)
    {
        var keyIndex = contents.AsSpan().IndexOf(key);
        if (keyIndex < 0)
        {
            if (value is not null)
            {
                throw new NotImplementedException("Adding a value is not supported");
            }
            return;
        }
        if (value is null)
        {
            throw new NotImplementedException("Removing a value is not supported");
        }
        var valueIndex = keyIndex + key.Length;
        var valueBytes = BitConverter.GetBytes((int)value);
        valueBytes.CopyTo(contents, valueIndex);
    }

    private static void WriteF32(byte[] contents, byte[] key, float? value)
    {
        var keyIndex = contents.AsSpan().IndexOf(key);
        if (keyIndex < 0)
        {
            if (value is not null)
            {
                throw new NotImplementedException("Adding a value is not supported");
            }
            return;
        }
        if (value is null)
        {
            throw new NotImplementedException("Removing a value is not supported");
        }
        var valueIndex = keyIndex + key.Length;
        var valueBytes = BitConverter.GetBytes((float)value);
        valueBytes.CopyTo(contents, valueIndex);
    }
}