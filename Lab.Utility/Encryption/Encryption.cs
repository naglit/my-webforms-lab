using Lab.Utility.Configuration;
using System;
using System.Collections;
using System.IO;
using System.Security.Cryptography;

namespace Lab.Utility.Encryption
{
	public class Encryption
	{
		private static EncryptionSetting _setting = EncryptionSetting.GetInstance;
		public static string _tempIV = "KVwvlUiUaAs69FV1d3RENA==";

		/// <summary>
		/// Encrypt string value
		/// </summary>
		/// <param name="plainText">Plain text to be encrypted</param>
		/// <param name="iv">Initial Vector</param>
		/// <returns>Cipher Text</returns>
		public static byte[] Encrypt(string plainText, byte[] iv)
		{
			if (string.IsNullOrEmpty(plainText) || (iv.Length == 0)) return new byte[] { };
			
			using (var aesAlg = new AesCryptoServiceProvider())
			{
				// Create an encryptor to perform the stream transform.
				var encryptor = aesAlg.CreateEncryptor(_setting.Key, iv);

				// Create the streams used for encryption.
				using (var msEncrypt = new MemoryStream())
				{
					using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
					{
						using (var swEncrypt = new StreamWriter(csEncrypt))
						{
							//Write all data to the stream.
							swEncrypt.Write(plainText);
						}
						var cipherText = msEncrypt.ToArray();
						return cipherText;
					}
				}
			}
		}

		/// <summary>
		/// Decrypt
		/// </summary>
		/// <param name="cipherText">String value to be decrypted</param>
		/// <param name="iv">Initial Vector</param>
		/// <returns>Cipher Text</returns>
		public static string Decrypt(byte[] cipherText, byte[] iv)
		{
			if ((cipherText.Length == 0) || (iv.Length == 0)) return "";

			// Create an AesCryptoServiceProvider object
			// with the specified key and IV.
			using (var aesAlg = new AesCryptoServiceProvider())
			{
				// Create a decryptor to perform the stream transform.
				var decryptor = aesAlg.CreateDecryptor(_setting.Key, iv);

				// Create the streams used for decryption.
				using (var msDecrypt = new MemoryStream(cipherText))
				{
					using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
					{
						using (var srDecrypt = new StreamReader(csDecrypt))
						{
							// Read the decrypted bytes from the decrypting stream
							// and place them in a string.
							var plaintext = srDecrypt.ReadToEnd();
							return plaintext;
						}
					}
				}
			}
		}
		
		public static string GenerateKey()
		{
			using (var aesAlg = new AesCryptoServiceProvider())
			{
				var key = Convert.ToBase64String(aesAlg.Key);
				return key;
			}
		}
		
		public static string GenerateIV()
		{
			using (var aesAlg = new AesCryptoServiceProvider())
			{
				var key = Convert.ToBase64String(aesAlg.IV);
				return key;
			}
		}

		public static void EncryptInputParams()
		{
			var original = "Here is some data to encrypt!";

			// Create a new instance of the AesCryptoServiceProvider
			// class.  This generates a new key and initialization
			// vector (IV).
			using (var aes = new AesCryptoServiceProvider())
			{
				// Encrypt the string to an array of bytes.
				var encrypted = EncryptStringToBytes_Aes(original, aes.Key, aes.IV);

				// Decrypt the bytes to a string.
				var roundtrip = DecryptStringFromBytes_Aes(encrypted, aes.Key, aes.IV);

				//Display the original data and the decrypted data.
				//Console.WriteLine("Original:   {0}", original);
				//Console.WriteLine("Round Trip: {0}", roundtrip);
			}
		}
		public static byte[] EncryptStringToBytes_Aes(string plainText, byte[] Key, byte[] IV)
		{
			// Check arguments.
			if (plainText == null || plainText.Length <= 0)
				throw new ArgumentNullException("plainText");
			if (Key == null || Key.Length <= 0)
				throw new ArgumentNullException("Key");
			if (IV == null || IV.Length <= 0)
				throw new ArgumentNullException("IV");
			byte[] encrypted;

			// Create an AesCryptoServiceProvider object
			// with the specified key and IV.
			using (AesCryptoServiceProvider aesAlg = new AesCryptoServiceProvider())
			{
				aesAlg.Key = Key;
				aesAlg.IV = IV;

				// Create an encryptor to perform the stream transform.
				ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

				// Create the streams used for encryption.
				using (MemoryStream msEncrypt = new MemoryStream())
				{
					using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
					{
						using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
						{
							//Write all data to the stream.
							swEncrypt.Write(plainText);
						}
						encrypted = msEncrypt.ToArray();
					}
				}
			}

			// Return the encrypted bytes from the memory stream.
			return encrypted;
		}

		static string DecryptStringFromBytes_Aes(byte[] cipherText, byte[] Key, byte[] IV)
		{
			// Check arguments.
			if (cipherText == null || cipherText.Length <= 0)
				throw new ArgumentNullException("cipherText");
			if (Key == null || Key.Length <= 0)
				throw new ArgumentNullException("Key");
			if (IV == null || IV.Length <= 0)
				throw new ArgumentNullException("IV");

			// Declare the string used to hold
			// the decrypted text.
			string plaintext = null;

			// Create an AesCryptoServiceProvider object
			// with the specified key and IV.
			using (AesCryptoServiceProvider aesAlg = new AesCryptoServiceProvider())
			{
				aesAlg.Key = Key;
				aesAlg.IV = IV;

				// Create a decryptor to perform the stream transform.
				ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

				// Create the streams used for decryption.
				using (MemoryStream msDecrypt = new MemoryStream(cipherText))
				{
					using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
					{
						using (StreamReader srDecrypt = new StreamReader(csDecrypt))
						{

							// Read the decrypted bytes from the decrypting stream
							// and place them in a string.
							plaintext = srDecrypt.ReadToEnd();
						}
					}
				}
			}

			return plaintext;
		}
	}
}