using InfixToPostfixPractice;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;

namespace InfixToPostfixPracticeTest.features
{
    [Binding]
    [Scope(Feature = "InfixToPostfix")]
    public class InfixToPostfixSteps
    {
        private InfixToPostfix target;

        [BeforeScenario]
        public void BeforeScenario()
        {
            this.target = new InfixToPostfix();
            
        }

        [Given(@"輸入 (.*)")]
        public void Given輸入(string input)
        {
            ScenarioContext.Current.Set<string>(input, "input");
        }
        
        [When(@"進行轉換後")]
        public void When進行轉換後()
        {
            var input = ScenarioContext.Current.Get<string>("input");
            string actual = this.target.GetResult(input);
            ScenarioContext.Current.Set<string>(actual, "actual");
        }

        [Then(@"結果為 (.*)")]
        public void Then結果為(string expected)
        {
            var actual = ScenarioContext.Current.Get<string>("actual");
            Assert.AreEqual(expected, actual);
        }
    }
}
