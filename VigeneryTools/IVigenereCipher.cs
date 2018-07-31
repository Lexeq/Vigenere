﻿namespace VigenereTools
{
    public interface IVigenereCipher
    {
        string Encrypt(string text, string key);

        string Decrypt(string text, string key);
    }
}
