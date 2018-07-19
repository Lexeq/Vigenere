using System;
using System.Text;
using System.Collections.Generic;
using VigenereTools;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace VigenereToolsTests
{
    [TestClass]
    public class VigenereCipherTest
    {
        VigenereCipher cipher;

        [TestInitialize]
        public void VigenereCipherTestInitialize()
        {
            cipher = new VigenereCipher(Enumerable.Range('a', 26).Select(x => (char)x).ToArray());
        }

        [TestMethod]
        public void EncryptTest()
        {
            const string text = "attackatdawn";
            const string key = "lemon";
            const string expected = "lxfopvefrnhr";

            var result = cipher.Encrypt(text, key);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void DecryptTest()
        {
            const string text = "lxfopvefrnhr"; 
            const string key = "lemon";
            const string expected = "attackatdawn";

            var result = cipher.Decrypt(text, key);

            Assert.AreEqual(expected, result);
        }
    }
}
