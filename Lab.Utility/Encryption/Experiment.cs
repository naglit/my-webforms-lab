using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab.Utility.Encryption
{
    public static class Experiment
    {
        private const string V = "11001011";
        private static char[] _Key = V.ToCharArray();

        public static void Demo()
        {
            Console.WriteLine("plain text: {0}", "10101010");
            var ciphertext = ExperimentallyEncrypt("10101010");
            Console.WriteLine("cipher text: {0}", ciphertext);
            Console.WriteLine("decrypted text: {0}", ExperimentallyDecrypt(ciphertext));
            Console.ReadKey();
        }


        public static string ExperimentallyEncrypt(string plainText)
        {
            var encrypted = new char[8];
            foreach(var item in plainText.Select((value, i) => new { value, i }))
            {
                // XORing
                encrypted[item.i] = (item.value != _Key[item.i]) 
                    ? char.Parse("1")
                    : char.Parse("0");
            }
            return new string(encrypted);
        }

        public static string ExperimentallyDecrypt(string encryptedText)
        {
            var roundTrip = new char[8];
            foreach (var item in encryptedText.Select((value, i) => new { value, i }))
            {
                // XORing
                roundTrip[item.i] = (item.value != _Key[item.i])
                    ? char.Parse("1")
                    : char.Parse("0");
            }
            return new string(roundTrip);
        }
    }
}
