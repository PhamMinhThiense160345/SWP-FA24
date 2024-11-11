using System.Security.Cryptography;
using System.Text;
using Vegetarians_Assistant.API.Helpers.AesEncryption;

namespace Vegetarians_Assistant.API.Helpers.Encryption;

public class EncryptionHelper : IEncryptionHelper
{
    public string Decrypt(string cipherText, string key)
    {
        using Aes aesAlg = Aes.Create();
        aesAlg.Key = Convert.FromBase64String(key);
        aesAlg.IV = Convert.FromBase64String("5RJdUwezWL23+MFBqPQ/sg==");

        using ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
        using MemoryStream msDecrypt = new(Convert.FromBase64String(cipherText));
        using CryptoStream csDecrypt = new(msDecrypt, decryptor, CryptoStreamMode.Read);
        using StreamReader srDecrypt = new(csDecrypt);
        return srDecrypt.ReadToEnd();
    }
}
