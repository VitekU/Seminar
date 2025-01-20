namespace abeceda
{ 
    class Node
    {
        public char Pismenko;
        public int Incoming;
        public List<Node> Sousede;

        public Node(char p, int i)
        {
            Pismenko = p;
            Incoming = i;
            Sousede = new List<Node>();
        }
    }  
    
    class Program
    {
        public static int FirstDifference(string s1, string s2)
        {
            for (int i = 0; i < Math.Min(s1.Length, s2.Length); i++)
            {
                if (s1[i] != s2[i])
                {
                    return i;
                }
            }

            return -1;
        }

        static void Main(string[] args)
        {
            string[] input = Console.ReadLine().Split().ToArray();

            Dictionary<char, Node> pismenkaSeznam = new Dictionary<char, Node>();

            foreach (var x in input)
            {
                foreach (var c in x)
                {
                    pismenkaSeznam.TryAdd(c, new Node(c, 0));
                }
            }
            for (int i = 0; i < input.Length - 1; i++)
            {
                int diff = FirstDifference(input[i], input[i + 1]);
                if (diff >= 0)
                {
                    char p1 = input[i][diff];
                    char p2 = input[i + 1][diff];
                
                    pismenkaSeznam[p1].Sousede.Add(pismenkaSeznam[p2]);
                    pismenkaSeznam[p2].Incoming ++;
                }
            }

            Queue<Node> q = new Queue<Node>();
            foreach (var x in pismenkaSeznam)
            {
                if (x.Value.Incoming == 0)
                {
                    q.Enqueue(x.Value);
                }
            }

            bool nejdeUrcit = q.Count > 1;
            string sort = "";
            int count = 0;
            while (q.Count > 0)
            {
                count++;
                Node cNode = q.Dequeue();
                sort += cNode.Pismenko + " -> " ;
                foreach (var soused in cNode.Sousede)
                {
                    soused.Incoming--;
                    if (soused.Incoming == 0)
                    {
                        q.Enqueue(soused);
                    }
                }
            }

            sort = sort.Remove(sort.Length - 3);
            if (count < pismenkaSeznam.Count)
            {
                Console.WriteLine("nejde, obsahuje cyklus");
            }
            else
            {
                Console.WriteLine(sort);
                if (nejdeUrcit)
                {
                    Console.WriteLine("neni jednoznacne");
                }
                
            }
            




        }
    }
}