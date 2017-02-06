using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using InfixToPostfixPractice;

namespace InfixToPostfixPracticeTest
{
    [TestClass]
    public class InfixToPostfixTest
    {
        [TestMethod]
        public void InfixToPostfix_1_add_2()
        {
            var input = "1+2";
            var expected = "12+";
            InfixToPostfix target = new InfixToPostfix(input);

            var actual = target.Result;

            Assert.AreEqual(expected, actual);
        }
    }
}
