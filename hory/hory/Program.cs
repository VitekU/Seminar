using System.Text;

namespace hory
{
    struct Tile
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Tile(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return $"{X} {Y}";
        }
    }
    class HledacCestyVHorach : StreamReader
    {
        private Dictionary<int, List<Tile>> laviny { get; set; }
        private List<int> timeline { get; set; }
        private static HashSet<Tile> hory { get; set; }
        private Dictionary<Tile, Tile> cesta { get; set; }
        private HashSet<Tile> visited { get; set; }
        private List<int[]> tahy { get; set; }
        private Queue<Tile> q { get; set; }
        private int k { get; set; }
        private static int n { get; set; }
        private int m { get; set; }
        private int timer { get; set; }
        private static Tile cil { get; set; }
        public string output { get; set; }
        
        
        public void NactiZadani()
        {
            n = int.Parse(ReadLine());
            m = int.Parse(ReadLine());
            cil = new Tile(n - 1, n - 1);
            for (int i = 0; i < m; i++)
            {
                int[] h = Array.ConvertAll(ReadLine().Split(), int.Parse);
                if (laviny.ContainsKey(h[2]))
                {
                    laviny[h[2]].Add(new Tile(h[0], h[1]));
                }
                else
                {
                    laviny.Add(h[2], new List<Tile> {new Tile(h[0], h[1])});
                }
                timeline.Add(h[2]);
            }
            timeline.Sort();
            
            k = int.Parse(ReadLine());
            
            for (int i = 0; i < k; i++)
            {
                int[] tah = Array.ConvertAll(ReadLine().Split(), int.Parse);
                tahy.Add(tah);
            }
        }
        public void NajdiCestu()
        {
            q.Enqueue(new Tile(0,0));
            visited.Add(new Tile(0,0));
            timer = 0;
            
            while (q.Count > 0)
            {
                LavinySpadnout();
                timer++;
                Tile currentTile = q.Dequeue();
                if (currentTile.X == n - 1 && currentTile.Y == n - 1)
                {
                    break;
                }
                for (int i = 0; i < k; i++)
                {
                    Tile targetTile = new Tile(currentTile.X + tahy[i][0], currentTile.Y + tahy[i][1]);
                    
                    if (ValidatePath(currentTile, targetTile) && visited.Contains(targetTile) == false)
                    {
                        cesta.Add(targetTile, currentTile);
                        visited.Add(targetTile);
                        q.Enqueue(targetTile);
                    }
                }
            }
        }
        private void LavinySpadnout()
        {
            if (laviny.ContainsKey(timer))
            {
                foreach (var l in laviny[timer])
                {
                    hory.Add(l);
                }
            }
        }
        private static bool ValidatePath(Tile s, Tile e)
        {
            int stepX = 0;
            int stepY = 0;

            if (e.X - s.X > 0)
            {
                stepX = 1;
            }
            else if (e.X - s.X < 0)
            {
                stepX = -1;
            }
            
            if (e.Y - s.Y > 0)
            {
                stepY = 1;
            }
            else if (e.Y - s.Y < 0)
            {
                stepY = -1;
            }
                
            while (true)
            {
                if (hory.Contains(s) || s.X < 0 || s.Y < 0 || s.X > n || s.Y > n)
                {
                    return false;
                }

                if (s.X == e.X && s.Y == e.Y)
                {
                    break;
                }
                s.X += stepX;
                s.Y += stepY;
            }

            return true;
        }
        public void CestaOut()
        {
            Tile cTile = cil;
            List<Tile> cestaOut = new List<Tile>();
            StringBuilder sb = new StringBuilder();
            
            if (cesta.ContainsKey(cTile))
            {
                while (true)
                {
                    cestaOut.Add(cTile);
                    cTile = cesta[cTile];
                    if (cTile.X == 0 && cTile.Y == 0)
                    {
                        break;
                    }
                }
            
                cestaOut.Add(cTile);
                cestaOut.Reverse();

                sb.Append(cestaOut.Count - 1).Append('\n');
                
                foreach (var tile in cestaOut)
                {
                    sb.Append($"[{tile.X},{tile.Y}] ");
                }
                
                output = sb.ToString();
            }
            else
            {
                output = "-1";
            }
        }
        public HledacCestyVHorach(string path) : base(path)
        {
            laviny = new Dictionary<int, List<Tile>>();
            timeline = new List<int>();
            hory = new HashSet<Tile>();
            cesta = new Dictionary<Tile, Tile>();
            visited = new HashSet<Tile>();
            tahy = new List<int[]>();
            q = new Queue<Tile>();
            output = "";
        }
    }
    class Program
    {
        static void Main(string[] args)
        { 
            string pathToInput = "input3.txt";
            try
            {
                using HledacCestyVHorach solver = new HledacCestyVHorach(pathToInput);
                using StreamWriter writer = new StreamWriter("output.txt");
                solver.NactiZadani();
                solver.NajdiCestu();
                solver.CestaOut();
                
                try
                {
                    writer.Write(solver.output);
                }
                catch (Exception)
                {
                    Console.WriteLine("Output file error.");
                }
            }
            catch (Exception e)
            {
                if (e is FileNotFoundException)
                {
                    Console.WriteLine($"Soubor s názvem {pathToInput} neexistuje");
                }
                else
                {
                    Console.WriteLine("Input file error.");
                }
            }
        }
    }
}