using System;
using System.Security.Cryptography;
using System.Text;
using AppliedSystems.Interfaces;
using Microsoft.AspNet.Identity;

namespace AppliedSystems.Services
{
    public class PasswordHasher : IAppliedSystemsPasswordHasher
    {
        private string _salt;

        public string Salt
        {
            get { return _salt; }
            set { _salt = value; }
        }

        public string HashPassword(string password)
        {
            return GenerateSecret(password, out _salt);
        }

        public PasswordVerificationResult VerifyHashedPassword(string hashedPassword, string providedPassword)
        {
            if (string.IsNullOrEmpty(hashedPassword))
            {
                throw new ArgumentNullException("hashedPassword");
            }

            if (string.IsNullOrEmpty(Salt))
            {
                throw new InvalidOperationException("Password Salt not set");
            }

            return VerifyHashedPassword(providedPassword, Salt, hashedPassword)
                ? PasswordVerificationResult.Success
                : PasswordVerificationResult.Failed;
        }

        public bool VerifyHashedPassword(string password, string salt, string hash)
        {
            var secret = $"{password}{salt}";
            var inputHash = Hash(secret);
            return inputHash.Equals(hash);
        }

        public string GenerateSecret(string password, out string salt)
        {
            salt = GenerateSalt();
            var secret = $"{password}{Salt}";
            var hash = Hash(secret);

            return hash;
        }

        private static string Hash(string stringToHash)
        {
            var sha256Provider = new SHA256CryptoServiceProvider();
            var hash = Convert.ToBase64String(sha256Provider.ComputeHash(Encoding.UTF8.GetBytes(stringToHash)));
            return hash;
        }

        private static string GenerateSalt()
        {
            var saltGenerator = new RNGCryptoServiceProvider();
            var saltBytes = new byte[10];
            saltGenerator.GetBytes(saltBytes);
            return Convert.ToBase64String(saltBytes);
        }
    }
}
