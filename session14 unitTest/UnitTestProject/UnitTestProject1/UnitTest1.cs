using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using ConsoleToTest;
using System.Collections.Generic;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Calculate classTobeTested = new Calculate();
            double result = classTobeTested.GetSuperPower(2, 3);
            Assert.AreEqual(16, result);
        }
        [TestMethod]
        public void TestMethod2()
        {
            Program Program = new Program();
            List<Person> actualList = Program.TestMethod();
            Assert.IsTrue(actualList.Count == 4);
        }

    }
}
