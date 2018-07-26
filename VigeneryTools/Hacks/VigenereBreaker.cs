using System;
using System.Linq;
using System.Text;

namespace VigenereTools.Hacks
{
    public class VigenereBreaker
    {
        private ICaesarBreaker caesarBreaker;

        private ICaesarCipher caesarCipher;

        private const double StandartIndexOfCoincidence = 0.0644;

        private const double StandartKeyLengthCoeff = 0.008;

        public double KeyLengthCoeff { get; set; }

        private readonly char[] alphabet;

        public VigenereBreaker()
        {
            KeyLengthCoeff = StandartKeyLengthCoeff;
            alphabet = Enumerable.Range(97, 26).Select(x => (char)x).ToArray();
            caesarBreaker = CaesarBreaker.EnglishBreaker;
            caesarCipher = new CaesarCipher(alphabet);
        }

        private int FindKeyLength(string message, int minKeyLength, int maxKeyLength)
        {
            for (int k = minKeyLength; k < maxKeyLength; k++)
            {
                var ic = GetIndexOfCoincidence(message, alphabet, k);
                if (Math.Abs(StandartIndexOfCoincidence - ic) <= KeyLengthCoeff)
                    return k;
            }

            return -1;
        }

        private double GetIndexOfCoincidence(string text, char[] alphabet, int keyLength)
        {
            if (keyLength <= 0)
                throw new ArgumentException(nameof(keyLength) + " must be positive numbe.");
            if (keyLength == 1)
                return GetIndexOfCoincidence(text, alphabet);

            StringBuilder sb = new StringBuilder();

            double sum = 0;
            for (int k = 0; k < keyLength; k++)
            {
                sb.Clear();
                for (int i = k; i < text.Length; i += keyLength)
                {
                    sb.Append(text[i]);
                }
                var ic = GetIndexOfCoincidence(sb.ToString(), alphabet);
                sum += ic;
            }

            return sum / keyLength;
        }

        private double GetIndexOfCoincidence(string text, char[] alphabet)
        {
            if (text == null)
                throw new ArgumentNullException(nameof(text));
            if (alphabet == null)
                throw new ArgumentNullException(nameof(alphabet));

            var groups = text.GroupBy(c => c).ToDictionary(g => g.Key, g => g.Count());
            double sum = 0;
            foreach (var c in alphabet)
            {
                var count = groups.ContainsKey(c) ? groups[c] : 0d;
                sum += ((count * (count - 1d)) / (text.Length * (text.Length - 1d)));
            }
            return sum;
        }

        public string TryFindKey(string input)
        {
            return TryFindKey(input, FindKeyLength(input, 1, input.Length));
        }

        public string TryFindKey(string input, int keyLength)
        {
            var pts = input.Cut(keyLength);

            StringBuilder builder = new StringBuilder(keyLength);
            for (int i = 0; i < keyLength; i++)
            {
                var offset = caesarBreaker.GetShift(pts[i]);

                var x = (char)('a' + ((26 - offset) % 26));
                var y = caesarCipher.Decrypt("a", -offset);
                builder.Append(y);
            }
            return builder.ToString();
        }
    }
}
