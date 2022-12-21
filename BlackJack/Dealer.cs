namespace BlackJack
{
    public class Dealer
    {
        public int Number_of_cards { get; set; }
        public int Total { get; set; }
        public List<string> Hand { get; set; }

        public Dealer(int _number_of_cards, int _total, List<string> _hand)
        {
            Number_of_cards = _number_of_cards;
            Total = _total;
            Hand = _hand;
        }

        public int DealerPick(Deck deck)
        {
            Console.WriteLine("\n----------------------------------\n");
            Console.WriteLine("\nDealerovi karty jsou: ");
            //loop dokud nemá dealer nad 17
            while (Total < 17)
            {
                deck.Shuffle(Hand);
                int Value_to_add = deck.AddToTotal(Hand, Total, Number_of_cards);
                Total = Total + Value_to_add; 
                Number_of_cards++;
            }
            return Total;
        }
    }
}
