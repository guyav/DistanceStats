using Microsoft.VisualStudio.TestTools.UnitTesting;
using DistanceCalculator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DistanceCalculator.Common.ExtensionMethods;

namespace DistanceCalculator.Tests
{
    [TestClass()]
    public class ProgramTests
    {
        [TestMethod()]
        public void IsStringHebrew_HebrewWithSpaces()
        {
            Assert.IsTrue("רמת גן".IsHebrew());
        }
        [TestMethod()]
        public void IsStringHebrew_HebrewWithNikud()
        {
            Assert.IsTrue("חוֹלוֹן".IsHebrew());
        }
        [TestMethod()]
        public void IsStringHebrew_HebrewWithSymbols()
        {
            Assert.IsTrue("בית ג'ן".IsHebrew());
        }
        [TestMethod()]
        public void IsStringHebrew_Regular()
        {
            Assert.IsTrue("גבעתיים".IsHebrew());
        }
        [TestMethod()]
        public void IsStringHebrew_NoHebrew()
        {
            Assert.IsFalse("Jerusalem".IsHebrew());
        }
        [TestMethod()]
        public void IsStringHebrew_HebrewWithEnglish()
        {
            Assert.IsFalse("Jerusalemירושלים".IsHebrew());
        }
    }
}