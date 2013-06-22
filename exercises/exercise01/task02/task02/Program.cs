using System;

namespace task02
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (Task02 game = new Task02())
            {
                game.Run();
            }
        }
    }
}

