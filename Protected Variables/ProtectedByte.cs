using System;
public partial class ProtectedByte
{
    private byte fakeValue1 = 0;
    private decimal protectedValue = 0M;
    private byte fakeValue2 = 0;
    private int encryptionKey = 0;
    private Random rnd = new Random();
    private byte fakeValue3 = 0;
    public ProtectedByte(byte valueToProtect)
    {
        encryptionKey = Convert.ToString(valueToProtect).Length + RandomUtils.GetRandomNumber(2, 5);
        fakeValue1 = valueToProtect;
        fakeValue2 = valueToProtect;
        fakeValue3 = valueToProtect;
        protectedValue = ProtectByte(valueToProtect);
    }
    private decimal ProtectByte(byte valueToProtect)
    {
        return (decimal)(valueToProtect * 3 - 50 * encryptionKey);
    }
    private byte UnprotectByte(decimal valueToUnprotect)
    {
        decimal coso = ((decimal)valueToUnprotect + (decimal)50.0 * (decimal)encryptionKey) / 3M;
        return byte.Parse(coso.ToString());
    }
    public byte GetValue()
    {
        return UnprotectByte(protectedValue);
    }
    public void SetValue(byte valueToProtect)
    {
        fakeValue1 = valueToProtect;
        fakeValue2 = valueToProtect;
        fakeValue3 = valueToProtect;
        protectedValue = ProtectByte(valueToProtect);
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