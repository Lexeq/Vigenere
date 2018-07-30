using System.Collections.Generic;
using VigenereTools;
using Hacks = VigenereTools.Hacks;

namespace VigenereBreaker.Model
{
    internal class Language
    {
        public string Name { get; }

        public IReadOnlyList<char> Alphabet { get; }

        public IVigenereCipher VigenereCipher { get; }

        public Hacks.VigenereBreaker VigenereBreaker { get; }

        public Hacks.CaesarBreaker CaesarBreaker { get; }

        public Language(string name, IList<char> alphabet, IVigenereCipher vigenereCipher, Hacks.VigenereBreaker vBreaker, Hacks.CaesarBreaker cBreaker)
        {
            Name = name;
            Alphabet = new List<char>(alphabet).AsReadOnly();
            VigenereCipher = vigenereCipher;
            CaesarBreaker = cBreaker;
            VigenereBreaker = vBreaker;
        }
    }
}