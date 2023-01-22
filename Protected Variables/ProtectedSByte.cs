using System;
public partial class ProtectedSByte
{
    private sbyte fakeValue1 = 0;
    private decimal protectedValue = 0M;
    private sbyte fakeValue2 = 0;
    private int encryptionKey = 0;
    private Random rnd = new Random();
    private sbyte fakeValue3 = 0;
    public ProtectedSByte(sbyte valueToProtect)
    {
        encryptionKey = Convert.ToString(valueToProtect).Length + RandomUtils.GetRandomNumber(2, 5);
        fakeValue1 = valueToProtect;
        fakeValue2 = valueToProtect;
        fakeValue3 = valueToProtect;
        protectedValue = ProtectByte(valueToProtect);
    }
    private decimal ProtectByte(sbyte valueToProtect)
    {
        return (decimal)(valueToProtect * 3 - 50 * encryptionKey);
    }
    private sbyte UnprotectByte(decimal valueToUnprotect)
    {
        decimal coso = ((decimal)valueToUnprotect + (decimal)50.0 * (decimal)encryptionKey) / 3M;
        return sbyte.Parse(coso.ToString());
    }
    public sbyte GetValue()
    {
        return UnprotectByte(protectedValue);
    }
    public void SetValue(sbyte valueToProtect)
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