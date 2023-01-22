using System;
public partial class ProtectedUInt
{
    private uint fakeValue1 = 0;
    private decimal protectedValue = 0M;
    private uint fakeValue2 = 0;
    private int encryptionKey = 0;
    private Random rnd = new Random();
    private uint fakeValue3 = 0;
    public ProtectedUInt(uint valueToProtect)
    {
        encryptionKey = valueToProtect.ToString().Length + RandomUtils.GetRandomNumber(2, 5);
        fakeValue1 = valueToProtect;
        fakeValue2 = valueToProtect;
        fakeValue3 = valueToProtect;
        protectedValue = ProtectUInt(valueToProtect);
    }
    private decimal ProtectUInt(uint valueToProtect)
    {
        return (decimal)(valueToProtect * 3 - 50 * encryptionKey);
    }
    private uint UnprotectUInt(decimal valueToUnprotect)
    {
        decimal coso = ((decimal)valueToUnprotect + (decimal)50.0 * (decimal)encryptionKey) / 3M;
        return uint.Parse(coso.ToString());
    }
    public uint GetValue()
    {
        return UnprotectUInt(protectedValue);
    }
    public void SetValue(uint valueToProtect)
    {
        fakeValue1 = valueToProtect;
        fakeValue2 = valueToProtect;
        fakeValue3 = valueToProtect;
        protectedValue = ProtectUInt(valueToProtect);
    }
    public void Dispose()
    {
        protectedValue = default;
        encryptionKey = default;
        rnd = null;
        GC.SuppressFinalize(protectedValue);
        GC.SuppressFinalize(encryptionKey);
        GC.SuppressFinalize(rnd);
        GC.Collect();
    }
    public bool IsViolated()
    {
        if (fakeValue1 != GetValue() || fakeValue2 != GetValue() || fakeValue3 != GetValue())
        {
            return true;
        }
        return false;
    }
}