using JMovies.Entities.UserManagement;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Security;
using System.Security.Cryptography;
using System.Text;

namespace JMovies.Utilities.Hashing
{
    public class HashHelper
    {
        public static bool Matches(SecureString password, string hashedPassword, string salt, HashTypeEnum hashType)
        {
            return hashedPassword == Hash(hashType, password, salt);
        }

        public static string Hash(HashTypeEnum hashType, SecureString password, string saltText)
        {
            KeyDerivationPrf keyDerivation;
            switch (hashType)
            {
                case HashTypeEnum.Sha512:
                    keyDerivation = KeyDerivationPrf.HMACSHA512;
                    break;
                case HashTypeEnum.Sha256:
                    keyDerivation = KeyDerivationPrf.HMACSHA256;
                    break;
                default:
                    keyDerivation = KeyDerivationPrf.HMACSHA1;
                    break;
            }

            byte[] salt;
            if (!string.IsNullOrEmpty(saltText))
            {
                salt = Encoding.UTF8.GetBytes(saltText);
            }
            else
            {
                salt = new byte[128 / 8];
                using (var rng = RandomNumberGenerator.Create())
                {
                    rng.GetBytes(salt);
                }
            }
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password.ToString(),
            salt: salt,
            prf: keyDerivation,
            iterationCount: 10000,
            numBytesRequested: 256 / 8));
        }
    }
}
