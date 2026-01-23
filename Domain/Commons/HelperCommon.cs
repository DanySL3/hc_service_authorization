using Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Caching.Memory;

namespace Domain.Commons
{
    public class HelperCommon : IHelperCommon
    {
        private readonly IMemoryCache cache;
        private readonly IConfiguration configuration;

        public HelperCommon(IMemoryCache _cache, IConfiguration _configuration)
        {
            cache = _cache;
            configuration = _configuration;
        }

        public string hashPassword(string cPassword)
        {
            var passwordHashing = BCrypt.Net.BCrypt.HashPassword(cPassword, BCrypt.Net.BCrypt.GenerateSalt());

            return passwordHashing;
        }

        public bool checkPassword(string hashedPassword, string password)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
    }
}
