using System;
using System.Text;
public partial class ProtectedString
{
    private string protectedValue = "";
    private string encryptionKey = "";
    private string fakeValue1 = "";
    private string fakeValue2 = "";
    private string fakeValue3 = "";

    public ProtectedString(string valueToProtect)
    {
        encryptionKey = RandomUtils.RandomNormalString(5);
        fakeValue1 = valueToProtect;
        fakeValue2 = valueToProtect;
        fakeValue3 = valueToProtect;
        protectedValue = ProtectString(valueToProtect);
    }

    private string ProtectString(string valueToProtect)
    {
        return Reverse(AES_Encrypt(Reverse(valueToProtect), encryptionKey));
    }

    private string UnprotectString(string valueToUnprotect)
    {
        return Reverse(AES_Decrypt(Reverse(valueToUnprotect), encryptionKey));
    }

    private string Reverse(string value)
    {
        var arr = value.ToCharArray();
        Array.Reverse(arr);
        return new string(arr);
    }

    public string GetValue()
    {
        return UnprotectString(protectedValue);
    }

    public void SetValue(string valueToProtect)
    {
        protectedValue = ProtectString(valueToProtect);
    }

    public void Dispose()
    {
        protectedValue = null;
        encryptionKey = null;
        GC.SuppressFinalize(protectedValue);
        GC.SuppressFinalize(encryptionKey);
        GC.Collect();
    }

    private string AES_Encrypt(string input, string pass)
    {
        var AES = new System.Security.Cryptography.RijndaelManaged();
        var Hash_AES = new System.Security.Cryptography.MD5CryptoServiceProvider();

        try
        {
            var hash = new byte[32];
            var temp = Hash_AES.ComputeHash(Encoding.ASCII.GetBytes(pass));
            Array.Copy(temp, 0, hash, 0, 16);
            Array.Copy(temp, 0, hash, 15, 16);
            AES.Key = hash;
            AES.Mode = System.Security.Cryptography.CipherMode.ECB;
            var DESEncrypter = AES.CreateEncryptor();
            var Buffer = Encoding.ASCII.GetBytes(input);
            return Convert.ToBase64String(DESEncrypter.TransformFinalBlock(Buffer, 0, Buffer.Length));
        }
        catch
        {

        }

        return "";
    }

    private string AES_Decrypt(string input, string pass)
    {
        var AES = new System.Security.Cryptography.RijndaelManaged();
        var Hash_AES = new System.Security.Cryptography.MD5CryptoServiceProvider();
        try
        {
            var hash = new byte[32];
            var temp = Hash_AES.ComputeHash(Encoding.ASCII.GetBytes(pass));
            Array.Copy(temp, 0, hash, 0, 16);
            Array.Copy(temp, 0, hash, 15, 16);
            AES.Key = hash;
            AES.Mode = System.Security.Cryptography.CipherMode.ECB;
            var DESDecrypter = AES.CreateDecryptor();
            var Buffer = Convert.FromBase64String(input);
            return Encoding.ASCII.GetString(DESDecrypter.TransformFinalBlock(Buffer, 0, Buffer.Length));
        }
        catch
        {

        }

        return "";
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