using System;
using System.Linq;
using System.Text;

namespace VigeneryTools
{
    public class CaesarCipher : ICaesarCipher
    {
        private char[] alphabet;

        public CaesarCipher(char[] alphabet)
        {
            if (alphabet == null)
                throw new ArgumentNullException(nameof(alphabet));
            if (alphabet.Length < 1)
                throw new ArgumentException("Alphabes must contain at least one character");
            if (alphabet.Distinct().Count() != alphabet.Length)
                throw new ArgumentException("Alphabet must not contain duplicate characters.", nameof(alphabet));

            this.alphabet = alphabet;
        }

        public string Decrypt(string text, int shift)
        {
            if (text == null)
                throw new ArgumentException(nameof(text));
            if (shift < 0)
                throw new ArgumentException("Shift must be positive number.", nameof(shift));

            StringBuilder builder = new StringBuilder(text.Length);

            foreach (var ch in text)
            {
                var x = alphabet[(Array.IndexOf(alphabet, ch) - (shift % alphabet.Length) + alphabet.Length) % alphabet.Length];
                builder.Append(x);
            }
            return builder.ToString();
        }

        public string Encrypt(string text, int shift)
        {
            if (text == null)
                throw new ArgumentException(nameof(text));
            if (shift < 0)
                throw new ArgumentException("Shift must be positive number.", nameof(shift));

            StringBuilder builder = new StringBuilder(text.Length);

            foreach (var ch in text)
            {
                var x = alphabet[(Array.IndexOf(alphabet, ch) + shift) % alphabet.Length];
                builder.Append(x);
            }
            return builder.ToString();
        }
    }
}
