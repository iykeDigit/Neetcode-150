using System;
using System.Collections.Generic;
using System.Linq;

namespace Stack
{
    class Program
    {
        static void Main(string[] args)
        {
            //var result = IsValid("()]");
            var stack = new MinStack();
            stack.Push(2);
            stack.Push(0);
            stack.Push(-3);
            
            stack.Pop();
            stack.Pop();
            stack.Top();
            stack.GetMin();

            Console.WriteLine("Hello World!");
        }

        /// <summary>
        /// Time: O(n)
        /// Space: O(n)
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsValid(string s)
        {
            var dict = new Dictionary<char, char>()
            {
                {']', '[' },
                {'}', '{' },
                {')', '(' }
            };

            var stack = new Stack<int>();
            foreach(var element in s)
            {
                if(!dict.ContainsKey(element))
                {
                    stack.Push(element);
                    continue;
                }

                if(stack.Count == 0 || stack.Peek() != dict[element])
                {
                    return false;
                }

                stack.Pop();
            }
            return stack.Count == 0;
        }

        public class MinStack
        {
            private Stack<int> stack = new Stack<int>();
            private Stack<int[]> minStack = new Stack<int[]>();
            public MinStack()
            {}

            public void Push(int val)
            {
                stack.Push(val);

                if(minStack.Count == 0 || val < minStack.Peek()[0])
                {
                    minStack.Push(new int[] { val, 1});
                }

                //If it's equal, incremement the count
                if(val == minStack.Peek()[0])
                {
                    minStack.Peek()[1]++;
                }

            }

            public void Pop()
            {
                if(minStack.Peek()[0] == stack.Peek())
                {
                    minStack.Peek()[1]--;
                }
                if(minStack.Peek()[1] == 0)
                {
                    minStack.Pop();
                }

                stack.Pop();
            }

            public int Top()
            {
                return stack.Peek();
            }

            public int GetMin()
            {
                return minStack.Peek()[0];
            }
        }
    }
}
