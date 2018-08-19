using System.Collections.Generic;

namespace VigenereTools
{
    public interface ICaesarCipher
    {
        char[] Alphabet { get; }

        string Encrypt(string text, int shift);

        string Decrypt(string text, int shift);
    }
}
