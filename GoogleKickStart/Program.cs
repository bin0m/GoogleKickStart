using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoogleKickStart
{
    public class Solution
    {
        static void Solve()
        {
            int n = ReadInt();
            int q = ReadInt();
            int[] a = ReadIntArr();

            long ans = 0;

            int[,] times = new int[n, n];
            int[,] sums = new int[n, n];

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j <= i; j++)
                {
                    if (j == 0)
                    {
                        times[i, j] = a[i];
                    }
                    else
                    {
                        times[i, j] = times[i, j - 1] + a[i];
                    }

                }
            }

            for (int i = 0; i < n; i++)
            {
                short sign = 1;
                for (int j = i; j < n; j++)
                {
                    if (j == 0)
                    {
                        sums[i, j] = a[i];
                    }
                    else
                    {
                        sums[i, j] = sums[i, j - 1] + sign * times[j, j - i];
                    }
                    sign *= -1;
                }
            }

            for (int qi = 0; qi < q; qi++)
            {
                char type = ReadChar();
                if (type == 'U')
                {
                    int x = ReadInt() - 1;
                    int v = ReadInt();
                    int prev = a[x];
                    a[x] = v;

                    for (int j = 0; j <= x; j++)
                    {
                        if (j == 0)
                        {
                            times[x, j] = a[x];
                        }
                        else
                        {
                            times[x, j] = times[x, j - 1] + a[x];
                        }
                    }

                    for (int i = 0; i <= x; i++)
                    {
                        short sign = (short)Math.Pow(-1, x - i);
                        for (int j = x; j < n; j++)
                        {
                            if (j == 0)
                            {
                                sums[i, j] = a[i];
                            }
                            else
                            {
                                sums[i, j] = sums[i, j - 1] + sign * times[j, j - i];
                            }
                            sign *= -1;
                        }
                    }
                }
                else
                {
                    int l = ReadInt() - 1;
                    int r = ReadInt() - 1;
                    ans += sums[l, r];
                }
            }

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
            while(_tokens == null || _tokenEnumarator?.Current == null)
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

        public static void Main(string[] args)
        {
            int t = ReadInt();
            
            for (_caseNum = 1; _caseNum <= t; _caseNum++)
            {
                Solve();
            }

            Console.ReadLine();
        }        
    }
}
