namespace Vegetarians_Assistant.API.Helpers.AesEncryption;

public interface IEncryptionHelper
{

    string Decrypt(string cipherText, string key);
}
