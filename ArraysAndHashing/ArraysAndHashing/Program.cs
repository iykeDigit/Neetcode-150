using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArraysAndHashing
{
    class Program
    {
        static void Main(string[] args)
        {
            var arr = new string[] { "eat", "tea", "tan", "ate", "nat", "bat" };
            var nums = new int[] { 100, 4, 200, 1, 3, 2, 101, 102, 103, 104, 105 };
            char[][] jArray = new char[9][];
            jArray[0] = new char[9] { '5', '3', '.', '.', '.', '.', '.', '.', '.', };
            jArray[1] = new char[9] { '6', '.', '.', '1', '9', '5', '.', '.', '.', };
            jArray[2] = new char[9] { '.', '9', '8', '.', '.', '.', '.', '6', '.', };
            jArray[3] = new char[9] { '8', '.', '.', '.', '6', '.', '.', '.', '3', };
            jArray[4] = new char[9] { '4', '.', '.', '8', '.', '3', '.', '.', '1', };
            jArray[5] = new char[9] { '7', '.', '.', '.', '2', '.', '.', '.', '6', };
            jArray[6] = new char[9] { '.', '6', '.', '.', '.', '.', '2', '8', '.', };
            jArray[7] = new char[9] { '.', '.', '.', '4', '1', '9', '.', '.', '5', };
            jArray[8] = new char[9] { '.', '.', '.', '.', '7', '.', '.', '.', '9', };


            //var x = IsValidSudoku(jArray);
            var codec = new Codec();
            var msg = codec.Encode(new List<string> { "", "4 " });
            var result = codec.Decode(msg);
            var x = LongestConsecutive(nums);
            Console.WriteLine("Hello World!");
        }

        public static int LongestConsecutive(int[] nums)
        {
            var set = new HashSet<int>(nums);
            var max = 0;
            foreach(var item in set)
            {
                if(set.Contains(item-1)) continue;
                else
                {
                    var count = 1;
                    while(set.Contains(item + count))
                    {
                        count++;
                    }
                    max = Math.Max(max, count);
                }
            }
            return max;
        }


        public class Codec
        {
            public string Encode(IList<string> strs)
            {
                var sb = new StringBuilder();
                var delimiter = (char)257;
                foreach (var element in strs)
                {
                    sb.Append(element);
                    sb.Append(delimiter);
                }
                var result = sb.ToString();
                return result.Substring(0, result.Length - 1);
            }

            // Decodes a single string to a list of strings.
            public IList<string> Decode(string s)
            {
                var str = s.Split((char)257);
                return str.ToList();
            }
        }

        public static bool IsValidSudoku(char[][] board)
        {
            var row = new HashSet<int>[9];
            var col = new HashSet<int>[9];
            var box = new HashSet<int>[9];

            for (int i = 0; i < 9; i++)
            {
                row[i] = new HashSet<int>();
                col[i] = new HashSet<int>();
                box[i] = new HashSet<int>();
            }

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    var item = board[i][j];
                    if (item == '.') continue;
                    if(item != '.')
                    {
                        if (row[i].Contains(item)) return false;
                        else
                        {
                            row[i].Add(item);
                        }

                        if (col[j].Contains(item)) return false;
                        else
                        {
                            col[j].Add(item);
                        }

                        var idx = (i / 3 * 3) + (j / 3);
                        if (box[idx].Contains(item)) return false;
                        else
                        {
                            box[idx].Add(item);
                        }
                    }

                    
                }
            }
            return true;
        }


        /// <summary>
        /// Time: O(n)
        /// Space:O(n)
        /// </summary>
        /// <param name="nums"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public static int[] TopKFrequent(int[] nums, int k)
        {
            var dict = new Dictionary<int, int>();
            var max = 0;
            foreach (var element in nums)
            {
                if (dict.ContainsKey(element))
                {
                    dict[element]++;
                }
                else
                {
                    dict[element] = 1;
                }
                max = Math.Max(max, dict[element]);
            }

            var group = new Dictionary<int, List<int>>();
            foreach (var item in dict)
            {
                if (!group.ContainsKey(item.Value))
                {
                    group[item.Value] = new List<int> { item.Key };
                }
                else
                {
                    group[item.Value].Add(item.Key);
                }
            }




            var finalResult = new int[k];
            var index = 0;

            for (int i = max; i > 0; i--)
            {
                if (!group.ContainsKey(i)) continue;
                if (group.ContainsKey(i))
                {
                    foreach (var element in group[i])
                    {
                        if (index < k)
                        {
                            finalResult[index] = element;
                            index++;
                        }
                        else
                        {
                            return finalResult;
                        }
                    }
                }

            }

            return finalResult;
        }

        /// <summary>
        /// Time: O(NK) => N is the number of elements in the array
        ///                K is the number of chars in each element in the string
        /// Space: O(N)
        /// </summary>
        /// <param name="strs"></param>
        /// <returns></returns>
        public static IList<IList<string>> GroupAnagrams(string[] strs)
        {
            var dict = new Dictionary<string, List<string>>();
            foreach (var item in strs)
            {
                var arr = new int[26];
                foreach (var element in item)
                {
                    arr[element - 'a']++;
                }

                var sb = new StringBuilder();
                foreach (var num in arr)
                {
                    sb.Append(num);
                    sb.Append("#");
                }

                string key = sb.ToString();

                if (dict.ContainsKey(key))
                {
                    dict[key].Add(item);
                }
                else
                {
                    dict[key] = new List<string> { item };
                }
            }

            var result = new List<IList<string>>();
            foreach (var list in dict.Values)
            {
                result.Add(list);
            }
            return result;
        }

        public bool ContainsDuplicate(int[] nums)
        {
            var set = new HashSet<int>();
            foreach (var item in nums)
            {
                if (!set.Add(item)) return true;
            }
            return false;
        }

        /// <summary>
        /// Time: O(n)
        /// Space: O(1)
        /// </summary>
        /// <param name="s"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static bool IsAnagram(string s, string t)
        {
            if (s.Length != t.Length) return false;

            var firstArr = new int[26];
            var secondArr = new int[26];

            for (int i = 0; i < s.Length; i++)
            {
                firstArr[s[i] - 'a']++;
                secondArr[t[i] - 'a']++;
            }

            for (int i = 0; i < firstArr.Length; i++)
            {
                if (firstArr[i] != secondArr[i])
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// time: O(n)
        /// space: O(n)
        /// </summary>
        /// <param name="nums"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public int[] TwoSum(int[] nums, int target)
        {
            int i = 0;
            var dict = new Dictionary<int, int>();
            foreach (var item in nums)
            {
                int complement = target - item;
                if (dict.ContainsKey(complement))
                {
                    return new int[] { dict[complement], i };
                }
                else
                {
                    dict[complement] = i;
                    i++;
                }
            }
            return new int[] { 0, 0 };
        }

        
    }
}
