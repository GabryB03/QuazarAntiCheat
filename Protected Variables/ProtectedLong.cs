using System;
public partial class ProtectedLong
{
    private long fakeValue1 = 0L;
    private decimal protectedValue = 0M;
    private long fakeValue2 = 0L;
    private int encryptionKey = 0;
    private Random rnd = new Random();
    private long fakeValue3 = 0L;
    public ProtectedLong(long valueToProtect)
    {
        encryptionKey = valueToProtect.ToString().Length + RandomUtils.GetRandomNumber(2, 5);
        fakeValue1 = valueToProtect;
        fakeValue2 = valueToProtect;
        fakeValue3 = valueToProtect;
        protectedValue = ProtectLong(valueToProtect);
    }
    private decimal ProtectLong(long valueToProtect)
    {
        return (decimal)(valueToProtect * 3 - 50 * encryptionKey);
    }
    private long UnprotectLong(decimal valueToUnprotect)
    {
        decimal coso = ((decimal)valueToUnprotect + (decimal)50.0 * (decimal)encryptionKey) / 3M;
        return long.Parse(coso.ToString());
    }
    public long GetValue()
    {
        return UnprotectLong(protectedValue);
    }
    public void SetValue(long valueToProtect)
    {
        fakeValue1 = valueToProtect;
        fakeValue2 = valueToProtect;
        fakeValue3 = valueToProtect;
        protectedValue = ProtectLong(valueToProtect);
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