using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VigenereTools;

namespace VigenereToolsTests
{
    [TestClass]
    public class CaesarCipherTest
    {
        CaesarCipher caesar;

        [TestInitialize]
        public void CaesarCipherTestInitialize()
        {
            caesar = new CaesarCipher(Enumerable.Range('a', 26).Select(x=>(char)x).ToArray());
        }

        [TestMethod]
        public void EncryptTest()
        {
            const string text = "abcxyz";
            const string expected = "efgbcd";
            const int shift = 4;

            string result = caesar.Encrypt(text, shift);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void DecryptTest()
        {
            const string text = "abcdefg";
            const string expected = "xyzabcd";
            const int shift = 3;

            string result = caesar.Decrypt(text, shift);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ThrowIfUnexpectableChar()
        {
            const string text = "abcd5fg";
            const int shift = 0;

            Assert.ThrowsException<CipherException>(()=>caesar.Encrypt(text, shift));
        }
    }
}
