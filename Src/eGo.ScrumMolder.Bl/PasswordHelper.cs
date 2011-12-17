using System;
using System.Security.Cryptography;
using System.Text;
using eGo.ScrumMolder.Bl.Interface;

namespace eGo.ScrumMolder.Bl
{
    public class PasswordHelper : IPasswordHelper
    {
        public string CreatePasswordHash(string password, string salt)
        {
            byte[] password_bytes = Encoding.Unicode.GetBytes(password);
            byte[] salt_bytes = Convert.FromBase64String(salt);

            byte[] hashBytes = new byte[salt_bytes.Length + password_bytes.Length];

            Buffer.BlockCopy(salt_bytes, 0, hashBytes, 0, salt_bytes.Length);
            Buffer.BlockCopy(password_bytes, 0, hashBytes, salt_bytes.Length, password_bytes.Length);

            using (HashAlgorithm hash = HashAlgorithm.Create("SHA1"))
            {
                hash.TransformFinalBlock(hashBytes, 0, hashBytes.Length);
                return Convert.ToBase64String(hash.Hash);
            }
        }

        public string CreateSalt()
        {
            //Generate a cryptographic random number.
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buff = new byte[4];
            rng.GetBytes(buff);

            // Return a Base64 string representation of the random number.
            return Convert.ToBase64String(buff);
        }

        public string GenerateRandomPassword()
        {
            var allowedChars = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
            var data = new byte[10];
            var crypt = new RNGCryptoServiceProvider();

            crypt.GetNonZeroBytes(data);
            var result = new StringBuilder();
            foreach (var b in data)
                result.Append(allowedChars[b % (allowedChars.Length - 1)]);

            return result.ToString();
        }
    }

}