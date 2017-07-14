using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeSolutions
{
    public class Solutions
    {
        //42. Trapping Rain Water
        public int Trap(int[] height)
        {
            int sum = 0;

            for(int i = 0; i < height.Length; i++)
            {
                int maxHeight = 0;
                int maxHeightIndex = 0;
                int maxHeightSum = 0;
                int heightSum = 0;
                int j;
                for (j = i + 1; j < height.Length; j++)
                {
                    if (height[j] < height[i])
                    {
                        heightSum += height[j];
                        if (height[j] >= maxHeight)
                        {
                            maxHeight = height[j];
                            maxHeightIndex = j;
                            maxHeightSum = heightSum;
                        }
                    }
                    else
                    {
                        maxHeightSum = heightSum; 
                        break;
                    }
                }

                if (j < height.Length)
                {
                    sum += height[i] * (j - i - 1) - maxHeightSum;

                    if (j == height.Length - 1)
                    {
                        break;
                    }

                    i = j - 1;
                }
                else
                {
                    sum += maxHeight * (maxHeightIndex - i - 1) - (maxHeightSum - maxHeight);

                    if(maxHeightIndex == height.Length - 1)
                    {
                        break;
                    }

                    i = maxHeightIndex - 1;
                }
            }

            return sum;
        }

        /// <summary>
        /// 43.大数乘法  
        /// </summary>
        /// <param name="num1"></param>
        /// <param name="num2"></param>
        /// <returns></returns>
        public string Multiply(string num1, string num2)
        {
            if (num1.Equals("0") || num2.Equals("0"))
            {
                return "0";
            }

            StringBuilder result = new StringBuilder();

            int mulFactor = 0;
            for(int i =  num2.Length - 1; i >= 0; i--)
            {
                int num2i = num2[i] - '0';

                if(num2i == 0)
                {
                    mulFactor++;
                }
                else
                {
                    StringBuilder stepiRes = Multiply(num1, num2i).Append('0', mulFactor++);
                    result = Add(result.ToString(), stepiRes.ToString());
                }
            }

            return result.ToString();
        }

        public StringBuilder Multiply(string num1, int num2)
        {
            if(num1.Equals('0') || num2 == 0)
            {
                return new StringBuilder(0);
            }

            StringBuilder result = new StringBuilder();
            int carry = 0;

            for(int i = num1.Length - 1; i >= 0; i--)
            {
                int num1i = num1[i] - '0';
                int total = num1i * num2 + carry;
                int extra = total % 10;
                carry = total / 10;
                result.Insert(0, extra);
            }

            if(carry > 0)
            {
                result.Insert(0, carry);
            }

            return result;
        }

        /// <summary>
        /// 43.大数加法
        /// </summary>
        /// <param name="num1"></param>
        /// <param name="num2"></param>
        /// <returns></returns>
        public StringBuilder Add(string num1, string num2)
        {
            if (num1.Equals("0"))
            {
                return new StringBuilder(num2);
            }
            else if(num2.Equals("0"))
            {
                return new StringBuilder(num1);
            }

            StringBuilder result = new StringBuilder();

            int carry = 0;
            int i, j;

            //计算公共长度相加
            for (i = num1.Length - 1, j = num2.Length - 1; i >= 0 && j >= 0; i--, j--)
            {
                int num1i = num1[i] - '0';
                int num2j = num2[j] - '0';
                int total = num1i + num2j + carry;
                int extra = total % 10;
                carry = total / 10;
                result.Insert(0, extra);
            }

            if (i >= 0 || j >= 0)
            {
                string leftString = i >= 0 ? num1.Substring(0, i + 1) : num2.Substring(0, j + 1);

                for (int l = leftString.Length - 1; l >= 0; l--)
                {
                    int leftl = leftString[l] - '0';

                    if (carry > 0)
                    {
                        int total = leftl + carry;
                        int extra = total % 10;
                        carry = total / 10;
                        result.Insert(0, extra);
                    }
                    else
                    {
                        result.Insert(0, leftString.Substring(0, l + 1));
                        break;
                    }
                }
            }

            if (carry > 0)
            {
                result.Insert(0, carry);
            }

            return result;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Solutions mySolutions = new Solutions();

            DateTime start = DateTime.Now;

            int result = mySolutions.Trap(new int[] { 5,4,1,2 });

            System.Console.WriteLine("计算结果：" + result);

            TimeSpan ts = DateTime.Now.Subtract(start);
            System.Console.WriteLine("消耗时间：" + ts.Milliseconds + "ms");
            System.Console.ReadKey();
        }
    }
}
