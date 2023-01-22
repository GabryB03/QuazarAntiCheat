using System;
public partial class ProtectedDecimal
{
    private decimal fakeValue1 = 0.0M;
    private decimal protectedValue = 0M;
    private decimal fakeValue2 = 0.0M;
    private decimal encryptionKey = 0.0M;
    private Random rnd = new Random();
    private decimal fakeValue3 = 0.0M;
    public ProtectedDecimal(decimal valueToProtect)
    {
        encryptionKey = valueToProtect.ToString().Length + RandomUtils.GetRandomNumber(2, 5);
        fakeValue1 = valueToProtect;
        fakeValue2 = valueToProtect;
        fakeValue3 = valueToProtect;
        protectedValue = ProtectDecimal(valueToProtect);
    }
    private decimal ProtectDecimal(decimal valueToProtect)
    {
        return (decimal)(valueToProtect * 3 - 50 * encryptionKey);
    }
    private decimal UnprotectDecimal(decimal valueToUnprotect)
    {
        decimal coso = ((decimal)valueToUnprotect + (decimal)50.0 * (decimal)encryptionKey) / 3M;
        return decimal.Parse(coso.ToString());
    }
    public decimal GetValue()
    {
        return UnprotectDecimal(protectedValue);
    }
    public void SetValue(decimal valueToProtect)
    {
        fakeValue1 = valueToProtect;
        fakeValue2 = valueToProtect;
        fakeValue3 = valueToProtect;
        protectedValue = ProtectDecimal(valueToProtect);
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