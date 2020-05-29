using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GoogleKickStart
{
    public class Solution
    {
        public static void Main(string[] args)
        {
            int t;
            string s = Console.ReadLine().Trim();
            t = int.Parse(s);

            for (int ti = 1; ti <= t; ti++)
            {
                s = Console.ReadLine().Trim();
                var ss = s.Split(' ');
                int n = int.Parse(ss[0]);
                int k = int.Parse(ss[1]);

                s = Console.ReadLine().Trim();
                int[] a = (from v in s.Split(' ') select int.Parse(v)).ToArray();

                Console.Write($"Case #{ti}: ");

                int count = 0;



                for (int i = n - 1; i >= 0; i--)
                {
                    if (a[i] == 1)
                    {
                        i--;
                        int nextCount = 2;
                        while (i >= 0 && nextCount <= k && a[i] == nextCount)
                        {
                            nextCount++;
                            i--;
                        }
                        if (nextCount - 1 == k)
                        {
                            count++;
                        }
                        i++;
                    }
                }

                Console.WriteLine(count);
            }
        }

        /*
         input:
         5 4
         1 3 9 8 2
         Q 2 4
         Q 5 5
         U 2 10
         Q 1 2   */
        static void TaskD_Candies()
        {
            int t;
            string s = Console.ReadLine().Trim();
            t = int.Parse(s);

            for (int ti = 1; ti <= t; ti++)
            {
                s = Console.ReadLine().Trim();
                var ss = s.Split(' ');
                int n = int.Parse(ss[0]);
                int q = int.Parse(ss[1]);
                s = Console.ReadLine().Trim();
                int[] a = (from v in s.Split(' ') select int.Parse(v)).ToArray();

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
                    s = Console.ReadLine().Trim();
                    ss = s.Split(' ');
                    char type = char.Parse(ss[0]);
                    if (type == 'U')
                    {
                        int x = int.Parse(ss[1]) - 1;
                        int v = int.Parse(ss[2]);
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
                        int l = int.Parse(ss[1]) - 1;
                        int r = int.Parse(ss[2]) - 1;
                        ans += sums[l, r];
                    }
                }

                Console.Write($"Case #{ti}: ");

                Console.WriteLine(ans);
            }
        }
    }
}
