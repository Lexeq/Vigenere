using System;
using System.Linq;
using System.Text;

namespace VigenereTools
{
    static class StringExtensions
    {
        public static string[] Split(this string str, int parts)
        {
            if (str == null)
                throw new ArgumentNullException(nameof(str));
            if (parts <= 0)
                throw new ArgumentException("Parts count must be positive number.", nameof(parts));
            if (parts == 1)
                return new string[1] { str };

            string[] result = new string[parts];

            for (int i = 0; i < result.Length; i++)
            {
                StringBuilder builder = new StringBuilder();
                for (int j = i; j < str.Length; j += parts)
                {
                    builder.Append(str[j]);
                }
                result[i] = builder.ToString();
            }

            return result;
        }

        public static string Merge(this string[] strings)
        {
            if (strings == null)
                throw new ArgumentNullException(nameof(strings));

            var maxLength = strings.Max(s => s.Length);
            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < maxLength; i++)
            {
                for (int j = 0; j < strings.Length; j++)
                {
                    if (strings[j].Length > i)
                        builder.Append(strings[j][i]);
                }
            }
            return builder.ToString();
        }
    }
}
