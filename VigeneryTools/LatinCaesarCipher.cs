using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;

namespace VigenereTools
{
    public class LatinCaesarCipher : ICaesarCipher
    {
        private const int AlphabetSize = 26;

        private static readonly char[] alphabet = Enumerable.Range('a', AlphabetSize).Select(x => (char)x).ToArray();

        public IReadOnlyCollection<char> Alphabet
        {
            get
            {
                return Array.AsReadOnly(alphabet);
            }
        }

        public string Decrypt(string text, int shift)
        {
            if (text == null)
                throw new ArgumentNullException(nameof(text));
            try
            {
                return Encrypt(text, -shift);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public string Encrypt(string text, int shift)
        {
            if (text == null)
                throw new ArgumentNullException(nameof(text));

            StringBuilder builder = new StringBuilder(text.Length);
            shift = shift % AlphabetSize;

            foreach (var ch in text)
            {
                if (ch < 'a' || ch > 'z')
                    throw new CipherException($"Unexpectable char: {ch}");

                var x = 'a' + (AlphabetSize + shift + ch - 'a') % AlphabetSize;
                builder.Append((char)x);
            }
            return builder.ToString();
        }
    }
}
