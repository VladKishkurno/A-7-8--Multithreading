using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Advanced_Lesson_6_Multithreading
{
    public static class RemoteDataBase
    {
        private static Random random = new Random();

        private static Dictionary<string, int> pages = new Dictionary<string, int>()
            {
                {"home",  0 },
                {"login",  0 },
                {"faq",  0 },
            };

        public static int GetViews(string page)
        {
            Thread.Sleep(random.Next(10));
            return pages[page];
        }

        public static void SetViews(string page, int views)
        {
            Thread.Sleep(random.Next(10));
            pages[page] = views;
        }

        public static string[] GetPages()
        {
            return pages.Keys.ToArray();
        }
    }
}
