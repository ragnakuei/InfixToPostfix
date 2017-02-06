using System.Collections.Generic;
using System.Linq;

namespace InfixToPostfixPractice
{
    public class InfixToPostfix
    {
        public InfixToPostfix() { }

        public string GetResult(string input)
        {
            Queue<string> inputByPart = InputToQueue(input);
            Queue<string> postfix = InputToPostFix(inputByPart);
            var result = string.Concat(postfix.ToList());
            return result;
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
        private Queue<string> InputToPostFix(Queue<string> input, int level = 0)
        {
            Stack<string> operand = new Stack<string>();
            Queue<string> result = new Queue<string>();

            string next = string.Empty;
            while (input.Count > 0)
            {
                next = input.Peek();
                if (IsLeftParentheses(next))
                {
                    input.Dequeue();
                    var tmpResult = InputToPostFix(input, level + 1);

                    foreach (var item in tmpResult)
                    {
                        result.Enqueue(item);
                    }

                    // 處理完 (
                    continue;
                }

                if (IsOperand(next))
                {
                    // 先乘除後加減的判斷
                    int previousPriority = operand.Count > 0
                        ? GetOperandPriority(operand.Peek())
                        : 0;
                    int nextPriority = GetOperandPriority(next);

                    if (previousPriority > nextPriority)
                    {
                        result.Enqueue(operand.Pop());
                        operand.Push(input.Dequeue());
                    }
                    else
                    {
                        operand.Push(input.Dequeue());
                    }
                }
                else if (IsRightParentheses(next))
                {
                    input.Dequeue(); // 刪掉 )
                    if (operand.Count <= 0) break;   // 為了 (( )) 而存在的判斷
                    if (IsOperand(operand.Peek())) result.Enqueue(operand.Pop());
                    break;
                }
                else
                {
                    result.Enqueue(input.Dequeue());
                }
            }

            while (operand.Count > 0)    // 要把最後的 operand 放至 result 中
            {
                result.Enqueue(operand.Pop());
            }

            return result;
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

        private int GetOperandPriority(string input)
        {
            int result = 0;
            _operandPriority.TryGetValue(input, out result);
            return result;
        }

        private Dictionary<string, int> _operandPriority = new Dictionary<string, int> {
            {"+",1 },
            {"-",1 },
            {"*",2 },
            {"/",2 },
        };
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
