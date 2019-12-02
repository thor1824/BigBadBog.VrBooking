using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using VrBooking.Core.Entity;

namespace VrBooking.RestApi.WebApp.Helper.impl
{
    public class AuthenticationHelper : IAuthenticationHelper
    {
        private readonly byte[] secretBytes;

        public AuthenticationHelper(byte[] secret)
        {
            secretBytes = secret;
        }



        /// <summary>
        /// This method computes a hashed and salted password using the HMACSHA512 algorithm.
        /// The HMACSHA512 class computes a Hash-based Message Authentication Code (HMAC) using 
        /// the SHA512 hash function. When instantiated with the parameterless constructor (as
        /// here) a randomly Key is generated. This key is used as a password salt.
        /// The computation is performed as shown below:
        /// passwordHash = SHA512(password + Key)
        /// 
        /// A password salt randomizes the password hash so that two identical passwords will have 
        /// significantly different hash values. This protects against sophisticated attempts to guess passwords, 
        /// such as a rainbow table attack. The password hash is 512 bits (=64 bytes) long. 
        /// The password salt is 1024 bits (=128 bytes) long.
        /// 
        /// </summary>
        /// <param name="password"></param>
        /// <param name="passwordHash"></param>
        /// <param name="passwordSalt"></param>
        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (HMACSHA512 hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        /// <summary>
        /// This method verifies that the password entered by a user corresponds to the stored
        /// password hash for this user. The method computes a Hash-based Message Authentication
        /// Code (HMAC) using the SHA512 hash function. The inputs to the computation is the
        /// password entered by the user and the stored password salt for this user. If the
        /// computed hash value is identical to the stored password hash, the password entered
        /// by the user is correct, and the method returns true.
        /// </summary>
        /// <param name="password"></param>
        /// <param name="storedHash"></param>
        /// <param name="storedSalt"></param>
        public bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            using (HMACSHA512 hmac = new HMACSHA512(storedSalt))
            {
                byte[] computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }
            return true;
        }

        /// <summary>
        /// This method generates and returns a JWT token for a user.
        /// </summary>
        /// <param name="user"></param>
        public string GenerateToken(LoginUser user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserInfo.Email)
            };

            if (user.IsAdmin)
                claims.Add(new Claim(ClaimTypes.Role, "Administrator"));

            JwtSecurityToken token = new JwtSecurityToken(
                new JwtHeader(new SigningCredentials(
                    new SymmetricSecurityKey(secretBytes),
                    SecurityAlgorithms.HmacSha256)),
                new JwtPayload(null, // issuer - not needed (ValidateIssuer = false)
                               null, // audience - not needed (ValidateAudience = false)
                               claims.ToArray(),
                               DateTime.Now,               // notBefore
                               DateTime.Now.AddMinutes(60)));  // expires

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
