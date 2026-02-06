using Domain.Interfaces;
using Microsoft.Extensions.Configuration;


namespace Domain.Commons
{
    public static class ConnectionCommon
    {
        private static ICryptoCommon crypto = new CryptoCommon();

        public static string CreateSqlConnection(string cCadena)
        {
            try
            {
                cCadena = cCadena.Replace(" ", "").Trim();
                string cStringCrypted = "";
                string cLastString = ";";
                int nFirstPosition = 0;
                int nLastPosition = 0;
                List<string> lstConeccion = new List<string>
                    {
                        "Username=",
                        "Password="
                    };
                foreach (var item in lstConeccion)
                {
                    cStringCrypted = cCadena;
                    nFirstPosition = cCadena.IndexOf(item) + item.Length;
                    cStringCrypted = cStringCrypted.Remove(0, nFirstPosition);
                    nLastPosition = nFirstPosition + cStringCrypted.IndexOf(cLastString);
                    cStringCrypted = cCadena.Substring(nFirstPosition, nLastPosition - nFirstPosition);
                    cCadena = cCadena.Replace(item + cStringCrypted, item + crypto.decryptString(cStringCrypted));
                }
                return cCadena;
            }
            catch
            {
                return "";
            }
        }

    }
}
