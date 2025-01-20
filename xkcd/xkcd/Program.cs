using System.Net;
using System;
using System.Collections;

namespace xkcd
{
    class HtmlReader : WebClient
    {
        private string _source;

        public string NewFile()
        {
            int sDiv = _source.IndexOf("id=\"comic\"");
            int sTitle = _source.IndexOf("title", sDiv);
            int s = sTitle + 7;
            int e = _source.IndexOf("\"", s);
            string title = _source.Substring(s, e - s);
            int sInsert = _source.IndexOf("</div>", sDiv);
            return _source.Insert(sInsert, $"<p>{title}</p>");
        }
        public HtmlReader(string s)
        {
            _source = DownloadString(s);
        }
        
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                using (HtmlReader reader = new HtmlReader("https://xkcd.com/293/"))
                {
                    try
                    {
                        using (StreamWriter writer = new StreamWriter("newpage.html"))
                        {
                            writer.WriteLine(reader.NewFile());
                        }
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Output file error.");
                    }
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Remote server error.");
            }
        }
    }
}