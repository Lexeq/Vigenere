using System;
using System.Linq;
using System.Text;

namespace VigenereTools.Hacks
{
    public class VigenereBreaker
    {
        public VigenereBreaker English
        {
            get
            {
                var v = new VigenereBreaker(CaesarBreaker.EnglishBreaker, 0.0644);
                v.caesarCipher = new LatinCaesarCipher();
                return v;
            }
        }

        private ICaesarBreaker caesarBreaker;

        private ICaesarCipher caesarCipher;

        private readonly double StandartIndexOfCoincidence;

        private const double StandartKeyLengthCoeff = 0.008;

        public double KeyLengthCoeff { get; set; }

        internal VigenereBreaker(ICaesarBreaker cBreaker, double ioc)
        {
            KeyLengthCoeff = StandartKeyLengthCoeff;
            caesarBreaker = cBreaker;
            caesarCipher = new CaesarCipher(cBreaker.Alphabet);
            StandartIndexOfCoincidence = ioc;
        }

   /*     internal VigenereBreaker(ICaesarCipher cCipher, ICaesarBreaker cBreaker, double ioc)
        {
            KeyLengthCoeff = StandartKeyLengthCoeff;
            caesarBreaker = cBreaker;
            caesarCipher = cCipher;
            StandartIndexOfCoincidence = ioc;
        }*/

        private int TryFindKeyLength(string message, int minKeyLength, int maxKeyLength)
        {
            for (int k = minKeyLength; k < maxKeyLength; k++)
            {
                var ic = GetIndexOfCoincidence(message, caesarBreaker.Alphabet, k);
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
            return TryFindKey(input, TryFindKeyLength(input, 1, input.Length));
        }

        public string TryFindKey(string input, int keyLength)
        {
            var pts = input.Cut(keyLength);

            StringBuilder builder = new StringBuilder(keyLength);
            for (int i = 0; i < keyLength; i++)
            {
                var offset = caesarBreaker.GetShift(pts[i]);

                var y = caesarCipher.Decrypt("a", offset);
                builder.Append(y);
            }
            return builder.ToString();
        }
    }
}
