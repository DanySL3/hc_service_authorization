using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Domain.Commons
{
    public class CryptoCommon : ICryptoCommon
    {
        private const string cInitVector = "pemgail9uzpgzl88";
        private const int nKeysize = 256;
        private string cPassPhrase;

        public CryptoCommon(string cPass = "AAED4554C235927C03BAD5BC313E81B0FBEB6FEC5BB60E682C70A7D5C8890B20")
        {
            this.cPassPhrase = cPass;
        }


        public string decryptString(string cIpherText)
        {
            byte[] bInitVectorBytes = Encoding.UTF8.GetBytes(cInitVector);
            try
            {
                byte[] bIpherTextBytes = Convert.FromBase64String(cIpherText);

                PasswordDeriveBytes password = new PasswordDeriveBytes(cPassPhrase, null);

                byte[] bKeyBytes = password.GetBytes(nKeysize / 8);

                using var symmetricKey = Aes.Create("AesManaged");

                symmetricKey.Mode = CipherMode.CBC;

                ICryptoTransform decryptor = symmetricKey.CreateDecryptor(bKeyBytes, bInitVectorBytes);

                MemoryStream memoryStream = new MemoryStream(bIpherTextBytes);

                CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);

                byte[] bPlainTextBytes = new byte[bIpherTextBytes.Length];

                int nDecryptedByteCount = cryptoStream.Read(bPlainTextBytes, 0, bPlainTextBytes.Length);

                memoryStream.Close();

                cryptoStream.Close();

                return Encoding.UTF8.GetString(bPlainTextBytes, 0, nDecryptedByteCount).Trim();
            }
            catch
            {
                return "";
            }
        }

    }
}
