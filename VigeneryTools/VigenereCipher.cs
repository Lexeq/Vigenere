using System;
using System.Linq;

namespace VigenereTools
{
    public class VigenereCipher : IVigenereCipher
    {
        private ICaesarCipher caesar;

        private char[] alphabet;

        public VigenereCipher(char[] alphabet)
            : this(new CaesarCipher(alphabet)) { }

        public VigenereCipher(ICaesarCipher caesar)
        {
            this.caesar = caesar ?? throw new ArgumentNullException(nameof(caesar));
            alphabet = Enumerable.ToArray(caesar.Alphabet);
        }

        public string Decrypt(string text, string key)
        {
            if (text == null)
                throw new ArgumentNullException(nameof(text));
            if (key == null)
                throw new ArgumentNullException(nameof(key));
            if (key.Length < 1)
                throw new ArgumentException("Key must contains at least one character.", nameof(key));

            int partsCount = Math.Min(key.Length, text.Length);
            var parts = text.Split(partsCount);
            string[] decrypted = new string[partsCount];
            for (int i = 0; i < parts.Length; i++)
            {
                decrypted[i] = caesar.Decrypt(parts[i], Array.IndexOf(alphabet, key[i]));
            }
            return decrypted.Merge();
        }

        public string Encrypt(string text, string key)
        {
            if (text == null)
                throw new ArgumentNullException(nameof(text));
            if (key == null)
                throw new ArgumentNullException(nameof(key));
            if (key.Length < 1)
                throw new ArgumentException("Key must contains at least one character.", nameof(key));

            int partsCounts = Math.Min(key.Length, text.Length);
            var parts = text.Split(partsCounts);
            string[] encrypted = new string[partsCounts];
            for (int i = 0; i < parts.Length; i++)
            {
                encrypted[i] = caesar.Encrypt(parts[i], Array.IndexOf(alphabet, key[i]));
            }
            return encrypted.Merge();
        }
    }
}