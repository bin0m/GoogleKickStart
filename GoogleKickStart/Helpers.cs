using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoogleKickStart
{
    static class Helpers
    {
        static void Solve()
        {
            int n = ReadInt();
            int ans = 123;
            
            PrintCase();
            Console.WriteLine(ans);
        }
        
        
        static int _caseNum;
        static IEnumerable<string> _tokens;
        static IEnumerator<string> _tokenEnumarator;

        static void PrintCase()
        {
            Console.Write($"Case #{_caseNum}: ");
        }

        static void PrintCaseLine()
        {
            Console.WriteLine($"Case #{_caseNum}: ");
        }

        static int ReadInt()
        {
            return int.Parse(ReadNextToken());
        }

        static long ReadLong()
        {
            return long.Parse(ReadNextToken());
        }

        static string ReadString()
        {
            return ReadNextToken();
        }

        static char ReadChar()
        {
            return char.Parse(ReadNextToken());
        }

        static int[] ReadIntArr()
        {
            string s = Console.ReadLine().Trim();
            int[] a = (from v in s.Split(' ') select int.Parse(v)).ToArray();
            return a;
        }

        static long[] ReadLongArr()
        {
            string s = Console.ReadLine().Trim();
            long[] a = (from v in s.Split(' ') select long.Parse(v)).ToArray();
            return a;
        }

        static string ReadNextToken()
        {
            while (_tokens == null || _tokenEnumarator?.Current == null)
            {
                _tokens = Console.ReadLine().Trim().Split(' ');
                _tokenEnumarator = _tokens.GetEnumerator();
                _tokenEnumarator.MoveNext();
            }
            string token = _tokenEnumarator.Current;
            if (!_tokenEnumarator.MoveNext())
            {
                _tokenEnumarator.Dispose();
                _tokenEnumarator = null;
            }
            return token;
        }

        public static void RenameToMain(string[] args)
        {
            int t = ReadInt();

            for (_caseNum = 1; _caseNum <= t; _caseNum++)
            {
                Solve();
            }

            //Console.ReadLine();
        }

    }
}
