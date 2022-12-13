using System;
using BlackJack;

namespace OOP
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Vítej, jsi nový hráč nebo už tady máš účet?(1/2) ");
            string start = Console.ReadLine();
            bool status = true;

            Console.WriteLine("Napiš zde svoje jméno: ");
            string name = Console.ReadLine();

            Game game = new Game(start, status, name);

            game.Login();

            game.Menu();

        }
    }
}