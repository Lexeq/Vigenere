using System;
using System.Linq;
using System.Text;

namespace VigenereTools.Hacks
{
    public class VigenereBreaker
    {
        public VigenereBreaker English
        {
            get { return new VigenereBreaker(new LatinCaesarCipher(), CaesarBreaker.EnglishBreaker, 0.0644); }
        }

        private const double DefaultIocDeviation = 0.008;

        private readonly double StandartIndexOfCoincidence;

        private ICaesarBreaker caesarBreaker;

        private ICaesarCipher caesarCipher;

        public double MaxIocDeviation { get; set; }

        internal VigenereBreaker(ICaesarCipher cCipher, ICaesarBreaker cBreaker, double ioc)
        {
            MaxIocDeviation = DefaultIocDeviation;
            caesarBreaker = cBreaker;
            caesarCipher = cCipher;
            StandartIndexOfCoincidence = ioc;
        }

        private int TryFindKeyLength(string message, int minKeyLength, int maxKeyLength)
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
            if (text == null)
                throw new ArgumentNullException(nameof(text));
            if (keyLength <= 0)
                throw new ArgumentException(nameof(keyLength) + " must be positive numbe.");

            if (keyLength == 1)
                return GetIndexOfCoincidence(text);

            var parts = text.Cut(keyLength);
            return parts.Select(x => GetIndexOfCoincidence(x)).Average();
        }

        private double GetIndexOfCoincidence(string text)
        {
            var groups = text.GroupBy(c => c).ToDictionary(g => g.Key, g => g.Count());
            double sum = 0;
            foreach (var g in groups)
            {
                var count = g.Value;
                sum += ((count * (count - 1d)) / (text.Length * (text.Length - 1d)));
            }
            return sum;
        }

        public string TryFindKey(string input)
        {
            return TryFindKey(input, TryFindKeyLength(input, 1, input.Length));
        }

        public string TryFindKey(string input, int keyLength)
        {
            var parts = input.Cut(keyLength);

            StringBuilder builder = new StringBuilder(keyLength);
            for (int i = 0; i < keyLength; i++)
            {
                var offset = caesarBreaker.GetShift(parts[i]);

                var y = caesarCipher.Encrypt(caesarCipher.Alphabet.First().ToString() , offset);
                builder.Append(y);
            }
            return builder.ToString();
        }
    }
}
