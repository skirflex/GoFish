using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Collections;

namespace GoFish
{
    class Deck : IEnumerable<Card>
    {
        List<Card> cards;
        public Card this[int index]
        {
            get => cards[index];
            set { cards[index] = value; }
        }
        public Deck()
        {
            cards = new List<Card>();
            for (int i = 0; i < 4; i++)
            {
                for (int j = 1; j < 14; j++)
                {
                    cards.Add(new Card(i, j));
                }
            }

        }

        public IEnumerator<Card> GetEnumerator()
        {
            return cards.GetEnumerator();
        }


        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Shuffle()
        {
            for (int i = 0; i < 200; i++)
            {

                var r1 = new Random().Next(this.Count() - 1);
                var r2 = new Random().Next(this.Count() - 1);
                Card tempCard = this[r1];
                this[r1] = this[r2];
                this[r2] = tempCard;

            }
        }

        public IEnumerable<Card> Take(int countCards)
        {
            
                var result = cards.Take(countCards).ToList();
                cards.RemoveRange(0, countCards);
                return result;
           
        }

        public void Remove()
        {
            cards.Remove(cards.First());
        }

    }
}
