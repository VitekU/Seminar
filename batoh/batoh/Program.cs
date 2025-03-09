
namespace batoh
{
    class Program
    {
        static int Solution(int[] v, int[] c, int k, int n, List<int> predmety)
        {
            if (k == 0 || n == 0)
            {
                return 0;
            }

            if (v[n - 1] > k)
            {
                return Solution(v, c, k, n - 1, predmety);
            }

            List<int> zahrnoutPredmety = new List<int>(predmety);
            List<int> nezahrnoutPredmety = new List<int>(predmety);

            int exclude = Solution(v, c, k, n - 1, nezahrnoutPredmety);
            int include = c[n - 1] + Solution(v, c, k - v[n - 1], n - 1, zahrnoutPredmety);
            
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
        static void Main(string[] args)
        {
            int[] vahy = Console.ReadLine().Split().Select(int.Parse).ToArray();
            int[] ceny = Console.ReadLine().Split().Select(int.Parse).ToArray();
            int kapacita = int.Parse(Console.ReadLine());

            List<int> predmety = new List<int>();
            Console.WriteLine(Solution(vahy, ceny, kapacita, vahy.Length, predmety));

            foreach (var p in predmety)
            {
                Console.Write(p + 1 + " ");
            }
            
            
        }
    }
}