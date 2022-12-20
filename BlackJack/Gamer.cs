﻿using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using System.IO;
using static System.Formats.Asn1.AsnWriter;

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
        

        public void PickFirstCards(Deck deck)
        {
            Console.WriteLine("\nVaše karty jsou: ");
            deck.Shuffle(Hand);

            int Value_to_add = deck.AddToTotal(Hand, Total, Number_of_cards);
            Total = Total + Value_to_add;
            
            deck.Shuffle(Hand);

            Number_of_cards++;

            int Value_to_add2 = deck.AddToTotal(Hand, Total, Number_of_cards);
            Total = Total + Value_to_add2;
        }

        public void Save()
        {
            //pokud nemá uživatel 0 bodů
            if (Money != 0)
            {
                string newFileName = @"zebricek.csv";
                string nameDetails = string.Format("{0},{1}\n", Name, Money);
                //pokud neexistuje file
                if (!File.Exists(newFileName))
                {
                    nameDetails = "name,money\n" + nameDetails;
                }
                File.AppendAllText(newFileName, nameDetails);
                Money = 0;
            }
        }

        public void ShowData()
        {
            string path = @"zebricek.csv";

            string[] lines = System.IO.File.ReadAllLines(path);
            int counter = 0;
            List<int> termsList = new List<int>();
            List<string> namesList = new List<string>();

            //funkce na dání values do listu
            foreach (string line in lines)
            {
                string[] columns = line.Split(',');

                //braní jednoho záznamu ze dvou
                foreach (string column in columns)
                {
                    //zjištění jestli je to skóre nebo jméno
                    if (int.TryParse(column, out int intColumn))
                    {
                        termsList.Add(intColumn);
                    }
                    //column je jméno
                    else
                    {
                        namesList.Add(column);
                    }
                }
            }


            int[] terms = termsList.ToArray();
            string[] names = namesList.ToArray();
            Array.Sort(terms);
            Array.Reverse(terms);
            int top = 0;
            Console.WriteLine("\n----------------------------------\n");
            Console.WriteLine("Žebříček top 5 hráčů: \n");

            //braní jednoho pořadí
            foreach (int term in terms)
            {
                int counteros = 0;
                //vybrání jednoho řádku z žebříčku
                foreach (string line in lines)
                {
                    string[] columns = line.Split(',');

                    //vybrání pořadí z values z žebříčku
                    if (int.TryParse(columns[1], out int intColumn)) ;

                    //validace jestli se pořadí shoduje z value z žebříčku
                    if (intColumn == term)
                    {
                        //pokud existuje jméno v arrayi nepoužitých jmen
                        if (names.Contains(columns[0]))
                        {
                            top++;
                            Console.WriteLine(top + ": " + columns[0] + " | počet vítězství: " + term);
                            counteros++;
                            names = names.Where(val => val != columns[0]).ToArray();
                        }
                    }
                    //konec vypisování
                    if (counteros > 0)
                    {
                        break;
                    }
                }
                //konec vypisování po 5 top nejlepších
                if (top >= 5)
                {
                    break;
                }
            }
        }

        public string firstChoose()
        {
            bool ok = true;
            Console.WriteLine("\n----------------------------------\n");
            Console.WriteLine("\nVáš součet karet je: " + Total + "\n\nChcete zdvojnásobit sázku? (d) \nnebo brát další kartu? (h) \na nebo stát? (s) ");
            string choose = Console.ReadLine();

            while (ok)
            {
                if (choose == "d" || choose == "h" || choose == "s")
                {
                    ok = false;
                }
                else
                {
                    Console.WriteLine("\n----------------------------------\n");
                    Console.WriteLine("Napsal jste špatný input, zkuste to znovu. Chcete zdvojnásobit sázku? (d) \n nebo brát další kartu? (h) \n a nebo stát? (s) ");
                    choose = Console.ReadLine();
                }
            }

            return choose;
        }
        public int GameContinue(Deck deck, string choose)
        {
            if (Total < 21)
            {
                

                while (choose != "s")
                {
                    if (choose == "d")
                    {
                        if (Total < 21)
                        {
                            Money = Money * 2;

                            deck.Shuffle(Hand);
                            Number_of_cards++;

                            int Value_to_add = deck.AddToTotal(Hand, Total, Number_of_cards);
                            Total = Total + Value_to_add;

                            if (Total < 21)
                            {
                                Console.WriteLine("\n----------------------------------\n");
                                Console.WriteLine("Váš součet karet je: " + Total + "\n chcete brát další kartu? (h) \n a nebo stát? (s) ");
                                choose = Console.ReadLine();
                            }
                            else
                            {
                                choose = "s";
                            }
                        }
                        else
                        {
                            choose = "s";
                        }

                    }
                    else if (choose == "h")
                    {
                        if (Total < 21)
                        {
                            deck.Shuffle(Hand);
                            Number_of_cards++;

                            int Value_to_add = deck.AddToTotal(Hand, Total, Number_of_cards);
                            Total = Total + Value_to_add;

                            if (Total < 21)
                            {
                                Console.WriteLine("\n----------------------------------\n");
                                Console.WriteLine("Váš součet karet je: " + Total + "\n chcete brát další kartu? (h) \n a nebo stát? (s) ");
                                choose = Console.ReadLine();
                            }
                            else
                            {
                                choose = "s";
                            }
                        }
                        else
                        {
                            choose = "s";
                        }
                    }
                }
            }
            return Total;
        }

        public int Bet(int money)
        {
            Console.WriteLine("\n----------------------------------\n");
            Console.WriteLine(" Kolik chcete vsadit? Máte k dispozici: " + money);
            string bet = Console.ReadLine();
            bool ok = true;
            int result = 0;

            while (ok)
            {
                if (int.TryParse(bet, out result))
                {
                    if (result > money)
                    {
                        Console.WriteLine("\n----------------------------------\n");
                        Console.WriteLine("Napsali jste špatný input, kolik chcete vsadit?");
                        bet = Console.ReadLine();
                    }
                    else
                    {
                        ok = false;
                    }
                }
                else
                {
                    Console.WriteLine("\n----------------------------------\n");
                    Console.WriteLine("Tolik peněz nemáte, vsaďte něco prosím pod: " + money);
                    bet = Console.ReadLine(); 
                }
            }
            return result;
        }
    }
}
