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

            string[] suits = {"♥", "♦", "♣", "♠"};

            string[] values = {"2", "3", "4", "5", "6", "7", "8", "9", "10", "K", "Q", "J", "A"};

            Deck deck = new Deck(suits, values);

            game.Login();

            game.Menu();


        }
    }
}