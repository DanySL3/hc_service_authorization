using Domain.Interfaces;
using System.Text.Json;
using System.Text.RegularExpressions;
using Novell.Directory.Ldap;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Reflection;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.Extensions.Caching.Memory;
using System.Security.Cryptography;
using SkiaSharp;
using System.DirectoryServices;
using Domain.Entities.Autenticacion;

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
