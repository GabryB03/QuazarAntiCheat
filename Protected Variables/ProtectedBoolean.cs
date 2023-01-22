using System;
public class ProtectedBoolean
{
    public int fakeValue1, fakeValue2, fakeValue3, encryptionKey;
    private Random rnd = new Random();
    public ProtectedBoolean(bool valueToProtect)
    {
        encryptionKey = RandomUtils.GetRandomNumber(2, 5);
        SetValue(valueToProtect);
    }
    public bool GetValue()
    {
        if ((fakeValue1 >= 573 * encryptionKey && fakeValue1 <= 9831 * encryptionKey) && (fakeValue2 >= 73261 * encryptionKey && fakeValue2 <= 121573 * encryptionKey) && (fakeValue3 >= 375132 * encryptionKey && fakeValue3 <= 891711 * encryptionKey))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void SetValue(bool valueToProtect)
    {
        if (valueToProtect)
        {
            fakeValue1 = RandomUtils.GetRandomNumber(573 * encryptionKey, 9831 * encryptionKey);
            fakeValue2 = RandomUtils.GetRandomNumber(73261 * encryptionKey, 121573 * encryptionKey);
            fakeValue3 = RandomUtils.GetRandomNumber(375132 * encryptionKey, 891711 * encryptionKey);
        }
        else
        {
            fakeValue1 = RandomUtils.GetRandomNumber(73 * encryptionKey, 371 * encryptionKey);
            fakeValue2 = RandomUtils.GetRandomNumber(12731 * encryptionKey, 56311 * encryptionKey);
            fakeValue3 = RandomUtils.GetRandomNumber(67421 * encryptionKey, 72000 * encryptionKey);
        }
    }
    public void Dispose()
    {
        fakeValue1 = default;
        fakeValue2 = default;
        fakeValue3 = default;
        rnd = null;
        GC.SuppressFinalize(fakeValue1);
        GC.SuppressFinalize(fakeValue2);
        GC.SuppressFinalize(fakeValue3);
        GC.SuppressFinalize(rnd);
        GC.Collect();
    }
    public bool isViolated()
    {
        bool realValue = GetValue();
        if (realValue)
        {
            if (!(fakeValue1 >= 573 * encryptionKey) || !(fakeValue1 <= 9831 * encryptionKey) || !(fakeValue2 >= 73261 * encryptionKey) || !(fakeValue2 <= 121573 * encryptionKey) || !(fakeValue3 >= 375132 * encryptionKey) || !(fakeValue3 <= 891711 * encryptionKey))
            {
                return true;
            }
        }
        else
        {
            if (!(fakeValue1 >= 73 * encryptionKey) || !(fakeValue1 <= 371 * encryptionKey) || !(fakeValue2 >= 12731 * encryptionKey) || !(fakeValue2 <= 56311 * encryptionKey) || !(fakeValue3 >= 67421 * encryptionKey) || !(fakeValue3 <= 72000 * encryptionKey))
            {
                return true;
            }
        }
        return false;
    }
}