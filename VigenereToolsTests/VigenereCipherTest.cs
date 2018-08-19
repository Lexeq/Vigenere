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

        #region EncryptTests
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
        public void EncryptWithEmptyKeyTest()
        {
            var text = "aaaaa";
            var key = "";

            Assert.ThrowsException<ArgumentException>(()=>cipher.Encrypt(text, key));
        }

        [TestMethod]
        public void EncryptEmptyStringTest()
        {
            var text = "";
            var key = "test";

            Assert.AreEqual(text, cipher.Encrypt(text, key));
        }

        [TestMethod]
        public void EncryptNullStringTest()
        {
            string text = null;
            var key = "test";

            Assert.ThrowsException<ArgumentNullException>(() => cipher.Encrypt(text, key));
        }

        [TestMethod]
        public void EncryptWithNullKeyTest()
        {
            string text = "abc";
            string key = null;

            Assert.ThrowsException<ArgumentNullException>(() => cipher.Encrypt(text, key));
        }

        [TestMethod]
        public void EncryptWithLongKey()
        {
            string text = "abc";
            string key = "thiskeylengthmorethentextlength";

            var expected = "tik";
            var actual = cipher.Encrypt(text, key);

            Assert.AreEqual(expected, actual);
        }
        #endregion

        #region DecryptTests
        [TestMethod]
        public void DecryptTest()
        {
            const string text = "lxfopvefrnhr";
            const string key = "lemon";
            const string expected = "attackatdawn";

            var result = cipher.Decrypt(text, key);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void DecryptWithEmptyKeyTest()
        {
            var text = "aaaaa";
            var key = "";

            Assert.ThrowsException<ArgumentException>(() => cipher.Decrypt(text, key));
        }

        [TestMethod]
        public void DecryptEmptyStringTest()
        {
            var text = "";
            var key = "test";

            Assert.AreEqual(text, cipher.Decrypt(text, key));
        }

        [TestMethod]
        public void DecryptNullStringTest()
        {
            string text = null;
            var key = "test";

            Assert.ThrowsException<ArgumentNullException>(() => cipher.Decrypt(text, key));
        }

        [TestMethod]
        public void DecryptWithNullKeyTest()
        {
            string text = "abc";
            string key = null;

            Assert.ThrowsException<ArgumentNullException>(() => cipher.Decrypt(text, key));
        }

        [TestMethod]
        public void DecryptWithLongKey()
        {
            string text = "tik";
            string key = "thiskeylengthmorethentextlength";

            var expected = "abc";
            var actual = cipher.Decrypt(text, key);

            Assert.AreEqual(expected, actual);
        }
        #endregion
    }
}
