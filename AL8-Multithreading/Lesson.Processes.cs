using Advanced_Lesson_6_Diagnostic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Advanced_Lesson_6_Multithreading
{
    public static partial class Lesson
    {
        public static void SinglePlayerExample()
        {
            var player = new Player("Player 1");
            player.Play();            

            Diagnostic.ListAllRunningProcesses();
            Diagnostic.ListAllProcessThreads();
            Diagnostic.ListAllProcessCodeModules();
            Diagnostic.ListAllAppDomains();
        }

        public static void AppDomainPlayersExample()
        {
            for (int i = 0; i < 4; i++)
            {
                var index = i + 1;
                AppDomain anotherAD = AppDomain.CreateDomain($"Player {index}");
                try
                {
                    var assempbly = anotherAD.Load("Advanced-Lesson-6-AppDomain-Player");
                    assempbly.EntryPoint.Invoke(null, new object[] { new string[] { (index).ToString() } });
                    
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            Diagnostic.ListAllAppDomains();
        }

        public static void MultiThreadingExample()
        {
            for (int i = 1; i <= 3; i++)
            {
                var thread = new Thread(() =>
                {
                    var player = new Player($"Thread {i}");
                    player.Play();
                });

                thread.Start();

                Console.WriteLine($"Diagnostic from thread #{i}");
                Diagnostic.ListAllProcessThreads();
            }

            Console.WriteLine("Diagnostic from main thread");
            Diagnostic.ListAllProcessThreads();
        }

        public static void ThreadPoolExample()
        {
            for (int i = 1; i <= 3; i++)
            {
                ThreadPool.QueueUserWorkItem((object state) =>
                {
                    var player = new Player($"Thread {i}");
                    player.Play();
                });

                
                Console.WriteLine($"Diagnostic from thread #{i}");
                Diagnostic.ListAllProcessThreads();
            }

            Console.WriteLine("Diagnostic from main thread");
            Diagnostic.ListAllProcessThreads();
        }

        public static void UnsyncPlayersExample()
        {
            for (int i = 1; i <= 3; i++)
            {
                var thread = new Thread(() =>
                {
                    var player = new Player($"Thread {i}");
                    player.List();
                    player.Play();
                });

                thread.Start();                
            }
        }

        public static void LockPlayersExample()
        {
            var obj = new Object();

            for (int i = 1; i <= 3; i++)
            {
                var thread = new Thread(() =>
                {
                    var player = new Player($"Thread {i}");
                    lock (obj)
                    {
                        player.List();
                    }                    
                    player.Play();
                });

                thread.Start();
            }
        }

        public static void MutexPlayerExample()
        {
            var mutex = new Mutex();

            for (int i = 1; i <= 3; i++)
            {
                var thread = new Thread(() =>
                {
                    var player = new Player($"Thread {i}");
                    mutex.WaitOne();
                    player.List();
                    mutex.ReleaseMutex();
                    player.Play();
                });

                thread.Start();
            }
        }
    }  


    public class Player
    {
        public string Name { get; set; }
       
        public Player(string name)
        {
            this.Name = name;
        }        

        public void Play()
        {
            var counter = 0;

            while(counter ++ < 10)
            {
                Console.WriteLine($"Playing from instance {this.Name}");

                Thread.Sleep(500);
                Console.ResetColor();
            }
        }

        public void List()
        {
            Console.WriteLine($"#{this.Name}:");

            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine($"---> Song #{i+1}");
                Thread.Sleep(2);
            }
        }
    }
}
