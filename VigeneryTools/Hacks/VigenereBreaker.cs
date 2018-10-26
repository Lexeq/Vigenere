using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VigenereTools.Hacks
{
    /// <summary>
    /// Provides methods for break Vigenere cipher.
    /// </summary>
    public class VigenereBreaker
    {
        private const double DefaultIocDeviation = 0.008;

        private readonly double StandartIndexOfCoincidence;

        private CaesarBreaker caesarBreaker;

        private ICaesarCipher caesarCipher;

        private double iocDeviation;


        /// <value> </value>
        public double MaxIocDeviation
        {
            get => iocDeviation;
            set
            {
                if (value > 0)
                    iocDeviation = value;
                else
                    throw new ArgumentException("Value must be positive.", nameof(MaxIocDeviation));
            }
        }

        /// <value></value>
        public double MaxCaesarDeviation
        {
            get => caesarBreaker.MaxDeviation;
            set
            {
                if (value > 0)
                    caesarBreaker.MaxDeviation = value;
                else
                    throw new ArgumentException("Value must be positive.", nameof(MaxIocDeviation));
            }
        }

        /// <summary>
        /// Initialize a new instance of <cref="VigenereTools.Hacks.VigenereBreaker"/> class.
        /// </summary>
        /// <param name="cCipher"></param>
        /// <param name="cBreaker"></param>
        /// <param name="ioc">Index of coincidence</param>
        /// <exception cref="ArgumentNullException"/>
        public VigenereBreaker(ICaesarCipher cCipher, CaesarBreaker cBreaker, double ioc)
        {
            caesarBreaker = cBreaker ?? throw new ArgumentNullException(nameof(cBreaker));
            caesarCipher = cCipher ?? throw new ArgumentNullException(nameof(cCipher));
            if (ioc <= 0)
                throw new ArgumentException("Value must be positive.", nameof(ioc));
            StandartIndexOfCoincidence = ioc;
            MaxIocDeviation = DefaultIocDeviation;
        }

        /// <summary>
        /// Finds most likely keyword.
        /// </summary>
        /// <param name="input">Encoded text</param>
        /// <returns>Probable keyword</returns>
        /// <exception cref="ArgumentNullException"/>
        public string FindKey(string input)
        {
            return FindKey(input, 1, (int)Math.Sqrt(input.Length));
        }

        /// <summary>
        /// Finds most likely keyword within specified range of length.
        /// </summary>
        /// <param name="input">Encoded text</param>
        /// <param name="minKeyLength">Included minimal keyword length</param>
        /// <param name="maxKeyLength">Included maximal keyword length</param>
        /// <returns>Probable keyword</returns>
        /// <exception cref="ArgumentNullException"/>
        public string FindKey(string input, int minKeyLength, int maxKeyLength)
        {
            if (input == null)
                throw new ArgumentNullException(input);
            if (minKeyLength < 1)
                throw new ArgumentException("Value must be positive number", nameof(minKeyLength));

            var kl = FindKeyLength(input, minKeyLength, maxKeyLength);
            return FindKey(input, kl > 0 ? kl : minKeyLength);
        }

        /// <summary>
        /// Finds most likely keyword with the specified length.
        /// </summary>
        /// <param name="input">Encoded text</param>
        /// <param name="keyLength">Keyword length</param>
        /// <returns>Probable keyword</returns>
        /// <exception cref="ArgumentNullException"/>
        public string FindKey(string input, int keyLength)
        {
            if (keyLength < 1)
                throw new ArgumentException("Value must be positive number", nameof(keyLength));
            if (input == null)
                throw new ArgumentNullException(input);
            if (input.Length == 0)
                return string.Empty;

            var parts = input.Split(keyLength);

            StringBuilder builder = new StringBuilder(keyLength);
            for (int i = 0; i < keyLength; i++)
            {
                var offset = caesarBreaker.GetShift(parts[i]);

                var y = caesarCipher.Encrypt(caesarCipher.Alphabet.First().ToString(), offset);
                builder.Append(y);
            }
            return builder.ToString();
        }

        private int FindKeyLength(string message, int minKeyLength, int maxKeyLength)
        {
            for (int k = minKeyLength; k <= maxKeyLength; k++)
            {
                var ioc = CalculateIndexOfCoincidence(message, k);
                if (Math.Abs(StandartIndexOfCoincidence - ioc) <= MaxIocDeviation)
                    return k;
            }

            return -1;
        }

        private double CalculateIndexOfCoincidence(string text, int keyLength)
        {
            var parts = text.Split(keyLength);
            return parts.Select(x => CalcualteIndexOfCoincidence(x)).Average();
        }

        private double CalcualteIndexOfCoincidence(string text)
        {
            var groups = text.GroupBy(c => c).ToDictionary(g => g.Key, g => g.Count());
            double sum = 0;
            foreach (var g in groups)
            {
                var count = g.Value;
                sum += (count * (count - 1d)) / (text.Length * (text.Length - 1d));
            }
            return sum;
        }
    }
}
