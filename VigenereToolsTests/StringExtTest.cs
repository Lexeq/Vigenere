using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace VigenereToolsTests
{
    [TestClass]
    public class StringExtTest
    {
        PrivateType stringExtensions;

        [TestInitialize]
        public void StringExtTestInitialize()
        {
            stringExtensions = new PrivateType("VigenereTools", "VigenereTools.StringExtensions");
        }

        #region SplitTests
        [TestMethod]
        public void SplitNullStringTest()
        {
            string text = null;
            int parts = 3;

            var func = new Func<string[]>(() => (string[])stringExtensions.InvokeStaticExt("Split", new object[2] { text, parts }));

            Assert.ThrowsException<ArgumentNullException>(func);
        }

        [TestMethod]
        public void SplitEmrtyStringTest()
        {
            string text = string.Empty;
            int parts = 3;

            var actual = (string[])stringExtensions.InvokeStaticExt("Split", new object[2] { text, parts });
            var expected = new[] { "", "", "" };

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void SplitIntoZeorPartsTest()
        {
            string text = "teststring";
            int parts = 0;

            var func = new Func<string[]>(() => (string[])stringExtensions.InvokeStaticExt("Split", new object[2] { text, parts }));

            Assert.ThrowsException<ArgumentException>(func);
        }

        [TestMethod]
        public void SplitIntoNegativePartsCountTest()
        {
            string text = "teststring";
            int parts = -3;

            var func = new Func<string[]>(() => (string[])stringExtensions.InvokeStaticExt("Split", new object[2] { text, parts }));

            Assert.ThrowsException<ArgumentException>(func);
        }

        [TestMethod]
        public void SplitIntoPartsCountMoreThenTextLengthTest()
        {
            string text = "abc";
            int parts = 5;

            var actual = (string[])stringExtensions.InvokeStaticExt("Split", new object[2] { text, parts });
            var expected = new[] { "a", "b", "c", "", "" };

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void SplitTest()
        {
            string text = "abc1abc2abc3ab";
            int parts = 4;

            var expected = new[] { "aaaa", "bbbb", "ccc", "123" };
            var actual = (string[])stringExtensions.InvokeStaticExt("Split", text, parts);

            CollectionAssert.AreEqual(expected, actual);
        }
        #endregion

        #region MergeTests
        [TestMethod]
        public void MergeTest()
        {
            var parts = new[] { "ad", "b", "ce" };

            string expected = "abcde";
            var actual = (string)stringExtensions.InvokeStatic("Merge", new object[] { parts });

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void MergeWithNullStringTest()
        {
            var parts = new[] { "ab", null, "ab" };

            var func = new Func<string>(() => (string)stringExtensions.InvokeStaticExt("Merge", new object[] { parts }));

            Assert.ThrowsException<NullReferenceException>(func);
        }

        [TestMethod]
        public void MergeEmptyStringsTest()
        {
            var parts = new[] { "", "", "" };

            var expected = string.Empty;
            var actual = (string)stringExtensions.InvokeStaticExt("Merge", new object[] { parts });

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void MergeNullArrayTest()
        {
            string[] parts = null;

            var func = new Func<string>(() => (string)stringExtensions.InvokeStaticExt("Merge", new object[] { parts }));

            Assert.ThrowsException<ArgumentNullException>(func);
        }

        [TestMethod]
        public void MergeEmptyArrayTest()
        {
            var parts = new string[] { };

            var actualc = (string)stringExtensions.InvokeStaticExt("Merge", new object[] { parts });
            var expected = string.Empty;

            Assert.AreEqual(expected, actualc);
        }

        [TestMethod]
        public void MergeWithEmptyStringTest()
        {
            var parts = new[] { "ab", "", "ab" };


            var expected = "aabb";
            var actual = (string)stringExtensions.InvokeStaticExt("Merge", new object[] { parts });

            Assert.AreEqual(expected, actual);
        }
        #endregion
    }
}
