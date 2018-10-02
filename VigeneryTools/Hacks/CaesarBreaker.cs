using System;
using System.Collections.Generic;
using System.Linq;

namespace VigenereTools.Hacks
{
    public class CaesarBreaker
    {
        private const double DefaultDeviation = 0.06;

        private readonly IDictionary<char, double> standartFrequency;

        private double maxDeviation;

        public double MaxDeviation
        {
            get { return maxDeviation; }
            set
            {
                if (value > 0)
                    maxDeviation = value;
                else
                    throw new ArgumentException("Value must be positive.", nameof(MaxDeviation));
            }
        }

        public char[] Alphabet
        {
            get
            {
                return standartFrequency.Keys.ToArray();
            }
        }

        public CaesarBreaker(IDictionary<char, double> frequency)
        {
            standartFrequency = frequency ?? throw new ArgumentNullException(nameof(frequency));
            MaxDeviation = DefaultDeviation;
        }

        public int GetShift(string text)
        {
            if (text == null)
                throw new ArgumentNullException(nameof(text));

            if (text.Length == 0)
                return 0;

            var alphabetSize = standartFrequency.Count;

            var currentFreq = text.GroupBy(x => x).ToDictionary(x => x.Key, x => x.Count() / (double)text.Length);
            int[] shifts = new int[alphabetSize];
            foreach (var standart in standartFrequency)
            {
                foreach (var current in currentFreq)
                {
                    if (!standartFrequency.ContainsKey(current.Key))
                        throw new CipherException($"Unexpectable character {current.Key}");

                    if (Math.Abs(standart.Value - current.Value) < MaxDeviation)
                        shifts[(current.Key - standart.Key + alphabetSize) % alphabetSize]++;
                }
            }
            var shift = shifts.Select((x, i) => new { Value = x, Index = i }).OrderBy(x => x.Value).Last().Index;
            return shift;
        }
    }
}