using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
namespace ConsoleApp1
{
    class URL
    {
        string url;
        int deep;
        public URL(string url,int deep)
        {
            this.url = url;
            this.deep = deep;
            
        }


    }

    

    class Crawler
    {
        string urlIndex = "http://www.studyphim.vn/movies/friends-season-1/play?episode=24";
        float timeout = 5f;
        int deepCrawler = 1;

        public void Craw()
        {
            Console.WriteLine("Load html");
            Console.WriteLine(urlIndex);
            Console.WriteLine(urlIndex);

            string html = GetStringFromUrls(urlIndex);

            string onlyText = html;
            string parttern = "(</((.)|(/n))*>)|(<((.)|(/n))*/>)|(<((.)|(/n))*>)";
            onlyText = Regex.Replace(html, parttern,"");
            Console.WriteLine("------------BEGIN TEXT------------");
            Console.WriteLine(onlyText);
            Console.WriteLine("------------END TEXT------------");


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
                if (Regex.IsMatch(m.ToString(), invalidFortmat) && IsUrls(m.ToString()))
                {
                    invalidUrls.Add(m.ToString());
                }

            }

            foreach (string url in urls)
            {
                Console.WriteLine(url);
                Console.WriteLine(ParseToURL(url,urlIndex));
            }
            Console.WriteLine("Found " + mc.Count + " URLs");
            Console.WriteLine("Found " + urls.Count + " URLs valid");
        }

        string RemoveHref(string page)
        {
            if (!page.Contains("href"))
                return page;
            page = page.Remove(0, 6);
            page = page.Remove(page.Length - 1);
            return page;
        }

        bool IsUrls(string s)
        {
            string parttern = ".png|.css|.html";
            if (Regex.IsMatch(s, parttern))
                return false;
            return true;
        }

        string ParseToURL(string page, string index = null)
        {
            ///1 check http://www
            ///2 check //www.
            ///3 split by first /

            //1,2
            page = RemoveHref(page);
            if (page.Contains("//"))
            {
                page = page.Remove(0, page.IndexOf("//") + 2);
            }

            if (page.EndsWith("/"))
                page = page.Remove(page.Length - 1, 1);
            //3
            // .png .css
            // .com .com.vn .net 
            string f1 = "", f2 = "";
            if (page.Contains("/"))
            {
                string f3 = page.Split('/')[0];
                if (IsValidURL(f3))
                {
                    f1 = f3;
                    f2 = page.Remove(0, page.IndexOf('/') + 1);
                }
                else
                {
                    f2 = page;
                }
            }
            else
            {
                if (IsValidURL(page))
                    f1 = page;
                else
                    f2 = page;
            }
            if (string.IsNullOrEmpty(f1))
                f1 += index;
            if (string.IsNullOrEmpty(f2))
                page = "http://" + f1;
            else
                page = "http://" + f1 + "/" + f2;

            return page;
        }

        bool IsValidURL(string url)
        {
            if (url.Contains(".com") || url.Contains(".net"))
                return true;
            return false;
        }

        bool IsInside(string page, string index)
        {
            return page.Contains(index);
        }
    }
}
