using System;
public partial class ProtectedInteger
{
    private int fakeValue1 = 0;
    private decimal protectedValue = 0M;
    private int fakeValue2 = 0;
    private int encryptionKey = 0;
    private Random rnd = new Random();
    private int fakeValue3 = 0;
    public ProtectedInteger(int valueToProtect)
    {
        encryptionKey = valueToProtect.ToString().Length + RandomUtils.GetRandomNumber(2, 5);
        fakeValue1 = valueToProtect;
        fakeValue2 = valueToProtect;
        fakeValue3 = valueToProtect;
        protectedValue = ProtectInteger(valueToProtect);
    }
    private decimal ProtectInteger(int valueToProtect)
    {
        return valueToProtect * 3 - 50 * encryptionKey;
    }
    private int UnprotectInteger(decimal valueToUnprotect)
    {
        decimal coso = (valueToUnprotect + 50 * encryptionKey) / 3M;
        return int.Parse(coso.ToString());
    }
    public int GetValue()
    {
        return UnprotectInteger(protectedValue);
    }
    public void SetValue(int valueToProtect)
    {
        fakeValue1 = valueToProtect;
        fakeValue2 = valueToProtect;
        fakeValue3 = valueToProtect;
        protectedValue = ProtectInteger(valueToProtect);
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