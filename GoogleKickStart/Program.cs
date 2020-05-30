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
