using System;
using System.Collections.Generic;

namespace TwoSum
{
    class Program
    {
        static void Main(string[] args)
        {
            //var result = IsPalindromeAscii("A man, a plan, a canal: Panama");
            var arr = new int[] { 0, 1, 0, 2, 1, 0, 1, 3, 2, 1, 2, 1 };
            var result = Trap(arr);
            Console.WriteLine("Hello World!");
        }

        public static  int Trap(int[] height)
        {
            var left = 0;
            var right = height.Length - 1;
            var leftMax = height[left];
            var rightMax = height[right];
            var res = 0;
            while (left < right)
            {
                if (leftMax < rightMax)
                {
                    left++;
                    leftMax = Math.Max(leftMax, height[left]);
                    res += leftMax - height[left];
                }
                else
                {
                    right--;
                    rightMax = Math.Max(rightMax, height[right]);
                    res += rightMax - height[right];
                }
            }
            return res;
        }

        


        /// <summary>
        /// Time: O(n)
        /// Space: constant
        /// </summary>
        /// <param name="height"></param>
        /// <returns></returns>
        public static int MaxArea(int[] height)
        {
            int left = 0, right = height.Length - 1;
            int maxArea = int.MinValue;

            while(left < right)
            {
                var currentArea = Math.Min(height[left], height[right]) * (right - left);
                maxArea = Math.Max(maxArea, currentArea);
                if (height[left] < height[right]) left++;
                else right--;
            }
            return maxArea;
        }



        #region TwoSum Recursive
        public static IList<IList<int>> fourSum(int[] nums, int target)
        {
            Array.Sort(nums);
            return kSum(nums, target, 0, 4);
        }

        public static IList<IList<int>> kSum(int[] nums, long target, int start, int k)
        {
            List<IList<int>> res = new List<IList<int>>();

            if (start == nums.Length)
            {
                return res;
            }

            long average_value = target / k;

            if (nums[start] > average_value || average_value > nums[nums.Length - 1])
            {
                return res;
            }

            if (k == 2)
            {
                return twoSum(nums, target, start);
            }

            for (int i = start; i < nums.Length; ++i)
            {
                if (i == start || nums[i - 1] != nums[i])
                {
                    foreach (List<int> subset in kSum(nums, target - nums[i], i + 1, k - 1))
                    {
                        List<int> newList = new List<int>();
                        newList.Add(nums[i]);
                        newList.AddRange(subset);
                        res.Add(newList);
                    }
                }
            }

            return res;
        }

        public static IList<IList<int>> twoSum(int[] nums, long target, int start)
        {
            List<IList<int>> res = new List<IList<int>>();
            int lo = start, hi = nums.Length - 1;

            while (lo < hi)
            {
                int currSum = nums[lo] + nums[hi];
                if (currSum < target || (lo > start && nums[lo] == nums[lo - 1]))
                {
                    ++lo;
                }
                else if (currSum > target || (hi < nums.Length - 1 && nums[hi] == nums[hi + 1]))
                {
                    --hi;
                }
                else
                {
                    res.Add(new List<int> { nums[lo++], nums[hi--] });
                }
            }

            return res;
        }
        #endregion


        //public static IList<IList<int>> twoSum(int[] nums, long target, int start)
        //{
        //    List<IList<int>> res = new List<IList<int>>();
        //    HashSet<long> s = new HashSet<long>();

        //    for (int i = start; i < nums.Length; ++i)
        //    {
        //        if (res.Count == 0 || res[res.Count - 1][1] != nums[i])
        //        {
        //            if (s.Contains(target - nums[i]))
        //            {
        //                List<int> newList = new List<int>();
        //                newList.Add((int)target - nums[i]);
        //                newList.Add(nums[i]);
        //                res.Add(newList);
        //            }
        //        }
        //        s.Add((long)nums[i]);
        //    }

        //    return res;
        //}

        public static IList<IList<int>> FourSum(int[] nums, int target)
        {
            var list = new List<IList<int>>();
            if (nums.Length < 4) return list;
            Array.Sort(nums);
            long val = (long)(int)target;
            for (int i = 0; i < nums.Length - 3; i++)
            {
                if (i == 0 || (i > 0 && nums[i] != nums[i - 1]))
                {
                    for (int j = i + 1; j < nums.Length - 2; j++)
                    {
                        if (j == i + 1 || (j > i + 1 && nums[j] != nums[j - 1]))
                        {
                            var left = j + 1;
                            var right = nums.Length - 1;
                            var sum = val - (nums[i] + nums[j]);
                            while (left < right)
                            {
                                if (target == nums[left] + nums[right])
                                {
                                    list.Add(new List<int> { nums[i], nums[j], nums[left], nums[right] });
                                    while (left < right && nums[left] == nums[left + 1]) left++;
                                    while (left < right && nums[right] == nums[right - 1]) right--;
                                    left++;
                                    right--;
                                }
                                else if (target < nums[left] + nums[right])
                                {
                                    left++;
                                    while (left < right && nums[left] == nums[left - 1]) left++;
                                }
                                else
                                {
                                    left++;
                                    while (left < right && nums[right] == nums[right + 1]) right--;
                                }
                            }
                        }
                    }
                }
            }
            return list;
        }

