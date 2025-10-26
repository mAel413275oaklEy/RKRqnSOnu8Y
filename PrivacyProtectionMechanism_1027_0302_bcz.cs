// 代码生成时间: 2025-10-27 03:02:18
using Microsoft.AspNetCore.Components;
using System;
using System.Security.Cryptography;
using System.Text;

namespace PrivacyProtection
{
    /// <summary>
    /// Privacy protection mechanism using C# and Blazor framework.
    /// </summary>
    public class PrivacyProtectionMechanism
    {
        private readonly NavigationManager navigationManager;

        /// <summary>
        /// Initializes a new instance of PrivacyProtectionMechanism.
        /// </summary>
        /// <param name="navigationManager">NavigationManager instance for handling navigation.</param>
        public PrivacyProtectionMechanism(NavigationManager navigationManager)
        {
            this.navigationManager = navigationManager;
        }

        /// <summary>
        /// Encrypts sensitive data using AES encryption.
        /// </summary>
        /// <param name="data">String data to be encrypted.</param>
        /// <returns>Encrypted data in base64 format.</returns>
        public string EncryptData(string data)
        {
            try
            {
                using (Aes aesAlg = Aes.Create())
                {
                    aesAlg.GenerateKey();
                    aesAlg.GenerateIV();

                    ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                    using (MemoryStream msEncrypt = new MemoryStream())
                    {
                        using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                        {
                            using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                            {
                                swEncrypt.Write(data);
                            }
                        }

                        return Convert.ToBase64String(msEncrypt.ToArray());
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception and return null or handle error as per requirement.
                Console.WriteLine($"Encryption error: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Decrypts sensitive data using AES decryption.
        /// </summary>
        /// <param name="data">Encrypted data in base64 format.</param>
        /// <returns>Decrypted string data.</returns>
        public string DecryptData(string data)
        {
            try
            {
                using (Aes aesAlg = Aes.Create())
                {
                    byte[] Key = aesAlg.Key;
                    byte[] IV = aesAlg.IV;

                    ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                    using (MemoryStream msDecrypt = new MemoryStream(Convert.FromBase64String(data)))
                    {
                        using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {
                            using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                            {
                                return srDecrypt.ReadToEnd();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception and return null or handle error as per requirement.
                Console.WriteLine($"Decryption error: {ex.Message}");
                return null;
            }
        }
    }
}
