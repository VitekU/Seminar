
namespace batoh
{
    class Program
    {

        static void BackTrack(int[] v, int[] c, int k)
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
            Console.WriteLine(SolutionBacktracking(v, c, k, v.Length, predmety));
            foreach (var e in predmety)
            {
                Console.Write(e + 1 + " ");
            }
        }
        
        static void Main(string[] args)
        {
            int[] vahy = Console.ReadLine().Split().Select(int.Parse).ToArray();
            int[] ceny = Console.ReadLine().Split().Select(int.Parse).ToArray();
            int kapacita = int.Parse(Console.ReadLine());

            
            BackTrack(vahy, ceny, kapacita);

            
        }
    }
}