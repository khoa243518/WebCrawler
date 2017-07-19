using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
namespace ConsoleApp1
{
    class Crawler
    {
        string url = "http://www.phimmoi.net/";
        float timeout = 5f;
        int deepCrawler = 1;

        public void Craw()
        {
            Console.WriteLine("Load html");
            string html = GetStringFromUrls(url);
            Console.WriteLine("Find URL");
            GetURLs(html);
            Console.ReadLine();
        }

        public static String GetStringFromUrls(string Url)
        {
            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(Url);
            myRequest.UserAgent = "A .NET Web Crawler";
            WebResponse myResponse = myRequest.GetResponse();
            Stream stream = myResponse.GetResponseStream();
            StreamReader reader = new StreamReader(stream);
            string result = reader.ReadToEnd();
            return result;
        }

        static void GetURLs(string s)
        {
            string parttern = "href=\"\\S*\"";
            Regex r = new Regex(parttern);
            MatchCollection mc = r.Matches(s);
            List<string> urls = new List<string>();
            List<string> invalidUrls = new List<string>();
            foreach (Match m in mc)
            {
                if (!urls.Contains(m.ToString()))
                {
                    urls.Add(m.ToString());
                }

                string invalidFortmat = "\\.\\w*\"";
                if (Regex.IsMatch(m.ToString(), invalidFortmat))
                {
                    invalidUrls.Add(m.ToString());
                }

            }

            foreach (string url in urls)
                Console.WriteLine(url);

            Console.WriteLine("Found " + mc.Count + " URLs");
            Console.WriteLine("Found " + urls.Count + " URLs valid");
        }

    }
}
