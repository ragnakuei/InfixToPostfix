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
            InputToQueue(input);
            InputToPostFix();
            ConvertToResult();
        }

        private void ConvertToResult()
        {
            Result.Clear();
            while (_store.Count > 0)
            {
                var tmp = _store.Dequeue();
                Result.Add(tmp);
            }
        }

        /// <summary>
        /// 將輸入的資料轉成 特殊符號 或 連續數字 為一組，來存入 Queue
        /// </summary>
        private void InputToQueue(string input)
        {
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
                    _input.Enqueue(input[i].ToString());
                }
                else
                {
                    _input.Enqueue(value);
                    _input.Enqueue(input[i].ToString());
                    value = string.Empty;
                }
            }
            _input.Enqueue(value);
        }

        /// <summary>
        /// 將 Input Queue 轉為 後序表示
        /// </summary>
        private void InputToPostFix()
        {
            while (_input.Count > 0)
            {
                // 如果下個讀取內容是 (

                // 如果下個讀取內容是 四則運算元

                // 下個讀取內容是 數字

                string tmp = _input.Dequeue();

                if (_operandsArithmetic.Contains(tmp))
                { InputToPostFix(); }

                _store.Enqueue(tmp);
            }
        }

        private List<string> _operandsArithmetic = new List<string> { "+", "*", "(", ")" };
        private Queue<string> _input = new Queue<string>();
        private Queue<string> _store = new Queue<string>();

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
