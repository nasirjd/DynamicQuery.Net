using System;

namespace DynamicQuery.Net.Test
{
    public static class TestHelpers
    {
        public static string GetRandomChars(int length)
        {
            const string chars =
                "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()_+-=[]{}|;:',.<>/?";
            var random = new Random();
            char[] result = new char[length];
            for (int i = 0; i < length; i++) 
                result[i] = chars[random.Next(chars.Length)];

            return new string(result);
        }
    }
}