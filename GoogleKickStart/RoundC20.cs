using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleKickStart
{
    class RoundC20
    {
        /*
          input:
          12 3
          1 2 3 7 9 3 2 1 8 3 2 1 */
        static void TaskA_Countdown()
        {
            int n = ReadInt();
            int k = ReadInt();

            int[] a = ReadIntArr();

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

            PrintCase();
            Console.WriteLine(count);
        }


        /*
            input:
            4 6
            ZOAAMM
            ZOAOMM
            ZOOOOM
            ZZZZOM  */
        static void TaskB_StableWall()
        {
            int r = ReadInt();
            int c = ReadInt();

            string[] rows = new string[r];

            for (int row = r - 1; row >= 0; row--)
            {
                rows[row] = Console.ReadLine().Trim();
            }

            PrintCase();

            var nodes = new HashSet<char>();
            var edges = new HashSet<Tuple<char, char>>();

            for (int row = 0; row < r; row++)
            {
                for (int col = 0; col < c; col++)
                {
                    char node = rows[row][col];
                    if (!nodes.Contains(node))
                    {
                        nodes.Add(node);
                    }

                    // add edges
                    if (row > 0 && rows[row - 1][col] != node)
                    {
                        char down = rows[row - 1][col];
                        var edge = new Tuple<char, char>(down, node);
                        if (!edges.Contains(edge))
                        {
                            edges.Add(edge);
                        }
                    }
                }
            }

            var sorted = TopologicalSort(nodes, edges);

            string ans;

            if (sorted != null)
            {
                ans = new string(sorted.ToArray());
            }
            else
            {
                ans = "-1";
            }

            Console.WriteLine(ans);
        }

        // Helper to TaskB_StableWall
        static List<char> TopologicalSort(HashSet<char> nodes, HashSet<Tuple<char, char>> edges)
        {
            // Empty list that will contain the sorted elements
            var L = new List<char>();

            // Set of all nodes with no incoming edges
            var S = new HashSet<char>(nodes.Where(n => edges.All(e => e.Item2.Equals(n) == false)));

            // while S is non-empty do
            while (S.Any())
            {
                //  remove a node n from S
                var n = S.First();
                S.Remove(n);

                // add n to tail of L
                L.Add(n);

                // for each node m with an edge e from n to m do
                foreach (var e in edges.Where(e => e.Item1.Equals(n)).ToList())
                {
                    var m = e.Item2;

                    // remove edge e from the graph
                    edges.Remove(e);

                    // if m has no other incoming edges then
                    if (edges.All(me => me.Item2.Equals(m) == false))
                    {
                        // insert m into S
                        S.Add(m);
                    }
                }
            }

            // if graph has edges then
            if (edges.Any())
            {
                // return error (graph has at least one cycle)
                return null;
            }
            else
            {
                // return L (a topologically sorted order)
                return L;
            }
        }


        /*
          input:
          5
          30 30 9 1 30  */
        static void TaskC_PerfectSbarray()
        {
            int t = ReadInt();

            var hs = new HashSet<int>();
            int upper = (int)Math.Sqrt(100 * 10000);
            if (t > 5)
            {
                upper = (int)Math.Sqrt(100 * 1000);
            }
            for (int i = 0; i <= upper; i++)
            {
                hs.Add(i * i);
            }

            for (_caseNum = 1; _caseNum <= t; _caseNum++)
            {
                int n = ReadInt();
                int[] a = ReadIntArr();
                
                int ans = 0;

                for (int i = 0; i < n; i++)
                {
                    int sum = 0;
                    for (int j = i; j < n; j++)
                    {
                        sum += a[j];
                        if (hs.Contains(sum))
                        {
                            ans++;
                        }
                    }
                }
                PrintCase();
                Console.WriteLine(ans);
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

        static int _caseNum = 1;
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
    }
}
