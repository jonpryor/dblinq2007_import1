﻿#region MIT license
// 
// Copyright (c) 2007-2008 Jiri Moudry
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
// 
#endregion
using DbLinq.Util;
using DbLinq.Util.Language.Implementation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using NUnit.Framework;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace DbLinqTest
{


    /// <summary>
    ///This is a test class for EnglishWordsTest and is intended
    ///to contain all EnglishWordsTest Unit Tests
    ///</summary>
    [TestFixture]
    [TestClass]
    public class EnglishWordsTest
    {
        public EnglishWordsTest()
        {
         englishWords = new EnglishWords();
         englishWords.Load();
        }

        public static void AssertAreIListEqual(IList<string> a, IList<string> b)
        {
            Assert.AreEqual(b.Count, a.Count);
            for (int index = 0; index < a.Count; index++)
                Assert.AreEqual(b[index], a[index]);
        }

        public static void AssertAreEqual(IList<string> a, params string[] b)
        {
            AssertAreIListEqual(a, b);
        }

        /*
        hiredate
        quantityperunit
        unitsinstock
        fkterrregion
        fkprodcatg
        */

        private EnglishWords englishWords;

        [TestMethod]
        [Test]
        public void GetWords0Test()
        {
            var actual = englishWords.GetWords("helloworld");
            AssertAreEqual(actual, "hello", "world");
        }
        [TestMethod]
        [Test]
        public void GetWords1Test()
        {
            var actual = englishWords.GetWords("hiredate");
            AssertAreEqual(actual, "hire", "date");
        }
        [TestMethod]
        [Test]
        public void GetWords2Test()
        {
            var actual = englishWords.GetWords("quantityperunit");
            AssertAreEqual(actual, "quantity", "per", "unit");
        }
        [TestMethod]
        [Test]
        public void GetWords3Test()
        {
            var actual = englishWords.GetWords("unitsinstock");
            AssertAreEqual(actual, "units", "in", "stock");
        }
        // we can't rely on this test, since "terr" is not a word, so the algorithm returs "ft" "t" "err" "region"
        //[TestMethod]
        //[Test]
        //public void GetWords4Test()
        //{
        //    var actual = englishWords.GetWords("fkterrregion");
        //    AssertAreEqual(actual, "fk", "terr", "region");
        //}
        [TestMethod]
        [Test]
        public void GetWords5Test()
        {
            var actual = englishWords.GetWords("fkprodcatg");
            AssertAreEqual(actual, "fk", "prod", "cat", "g");
        }
        [TestMethod]
        [Test]
        public void GetWords6Test()
        {
            var actual = englishWords.GetWords("catg");
            AssertAreEqual(actual, "cat", "g");
        }

        [TestMethod]
        [Test]
        public void GetWords7Test()
        {
            var actual = englishWords.GetWords("customerid");
            AssertAreEqual(actual, "customer", "id");
        }

        [TestMethod]
        [Test]
        public void GetWords8Test()
        {
            var actual = englishWords.GetWords("supplierid");
            AssertAreEqual(actual, "supplier", "id");
        }

        [TestMethod]
        [Test]
        public void GetNote1Test()
        {
            Assert.IsTrue(englishWords.GetNote(new[] { "toothpaste" }) > englishWords.GetNote(new[] { "tooth", "paste" }));
        }

        [TestMethod]
        [Test]
        public void GetNote2Test()
        {
            Assert.IsTrue(englishWords.GetNote(new[] { "per", "unit" }) > englishWords.GetNote(new[] { "peru", "nit" }));
        }

        [TestMethod]
        [Test]
        public void GetNote3Test()
        {
            Assert.IsTrue(englishWords.GetNote(new[] { "hello" }) > englishWords.GetNote(new[] { "h", "e", "l", "l", "o" }));
        }
    }
}
