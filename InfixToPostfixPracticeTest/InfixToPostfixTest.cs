using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using InfixToPostfixPractice;
using System.Collections.Generic;
using ExpectedObjects;

namespace InfixToPostfixPracticeTest
{
    [TestClass]
    public class InfixToPostfixTest
    {
        [TestMethod]
        public void InfixToPostfix_1_add_2()
        {
            var input = "1+2";
            var expected = new List<string> {
                "1",
                "2",
                "+",
            };
            InfixToPostfix target = new InfixToPostfix(input);

            var actual = target.Result;

            expected.ToExpectedObject().ShouldEqual(actual);
        }
    }
}
