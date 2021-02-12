using System;
using System.Security.Cryptography;
using System.Text;

namespace Lab.Utility.Encryption
{
	public class Encryption
	{
		public byte[] Testing(string input)
		{
			var inputs = UTF8Encoding.UTF8.GetBytes(input);
			return inputs;
		}

		public static string Encrypt(string input, string key)
		{
			var inputs = UTF8Encoding.UTF8.GetBytes(input);
			var tripleDES = new TripleDESCryptoServiceProvider();
			tripleDES.Key = UTF8Encoding.UTF8.GetBytes(key);
			tripleDES.Mode = CipherMode.ECB;
			tripleDES.Padding = PaddingMode.PKCS7;
			ICryptoTransform cTransform = tripleDES.CreateEncryptor();
			byte[] resultArray = cTransform.TransformFinalBlock(inputs, 0, inputs.Length);
			tripleDES.Clear();
			return Convert.ToBase64String(resultArray, 0, resultArray.Length);
		}
		public static string Decrypt(string input, string key)
		{
			byte[] inputArray = Convert.FromBase64String(input);
			TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider();
			tripleDES.Key = UTF8Encoding.UTF8.GetBytes(key);
			tripleDES.Mode = CipherMode.ECB;
			tripleDES.Padding = PaddingMode.PKCS7;
			ICryptoTransform cTransform = tripleDES.CreateDecryptor();
			byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
			tripleDES.Clear();
			return UTF8Encoding.UTF8.GetString(resultArray);
		}
	}
}