using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VigenereTools
{
    public class CaesarCipher : ICaesarCipher
    {
        private char[] alphabet;

        public IReadOnlyCollection<char> Alphabet
        {
            get
            {
                return Array.AsReadOnly(alphabet);
            }
        }

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
            try
            {
               return Encrypt(text, -shift);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string Encrypt(string text, int shift)
        {
            if (text == null)
                throw new ArgumentException(nameof(text));

            shift %= alphabet.Length;

            StringBuilder builder = new StringBuilder(text.Length);
            foreach (var ch in text)
            {
                var chPosition = Array.IndexOf(alphabet, ch);
                if (chPosition < 0)
                    throw new CipherException($"Unexpectable character {ch}");

                var x = alphabet[(alphabet.Length + chPosition + shift) % alphabet.Length];
                builder.Append(x);
            }
            return builder.ToString();
        }
    }
}
