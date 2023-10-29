namespace MadnessEngineTools.IO;

public class GameFormatIO : IVehicleReader
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
        var massValueIndex = keyIndex + key.Length;
        var valueBytes = contents.AsSpan(massValueIndex, sizeof(int));
        return BitConverter.ToInt32(valueBytes);
    }

    private static float? ReadF32(byte[] contents, byte[] key)
    {
        var keyIndex = contents.AsSpan().IndexOf(key);
        if (keyIndex < 0)
        {
            return null;
        }
        var massValueIndex = keyIndex + key.Length;
        var valueBytes = contents.AsSpan(massValueIndex, sizeof(float));
        return BitConverter.ToSingle(valueBytes);
    }
}