using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace FortyLife.DataAccess
{
    internal class UserAuthenticator
    {
        public static bool IsValid(string email, string rawPassword)
        {
            using (var db = new FortyLifeDbContext())
            {
                var user = db.ApplicationUsers.FirstOrDefault(i => i.Email == email);

                if (user == null) return false;

                return ComputeHash(rawPassword, user.PasswordSalt) == user.PasswordHash;
            }
        }

        /// <summary>
        /// Used for password checking. Creates a string hash value given plain text and a salt.
        /// </summary>
        /// <param name="plainText">The plain text to be hashed.</param>
        /// <param name="salt">The salt value to be hashed.</param>
        /// <returns>The string hash of the plain text and the salt.</returns>
        public static string ComputeHash(string plainText, string salt)
        {
            // Convert plain text into a byte array.
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            var saltBytes = Encoding.UTF8.GetBytes(salt);

            var hash = new SHA256Managed();

            // Compute hash value of salt.
            var plainHash = hash.ComputeHash(plainTextBytes);

            var concat = new byte[plainHash.Length + saltBytes.Length];

            Buffer.BlockCopy(saltBytes, 0, concat, 0, saltBytes.Length);
            Buffer.BlockCopy(plainHash, 0, concat, saltBytes.Length, plainHash.Length);

            var tHashBytes = hash.ComputeHash(concat);

            // Convert result into a base64-encoded string.
            var hashValue = Convert.ToBase64String(tHashBytes);

            // Return the result.
            return hashValue;
        }

        public static string GetHashString(string plainText)
        {
            return Convert.ToBase64String(new SHA256Managed().ComputeHash(Encoding.UTF8.GetBytes(plainText)));
        }
    }
}
