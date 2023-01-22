using System;
public partial class ProtectedChar
{
    public ProtectedInteger integerLol;
    public ProtectedChar(char valueToProtect)
    {
        integerLol = new ProtectedInteger((int)valueToProtect);
    }
    public char GetValue()
    {
        return (char)integerLol.GetValue();
    }
    public void SetValue(char valueToProtect)
    {
        integerLol.SetValue((int)valueToProtect);
    }
    public void Dispose()
    {
        integerLol.Dispose();
        integerLol = null;
        GC.SuppressFinalize(integerLol);
        GC.Collect();
    }
    public bool isViolated()
    {
        return integerLol.IsViolated();
    }
}