﻿namespace BlackJack
{
    public class Card
    {
        public string Suit { get; set; }  
        public string Value { get; set; }

        public Card(string _value, string _suit)
        {
            Suit = _suit;
            Value = _value;
        }

    }
}
