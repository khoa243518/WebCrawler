using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
namespace ConsoleApp1
{
    class Crawler
    {
        string urlIndex = "http://www.phimmoi.net";
        float timeout = 5f;
        int deepCrawler = 1;

        public void Craw()
        {
            Console.WriteLine("Load html");
            Console.WriteLine(urlIndex);
            urlIndex = ParseToURL(urlIndex);
            Console.WriteLine(urlIndex);
            string html = GetStringFromUrls(urlIndex);
            Console.WriteLine("Find URL");
            GetURLs(html);
            Console.ReadLine();
        }

        public String GetStringFromUrls(string Url)
        {
            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(Url);
            myRequest.UserAgent = "A .NET Web Crawler";
            WebResponse myResponse = myRequest.GetResponse();
            Stream stream = myResponse.GetResponseStream();
            StreamReader reader = new StreamReader(stream);
            string result = reader.ReadToEnd();
            return result;
        }

        void GetURLs(string s)
        {
            string parttern = "href=\"\\S*\"";
            Regex r = new Regex(parttern);
            MatchCollection mc = r.Matches(s);
            List<string> urls = new List<string>();
            List<string> invalidUrls = new List<string>();
            foreach (Match m in mc)
            {
                if (!urls.Contains(m.ToString()) && IsUrls(m.ToString()))
                {
                    urls.Add(m.ToString());
                }

                string invalidFortmat = "\\.\\w*\"";
                if (Regex.IsMatch(m.ToString(), invalidFortmat)  && IsUrls(m.ToString()) )
                {
                    invalidUrls.Add(m.ToString());
                }

            }

            foreach (string url in urls)
            {
                Console.WriteLine(url);
               // Console.WriteLine(ParseToURL(url,urlIndex));
            }
            Console.WriteLine("Found " + mc.Count + " URLs");
            Console.WriteLine("Found " + urls.Count + " URLs valid");
        }

        string RemoveHref(string page)
        {
            if(!page.Contains("href"))
             return page;
            page = page.Remove(0, 6);
            page = page.Remove(page.Length - 1);
            return page;
        }

        bool IsUrls(string s)
        {
            string parttern = ".png|.css";
            if (Regex.IsMatch(s, parttern))
                return false;
            return true;
        }

        string ParseToURL(string page,string index=null)
        {
            //example phimmoi.net       -> http://phimmoi.net
            //example phimmoi.com.vn    -> http://phimmoi.com.vn
            //example http://phimmoi.com.vn/    -> http://phimmoi.com.vn
            if (index != null)
                page = RemoveHref(page);

            if (index!=null)
              if (!page.Contains(index))
                    page = index + "/" + page;
            

            string[] s = page.Split('.');
          //  if(s.Length<)

            //1 http://     false   
            //2 www.        false
            //3 phimmoi.    true
            //4 com.vn      true
            string s1="", s2="", s3="";
            if(s.Length==2)
            {
                s2 = s[0];
                s3 = s[1];
            }
            else if(s.Length==3)
            {
                if(s[0].Contains("www"))
                {
                    s1 = s[0];
                    s2 = s[1];
                    s3 = s[2];
                }
                else
                {
                    s2 = s[0];
                    s3 = s[1]+"."+s[2];
                }
            }
            else
            {
                Console.WriteLine("bug");
                Console.WriteLine(page);
                Console.WriteLine(index);
                Console.ReadLine();
            }


            s1 = "http://www";
            if (s3.EndsWith("/"))
                s3.Remove(s3.Length-1);
            page = s1 + "." + s2 + "." + s3;
            return page;
        }

        bool IsInside(string page,string index)
        {
            return page.Contains(index);
        }
    }
}
