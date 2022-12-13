using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJack;

namespace BlackJack
{
    public class Deck
    {
        public List<Card> Cards = new List<Card>();

        public Deck(string[] _suits, string[] _values )
        {
            foreach(string suit in _suits)
            {
                foreach(string value in _values)
                {
                    Cards.Add(new Card(value, suit));
                }
            }
        }

        public void Shuffle()
        {
            var random = new Random();
            int num = random.Next(0, Cards.Count);

            string value = Cards[num].Value;
            string suit = Cards[num].Suit;
            Console.WriteLine(value + suit);
        }
    }
}
