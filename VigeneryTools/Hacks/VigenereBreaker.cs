using System;
using System.Linq;
using System.Text;

namespace VigenereTools.Hacks
{
    public class VigenereBreaker
    {
        public static VigenereBreaker English
        {
            get { return new VigenereBreaker(new LatinCaesarCipher(), CaesarBreaker.English, 0.0644); }
        }

        private const double DefaultIocDeviation = 0.008;

        private readonly double StandartIndexOfCoincidence;

        private CaesarBreaker caesarBreaker;

        private ICaesarCipher caesarCipher;

        public double MaxIocDeviation { get; set; } //TODO : check

        public double MaxCaesarDeviation
        {
            get { return caesarBreaker.MaxDeviation; }
            set { caesarBreaker.MaxDeviation = value; }
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
            for (int k = minKeyLength; k < maxKeyLength; k++)
            {
                var ioc = GetIndexOfCoincidence(message, k);
                if (Math.Abs(StandartIndexOfCoincidence - ioc) <= MaxIocDeviation)
                    return k;
            }

            return -1;
        }

        private double GetIndexOfCoincidence(string text, int keyLength)
        {
            if (keyLength == 1)
                return GetIndexOfCoincidence(text);

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

        public bool FindKey(string input, out string key)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));
            if (input.Length == 0)
            {
                key = "";
                return true;
            }

            key = default(string);
            var keyLength = FindKeyLength(input, 1, input.Length);

            if (keyLength < 0)
                return false;
            else
                return FindKey(input, keyLength, out key);
        }

        public bool FindKey(string input, int keyLength, out string key)
        {
            if (keyLength < 1)
                throw new ArgumentException("Value must be positive number", nameof(keyLength));
            if (input == null)
                throw new ArgumentNullException(input);
            if (input.Length == 0)
            {
                key = "";
                return true;
            }

            var parts = input.Split(keyLength);

            StringBuilder builder = new StringBuilder(keyLength);
            for (int i = 0; i < keyLength; i++)
            {
                var offset = caesarBreaker.GetShift(parts[i]);

                var y = caesarCipher.Encrypt(caesarCipher.Alphabet.First().ToString(), offset);
                builder.Append(y);
            }
            key = builder.ToString();
            return true;
        }
    }
}