        /// <summary>
        /// Time: O(nLogn) + O(n2)
        /// Space: O(1) or O(n)
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static IList<IList<int>> ThreeSum(int[] nums)
        {
            Array.Sort(nums);
            var result = new List<IList<int>>();

            for (int i = 0; i < nums.Length - 2; i++)
            {
                if(i == 0 || (i > 0 && nums[i] != nums[i - 1]))
                {
                    int left = i+1;
                    int right = nums.Length - 1;
                    int target = 0 - nums[i];

                    while(left < right)
                    {
                        if (target == nums[left] + nums[right])
                        {
                            result.Add(new List<int> { nums[left], nums[right], nums[i] });
                            while (left < right && nums[left] == nums[left + 1]) left++;
                            while (left < right && nums[right] == nums[right - 1]) right--;
                            left++;
                            right--;
                        }
                        else if (nums[left] + nums[right] > target)
                        {
                            right--;
                        }
                        else if (nums[left] + nums[right] < target)
                        {
                            left++;
                        }
                    }
                }
            }
            return result;
                
        }

        /// <summary>
        /// Time: 0(n)
        /// Space: 0(1)
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsPalindrome(string s)
        {
            if (s.Length < 0) return true;
            s = s.ToLower();

            for(int i = 0, j = s.Length-1; i < j; i++, j--)
            {
                while(i < j && !char.IsLetterOrDigit(s[i]))
                {
                    i++;
                }

                while(i < j && !char.IsLetterOrDigit(s[j]))
                {
                    j--;
                }

                if (s[i] != s[j]) return false;
            }

            return true;
        }

        public static bool IsPalindromeAscii(string s)
        {
            if (s.Length < 0) return true;
            s = s.ToLower();

            for (int i = 0, j = s.Length - 1; i < j; i++, j--)
            {
                while (i < j && !CheckAlphaNumeric(s[i]))
                {
                    i++;
                }

                while (i < j && !CheckAlphaNumeric(s[j]))
                {
                    j--;
                }

                if (s[i] != s[j]) return false;
            }

            return true;
        }

        public static bool CheckAlphaNumeric(char m)
        {
            var asciiVal = (int)m;

            Console.WriteLine(asciiVal);


            if((asciiVal >= (int)'0' && asciiVal <= (int)'9') 
                || (asciiVal >= (int)'a' && asciiVal <= (int)'z'))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool IsPalindromeRecur(string s, int i)
        {
            s.ToLower();
            if (i >= s.Length / 2) return true;
            int j = s.Length - 1;

            while (i < j && !char.IsLetterOrDigit(s[i])) i++;
            while (i < j && !char.IsLetterOrDigit(s[j])) j--;

            if (i == j) return true;
            if (s[i] != s[j]) return false;
            if(s[i] == s[j])
            {
                IsPalindromeRecur((s.Substring(i+1, j-1)), i);
            }
            return true;
        }

        public static bool IsPalindromeRecur(string s)
        {
            s= s.ToLower();
            Console.WriteLine(s);
            Console.WriteLine(s.Length);
            int i = 0, j = s.Length - 1;
            if (i == j) return true;

            Console.WriteLine(i);
            Console.WriteLine(j);

            while (i < j && !char.IsLetterOrDigit(s[i])) i++;
            while (i < j && !char.IsLetterOrDigit(s[j])) j--;

            Console.WriteLine(i);
            Console.WriteLine(j);
            
            
            while(i != j)
            {
                Console.WriteLine(s[i]);
                Console.WriteLine(s[j]);
                Console.WriteLine();
                if (s[i] != s[j]) return false;
                if (s[i] == s[j])
                {
                    IsPalindromeRecur((s.Substring(i, s.Length-(i+1))));
                }
            }
            return true;
        }

        /// <summary>
        /// Time: O(n)
        /// Space: O(1)
        /// </summary>
        /// <param name="numbers"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public int[] TwoSum(int[] numbers, int target)
        {
            int left = 0, right = numbers.Length - 1;

            while (left < right)
            {
                var sum = numbers[left] + numbers[right];
                if (sum == target)
                {
                    return new int[] { left + 1, right + 1 };
                }
                else if (sum > target)
                {
                    right--;
                }
                else
                {
                    left++;
                }
            }

            return new int[2];
        }
    }
}
