using System;
public partial class ProtectedDouble
{
    private double fakeValue1 = 0.0D;
    private decimal protectedValue = 0M;
    private double fakeValue2 = 0.0D;
    private double encryptionKey = 0.0D;
    private Random rnd = new Random();
    private double fakeValue3 = 0.0D;
    public ProtectedDouble(double valueToProtect)
    {
        encryptionKey = valueToProtect.ToString().Length + RandomUtils.GetRandomNumber(2, 5);
        fakeValue1 = valueToProtect;
        fakeValue2 = valueToProtect;
        fakeValue3 = valueToProtect;
        protectedValue = ProtectDouble(valueToProtect);
    }
    private decimal ProtectDouble(double valueToProtect)
    {
        return (decimal)(valueToProtect * 3 - 50 * encryptionKey);
    }
    private double UnprotectDouble(decimal valueToUnprotect)
    {
        decimal coso = ((decimal)valueToUnprotect + (decimal)50.0 * (decimal)encryptionKey) / 3M;
        return double.Parse(coso.ToString());
    }
    public double GetValue()
    {
        return UnprotectDouble(protectedValue);
    }
    public void SetValue(double valueToProtect)
    {
        fakeValue1 = valueToProtect;
        fakeValue2 = valueToProtect;
        fakeValue3 = valueToProtect;
        protectedValue = ProtectDouble(valueToProtect);
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