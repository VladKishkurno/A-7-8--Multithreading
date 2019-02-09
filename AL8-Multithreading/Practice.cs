using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Advanced_Lesson_6_Multithreading
{
    class Practice
    {      
        /// <summary>
        /// LA8.P1/X. Написать консольные часы, которые можно останавливать и запускать с 
        /// консоли без перезапуска приложения.
        /// </summary>
        public static void LA8_P1_5()
        {
            var thread = new Thread(() =>
            {
                while (true)
                {
                    Console.WriteLine(DateTime.Now.ToLocalTime());
                    Thread.Sleep(1000);
                    Console.Clear();
                }
            }
            );

            thread.Start();
            while(true)
            {
                switch(Console.ReadKey().KeyChar)
                {
                    case '1': thread.Resume(); break;
                    case '2': thread.Suspend(); break;
                }
            }
        }

        /// <summary>
        /// LA8.P2/X. Написать консольное приложение, которое “делает массовую рассылку”. 
        /// </summary>
        public static void LA8_P2_5()
        {
            Random random = new Random();

            for (int i = 0; i < 50; i++)
            {
                //var thread = new Thread(() =>
                ThreadPool.QueueUserWorkItem((object state) =>
                {
                    int rand = random.Next();

                    System.IO.File.AppendAllText(@"d:\" + rand + ".txt", i.ToString());
                    Thread.Sleep(random.Next(0, 1000));
                });
                
                //thread.Start();
                //Thread.Sleep(random.Next(0, 1000));
                // во всех испытаниях thread оказался быстрее на ноуте, на рабочем компьютере ThreadPool чуточку быстрее работает не зависимо от расположения Sleep
            }
            while(true)
            {
                Console.ReadKey();
            }
        }

        /// <summary>
        /// Написать код, который в цикле (10 итераций) эмулирует посещение 
        /// сайта увеличивая на единицу количество посещений для каждой из страниц.  
        /// </summary>
        public static void LA8_P3_5()
        {            
        }

        /// <summary>
        /// LA8.P4/X. Отредактировать приложение по “рассылке” “писем”. 
        /// Сохранять все “тела” “писем” в один файл. Использовать блокировку потоков, чтобы избежать проблем синхронизации.  
        /// </summary>
        public static void LA8_P4_5()
        {
            Random random = new Random();

            var mutex = new Mutex();
            var obj = new Object();

            for (int i = 0; i < 50; i++)
            {
                var thread = new Thread(() =>
                {
                    //mutex.WaitOne();
                    lock (obj)
                    {
                        System.IO.File.AppendAllText(@"d:\AllEmails.txt", "Thread : " + i.ToString() + "_" + random.Next().ToString());
                    }
                    //mutex.ReleaseMutex();
                    //Thread.Sleep(random.Next(0, 1000));
                });

                thread.Start();
                //Thread.Sleep(random.Next(0, 1000));
            }
        }

        /// <summary>
        /// LA8.P5/5. Асинхронная “отсылка” “письма” с блокировкой вызывающего потока 
        /// и информировании об окончании рассылки (вывод на консоль информации 
        /// удачно ли завершилась отсылка). 
        /// </summary>
        public async static void LA8_P5_5()
        {
            Random random = new Random();

            for (int i = 0; i < 50; i++)
            {
                var x = await SmptServer.SendEmail(@"Thread : " + i.ToString() + "_" + random.Next().ToString());
                if (x == true)
                {
                    Console.WriteLine("Отправлено");
                }
                else
                {
                    Console.WriteLine("Ошибка отправки, повторите попытку позже");
                }
            }
        }
    }    
}
