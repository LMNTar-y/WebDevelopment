using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace WebDevelopment.HostClient.Security
{
    public static class EmailPasswordDecryptor
    {
        public const string EncryptKey = "SuperSecretSecurityKey";
        public static string DecryptCipherTextToPlainText(string cipherText, string securityKey)
        {
            var toEncryptArray = Convert.FromBase64String(cipherText);
            var objMd5CryptoService = MD5.Create();
            var securityKeyArray = objMd5CryptoService.ComputeHash(Encoding.UTF8.GetBytes(securityKey));
            objMd5CryptoService.Clear();

            var objTripleDesCryptoService = Aes.Create();
            objTripleDesCryptoService.Key = securityKeyArray;
            objTripleDesCryptoService.Mode = CipherMode.ECB;
            objTripleDesCryptoService.Padding = PaddingMode.PKCS7;

            var objCryptoTransform = objTripleDesCryptoService.CreateDecryptor();
            var resultArray = objCryptoTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            objTripleDesCryptoService.Clear();

            return Encoding.UTF8.GetString(resultArray);
        }
    }
}
