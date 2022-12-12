using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJack;

namespace BlackJack
{
    public class Game
    {
        public bool Status;
        public string Continue;
        public string Name;
        public Game(string _continue, bool _status, string _name) 
        {
            Status = _status;
            Continue = _continue;
            Name = _name;
        }

        public void Login()
        {
            bool validation = true;

            while (validation)
            {
                if (Continue == "1")
                {
                    Console.WriteLine("Vítáme tě tu znovu " + Name);
                    validation = false;
                }
                else if (Continue == "2")
                {
                    Console.WriteLine("Vítej " + Name);
                    validation = false;
                }
                else
                {
                    Console.WriteLine("Vybral jsi špatnou možnost, napiš prosím pouze 1 nebo 2! ");
                    Continue = Console.ReadLine();
                }
            }
        }

        public void Menu()
        {
            while (Status)
            {
                Console.WriteLine("1: začít novou hru\n 2: žebříček \n 3: pravidla \n 4: konec hry");
                string menu = Console.ReadLine();

                if (menu != null)
                {
                    if (menu == "1")
                    {
                        Console.WriteLine("hra");
                    }
                    else if (menu == "2")
                    {
                        Console.WriteLine("žebříček");
                    }
                    else if (menu == "3")
                    {
                        Console.WriteLine("Základní princip hry je, že hráč chce mít hodnotu karet blíže 21 než krupiér, " +
                            "\nale přitom 21 nepřekročit. Vyhrává ten, kdo má po ukončení hry v ruce nejvyšší součet, " +
                            "\naniž by překročil 21. Hráč, který má v ruce součet karet větší než 21, je takzvaně " +
                            "\n„trop“ neboli „přes“.");
                    }
                    else if (menu == "4")
                    {
                        Console.WriteLine("Konec hry");
                        Status = false;
                    }
                }
            }
        }
    }
}
