namespace VigenereBreaker.Model
{
    internal static class Alphabets
    {
        public static char[] English { get { return "abcdefghijklmnopqrstuvwxyz".ToCharArray(); } }

        public static char[] Russian { get { return "абвгдеёжзийклмнопрстуфхцчшщъыьэюя".ToCharArray(); } }
    }
}
