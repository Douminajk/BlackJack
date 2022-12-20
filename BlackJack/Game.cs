using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace BlackJack
{
    public class Game
    {
        public bool Status;
        public string Continue;
        public string Name;
        public Game(bool _status, string _name) 
        {
            Status = _status;
            Name = _name;
        }

        public void Login()
        {
            bool nameControl = true;
            //loop
            while (nameControl)
            {
                //jestli obsahuje validní charaktery
                if (!Regex.IsMatch(Name, @"^[a-zA-Z0-9_]+$"))
                {
                    Console.WriteLine("\nTvé jméno se neskládá z povolených znaků, piš pouze písmena! => \n");
                    Name = Console.ReadLine();
                }
                //jestli je délka slova moc malá
                else if (Name.Length < 3)
                {
                    Console.WriteLine("\nTvé jméno je moc krátké, zvol prosím jiné => \n");
                    Name = Console.ReadLine();
                }
                //jestli je jméno moc dlouhé
                else if (Name.Length > 15)
                {
                    Console.WriteLine("\nTvé jméno je moc dlouhé, zvol prosím jiné => \n");
                    Name = Console.ReadLine();
                }
                //vše ok
                else
                {
                    nameControl = false;
                }

            }
        }

        public void Files()
        {
            string fileName3 = @"zebricek.csv";
            //jestli existuje zebricek.csv
            try
            {
                if (!File.Exists(fileName3))
                {
                    File.Create(fileName3);
                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.ToString());
            }
        }

        public void GameLoop()
        {
            int wallet = 5000;
            int numberofcards = 0;
            int total = 0;
            List<string> hands = new List<string>();

            Gamer gamer = new Gamer(Name, numberofcards, total, wallet, hands);

            if (wallet == 0)
            {
                wallet = 5000;
                Console.WriteLine("\n----------------------------------\n");
                Console.WriteLine("Došli Vám peníze, doplnili jsme je vám, vyberte další možnost\n");
            }

            while (Status)
            {
                Console.WriteLine("\n----------------------------------\n");
                Console.WriteLine("1: začít novou hru\n2: žebříček \n3: pravidla \n4: konec hry");
                Console.WriteLine("\n----------------------------------\n");
                string menu = Console.ReadLine();

                if (menu != null)
                {
                    if (menu == "1")
                    {  
                        string[] suits = { "♥", "♦", "♣", "♠" };

                        string[] values = { "2", "3", "4", "5", "6", "7", "8", "9", "10", "K", "Q", "J", "A" };

                        Deck deck = new Deck(suits, values);
                        int bet = gamer.Bet(wallet);
                        gamer.PickFirstCards(deck);
                        string chosen = gamer.firstChoose();

                        if (chosen == "d")
                        {
                            bet = bet * 2;
                        }

                        int GamersTotal = gamer.GameContinue(deck, chosen);

                        int numberofcardsDealer = 0;
                        int totalDealer = 0;
                        List<string> handsDealer = new List<string>();

                        Dealer dealer = new Dealer(numberofcardsDealer, totalDealer, handsDealer);

                        int DealersTotal = dealer.DealerPick(deck);

                        Console.WriteLine("\n----------------------------------\n");
                        Console.WriteLine("Součet Dealera je: " + DealersTotal);

                        if (GamersTotal == 21)
                        {
                            Console.WriteLine("\n----------------------------------\n");
                            Console.WriteLine("Gratulujeme, vyhrál jste, BlackJack!");
                            wallet = wallet + bet;
                        }
                        else if (GamersTotal > 21)
                        {
                            Console.WriteLine("\n----------------------------------\n");
                            Console.WriteLine("Prohrál jste, váš součet karet je: " + GamersTotal);
                            wallet = wallet - bet;
                        }
                        else if (GamersTotal < 21 && DealersTotal > 21)
                        {
                            Console.WriteLine("\n----------------------------------\n");
                            Console.WriteLine("Gratulujeme, vyhrál jste, dealerův součet je: " + DealersTotal);
                            wallet = wallet + bet;
                        }
                        else if (GamersTotal < DealersTotal && GamersTotal < 21 && DealersTotal < 21)
                        {
                            Console.WriteLine("\n----------------------------------\n");
                            Console.WriteLine("Prohrál jste, vaše karty mají hodnotu: " + GamersTotal + " a dealerovi karty mají hodnotu: " + DealersTotal);
                            wallet = wallet - bet;
                        }
                        else if (GamersTotal > DealersTotal && GamersTotal < 21 && DealersTotal < 21)
                        {
                            Console.WriteLine("\n----------------------------------\n");
                            Console.WriteLine("Vyhrál jste, vaše karty mají hodnotu: " + GamersTotal + " a dealerovi karty mají hodnotu: " + DealersTotal);
                            wallet = wallet + bet;
                        }
                        else if (GamersTotal == DealersTotal)
                        {
                            Console.WriteLine("\n----------------------------------\n");
                            Console.WriteLine("Nikdo nevyhrál! Vaše skóre jsou: " + GamersTotal + " a " + DealersTotal);
                        }
                        else if (DealersTotal == 21)
                        {
                            Console.WriteLine("\n----------------------------------\n");
                            Console.WriteLine("Dealer dostal BlackJack!");
                            wallet = wallet - bet;
                        }
                        else
                        {
                            Console.WriteLine("\n----------------------------------\n");
                            Console.WriteLine("něco se posralo hihihiha");
                        }
                    }
                    else if (menu == "2")
                    {
                        gamer.Save();
                        gamer.ShowData();
                    }
                    else if (menu == "3")
                    {
                        Console.WriteLine("\n----------------------------------\n");
                        Console.WriteLine("Základní princip hry je, že hráč chce mít hodnotu karet blíže 21 než krupiér, " +
                            "\nale přitom 21 nepřekročit. Vyhrává ten, kdo má po ukončení hry v ruce nejvyšší součet, " +
                            "\naniž by překročil 21. Hráč, který má v ruce součet karet větší než 21, je takzvaně " +
                            "\n„trop“ neboli „přes“.");
                    }
                    else if (menu == "4")
                    {
                        Console.WriteLine("\n----------------------------------\n");
                        Console.WriteLine("Konec hry");
                        Status = false;
                        gamer.Save();
                    }
                }
            }
        }
    }
}
