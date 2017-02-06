using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfixToPostfixPractice
{
    public class InfixToPostfix
    {
        public InfixToPostfix(string input)
        {
            Queue<string> inputByPart = InputToQueue(input);
            Queue<string> result = InputToPostFix(inputByPart);
            Result = result.ToList();
        }

        /// <summary>
        /// 將輸入的資料轉成 特殊符號 或 連續數字 為一組，來存入 Queue
        /// </summary>
        private Queue<string> InputToQueue(string input)
        {
            Queue<string> result = new Queue<string>();
            string value = string.Empty;
            bool digitIsNumber = false;

            for (int i = 0; i < input.Count(); i++)
            {
                digitIsNumber = (input[i].IsNumber()) ? true : false;

                if (digitIsNumber == true)
                {
                    value += input[i];
                    continue;
                }

                if (value == string.Empty)
                {  // 發生情況為 第一個字元為特殊符號 或是 連續字元為特殊符號
                    result.Enqueue(input[i].ToString());
                }
                else
                {
                    result.Enqueue(value);
                    result.Enqueue(input[i].ToString());
                    value = string.Empty;
                }
            }
            result.Enqueue(value);
            return result;
        }

        /// <summary>
        /// 將 Input Queue 轉為 後序表示
        /// </summary>
        private Queue<string> InputToPostFix(Queue<string> input)
        {
            Queue<string> result = new Queue<string>();

            while (input.Count > 0)
            {
                string nextConteent = input.Peek();
                if (nextConteent == _parentheses[0])
                {      // 如果下個讀取內容是 (
                    result = ParenthesesToPostFix(input);
                }
                else
                {      // 如果下個讀取內容不是 (
                    result = NoParenthesesToPostFix(input);
                }
            }

            return result;
        }

        /// <summary>
        /// 將沒有引號的 Queue 轉成 PostFix
        /// </summary>
        private Queue<string> NoParenthesesToPostFix(Queue<string> input)
        {
            Queue<string> result = new Queue<string>();
            while (input.Count > 0)
            {
                string tmp = input.Dequeue();

                if (_operandsArithmetic.Contains(tmp))
                {
                    var tmpResult = NoParenthesesToPostFix(input);

                    // 將 Queue 加至另一個 Queue 後面
                    while (tmpResult.Count > 0)
                    {
                        result.Enqueue(tmpResult.Dequeue());
                    }
                }

                result.Enqueue(tmp);
            }
            return result;
        }

        private Queue<string> ParenthesesToPostFix(Queue<string> input)
        {
            Queue<string> result = new Queue<string>();
            while (input.Count > 0)
            {
                string tmp = input.Dequeue();

                if (_operandsArithmetic.Contains(tmp))
                {
                    var tmpResult = NoParenthesesToPostFix(input);

                    // 將 Queue 加至另一個 Queue 後面
                    while (tmpResult.Count > 0)
                    {
                        result.Enqueue(tmpResult.Dequeue());
                    }
                }

                result.Enqueue(tmp);
            }
            return result;
        }

        private List<string> _operandsArithmetic = new List<string> { "+", "-", "*", "/"};
        private List<string> _parentheses = new List<string> { "(", ")" };

        public List<string> Result { get; private set; } = new List<string>();
    }

    public static class MyExend
    {
        public static bool IsNumber(this char digit)
        {
            const char number0 = '0';
            const char number9 = '9';
            if (digit > number0 && digit < number9)
            {
                return true;
            }
            return false;
        }
    }
}
