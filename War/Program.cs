using System;
using System.Collections.Generic;

namespace War
{
    class Program
    {
        public static string player1;
        public static string player2;

        static void Main(string[] args)
        {
            Program myProg = new Program();
            List<List<string>> Decks = myProg.GetDecks();
            List<string> deck1 = Decks[0];
            List<string> deck2 = Decks[1];
            bool exit = false;
            string playNext = "Y";
            Console.WriteLine("Welcome to WAR");
            Console.WriteLine("Name Your Army: ");
            player1 = Console.ReadLine();
            Console.WriteLine("Name Your Opponents Army: ");
            player2 = Console.ReadLine();
            Console.WriteLine("The WAR starts Now");
            while (/*!exit &&*/ deck1.Count > 0 && deck2.Count > 0)
            {
                Console.WriteLine($"{player1} Card: " + deck1[0]);
                Console.WriteLine($"{player2} Card: " + deck2[0]);
                //Get Card value
                int card1 = myProg.cardValue(deck1[0][0]);
                int card2 = myProg.cardValue(deck2[0][0]);
                //Compare Cards
                if (card1 > card2)
                {
                    Console.WriteLine($"{player1} wins the battle!");
                    deck1.Add(deck1[0]);
                    deck1.Add(deck2[0]);
                    deck1.RemoveAt(0);
                    deck2.RemoveAt(0);

                }
                else if (card2 > card1)
                {
                    Console.WriteLine($"{player2} wins the battle!");
                    deck2.Add(deck1[0]);
                    deck2.Add(deck2[0]);
                    deck1.RemoveAt(0);
                    deck2.RemoveAt(0);
                }
                else
                {
                    Console.WriteLine("WAR");
                    List<string> cardsInPlay = new List<string>();
                    List<List<string>> postWarDecks = new List<List<string>>();
                    cardsInPlay.Add(deck1[0]);
                    cardsInPlay.Add(deck2[0]);
                    deck1.RemoveAt(0);
                    deck2.RemoveAt(0);
                    postWarDecks = myProg.War(deck1, deck2, cardsInPlay);
                    deck1 = postWarDecks[0];
                    deck2 = postWarDecks[1];
                }
                Console.WriteLine("Play next battle? (Type Y for yes)");
                //playNext = Console.ReadLine();
                if (playNext.ToUpper() != "Y")
                    exit = true;
            }
            //Print the winner
            if (deck1.Count > deck2.Count)
                Console.WriteLine($"{player1} won the war!");
            else if (deck2.Count > deck1.Count)
                Console.WriteLine($"{player2} won the war!");
        }

        public List<List<String>> War(List<string> deck1, List<string> deck2, List<string> inPlayCards)
        {
            List<List<string>> decks = new List<List<string>>();
            //Check if either player is out of cards
            if (deck1.Count == 0)
            {
                deck2.AddRange(inPlayCards);
                decks.Add(deck1);
                decks.Add(deck2);
            }
            else if (deck2.Count == 0)
            {
                deck1.AddRange(inPlayCards);
                decks.Add(deck1);
                decks.Add(deck2);
            }
            else
            {
                //Get Solider Cards
                if (deck1.Count < 4 && deck1.Count > 1)
                {
                    inPlayCards.AddRange(deck1.GetRange(0, deck1.Count - 2));
                    inPlayCards.AddRange(deck2.GetRange(0, 3));
                    deck1.RemoveRange(0, deck1.Count - 2);
                    deck2.RemoveRange(0, 3);
                }
                else if (deck2.Count < 4 && deck2.Count > 1)
                {
                    inPlayCards.AddRange(deck2.GetRange(0, deck2.Count - 2));
                    inPlayCards.AddRange(deck1.GetRange(0, 3));
                    deck2.RemoveRange(0, deck2.Count - 2);
                    deck1.RemoveRange(0, 3);
                }
                else if(deck1.Count > 1 && deck2.Count > 1)
                {
                    inPlayCards.AddRange(deck1.GetRange(0, 3));
                    inPlayCards.AddRange(deck2.GetRange(0, 3));
                    deck1.RemoveRange(0, 3);
                    deck2.RemoveRange(0, 3);
                }

                //Get and Compare General Cards
                //Give winning side all the Cards
                int card1 = cardValue(deck1[0][0]);
                int card2 = cardValue(deck2[0][0]);
                Console.WriteLine($"{player1} Card: " + deck1[0]);
                Console.WriteLine($"{player2} Card: " + deck2[0]);
                if (card1 > card2)
                {
                    Console.WriteLine($"{player1} wins the battle!");
                    deck1.Add(deck1[0]);
                    deck1.Add(deck2[0]);
                    deck1.RemoveAt(0);
                    deck2.RemoveAt(0);

                    deck1.AddRange(inPlayCards);
                    decks.Add(deck1);
                    decks.Add(deck2);
                }
                else if (card2 > card1)
                {
                    Console.WriteLine($"{player2} wins the battle!");
                    deck2.Add(deck1[0]);
                    deck2.Add(deck2[0]);
                    deck1.RemoveAt(0);
                    deck2.RemoveAt(0);

                    deck2.AddRange(inPlayCards);
                    decks.Add(deck1);
                    decks.Add(deck2);
                }
                else
                {
                    Console.WriteLine("WAR (Again)");
                    decks = War(deck1, deck2, inPlayCards);
                }
            }

            return decks;
        }

        public int cardValue(char C)
        {
            int value = 0;
            if (C == 'A')
                value = 14;
            else if (C == 'J')
                value = 11;
            else if (C == 'Q')
                value = 12;
            else if (C == 'K')
                value = 13;
            else
                value = int.Parse(C.ToString());

            return value;
        }

        public List<List<string>> GetDecks()
        {
            List<List<string>> Decks = new List<List<string>>();
            List<string> cards = new List<string>();
            List<string> deck1 = new List<string>();
            List<string> deck2 = new List<string>();
            for (int i = 1; i < 14; i++)
            {
                string cardNum = "";
                if (i == 1)
                    cardNum = "A";
                else if (i == 11)
                    cardNum = "J";
                else if (i == 12)
                    cardNum = "Q";
                else if (i == 13)
                    cardNum = "K";
                else
                    cardNum = i.ToString();

                for (int j = 1; j < 5; j++)
                {
                    string cardSuit = "";
                    if (j == 1)
                        cardSuit = "D";
                    else if (j == 2)
                        cardSuit = "H";
                    else if (j == 3)
                        cardSuit = "C";
                    else if (j == 4)
                        cardSuit = "S";

                    cards.Add(cardNum + cardSuit);
                }
            }
            Random cardpicker = new Random();

            while(cards.Count > 0)
            {
                int cardindex = 0;
                cardindex = cardpicker.Next(0, cards.Count);
                deck1.Add(cards[cardindex]);
                cards.RemoveAt(cardindex);
                cardindex = cardpicker.Next(0, cards.Count);
                deck2.Add(cards[cardindex]);
                cards.RemoveAt(cardindex);
            }

            Decks.Add(deck1);
            Decks.Add(deck2);

            return Decks;
        }
    }
}
