using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Text.RegularExpressions;
using System.Net;
using System.Threading;
namespace ConsoleApp1
{
    class Program
    {

        public static String Code(string Url)
        {
            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(Url);
            myRequest.UserAgent = "A .NET Web Crawler";
            WebResponse myResponse = myRequest.GetResponse();
            Stream stream = myResponse.GetResponseStream();
            StreamReader reader = new StreamReader(stream);
            string result = reader.ReadToEnd();
            return result;
        }

        static void Main(string[] args)
        {
            string html = Code("http://www.phimmoi.net/");
            Console.WriteLine(html);
            Console.WriteLine("Find URL");
            GetURL(html);
            Console.ReadLine();
        }

        static void GetURL(string s)
        {
            string parttern = "href=\"\\S*\"";
            Regex r = new Regex(parttern);
            MatchCollection mc = r.Matches(s);
            List<string> urls = new List<string>();
            List<string> invalidUrls = new List<string>();
            foreach (Match m in mc)
            {
                //   Console.WriteLine(m.ToString());

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

        static void Example1()
        {

            string chuoi = "Mot, Hai, Ba, Bon, NEVERLAND.";
            string pattern = @",|a";
            Regex myRegex = new Regex(pattern);
            string[] end = myRegex.Split(chuoi);
            foreach (string e in end)
            {
                Console.WriteLine(e);
            }
        }

        static void Example2()
        {

            string chuoi = "123abcd456bdabc";
            string pattern = "abc";
            Regex myRegex = new Regex(pattern);
            Match m = myRegex.Match(chuoi);
            if (m.Success)
            {
                Console.WriteLine("Tim thay chuoi con {0} o vi tri thu {1} trong chuoi", m.Value, m.Index);
                Console.WriteLine(m.NextMatch());

                Console.WriteLine("Tim thay chuoi con {0} o vi tri thu {1} trong chuoi", m.Value, m.Index);
                // Console.WriteLine( m.)
            }
            else

                Console.WriteLine("Khong tim thay chi ca");
        }

        static void Example3()
        {
            MatchCollection mc;
            string chuoi = "I like money, like woman and like C#";
            string pattern = "like";
            Regex myRegex = new Regex(pattern);
            mc = myRegex.Matches(chuoi);
            foreach (Match m in mc)
            {
                Console.WriteLine("Chuoi con '{0}' xuat hien o chi muc {1}", m.Value, m.Index);
            }
        }


        static void Example4()
        {
            MatchCollection mc;
            string chuoi = "This is a example string.";
            string pattern = @"\S+\s";
            Regex myRegex = new Regex(pattern);
            mc = myRegex.Matches(chuoi);
            for (int i = 0; i < mc.Count; i++)
            {
                Console.WriteLine("The match[{0}]: '{1}' co chieu dai la {2}", i, mc[i].Value, mc[i].Length);
            }
        }

        static void Example5()
        {
            //một chuỗi ví dụ
            string chuoi = "04:03:27 127.0.0.0 khanh.com.vn";
            //group time = một hoặc nhiều digit hoặc dấu hai chấm
            //theo sau bởi khoảng trắng
            string timePattern = @"(?(\d|\:)+)\s";
            string ipPattren = @"(?(\d|\.)+)\s";
            string sitePattern = @"(?\S+)";
            string pattern = timePattern + ipPattren + sitePattern;
            Regex myRegex = new Regex(pattern);
            //đi lấy tập hợp những so khớp
            MatchCollection matches = myRegex.Matches(chuoi);

            foreach (Match match in matches)
            {
                if (match.Length != 0)
                {
                    Console.WriteLine("\nMatch: {0}", match.ToString());
                    Console.WriteLine("\nTime: {0}", match.Groups["time"]);
                    Console.WriteLine("\nIP: {0}", match.Groups["ip"]);
                    Console.WriteLine("\nSite: {0}", match.Groups["site"]);
                }
            }
        }

    }
}
