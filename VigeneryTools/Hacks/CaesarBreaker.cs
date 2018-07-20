using System;
using System.Collections.Generic;
using System.Linq;

namespace VigenereTools.Hacks
{
    internal class CaesarBreaker
    {
        private const double DefaultDeviation = 0.06;

        private static readonly Dictionary<char, double> standartFrequency = new Dictionary<char, double>()
        {
            {'a', 0.08167},
            {'b', 0.01492},
            {'c', 0.02782},
            {'d', 0.04253},
            {'e', 0.12702},
            {'f', 0.02228},
            {'g', 0.02015},
            {'h', 0.06094},
            {'i', 0.06966},
            {'j', 0.00153},
            {'k', 0.00772},
            {'l', 0.04025},
            {'m', 0.02406},
            {'n', 0.06749},
            {'o', 0.07507},
            {'p', 0.01929},
            {'q', 0.00095},
            {'r', 0.05987},
            {'s', 0.06327},
            {'t', 0.09056},
            {'u', 0.02758},
            {'v', 0.00978},
            {'w', 0.02360},
            {'x', 0.00150},
            {'y', 0.01974},
            {'z', 0.00074}
        };

        private double maxDeviation;

        public double MaxDeviation
        {
            get { return maxDeviation; }
            set
            {
                if (value > 0)
                    maxDeviation = value;
                else
                    throw new ArgumentException($"{nameof(MaxDeviation)} value must be positive.");
            }
        }

        public CaesarBreaker()
        {
            MaxDeviation = DefaultDeviation;
        }

        public int GetShift(string text)
        {
            if (text == null)
                throw new ArgumentNullException(nameof(text));

            var currentFreq = text.GroupBy(x => x).ToDictionary(x => x.Key, x => x.Count() / (double)text.Length);
            int[] shifts = new int[26];
            foreach (var standart in standartFrequency)
            {
                foreach (var current in currentFreq)
                {
                    if (Math.Abs(standart.Value - current.Value) < MaxDeviation)
                        shifts[(current.Key - standart.Key + 26) % 26]++;
                }
            }
            var shift = shifts.Select((x, i) => new { Value = x, Index = i }).OrderBy(x => x.Value).Last().Index;
            return shift;
        }
    }
}