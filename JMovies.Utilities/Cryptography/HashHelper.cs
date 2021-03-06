﻿using JMovies.Entities.UserManagement;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Security;

namespace JMovies.Utilities.Cryptography
{
    public class HashHelper
    {
        public static bool Matches(SecureString password, string hashedPassword, string salt, HashTypeEnum hashType)
        {
            return hashedPassword == Hash(hashType, password, salt);
        }

        public static string GenerateSalt()
        {
            return RandomHelper.GenerateRandomString(128 / 8);
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

            if (string.IsNullOrEmpty(saltText))
            {
                saltText = GenerateSalt();
            }
            byte[] salt = Convert.FromBase64String(saltText);
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password.ToPlainString(),
            salt: salt,
            prf: keyDerivation,
            iterationCount: 10000,
            numBytesRequested: 256 / 8));
        }
    }
}
