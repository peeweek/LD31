using System;
namespace LD31
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Game game = new Game();
            game.Run();
        }
    }
}

