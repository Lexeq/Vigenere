using System.Text;
using System;

namespace VigenereTools
{
    internal class LatinCaesarCipher : ICaesarCipher
    {
        private const int AlphabetSize = 26;

        public string Decrypt(string text, int shift)
        {
            if (text == null)
                throw new ArgumentNullException(nameof(text));

            return Encrypt(text, -shift);
        }

        public string Encrypt(string text, int shift)
        {
            if (text == null)
                throw new ArgumentNullException(nameof(text));

            StringBuilder builder = new StringBuilder(text.Length);
            shift = shift % AlphabetSize;

            foreach (var ch in text)
            {
                var x = 'a' + (AlphabetSize + shift + ch - 'a') % AlphabetSize;
                builder.Append((char)x);
            }
            return builder.ToString();
        }
    }
}
