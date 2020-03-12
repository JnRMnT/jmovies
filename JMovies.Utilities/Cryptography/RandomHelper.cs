using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace JMovies.Utilities.Cryptography
{
    public class RandomHelper
    {
        public static byte[] GenerateRandom(int byteLength)
        {
            byte[] random = new byte[byteLength];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(random);
            }
            return random;
        }

        public static string GenerateRandomString(int byteLength)
        {
            return Convert.ToBase64String(GenerateRandom(byteLength));
        }
    }
}
