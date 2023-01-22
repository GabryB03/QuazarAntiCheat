using System;
using System.Security.Cryptography;
using System.Text;

public class RandomUtils
{
    internal static readonly char[] chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
    internal static readonly char[] numbers = "123456789".ToCharArray();
    public static Random rand = new Random();

    public static string RandomNormalString(int size)
    {
        byte[] data = new byte[4 * size];

        using (RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider())
        {
            crypto.GetBytes(data);
        }

        StringBuilder result = new StringBuilder(size);

        for (int i = 0; i < size; i++)
        {
            var rnd = BitConverter.ToUInt32(data, i * 4);
            var idx = rnd % chars.Length;

            result.Append(chars[idx]);
        }

        return result.ToString();
    }

    public static int GetRandomNumber(int cap)
    {
        return rand.Next(0, cap);
    }

    public static int GetRandomNumber(int min, int cap)
    {
        return rand.Next(min, cap);
    }
}