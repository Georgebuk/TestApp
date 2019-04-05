using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace TestApp
{
    public static class PasswordHasher
    {
        public static string Hash(string passwordToHash)
        {
            //Create the salt value with a cryptographic PRNG:
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);
            //Create the Rfc2898DeriveBytes and get the hash value:
            var pbkdf2 = new Rfc2898DeriveBytes(passwordToHash, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);
            //Combine the salt and password bytes for later use
            byte[] hashbytes = new byte[36];
            Array.Copy(salt, 0, hashbytes, 0, 16);
            Array.Copy(hash, 0, hashbytes, 16, 20);
            //Return combined salt+hash as a string
            return Convert.ToBase64String(hashbytes);
        }

        public static bool Verify(string enteredPass, string savedPassword)
        {
            bool verified = true;
            byte[] hashBytes = Convert.FromBase64String(savedPassword);

            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);

            var pbkdf2 = new Rfc2898DeriveBytes(enteredPass, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);

            for (int i = 0; i < 20; i++)
                if (hashBytes[i + 16] != hash[i])
                    verified = false;

            return verified;
        }
    }
}
