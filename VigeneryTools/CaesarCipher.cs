﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VigenereTools
{
    public class CaesarCipher : ICaesarCipher
    {
        private char[] alphabet;

        public char[] Alphabet
        {
            get
            {
                return alphabet.ToArray();
            }
        }

        public CaesarCipher(ICollection<char> alphabet)
        {
            if (alphabet == null)
                throw new ArgumentNullException(nameof(alphabet));
            if (alphabet.Count < 1)
                throw new ArgumentException("Alphabes must contains at least one character");
            if (alphabet.Distinct().Count() != alphabet.Count)
                throw new ArgumentException("Alphabet must not contains duplicate characters.", nameof(alphabet));

            this.alphabet = alphabet.ToArray();
        }

        public string Decrypt(string text, int shift)
        {
            return Encrypt(text, -shift);
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
