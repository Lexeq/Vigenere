using System;
using System.Linq;
using System.Text;

namespace VigenereTools.Hacks
{
    public class VigenereBreaker
    {
        private const double DefaultIocDeviation = 0.008;

        private readonly double StandartIndexOfCoincidence;

        private CaesarBreaker caesarBreaker;

        private ICaesarCipher caesarCipher;

        private double iocDeviation;
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

        private VigenereBreaker(ICaesarCipher cCipher, CaesarBreaker cBreaker, double ioc)
        {
            MaxIocDeviation = DefaultIocDeviation;
            caesarBreaker = cBreaker;
            caesarCipher = cCipher;
            StandartIndexOfCoincidence = ioc;
        }

        private int FindKeyLength(string message, int minKeyLength, int maxKeyLength)
        {
            for (int k = minKeyLength; k <= maxKeyLength; k++)
            {
                var ioc = GetIndexOfCoincidence(message, k);
                if (Math.Abs(StandartIndexOfCoincidence - ioc) <= MaxIocDeviation)
                    return k;
            }

            return -1;
        }

        private double GetIndexOfCoincidence(string text, int keyLength)
        {
            var parts = text.Split(keyLength);
            return parts.Select(x => GetIndexOfCoincidence(x)).Average();
        }

        private double GetIndexOfCoincidence(string text)
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

        public string FindKey(string input)
        {
            return FindKey(input, 1, (int)Math.Sqrt(input.Length));
        }

        public string FindKey(string input, int minKeyLength, int maxKeyLength)
        {
            if (input == null)
                throw new ArgumentNullException(input);
            if (minKeyLength < 1)
                throw new ArgumentException("Value must be positive number", nameof(minKeyLength));

            var kl = FindKeyLength(input, minKeyLength, maxKeyLength);
            return FindKey(input, kl > 0 ? kl : minKeyLength);
        }

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
    }
}
