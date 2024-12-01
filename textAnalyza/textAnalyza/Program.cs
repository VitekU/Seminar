using System;
using System.Collections.Generic;
using System.Text;

namespace textAnalyza
{
    class TextAnalyzer : StreamReader
    {
        private Dictionary<string, int> _words { get; }
        public int WordCount { get; }
        public int CharactersNoSpacesCount { get; private set; }
        public int CharactersCount { get; private set; }
        private List<List<string>> radkySlov { get; set; }

        public string slovaOut()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var r in radkySlov)
            {
                if (r.Count > 0)
                {
                    for (int i = 0; i < r.Count - 1; i++)
                    {
                        sb.Append(r[i]);
                        sb.Append(' ');
                    }

                    sb.Append(r.Last());
                    sb.Append('\n');
                }
                else
                {
                    sb.Append('\n');
                }
            }
            return sb.ToString();
        }

        public string slovaCountOut()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var w in _words)
            {
                sb.Append($"{w.Key}: {w.Value}\n");
            }
            return sb.ToString();
        }
        
        public TextAnalyzer(string path) : base(path)
        {
            _words = new Dictionary<string, int>();
            WordCount = 0;
            CharactersCount = 0;
            CharactersNoSpacesCount = 0;
            radkySlov = new List<List<string>>() {new List<string>()};
            int cRadek = 0;
            
            string cWord = "";
            while (true)
            {
                int x = Read();
                if (x == -1)
                {
                    if (cWord.Length > 0)
                    {
                        WordCount++;
                        radkySlov[cRadek].Add(cWord);
                        if (_words.ContainsKey(cWord))
                        {
                            _words[cWord]++;
                        }
                        else
                        {
                            _words.Add(cWord, 1);
                        }
                    }
                    
                    break;
                }

                char c = (char)x;
                CharactersCount++;
                char[] whiteSpaces = { ' ', '\t','\n', '\r' };
                
                if (whiteSpaces.Contains(c))
                {
                    
                    if (cWord.Length > 0)
                    {
                        WordCount++;
                        if (_words.ContainsKey(cWord))
                        {
                            _words[cWord]++;
                        }
                        else
                        {
                            _words.Add(cWord, 1);
                        }
                        radkySlov[cRadek].Add(cWord);
                        cWord = "";
                    }

                    if (c == '\n')
                    {
                        radkySlov.Add(new List<string>());
                        cRadek++;
                    }
                }
                else
                {
                    CharactersNoSpacesCount++;
                    cWord += c;
                }
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            
            string inputPath = "vstupy/3_vstup.txt";
            string outputPath = "output.txt";
            
            try
            {
                using (TextAnalyzer t = new TextAnalyzer(inputPath))
                {
                    try
                    {
                        using (StreamWriter writer = new StreamWriter(outputPath))
                        {
                            writer.WriteLine($"Počet slov: {t.WordCount}");
                            writer.WriteLine($"Počet znaků (bez bílých znaků): {t.CharactersNoSpacesCount}");
                            writer.WriteLine($"Počet znaků (s bílými znaky): {t.CharactersCount}\n");
                            writer.WriteLine(t.slovaCountOut());
                            writer.WriteLine(t.slovaOut());
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Output File Error");
                    }
                }
            }
            catch (Exception e)
            {
                if (e is FileNotFoundException)
                {
                    Console.WriteLine($"Soubor jménem {inputPath} neexistuje");
                }
                else
                {
                    Console.WriteLine("Input File Error");
                }
            }
        }
    }
}

