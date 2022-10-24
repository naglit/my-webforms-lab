using Lab.Utility.Configuration;
using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Lab.Utility.Encryption
{
	public class Encryption
	{
		private static EncryptionSetting _setting = EncryptionSetting.GetInstance;
		public static string _tempIV = "KVwvlUiUaAs69FV1d3RENA==";

		public static void CheckTheLengthOffCipherText()
		{
			var nums = Enumerable.Range(0, 100);
			foreach (var num in nums)
			{
				var ct = Encrypt(CsharpExperiment.Csharp.GenerateRandomString(num), GenerateIV());
				Console.WriteLine("The length of plain text : The length of cipher text = {0} : {1}", num, ct.Length);
			}
		}

		/// <summary>
		/// Encrypt string value
		/// </summary>
		/// <param name="plainText">Plain text to be encrypted</param>
		/// <param name="iv">Initial Vector</param>
		/// <returns>Cipher Text</returns>
		public static byte[] Encrypt(string plainText, string iv)
		{
			if (string.IsNullOrEmpty(plainText) || (iv.Length == 0)) return new byte[] { };
			
			using (var aesAlg = new AesCryptoServiceProvider())
			{
				// Create an encryptor to perform the stream transform.
				var encryptor = aesAlg.CreateEncryptor(_setting.Key, Convert.FromBase64String(iv));

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
		public static string Decrypt(string cipherText, string iv)
		{
			if ((cipherText.Length == 0) || (iv.Length == 0)) return "";

			// Create an AesCryptoServiceProvider object
			// with the specified key and IV.
			using (var aesAlg = new AesCryptoServiceProvider())
			{
				// Create a decryptor to perform the stream transform.
				var decryptor = aesAlg.CreateDecryptor(_setting.Key, Convert.FromBase64String(iv));

				// Create the streams used for decryption.
				using (var msDecrypt = new MemoryStream(Convert.FromBase64String(cipherText)))
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
		
		/// <summary>
		/// Generate a key
		/// </summary>
		/// <returns>key</returns>
		public static string GenerateKey()
		{
			using (var aesAlg = new AesCryptoServiceProvider())
			{
				var key = Convert.ToBase64String(aesAlg.Key);
				return key;
			}
		}
		
		/// <summary>
		/// Generate an IV
		/// </summary>
		/// <returns>IV</returns>
		public static string GenerateIV()
		{
			using (var aesAlg = new AesCryptoServiceProvider())
			{
				var key = Convert.ToBase64String(aesAlg.IV);
				return key;
			}
		}

		/// <summary>
		/// 
		/// </summary>
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

		public static void TestSomeCasesForEncryptedTextLengthCalculator()
		{
			TestEncryptedTextLengthCalculator("あ");
			TestEncryptedTextLengthCalculator("1234512");
			TestEncryptedTextLengthCalculator("ddddddddddfvmascshfoiwht34uty298ysdjfkasjdhfot984dsjnkasnvksahoihkjsdfkasnknjfksjdbfikho203");
			TestEncryptedTextLengthCalculator("ᨐ");
			TestEncryptedTextLengthCalculator("ᨐᨐ");
			TestEncryptedTextLengthCalculator("ᨐᨐᨐ");
			TestEncryptedTextLengthCalculator("ᨐᨐᨐᨐ");
			TestEncryptedTextLengthCalculator("ᨐᨐᨐᨐᨐ");
			TestEncryptedTextLengthCalculator("𪛊");
			TestEncryptedTextLengthCalculator("𪛊𪛊");
			TestEncryptedTextLengthCalculator("𪛊𪛊𪛊");
			TestEncryptedTextLengthCalculator("𪛊𪛊𪛊𪛊");
			TestEncryptedTextLengthCalculator("𪛊𪛊𪛊𪛊𪛊");
			TestEncryptedTextLengthCalculator("𪛊𪛊𪛊𪛊𪛊𪛊𪛊𪛊𪛊𪛊𪛊𪛊");
			TestEncryptedTextLengthCalculator("012345678901𪛊𪛊𪛊𪛊𪛊");
			// Surrogate Pair characters take 8 bytes for each
			TestEncryptedTextLengthCalculator("12312r354353ᨐᨐ𪛊𪛊𪛊𪛊123𪛊𪛊𪛊𪛊𪛊");
		}

		/// <summary>
		/// Test Calculation of Encrypted Text Length in AES CBC - 256
		/// </summary>
		/// <param name="plainText">plain text</param>
		public static void TestEncryptedTextLengthCalculator(string plainText)
		{
			Console.WriteLine(plainText);

			// write unicode
			foreach (var ch in plainText)
			{
				break;
				Console.WriteLine("U+{0:X4}", Convert.ToUInt16(ch));
			}
			
			var plainTextSizeInBytes = Encoding.Unicode.GetByteCount(plainText);

			// Calculation
			var calculationResult = CalculateSizeOfEncryptedText(plainTextSizeInBytes);

			// Encryption
			var iv = GenerateIV();
			var cipherTextInByteArray = Encrypt(plainText, iv);
			var cipherText = Convert.ToBase64String(cipherTextInByteArray);

			// Display Result
			var result = (calculationResult == cipherText.Length) ? "TRUE" : "FALSE";
			Console.WriteLine(
				"{0}; calculation result: {1}, cipher text length: {2}{3}",
				result,
				calculationResult,
				cipherText.Length,
				Environment.NewLine);
		}

		/// <summary>
		/// Compare the length of a plain text and the encrypted text
		/// </summary>
		/// <param name="arrayLength">length</param>
		public static void ComparePlainAndEncryptedTextLengths(int arrayLength)
		{
			var strings = new string[arrayLength];
			foreach (var i in Enumerable.Range(1, arrayLength))
			{
				// Generate Random String
				var plainText = CsharpExperiment.Csharp.GenerateRandomString(i);

				//plainText += "𪛊";
				Console.WriteLine(Environment.NewLine);
				
				var plainTextSizeInBytes = Encoding.Unicode.GetByteCount(plainText);

				// Encrypt
				var iv = GenerateIV();
				var cipherTextInByteArray = Encrypt(plainText, iv);
				var cipherText = Convert.ToBase64String(cipherTextInByteArray);
				
				// Display the length of encrypted value
				Console.WriteLine("{0}, {1}", plainTextSizeInBytes, cipherText.Length);
			}
		}

		/// <summary>
		/// Calculate ci[her text size
		/// </summary>
		/// <param name="plainTextSizeInBytes">Plain text size in bytes</param>
		/// <returns>cipher text size</returns>
		public static int CalculateSizeOfEncryptedText(int plainTextSizeInBytes)
		{
			if (plainTextSizeInBytes == 0) return 0;
			
			var a = plainTextSizeInBytes / 32;
			var b = a / 3;
			var cipherTextSize = 20 * a + 4 * b + 24;

			Console.WriteLine("{0} bytes -> string length: {1}", plainTextSizeInBytes, cipherTextSize);
			return cipherTextSize;
		}

		public static void FindEncryptedTextLenth(int inputSizeInBytes)
		{
				
			long size = inputSizeInBytes;
			
			long postAesSize = inputSizeInBytes + (16- inputSizeInBytes % 16);
			Console.WriteLine("{0}, {1}", inputSizeInBytes, postAesSize);
		}
	}
}