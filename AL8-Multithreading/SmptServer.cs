using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Advanced_Lesson_6_Multithreading
{
    public static class SmptServer
    {
        private static Random random = new Random();

        public static Task<bool> SendEmail(string email)
        {
            return Task.Run(() => {
                Thread.Sleep(random.Next(1000));
                var succeed = random.Next(0, 10) < 5;
                if (succeed)
                {
                    Console.WriteLine("****************");
                    Console.WriteLine(email);
                    Console.WriteLine("****************");
                }
                return succeed;
            });
        }
    }
}
