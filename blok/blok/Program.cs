using System.Text;

namespace blok
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = "LoremIpsum.txt";
            try
            {
                using StreamReader reader = new StreamReader(path);
                List<List<string>> paragraphs = new List<List<string>>() { new List<string>() };
                string? radek = reader.ReadLine();
                bool newPar = false;
                int parCount = 0;
                int k = 0;
                try
                {
                    k = int.Parse(Console.ReadLine());
                    if (k < 0)
                    {
                        throw new ArgumentException();
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Argument Exception");
                }

                while (radek != null)
                {
                    string[] radekSplit = radek.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    if (radekSplit.Length > 0)
                    {
                        paragraphs[parCount].AddRange(radekSplit);
                        newPar = true;
                    }
                    else
                    {
                        if (newPar)
                        {
                            parCount++;
                            paragraphs.Add(new List<string>());
                            newPar = false;
                        }
                    }

                    radek = reader.ReadLine();
                }

                StringBuilder sb = new StringBuilder();

                foreach (var p in paragraphs)
                {
                    int pL = p.Count;
                    int wordIndex = 0;
                    List<string> line = new List<string>();
                    int lineCharCount = 0;
                    while (wordIndex < pL)
                    {
                        line.Clear();
                        line.Add(p[wordIndex]);
                        lineCharCount = line[0].Length;
                        wordIndex++;
                        while (wordIndex < pL)
                        {
                            if (p[wordIndex].Length + 1 + lineCharCount <= k)
                            {
                                line.Add(p[wordIndex]);
                                lineCharCount += p[wordIndex].Length + 1;
                                wordIndex++;
                            }
                            else
                            {
                                break;
                            }
                        }
                        if (line.Count == 1)
                        {
                            sb.Append(line[0]).Append('\n');
                        }
                        else if (wordIndex == pL)
                        {
                            for (int i = 0; i < line.Count - 1; i++)
                            {
                                sb.Append(line[i]).Append(' ');
                            }

                            sb.Append(line.Last()).Append('\n');
                        }
                        else
                        {
                            int wspacesLeft = k - lineCharCount;
                            int regularSpaces = wspacesLeft / (line.Count - 1) + 1;
                            int leftOverSpaces = wspacesLeft % (line.Count - 1);
                            for (int i = 0; i < line.Count - 1; i++)
                            {
                                sb.Append(line[i]).Append(' ', regularSpaces);
                                if (leftOverSpaces > 0)
                                {
                                    sb.Append(' ');
                                    leftOverSpaces--;
                                }
                            }

                            sb.Append(line.Last()).Append('\n');
                        }
                    }

                    sb.Append('\n');
                }

                sb.Remove(sb.Length - 1, 1);

                try
                {
                    using StreamWriter writer = new StreamWriter("output.txt");
                    writer.Write(sb.ToString());
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
                    Console.WriteLine($"Soubor jménem {path} neexistuje");
                }
                else
                {
                    Console.WriteLine("Input file error.");
                }
            }
        }
    }
}