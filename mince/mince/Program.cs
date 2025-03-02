using System.Text;

namespace mince
{
    class Program
    {
        private static StringBuilder _sb = new StringBuilder();
        static int Reseni(int[] mince, int suma, List<int> pouziteMince)
        {
            if (suma == 0)
            {
                foreach (var m in pouziteMince)
                {
                    _sb.Append(m).Append(' ');
                }
                _sb.Append('\n');
                return 1;   
            }

            if (mince.Length == 0 || suma < 0)
            {
                return 0;
            }

            pouziteMince.Add(mince[0]);
            int pouzitMinci = Reseni(mince, suma - mince[0], pouziteMince);
            pouziteMince.RemoveAt(pouziteMince.Count - 1);
            int nepouzitMinci = Reseni(new Span<int>(mince, 1, mince.Length - 1).ToArray(), suma, pouziteMince);
            return pouzitMinci + nepouzitMinci;
        }
        static void Main(string[] args)
        {
            int[] mince = Console.ReadLine().Split().Select(int.Parse).ToArray();
            int suma = int.Parse(Console.ReadLine());
            _sb.Append('\n');
            
            foreach (var m in mince)
            {
                if (m == 0)
                {
                    _sb.Append("Dobrý pokus shodit program mincí 0 :)");
                    Console.WriteLine(_sb.ToString());
                    return;
                }
            }
            
            int pocetReseni = Reseni(mince, suma, new List<int>());
            if (pocetReseni == 0)
            {
                _sb.Append("Nelze.");
            }
            else if (_sb.Length == 2)
            {
                _sb.Append("Sumu 0 lze zaplatit vždy.");
            }

            Console.WriteLine(_sb.ToString());
        }
    }
}