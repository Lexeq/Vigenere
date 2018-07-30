using System;

namespace VigenereTools
{
    public class VigenereCipher:IVigenereCipher
    {
        char[] alphabet;
        ICaesarCipher caesar;

        public VigenereCipher(char[] alphabet)
        {
            this.alphabet = alphabet;
            caesar = new CaesarCipher(alphabet);
        }

        public VigenereCipher(ICaesarCipher caesar)
        {
            this.caesar = caesar;
        }

        public string Decrypt(string text, string key)
        {
            if (text == null)
                throw new ArgumentNullException(nameof(text));
            if (key == null)
                throw new ArgumentNullException(nameof(key));


            int partsCount = key.Length <= text.Length ? key.Length : text.Length;
            var parts = text.Cut(partsCount);
            string[] decrypted = new string[partsCount];
            for (int i = 0; i <parts.Length; i++)
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

            int partsCounts = key.Length <= text.Length ? key.Length : text.Length;
            var parts = text.Cut(partsCounts);
            string[] encrypted = new string[partsCounts];

            for (int i = 0; i < parts.Length; i++)
            {
                encrypted[i] = caesar.Encrypt(parts[i], Array.IndexOf(alphabet, key[i]));
            }

            return encrypted.Merge();
        }
    }
}