using BlackJack;

namespace OOP
{
    class Program
    {
        static void Main()
        {
            bool status = true;
            Console.WriteLine("Napiš zde svoje jméno: ");
            string name = Console.ReadLine();

            Game game = new Game(status, name);
            //login hráče
            game.Login();
            //ověření existence filu
            game.Files();
            //loop celé hry
            game.GameLoop();
        }
    }
}