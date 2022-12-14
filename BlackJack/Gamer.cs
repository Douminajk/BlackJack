using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using BlackJack;

namespace BlackJack
{
    public class Gamer
    {
        public string Name { get; set; }
        public int Number_of_cards { get; set; }
        public int Total { get; set; }
        public int Money { get; set; }
        public List<string> Hand { get; set; }

        public Gamer(string _name, int _number_of_cards, int _total, int _money, List<string> _hand)
        {
            Name = _name;
            Number_of_cards = _number_of_cards;
            Total = _total;
            Money = _money;
            Hand = _hand;
        }

        public void PickFirstCards()
        {
            string[] suits = { "♥", "♦", "♣", "♠" };

            string[] values = { "2", "3", "4", "5", "6", "7", "8", "9", "10", "K", "Q", "J", "A" };

            Deck deck = new Deck(suits, values);

            deck.Shuffle(Hand);

            if(!int.TryParse(Hand[0], out int result))
            {
                if (Hand[0] != "A")
                {
                    result = 10;
                }
                else
                {
                    if (Total > 10)
                    {
                        result = 1;
                    }
                    else
                    {
                        result = 11;
                    }
                }
            }
            int Value_to_add = result;
            Total = Total + Value_to_add;
            
            deck.Shuffle(Hand);

            if (!int.TryParse(Hand[1], out int result2))
            {
                if (Hand[0] != "A")
                {
                    result2 = 10;
                }
                else
                {
                    if (Total > 10)
                    {
                        result2 = 1;
                    }
                    else
                    {
                        result2 = 11;
                    }
                }
            }

            int Value_to_add2 = result2;
            Total = Total + Value_to_add2;
            Console.WriteLine("Váš součet karet je: " + Total + "\n chcete zdvojnásobit sázku? (d) \n nebo brát další kartu? (h) \n a nebo stát? (s) ");
            string choose = Console.ReadLine();
        }
    }
}
