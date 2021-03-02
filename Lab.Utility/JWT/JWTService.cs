using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Lab.Utility.JWT;
using Microsoft.IdentityModel.Tokens;

namespace Lab.Utility
{
    /// <summary>
    /// JWT Service Class
    /// </summary>
    public class JWTService
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="secretKey">秘密鍵</param>
        public JWTService(string secretKey)
        {
            this.SecretKey = secretKey;
        }

        /// <summary>
        /// Validate a Token
        /// </summary>
        /// <param name="token">Token</param>
        /// <returns>if the token is valid</returns>
        public bool ValidateToken(string token)
        {
            if (string.IsNullOrEmpty(token)) return false;

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                IssuerSigningKey = GetSymmetricSecurityKey(),
            };

            SecurityToken validatedToken;
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            try
            {
                var tokenValid = jwtSecurityTokenHandler.ValidateToken(token, tokenValidationParameters, out validatedToken);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Generate a Token
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Token</returns>
        public string GenerateToken()
        {
            if (this.Setting == null || this.Claims == null || this.Claims.Length == 0) return string.Empty;

            var securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(this.Claims),
                Expires = DateTime.UtcNow.AddDays(Convert.ToInt32(this.Setting.ExpireDays)),
                SigningCredentials = new SigningCredentials(GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256Signature)
            };

            // Generate a token
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var securityToken = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            var token = jwtSecurityTokenHandler.WriteToken(securityToken);

            return token;
        }

        /// <summary>
        /// Get a Symmetric Key from the Security Key
        /// </summary>
        /// <returns>Symmetric Key</returns>
        public SecurityKey GetSymmetricSecurityKey()
        {
            var key = new SymmetricSecurityKey(Convert.FromBase64String(this.SecretKey));
            return key;
        }

		/// <summary>
		/// Generate a secret key
		/// </summary>
		/// <returns>secret key in string type</returns>
		public string GenerateSecretKey()
		{
			var hmac = new HMACSHA256();
			var key = Convert.ToBase64String(hmac.Key);
			return key;
		}

        /// <summary>
        ///  JWT Setting Class
        /// </summary>
        public JWTSetting Setting { get; set; }
        /// <summary>
        /// Secret Key
        /// </summary>
        public string SecretKey { get; set; }
        /// <summary>
        /// Claims
        /// </summary>
        public Claim[] Claims { get; set; }
    }
}
