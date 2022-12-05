using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading;

namespace GoFish
{
    class Player
    {


        private List<Card> hand = new List<Card>();

        private List<string> books = new List<string>(); 

        public readonly string Name;

        internal List<Card> Hand { get => hand; }
        public List<string> Books { get => books; private set => books = value; }

        public Player(IEnumerable<Card> startHand, string name)
        {
            Name = name;
            foreach (var card in startHand)
            {
                TakeCard(card);
            }
        }
        private string TakeCard(Card card)
        {
            Hand.Add(card);
            Sorthand();
            return CheckBooks();
        }

        private object GiveCard(Card.Values card)
        {
            Card cardReturn;
            foreach (var item in Hand)
            {
                if (item.Value == card)
                {
                    cardReturn = item;
                    Hand.Remove(item);
                    Sorthand();
                    return cardReturn;

                }
            }
            return null;
        }

        private void Sorthand()
        {
            Hand.Sort(new ComparerDesk().Compare);
        }

        private string CheckBooks()
        {
            if (Hand.Count > 0)
            {
                List <Card> deleteCardsFromHand = new List<Card>();
                int countCardssOneValues = 0;
                Card.Values tempCard = Hand[0].Value;
                foreach (var card in Hand)
                {
                    if (card.Value == tempCard)
                    {
                        countCardssOneValues++;
                        if (countCardssOneValues == 4)
                        {
                            for (int i = 0; i < 4; i++)
                            {
                                deleteCardsFromHand.Add(new Card((Card.Suits)i, card.Value));
                            }
                            Books.Add(card.Value.ToString());
                        }
                    }
                    else
                    {
                        tempCard = card.Value;
                        countCardssOneValues = 1;
                    }
                }
                if (deleteCardsFromHand.Count > 0)
                {
                    string returnedValue = "";
                    foreach (var card in deleteCardsFromHand)
                    {
                        this.Hand.Remove(card);
                        returnedValue = card.Value.ToString();
                    }
                    returnedValue = $"\n{Name} has a book of  {returnedValue.ToUpper()}S";
                    return returnedValue;
                }
            }
            return "";
        } // Проверяем на собранные квартеты

        public Tuple<string,string,bool> DoTurn(Card.Values card, Player oponentPlayer, Deck deck) 
        {
            bool IsRight = false;
            string resultForProgress = $"\n{this.Name} asked {oponentPlayer.Name} for {card}\n";
            string resultForBooks = "";
            var givenCard = oponentPlayer.GiveCard(card);
            if (givenCard != null)
            {
                
                resultForProgress += $"{oponentPlayer.Name} has {card}s";
                resultForBooks += this.TakeCard((Card)givenCard);
                IsRight = true;


            }
            else
            {
                if (deck.Count() > 0)
                {
                    resultForProgress += $"{this.Name} drew a card";
                    resultForBooks += this.TakeCard(deck.Take(1).First());
                }
                else resultForProgress += $"The deck is empty";

            }

            return Tuple.Create(resultForProgress,resultForBooks, IsRight);

        }

        public object GetNeededCard() // Алгоритм поиска нужной карты для компуктерного игрока
        {
            if (hand.Count > 1)
            {
                var result = hand.GroupBy(x => x.Value).Select(x => new { Card = x.Key, Count = x.Count() }).OrderByDescending(x=> x.Count).First();
                if (result.Count == 1) return hand[new Random().Next(hand.Count - 1)].Value;
                else return result.Card;
                

            }
            if (hand.Count == 1)
            {
                return hand[0].Value;
            }
            return null;
        }
    }
}
