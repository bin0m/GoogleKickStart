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
            4 6
            ZOAAMM
            ZOAOMM
            ZOOOOM
            ZZZZOM  */
        static void TaskB_StableWall()
        {
            int t;
            string s = Console.ReadLine().Trim();
            t = int.Parse(s);

            for (int ti = 1; ti <= t; ti++)
            {
                s = Console.ReadLine().Trim();
                var ss = s.Split(' ');
                int r = int.Parse(ss[0]);
                int c = int.Parse(ss[1]);

                string[] rows = new string[r];

                for (int ri = r - 1; ri >= 0; ri--)
                {
                    rows[ri] = Console.ReadLine().Trim();
                }

                Console.Write($"Case #{ti}: ");

                var nodes = new HashSet<char>();
                var edges = new HashSet<Tuple<char, char>>();


                for (int ri = 0; ri < r; ri++)
                {
                    for (int j = 0; j < c; j++)
                    {
                        char ch = rows[ri][j];
                        if (!nodes.Contains(ch))
                        {
                            nodes.Add(ch);
                        }

                        // add edges
                        if (ri > 0 && rows[ri - 1][j] != ch)
                        {
                            char down = rows[ri - 1][j];
                            var edge = new Tuple<char, char>(down, ch);
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
            int t;
            string s = Console.ReadLine().Trim();
            t = int.Parse(s);

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

            for (int ti = 1; ti <= t; ti++)
            {
                s = Console.ReadLine().Trim();
                int n = int.Parse(s);
                s = Console.ReadLine().Trim();
                int[] a = (from v in s.Split(' ') select int.Parse(v)).ToArray();

                Console.Write($"Case #{ti}: ");
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

                Console.WriteLine(ans);
            }
        }
    }
}
