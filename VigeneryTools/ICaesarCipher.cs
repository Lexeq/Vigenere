namespace VigenereTools
{
    public interface ICaesarCipher
    {
        string Encrypt(string text, int shift);

        string Decrypt(string text, int shift);
    }
}
