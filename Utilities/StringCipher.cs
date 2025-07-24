using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
namespace VisitorManagementSystem.Utilities
{
    public class StringCipher
    {
        private const int KeySize = 256; 
        private const int DerivationIterations = 1000; 
        private static readonly string passPhrase = "SirketIsmi2025"; 

        public static string Encrypt(string plainText)
        {
            byte[] saltBytes = GenerateRandomBytes(32); 
            byte[] ivBytes = GenerateRandomBytes(16); 
            byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);

            using (var password = new Rfc2898DeriveBytes(passPhrase, saltBytes, DerivationIterations, HashAlgorithmName.SHA256))
            {
                byte[] keyBytes = password.GetBytes(KeySize / 8); 

                using (Aes aes = Aes.Create())
                {
                    aes.Key = keyBytes;
                    aes.IV = ivBytes;
                    aes.Mode = CipherMode.CBC;
                    aes.Padding = PaddingMode.PKCS7;

                    using (var memoryStream = new MemoryStream())
                    {
                        using (var cryptoStream = new CryptoStream(memoryStream, aes.CreateEncryptor(), CryptoStreamMode.Write))
                        {
                            cryptoStream.Write(plainBytes, 0, plainBytes.Length);
                            cryptoStream.FlushFinalBlock();

                            byte[] encryptedBytes = saltBytes.Concat(ivBytes).Concat(memoryStream.ToArray()).ToArray();

                            return Convert.ToBase64String(encryptedBytes);
                        }
                    }
                }
            }
        }

        public static string Decrypt(string cipherText)
        {
            try
            {
                byte[] cipherBytes = Convert.FromBase64String(cipherText);
                byte[] saltBytes = cipherBytes.Take(32).ToArray();
                byte[] ivBytes = cipherBytes.Skip(32).Take(16).ToArray();
                byte[] encryptedBytes = cipherBytes.Skip(48).ToArray();

                using (var password = new Rfc2898DeriveBytes(passPhrase, saltBytes, DerivationIterations, HashAlgorithmName.SHA256))
                {
                    byte[] keyBytes = password.GetBytes(KeySize / 8);

                    using (Aes aes = Aes.Create())
                    {
                        aes.Key = keyBytes;
                        aes.IV = ivBytes;
                        aes.Mode = CipherMode.CBC;
                        aes.Padding = PaddingMode.PKCS7;

                        using (var memoryStream = new MemoryStream(encryptedBytes))
                        {
                            using (var cryptoStream = new CryptoStream(memoryStream, aes.CreateDecryptor(), CryptoStreamMode.Read))
                            using (var streamReader = new StreamReader(cryptoStream, Encoding.UTF8))
                            {
                                return streamReader.ReadToEnd();
                            }
                        }
                    }
                }
            }

            catch
            {   
                return null;          
            }
        }
        
        private static byte[] GenerateRandomBytes(int size)
        {
            byte[] randomBytes = new byte[size];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomBytes);
            }
            return randomBytes;
        }
    }
}
