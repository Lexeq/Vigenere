namespace VigenereTools.Hacks
{
    internal interface ICaesarBreaker
    {
        char[] Alphabet { get; }
        int GetShift(string text);
    }
}
