using System.Security.Cryptography;
using System.IO;

namespace ProvisioningPrototype.Utility
{
    // Password Encryption and Decryption utility
    public class PasswordUtill
    {
        public static string Encrypt(string Password)
        {
            byte[] data = System.Text.ASCIIEncoding.ASCII.GetBytes(Password);
            byte[] rgbKey = System.Text.ASCIIEncoding.ASCII.GetBytes("56565656");
            byte[] rgbIV = System.Text.ASCIIEncoding.ASCII.GetBytes("78787878");

            //1024-bit encryption
            MemoryStream memoryStream = new MemoryStream(1024);
            DESCryptoServiceProvider desCryptoServiceProvider =
            new DESCryptoServiceProvider();

            CryptoStream cryptoStream = new CryptoStream(memoryStream,
            desCryptoServiceProvider.CreateEncryptor(rgbKey, rgbIV),
            CryptoStreamMode.Write);

            cryptoStream.Write(data, 0, data.Length);
            cryptoStream.FlushFinalBlock();

            byte[] result = new byte[(int)memoryStream.Position];
            memoryStream.Position = 0;
            memoryStream.Read(result, 0, result.Length);

            cryptoStream.Close();

            string toDecrypt = System.Convert.ToBase64String(result);

            //DecryptIt(toDecrypt);
            return toDecrypt;
        }
        public static string Decrypt(string toDecrypt)
        {
            byte[] data = System.Convert.FromBase64String(toDecrypt);
            byte[] rgbKey = System.Text.ASCIIEncoding.ASCII.GetBytes("56565656");
            byte[] rgbIV = System.Text.ASCIIEncoding.ASCII.GetBytes("78787878");

            MemoryStream memoryStream = new MemoryStream(data.Length);

            DESCryptoServiceProvider desCryptoServiceProvider =
            new DESCryptoServiceProvider();

            CryptoStream cryptoStream = new CryptoStream(memoryStream,
            desCryptoServiceProvider.CreateDecryptor(rgbKey, rgbIV),
            CryptoStreamMode.Read);

            memoryStream.Write(data, 0, data.Length);
            memoryStream.Position = 0;

            string decrypted = new StreamReader(cryptoStream).ReadToEnd();

            cryptoStream.Close();
            return decrypted;
        }
    }
}