using System.Collections.Generic;
using VigenereTools.Hacks;

namespace VigenereBreaker.Model
{
    internal static class LanguagesList
    {
        public static IList<Language> Languages { get; }

        static LanguagesList()
        {
            //english
            var engCaesar = new VigenereTools.LatinCaesarCipher();
            var engVigenere = new VigenereTools.VigenereCipher(Alphabets.English);
            var engCaesarBreaker = new CaesarBreaker(CharFrequecies.English);
            var engVigenereBreaker = new VigenereTools.Hacks.VigenereBreaker(engCaesar, engCaesarBreaker, 0.0644);
            var eng = new Language("English", Alphabets.English, engVigenere, engVigenereBreaker, engCaesarBreaker);

            //russian
            var rusCaesar = new VigenereTools.CaesarCipher(Alphabets.Russian);
            var rusVigenere = new VigenereTools.VigenereCipher(Alphabets.Russian);
            var rusCaesarBreaker = new CaesarBreaker(CharFrequecies.Russian);
            var rusVigenereBreaker = new VigenereTools.Hacks.VigenereBreaker(rusCaesar, rusCaesarBreaker, 0.0553);
            var rus = new Language("Russian", Alphabets.Russian, rusVigenere, rusVigenereBreaker, rusCaesarBreaker);

            Languages = new Language[2]
            {
                eng, rus
            };
        }
    }
}
