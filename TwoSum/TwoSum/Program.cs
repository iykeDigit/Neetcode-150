using System;

namespace TwoSum
{
    class Program
    {
        static void Main(string[] args)
        {
            var result = IsPalindromeRecur("A man, a plan, a canal: Panama");
            Console.WriteLine("Hello World!");
        }

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
            s.ToLower();
            int i = 0, j = s.Length - 1;
            if (i == j) return true;
            

            while (i < j && !char.IsLetterOrDigit(s[i])) i++;
            while (i < j && !char.IsLetterOrDigit(s[j])) j--;

            
            while(i != j)
            {
                if (s[i] != s[j]) return false;
                if (s[i] == s[j])
                {
                    IsPalindromeRecur((s.Substring(i + 1, j - 1)));
                }
            }
            return true;
        }
    }
}
