using System;

namespace Advanced_Lesson_6_AppDomain_Player
{
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

            while (counter++ < 10)
            {
                Console.WriteLine($"Playing from instance {this.Name}");
            }
        }
    }
}
