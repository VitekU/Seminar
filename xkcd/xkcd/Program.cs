using System.Diagnostics.Tracing;
using System.Net;

namespace xkcd
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (WebClient client = new WebClient()) 
            {
                string source = client.DownloadString("https://xkcd.com/2913/");

                int sDiv = source.IndexOf("id=\"comic\"");
                int sTitle = source.IndexOf("title", sDiv);
                int s = sTitle + 7;
                int e = source.IndexOf("\"", s);
                string output = source.Substring(s, e - s);
                int sInsert = source.IndexOf("</div", sDiv);

                using (StreamWriter sw = new StreamWriter("newpage.html"))
                {
                    string o = source.Insert(sInsert, $"<p>{output}</p>");
                    sw.WriteLine(o);
                }
            }
        }
    }
}
