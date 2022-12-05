using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace GoFish
{
    
    class Card : IEquatable<Card>
    {
        public enum Suits 
        { 
            Diamonds,
            Clubs,
            Hearts,
            Spades
        }
        public enum Values
        {
            Ace = 1,
            Two,
            Three,
            Four,
            Five,
            Six,
            Seven,
            Eight,
            Nine,
            Ten,
            Jack,
            Queen,
            King    
        }
        public Suits Suit { get; private set; }

        public Values Value { get; private set; }


        public Card(Suits suit, Values value)
        {
            Suit = suit;
            Value = value;     
        }

        public Card(int suit, int value)
        {
            Suit = (Suits)suit;
            Value = (Values)value;
        }

        public override string ToString()
        {
            return $"{Value} of {Suit}";
        }

        public bool Equals([AllowNull] Card other) => this.Value == other.Value && this.Suit == other.Suit ? true : false;

    }
}
