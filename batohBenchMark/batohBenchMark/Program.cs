using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace batohBenchMark
{
    public class Program
    {
        static void Main(string[] args)
        {
            var results = BenchmarkRunner.Run<MyBenchmark>(); // spustíme benchmarky (vše, co je označeno [Benchmark])
            
            /*MyBenchmark benchmark = new MyBenchmark();
            benchmark.Knapsack_Backtracking();
            Console.WriteLine("-------");
            benchmark.Knapsack_DynamicProgramming();*/

        }
    }
    
    [MemoryDiagnoser]
    public class MyBenchmark
    {
        static int[] w;
        static int[] c;
        static int capacity;
        
        public MyBenchmark()
        {
            Random random = new Random();
            int s = random.Next(5, 20);
            capacity = random.Next(20, 100);
            w = new int[s];
            c = new int[s];

            for (int i = 0; i < s; i++)
            {
                w[i] = random.Next(1, 30);
                c[i] = random.Next(1, 30);
            }

        }
        
        [Benchmark]
        public void Knapsack_Backtracking()
        {
            static int SolutionBacktracking(int[] v, int[] c, int k, int n, List<int> predmety)
            {
                if (k == 0 || n == 0)
                {
                    return 0;
                }

                if (v[n - 1] > k)
                {
                    return SolutionBacktracking(v, c, k, n - 1, predmety);
                }

                List<int> zahrnoutPredmety = new List<int>(predmety);
                List<int> nezahrnoutPredmety = new List<int>(predmety);

                int exclude = SolutionBacktracking(v, c, k, n - 1, nezahrnoutPredmety);
                int include = c[n - 1] + SolutionBacktracking(v, c, k - v[n - 1], n - 1, zahrnoutPredmety);
            
                if (include > exclude)
                {
                    zahrnoutPredmety.Add(n - 1);
                    predmety.Clear();
                    predmety.AddRange(zahrnoutPredmety);
                    return include;
                }
                predmety.Clear();
                predmety.AddRange(nezahrnoutPredmety);
                return exclude;
            }
            List<int> predmety = new List<int>();
            
            
            int max = SolutionBacktracking(w, c, capacity, w.Length, predmety);
            /*Console.WriteLine(max);
            foreach (var e in predmety)
            {
                Console.Write(e + 1 + " ");
            }*/
        }

        [Benchmark]
        public void Knapsack_DynamicProgramming()
        {
            int[,] table = new int[w.Length + 1, capacity + 1];

            for (int i = 0; i < capacity + 1; i++)
            {
                table[0, i] = 0;
            }
            
            for (int i = 1; i < w.Length + 1; i++)
            {
                int currentItemWeight = w[i - 1];
                int currentItemCost = c[i - 1];
                
                for (int j = 0; j < capacity + 1; j++)
                {
                    if (currentItemWeight > j)
                    {
                        table[i, j] = table[i - 1, j];
                    }
                    else
                    {
                        table[i, j] = Math.Max(table[i - 1, j - currentItemWeight] + currentItemCost, table[i - 1,j]);
                    }
                }
            }

            int max = table[w.Length, capacity];
            int itemCount = w.Length;
            
            //Console.WriteLine(max);
            for (int i = itemCount; i > 0 && max > 0; i--) { 
                if (max != table[i - 1, capacity])
                {
                    //Console.Write(i + " "); 
                    max -= c[i - 1]; 
                    capacity -= w[i - 1]; 
                }
            }

            //Console.WriteLine();
            
        }
    }

}