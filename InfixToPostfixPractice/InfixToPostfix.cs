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
            if(value != string.Empty) result.Enqueue(value);
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
                if (nextConteent == _leftParentheses)
                {      // 如果下個讀取內容是 (
                    var tmpResult = ParenthesesToPostFix(input);
                    QueueAdd(result, tmpResult);
                }
                else
                {      // 如果下個讀取內容不是 (
                    var tmpResult = NoParenthesesToPostFix(input);
                    QueueAdd(result, tmpResult);
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
                    QueueAdd(result, tmpResult);
                }

                result.Enqueue(tmp);
            }
            return result;
        }

        /// <summary>
        /// 將有引號的 Queue 轉成 PostFix
        /// </summary>
        private Queue<string> ParenthesesToPostFix(Queue<string> input)
        {
            string nextContent = input.Dequeue();
            Queue<string> noParenthesesResult = new Queue<string>();
            
            // 將 ( ) 中間的部份儲存下來，丟給 NoParenthesesToPostFix() 處理
            // 如果 ( ) 裡面還有 ( ，就遞迴處理
            while (nextContent != _rightParentheses)
            {
                nextContent = input.Peek();
                if (nextContent == _leftParentheses)
                {
                    var parenthesesResult = ParenthesesToPostFix(input);
                    QueueAdd(input, parenthesesResult);
                    nextContent = input.Peek();
                }
                
                noParenthesesResult.Enqueue(input.Dequeue());
                nextContent = input.Peek();
            }

            // 刪掉 )
            nextContent = input.Dequeue();

            //Queue<string> result = NoParenthesesToPostFix(noParenthesesResult);
            //return result;
            return NoParenthesesToPostFix(noParenthesesResult);
        }

        private static void QueueAdd(Queue<string> result, Queue<string> input)
        {
            while (input.Count > 0)
            {
                result.Enqueue(input.Dequeue());
            }
        }

        private List<string> _operandsArithmetic = new List<string> { "+", "-", "*", "/" };
        private const string _leftParentheses = "(";
        private const string _rightParentheses = ")";

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
