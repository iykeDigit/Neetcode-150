using System;
using System.Collections.Generic;
using System.Linq;

namespace SlidingWindow
{
    class Program
    {
        static void Main(string[] args)
        {
            var arr = new int[] { 1,3,-1,-3,5 };
            var strArr = new string[] { "a", "a", "a", "h", "h", "i", "b", "c" };
           // var str = "abcabcbb";
            var res = MaxSlidingWindow(arr, 3);

           
            Console.WriteLine("Hello World!");
        }

        public static int[] MaxSlidingWindow(int[] nums, int k)
        {
            var queue = new LinkedList<int>();
            var res = new List<int>();

            for (int i = 0; i < nums.Length; i++)
            {
                var window = i - k + 1;

                //check if the first value is part of the window
                while (queue.Count > 0 && queue.First.Value < window)
                {
                    queue.RemoveFirst();
                }

                var currentVal = nums[i];
                while (queue.Count > 0 && nums[queue.Last.Value] < currentVal)
                {
                    queue.RemoveLast();
                }

                //we're adding the index
                queue.AddLast(i);

                //we start adding to the result when i >= k-1
                if (i >= k - 1)
                {
                    res.Add(nums[queue.First.Value]);
                }
            }
            return res.ToArray();
        }

        /// <summary>
        /// Space: O(s + t)
        /// Time: O(n)
        /// </summary>
        /// <param name="s"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static string MinWindow(string s, string t)
        {
            if (t.Length > s.Length || t == "") return "";

            var dictT = new Dictionary<char, int>();
            var dictS = new Dictionary<char, int>();

            //populate dictT
            foreach(var c in t)
            {
                dictT[c] = 1 + dictT.GetValueOrDefault(c, 0);
            }

            //initialize the variables
            int left = 0, res = int.MaxValue, have = 0, need = dictT.Count;
            int[] arr = new int[] { 0, 0 };

            //iterate through S
            for(int i = 0; i < s.Length; i++)
            {
                var m = s[i];
                Console.WriteLine(m);
                dictS[m] = 1 + dictS.GetValueOrDefault(m, 0);

                if(dictT.ContainsKey(m) && dictT[m] == dictS[m])
                {
                    have++;
                }

                while(have == need)
                {
                    //compare window size
                    if(i-left+1 < res)
                    {
                        arr = new int[] { left, i };
                        res = i - left + 1;
                    }
                    dictS[s[left]]--;

                    if(dictT.ContainsKey(s[left]) && dictT[s[left]] > dictS[s[left]])
                    {
                        have--;
                    }
                    left++;
                }
            }
            return res != int.MaxValue ? s.Substring(arr[0], arr[1] - arr[0] + 1) : "";

        }

        public static string MinWindowAlt(string s, string t)
        {
            int s1 = s.Length, t1 = t.Length;
            if (t1 > s1)
                return string.Empty; //Edge case

            int[] Tarr = new int[58];  //58 = upper bound - lower bound of ascii chars ('z' = 122 and 'A' = 65)  
            int[] Sarr = new int[58];
            int i = 0, j = i, len = s1, res = int.MaxValue, startIndex = 0; //2 pointers i and j

            foreach (var c in t)
                Tarr[c - 'A']++;
            Sarr[s[0] - 'A']++;  //add the first char as min length of s is 1

            while (i < s1 && j < s1 && i <= j) //sliding window loop
            {
                if (SEqualsT(Tarr, Sarr))
                {
                    len = j - i + 1;
                    if (len < res)
                    {
                        res = len;
                        startIndex = i;
                    }
                    Sarr[s[i] - 'A']--;
                    ++i;
                }
                else
                {
                    if (j == s1 - 1)
                    {
                        Sarr[s[i] - 'A']--;
                        ++i;
                    }
                    else
                    {
                        ++j;
                        Sarr[s[j] - 'A']++;
                    }
                }
            }
            return res == int.MaxValue ? string.Empty : s.Substring(startIndex, res);
        }

