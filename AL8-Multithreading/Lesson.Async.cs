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
        public static Random random = new Random();       

        public static void AsyncPlayerExample()
        {
            var player = new AsyncPlayer("AsyncPlayerExample");   
            player.Play();
        }

        public static void AMPPlayerExample()
        {
            var player = new AMPPlayer();
            player.BeginPlay((result) =>
            {
                Console.WriteLine("Player finished playing");
            }, null);
        }

        public static void EventBasedPlayerExample()
        {
            var player = new EventBasedPlayer();
            player.PlayCompleted += ((object sender, AsyncCompletedEventArgs e) =>
            {
                Console.WriteLine("Player finished playing");
            });
            player.PlayAsync();
        }

        public static void TaskPlayerExample()
        {
            var player = new TaskPlayer();
            player
                .PlayAsync()
                .ContinueWith((playCompletedTask) =>
                {
                    Console.WriteLine("Player finished playing");
                });
        }

        public static void TaskPlayerExample2()
        {
            var player = new TaskPlayer();
            player
                .LoadAsync("c://temp")
                .ContinueWith((loadCompletedTask) =>
                {
                    player.Songs = loadCompletedTask.Result;
                    player.PlayAsync();
                });
        }

    }  


    public class AsyncPlayer
    {
        public string Name { get; set; }
       
        public AsyncPlayer(string name)
        {
            this.Name = name;
        }        

        public void Play()
        {           

            var thread = new Thread(() =>
            {
                var songNumber = 0;

                while (songNumber++ < 10)
                {
                    var duration = Lesson.random.Next(10);
                    PlaySong(songNumber, duration);
                }
            });

            thread.Start();
        } 
        
        private void PlaySong(int number, int duration)
        {
            var position = 0;
            
            while (position++ < duration)
            {
                Console.WriteLine($"#--> Song #{number}, {position}:{duration}");
                Thread.Sleep(1000);               
            }
        }
    }

    public class AMPPlayer
    {
        public IAsyncResult BeginPlay(AsyncCallback callback, object state)
        {
            var result = new PlayAsyncResult();

            var thread = new Thread(() =>
            {
                Console.WriteLine("Playing...");                
                callback?.Invoke(result);
            });

            thread.Start();

            return result;
        }

        public void EndPlay(IAsyncResult asyncResult)
        {
        }
    }

    public class PlayAsyncResult : IAsyncResult
    {
        public bool IsCompleted { get; set; }

        public WaitHandle AsyncWaitHandle { get; set; }

        public object AsyncState { get; set; }

        public bool CompletedSynchronously { get; set; }
}

    public class EventBasedPlayer
    {
        public event AsyncCompletedEventHandler PlayCompleted;

        public void PlayAsync()
        {
            var thread = new Thread(() =>
            {
                Console.WriteLine("Playing...");
                PlayCompleted?.Invoke(this, null);
            });

            thread.Start();           
        }        
    }

    public class TaskPlayer
    {
        public string[] Songs { get; set; }

        public Task PlayAsync()
        {
            var task = new Task(() =>
            {
                Console.WriteLine("Playing...");                
            });

            task.Start();

            return task;
        }

        public Task<string[]> LoadAsync(string folder)
        {
            var task = new Task<string[]>(() =>
            {
                Console.WriteLine($"Loading songs from {folder} ...");
                return new string[5];
            });

            task.Start();

            return task;
        }
    }
}
