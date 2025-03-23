namespace nrp
{
    class Program
    {

        static int[] Solution(int[] s)
        {
            int[] t = new int[s.Length + 1];
            int[] p = new int[s.Length + 1];
            
            for (int i = s.Length; i >= 0; i--)
            {
                t[i] = 1;
                p[i] = 0;

                for (int j = i + 1; j < s.Length; j++)
                {
                    if (s[i] < s[j] && t[i] < 1 + t[j])
                    {
                        t[i] = 1 + t[j];
                        p[i] = j;
                    }
                }
            }

            int[] nrp = new int[t[0] - 1];
            int index = p[0];
            int ptr = 0;
            while (index != 0)
            {
                nrp[ptr] = s[index];
                index = p[index];
                ptr++;
            }
            
        
            return nrp;
        } 
        static void Main(string[] args)
        {
            using (StreamReader reader = new StreamReader("vstupy.txt"))
            {
                string line = reader.ReadLine();
                while (line != null)
                {
                    if (line == string.Empty)
                    {
                        Console.WriteLine("Posloupnost je prázdná!");
                    }
                    else
                    {
                        List<int> i = line.Split().Select(int.Parse).ToList();
                        i.Insert(0, int.MinValue);
                        int[] s = i.ToArray();

                        foreach (var e in Solution(s))
                        {
                            Console.Write(e + " ");
                        }

                        Console.WriteLine();
                    
                        //Console.WriteLine(line);
                    }
                    
                    reader.ReadLine();
                    line = reader.ReadLine();
                    

                }
            }
        }
    }
}