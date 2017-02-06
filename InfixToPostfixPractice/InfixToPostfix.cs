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
            if (value != string.Empty) result.Enqueue(value);
            return result;
        }

        /// <summary>
        /// 將 Input Queue 轉為 後序表示
        /// </summary>
        private Queue<string> InputToPostFix(Queue<string> input)
        {
            Stack<string> operand = new Stack<string>();
            Queue<string> result = new Queue<string>();

            string next = string.Empty;
            bool isHighOperand = false;
            while (input.Count > 0)
            {
                next = input.Peek();
                if (IsOperand(next) || IsLeftParentheses(next))
                {
                    if (IsHighOperand(next)) isHighOperand = true;
                    operand.Push(input.Dequeue());
                }
                else if (IsRightParentheses(next))
                {
                    input.Dequeue(); // 刪掉 )
                    if (IsOperand(operand.Peek())) result.Enqueue(operand.Pop());
                    operand.Pop();   // 刪掉 (
                }
                else
                {
                    result.Enqueue(input.Dequeue());
                    if (isHighOperand)
                    {
                        result.Enqueue(operand.Pop());
                        isHighOperand = false;
                    }
                }
            }

            while (operand.Count > 0)    // 要把最後的 operand 放至 result 中
            {
                result.Enqueue(operand.Pop());
            }

            return result;
        }


        private static void QueueAdd(Queue<string> result, Queue<string> input)
        {
            while (input.Count > 0)
            {
                result.Enqueue(input.Dequeue());
            }
        }

        private bool IsOperand(string input)
        {
            string[] operands = new string[] { "+", "-", "*", "/" };
            return operands.Contains(input);
        }

        private bool IsHighOperand(string input)
        {
            string[] operands = new string[] { "*", "/" };
            return operands.Contains(input);
        }

        private bool IsLeftParentheses(string input)
        {
            return input == "(";
        }

        private bool IsRightParentheses(string input)
        {
            return input == ")";
        }

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
