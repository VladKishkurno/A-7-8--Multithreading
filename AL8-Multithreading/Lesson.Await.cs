using Advanced_Lesson_6_Diagnostic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Advanced_Lesson_6_Multithreading
{
    public static partial class Lesson
    {

        //Текущий поток блокируется
        public static void AwaitThreadPlayer()
        {
            var player = new AwaitThreadPlayer();
            var thrd = player.Load(new string[5]);
            //thrd.Join();
            player.Play();
        }

        
        //Текущий поток блокируется
        public static void AwaitTaskPlayerExample()
        {
            var player = new AwaitTaskPlayer();
            var task = player.Load("folder");
            task.Wait();
            player.Play();                
        }

        //Текущий поток не блокируется
        public static void AwaitTaskPlayerExample2()
        {
            var player = new AwaitTaskPlayer();
            var task = player.Load("folder");
            task.ContinueWith((t) => player.Play());           
        }

        
        public static async Task AsyncAwaitTaskPlayerExample()
        {
            var player = new AwaitTaskPlayer();            
            await player.Load("folder");
            await player.Play();
        }
    }


    public class AwaitThreadPlayer
    {
        public Thread Load(string[] songs)
        {
            var thread = new Thread(() =>
            {
                for (int i = 0; i < songs.Length; i++)
                {
                    Console.WriteLine($"#--> Song #{i + 1} loading");
                    Thread.Sleep(1000);
                }
            });

            thread.Start();

            return thread;
        }

        public void Play()
        {
            Console.WriteLine("Player is playing...");
        }
    }
    

    public class AwaitTaskPlayer
    {
        public string[] Songs { get; set; }

        public Task Play()
        {
            return Task.Run(() =>
            {
                Console.WriteLine("Playing...");
            });
        }

        public Task Load(string folder)
        {
            return Task.Run(() =>
            {
                Console.WriteLine($"Loading songs from {folder} ...");
                this.Songs = new string[5];
            });
        }
    }

    public class AsyncAwaitTaskPlayer
    {
        public string[] Songs { get; set; }

        public Task Play()
        {
            return Task.Run(() =>
            {
                Console.WriteLine("Playing...");
            });
        }

        public Task Load(string folder)
        {
            return Task.Run(() =>
            {
                Console.WriteLine($"Loading songs from {folder} ...");
                this.Songs = new string[5];
            });
        }
    }
 
}
