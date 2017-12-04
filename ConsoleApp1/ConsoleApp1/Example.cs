using System;
using System.Text.RegularExpressions;

namespace ConsoleApp1
{
    class Example
    {
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
