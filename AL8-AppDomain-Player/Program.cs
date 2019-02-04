using System;

namespace Advanced_Lesson_6_AppDomain_Player
{
    public class Program
    {
        static void Main(string[] args)
        {
            var player = new Player($"Player {args[0]}");
            player.Play();            
        }
    }
}