        private static bool SEqualsT(int[] T, int[] S) //Does current window of S have all letters of T?
        {
            for (int i = 0; i < T.Length; i++)
            {
                if (T[i] > 0 && S[i] < T[i])
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Time: O(26) + O(n) = O(n)
        /// Space: O(26) * 2= O(1)
        /// </summary>
        /// <param name="s1"></param>
        /// <param name="s2"></param>
        /// <returns></returns>
        public static bool CheckInclusion(string s1, string s2)
        {
            if (s1.Length > s2.Length) return false;
            var arr1 = new int[26];
            var arr2 = new int[26];

            for (int i = 0; i < s1.Length; i++)
            {
                arr1[s1[i] - 'a']++;
                arr2[s2[i] - 'a']++;
            }

            int count = 0;
            for (int i = 0; i < 26; i++)
            {
                count = arr2[i] == arr1[i] ? ++count : count;
            }

            for (int i = 0; i < (s2.Length - s1.Length); i++)
            {
                var right = s2[i + s1.Length] - 'a';
                var left = s2[i] - 'a';

                if (count == 26) return true;

                arr2[right]++;
                count = arr2[right] == arr1[right] ? ++count : count;
                count = arr2[right] == arr1[right] + 1 ? --count : count;

                arr2[left]--;
                count = arr2[left] == arr1[left] ? ++count : count;
                count = arr2[left] == arr1[left] - 1 ? --count : count;
            }
            return count == 26;
        }

        /// <summary>
        /// Space: O(n)
        /// Time: O(n)
        /// </summary>
        /// <param name="s"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public static int CharacterReplacementOptimized(string s, int k)
        {
            int left = 0, maxf = 0, res = 0;
            var dict = new Dictionary<char, int>();
            for(int i = 0; i < s.Length; i++)
            {
                dict[s[i]] = dict.ContainsKey(s[i]) ? ++dict[s[i]] : 1;
                maxf = Math.Max(maxf, dict[s[i]]);

                //check if the no. of chars to replace is greater than k: windowsize - maxf
                if(i-left+1 - maxf > k)
                {
                    dict[s[left]]--;
                    left++;
                }
                res = Math.Max(res, i - left + 1);
            }
            return res;
        }
        public static int CharacterReplacement(string s, int k)
        {
            int left = 0, max = 0;

            var dict = new Dictionary<char, int>();
            for(int i = 0; i < s.Length; i++)
            {
                dict[s[i]] = dict.ContainsKey(s[i]) ? ++dict[s[i]] : 1;

                //check if the no of chars to replace is valid (windowSize - highest char count)
                if (i - left + 1 - dict.Values.Max() > k)
                {
                    dict[s[left]]--;
                    left++;
                }
                max = Math.Max(max, i - left + 1);
            }
            return max;
        }

        /// <summary>
        /// Time: O(n)
        /// Space: O(n)
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static int LengthOfLongestSubstring(string s)
        {
            int left = 0, max = 0;
            var set = new HashSet<char>();
            for(int i = 0; i < s.Length; i++)
            {
                if (!set.Add(s[i]))
                {
                    while(set.Contains(s[i]))
                    {
                        set.Remove(s[left]);
                        left++;
                    }
                    set.Add(s[i]);
                }
                max = Math.Max(i - left + 1, max);
            }
            return max;
        }

        /// <summary>
        /// Time: O(n)
        /// Space: Constant
        /// </summary>
        /// <param name="prices"></param>
        /// <returns></returns>
        public static int MaxProfit(int[] prices)
        {
            var left = 0;
            var max = int.MinValue;
            for (int i = 1; i < prices.Length; i++)
            {
                var currProfit = prices[i] - prices[left];
                Console.WriteLine(currProfit);
                max = Math.Max(max, prices[i] - prices[left]);
                left = prices[left] > prices[i] ? i : left;
            }
            return max < 0 ? 0 : max;
        }

        public static int FindMaxSumSubArray(int[] arr, int k)
        {
            var max = int.MinValue;
            var currSum = 0;

            for(int i = 0; i < arr.Length-1; i++)
            {
                currSum += arr[i];
                {
                    if(i+1 >= k)
                    {
                        max = Math.Max(max, currSum);
                        currSum -= arr[i - (k - 1)];
                    }
                }
            }
            return max;
        }

        public static int SmallestSubArray(int[] arr, int target)
        {
            int minWindowSize = int.MaxValue;
            int currentWindowSum = 0;
            int windowStart = 0;
            for(int windowEnd = 0; windowEnd < arr.Length; windowEnd++)
            {
                currentWindowSum += arr[windowEnd];

                while(currentWindowSum >= target)
                {
                    minWindowSize = Math.Min(minWindowSize, (windowEnd - windowStart) + 1);
                    currentWindowSum -= arr[windowStart];
                    windowStart++;
                }
            }
            return minWindowSize;
        }

        /// <summary>
        /// Time: O(n)
        /// Space: O(k+1)
        /// </summary>
        /// <param name="s"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public static int LongestSubstringWithKDistinct(string[] s, int k)
        {
            var dict = new Dictionary<string, int>();
            var start = 0;
            var max = 0;
            
            for(int i = 0; i < s.Length; i++)
            {
                if (dict.ContainsKey(s[i]))
                {
                    dict[s[i]]++;
                }
                else
                {
                    dict[s[i]] = 1;
                }

                while(dict.Count > k)
                {
                    dict[s[start]]--;
                    if(dict[s[start]] == 0)
                    {
                        dict.Remove(s[start]);
                    }
                    start++;
                }
                max = Math.Max(max, i - start + 1);

            }
            return max;
        }

        /// <summary>
        /// Time: O(n)
        /// Space: O(n)
        /// </summary>
        /// <param name="s"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public static int LengthOfLongestSubstringKDistinctOptimized(string[] s, int k)
        {
            int maxSize = 0;
            var dict = new Dictionary<string, int>();

            for(int i = 0; i < s.Length; i++)
            {
                if (dict.ContainsKey(s[i]))
                {
                    dict[s[i]]++;
                }
                else
                {
                    dict[s[i]] = 1;
                }

                if(dict.Count <= k)
                {
                    maxSize++;
                }
                else
                {
                    var leftChar = s[i - maxSize];
                    dict[leftChar]--;
                    if (dict[leftChar] == 0) dict.Remove(leftChar);
                }
            }
            return maxSize;
        }

        
    }
}
