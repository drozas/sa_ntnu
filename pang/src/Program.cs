using System;

namespace pang_01
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (Pang game = new Pang())
            {
                game.Run();
            }
        }
    }
}

