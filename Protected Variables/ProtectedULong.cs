using System;
public partial class ProtectedULong
{
    private ulong fakeValue1 = 0;
    private decimal protectedValue = 0M;
    private ulong fakeValue2 = 0;
    private int encryptionKey = 0;
    private Random rnd = new Random();
    private ulong fakeValue3 = 0;
    public ProtectedULong(ulong valueToProtect)
    {
        encryptionKey = valueToProtect.ToString().Length + RandomUtils.GetRandomNumber(2, 5);
        fakeValue1 = valueToProtect;
        fakeValue2 = valueToProtect;
        fakeValue3 = valueToProtect;
        protectedValue = ProtectULong(valueToProtect);
    }
    private decimal ProtectULong(ulong valueToProtect)
    {
        return (decimal)(valueToProtect * 3 - 50 * (ulong)encryptionKey);
    }
    private ulong UnprotectULong(decimal valueToUnprotect)
    {
        decimal coso = ((decimal)valueToUnprotect + (decimal)50.0 * (decimal)encryptionKey) / 3M;
        return ulong.Parse(coso.ToString());
    }
    public ulong GetValue()
    {
        return UnprotectULong(protectedValue);
    }
    public void SetValue(ulong valueToProtect)
    {
        fakeValue1 = valueToProtect;
        fakeValue2 = valueToProtect;
        fakeValue3 = valueToProtect;
        protectedValue = ProtectULong(valueToProtect);
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