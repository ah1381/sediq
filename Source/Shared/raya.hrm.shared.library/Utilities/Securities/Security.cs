using Microsoft.IdentityModel.Tokens;
using Raya.Hrm.Shared.Library.Enums;
using Raya.Hrm.Shared.Library.Models.Auth;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Raya.Hrm.Shared.Library.Utilities.Securities
{
    public class Security
    {
        private static readonly string Key = "1234567890RayaALRayaRayaRayaRaya"; // 32 characters for AES-256
        private static readonly string IV = "qazwsxedcrfvtg1Q"; // 16 characters IV

        /// <summary>
        /// Encrypts a password using AES encryption.
        /// </summary>
        public static string Encrypt(string plainText)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(Key);
                aes.IV = Encoding.UTF8.GetBytes(IV);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream(
                               memoryStream, aes.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter(cryptoStream))
                        {
                            streamWriter.Write(plainText);
                        }
                    }

                    return Convert.ToBase64String(memoryStream.ToArray());
                }
            }
        }

        /// <summary>
        /// Decrypts an AES-encrypted password.
        /// </summary>
        public static string Decrypt(string encryptedText)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(Key);
                aes.IV = Encoding.UTF8.GetBytes(IV);

                using (MemoryStream memoryStream = new MemoryStream(Convert.FromBase64String(encryptedText)))
                {
                    using (CryptoStream cryptoStream = new CryptoStream(
                               memoryStream, aes.CreateDecryptor(), CryptoStreamMode.Read))
                    {
                        using (StreamReader streamReader = new StreamReader(cryptoStream))
                        {
                            return streamReader.ReadToEnd();
                        }
                    }
                }
            }
        }


        /// <summary>
        /// jwt
        /// </summary>

        public static string GenerateJSONWebToken(LoginRequestModel userInfo, JwtConfig config, UserType userType)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userInfo.Username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role, userType.ToString()) // Role from enum
            };

            var token = new JwtSecurityToken(
                config.Issuer[0],
                config.Audience[0],
                claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
