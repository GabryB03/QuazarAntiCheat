using System;
public partial class ProtectedFloat
{
    private float fakeValue1 = 0.0F;
    private decimal protectedValue = 0M;
    private float fakeValue2 = 0.0F;
    private float encryptionKey = 0.0F;
    private Random rnd = new Random();
    private float fakeValue3 = 0.0F;
    public ProtectedFloat(float valueToProtect)
    {
        encryptionKey = valueToProtect.ToString().Length + RandomUtils.GetRandomNumber(2, 5);
        fakeValue1 = valueToProtect;
        fakeValue2 = valueToProtect;
        fakeValue3 = valueToProtect;
        protectedValue = ProtectFloat(valueToProtect);
    }
    private decimal ProtectFloat(float valueToProtect)
    {
        return (decimal) (valueToProtect * 3 - 50 * encryptionKey);
    }
    private float UnprotectFloat(decimal valueToUnprotect)
    {
        decimal coso = ((decimal)valueToUnprotect + (decimal)50.0 * (decimal)encryptionKey ) / 3M;
        return float.Parse(coso.ToString());
    }
    public float GetValue()
    {
        return UnprotectFloat(protectedValue);
    }
    public void SetValue(float valueToProtect)
    {
        fakeValue1 = valueToProtect;
        fakeValue2 = valueToProtect;
        fakeValue3 = valueToProtect;
        protectedValue = ProtectFloat(valueToProtect);
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