using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThebesCore
{
    /// <summary>
    /// Manages the card deck
    /// </summary>
    public interface IDeck
    {
        /// <summary>
        /// Discards a used card (the card can be drawn again later in the game)
        /// </summary>
        /// <param name="card"></param>
        void Discard(ICard card);

        /// <summary>
        /// Draws a card from the deck
        /// </summary>
        /// <returns>card drawn</returns>
        ICard DrawCard();

        /// <summary>
        /// Clones this instance in its particular state
        /// </summary>
        /// <returns>Copy of this instance</returns>
        Deck Clone();
    }

    [Serializable]
    public class Deck : IDeck
    {
        private Queue<ICard> CardDeck { get; set; }
        private List<ICard> DiscardPile { get; set; }
        Random random;

        public Deck() { }
        public Deck(List<ICard> cards, int playerCount)
        {
            DiscardPile = new List<ICard>();
            random = new Random();
            CardDeck = BuildDeck(cards, playerCount);
        }

        /// <summary>
        /// Builds a properly shuffled deck from given card with regards to player count and exhibition positioning.
        /// </summary>
        /// <param name="cards">List of cards to build the deck from</param>
        /// <param name="playerCount">Number of players participating in the game</param>
        /// <returns>Queue representation of the final deck</returns>
        private Queue<ICard> BuildDeck(List<ICard> cards, int playerCount)
        {
            List<ICard> cardsCopy = new List<ICard>(cards);
            if (playerCount < 2 || playerCount > 4)
            {
                throw new InvalidOperationException("Invalid number of players. This deck implemetation only works for 2 to 4 players");
            }

            List<ICard> newDeck = new List<ICard>();
            List<IExhibitionCard> smallExhibitions = new List<IExhibitionCard>();
            List<IExhibitionCard> largeExhibitions = new List<IExhibitionCard>();

            while (cardsCopy.Count != 0)
            {
                int randomIndex = random.Next(cardsCopy.Count);
                ICard randomCard = cardsCopy[randomIndex];

                if (randomCard is IExhibitionCard)
                {
                    if (((IExhibitionCard)randomCard).IsSmallExhibition())
                    {
                        smallExhibitions.Add((IExhibitionCard)randomCard);
                    }
                    else
                    {
                        largeExhibitions.Add((IExhibitionCard)randomCard);
                    }
                }
                else
                {
                    newDeck.Add(randomCard);
                }

                cardsCopy.RemoveAt(randomIndex);
            }

            // Positioning exhibition cards in the deck according to the rules.
            // Small exhibitions go between 1/3 and 2/3 of the deck.
            // Large exhibitions between 2/3 and the end. In case of two players, same as small exhibitions

            int smallExhibitionLowBoundPosition = newDeck.Count / 3 + 4;
            int smallExhibitionUpperBoundPostition = smallExhibitionLowBoundPosition * 2;
            int largeExhibitionLowBoundPosition, largeExhibitionUpperBoundPosition;

            if (playerCount == 2)
            {
                largeExhibitionLowBoundPosition = smallExhibitionLowBoundPosition;
                largeExhibitionUpperBoundPosition = smallExhibitionUpperBoundPostition;
            }
            else
            {
                largeExhibitionLowBoundPosition = smallExhibitionUpperBoundPostition + 1;
                largeExhibitionUpperBoundPosition = newDeck.Count;
            }

            foreach (IExhibitionCard smallExhibition in smallExhibitions)
            {
                newDeck.Insert(random.Next(smallExhibitionLowBoundPosition, smallExhibitionUpperBoundPostition), smallExhibition);
            }

            foreach (IExhibitionCard largeExhibition in largeExhibitions)
            {
                newDeck.Insert(random.Next(largeExhibitionLowBoundPosition, largeExhibitionUpperBoundPosition), largeExhibition);
            }

            return new Queue<ICard>(newDeck);
        }

        /// <summary>
        /// Draws a card from <see cref="CardDeck"/>. If empty, recycles <see cref="DiscardPile"/> using  <see cref="recycleDeck"/>
        /// </summary>
        /// <returns>Top card from <see cref="CardDeck"/></returns>
        public ICard DrawCard()
        {
            if (CardDeck.Count == 0)
            {
                if (DiscardPile.Where(c => !(c is IExhibitionCard)).Count() == 0)
                {
                    return null;
                }
                recycleDeck();
            }
            return CardDeck.Dequeue();
        }

        /// <summary>
        /// Shuffles discarted cards and builds a new deck from them.
        /// </summary>
        private void recycleDeck()
        {
            if (CardDeck.Count != 0)
            {
                throw new InvalidOperationException("Deck is not empty yet. No reason to recycle.");
            }
            if (DiscardPile.Count == 0)
            {
                throw new InvalidOperationException("Discard pile is empty while deck recycling.");
            }

            Shuffle(DiscardPile);
            CardDeck = new Queue<ICard>(DiscardPile);
            DiscardPile.Clear();
        }

        /// <summary>
        /// Shuffles a given card deck using Fisher-Yates shuffle algorithm.
        /// </summary>
        /// <param name="deck">Deck to shuffle</param>
        private void Shuffle(List<ICard> deck)
        {
            int index = deck.Count;
            while (index > 1)
            {
                index--;
                int swapPosition = random.Next(index + 1);
                ICard card = deck[swapPosition];
                deck[swapPosition] = deck[index];
                deck[index] = card;
            }
        }

        /// <summary>
        /// Adds a card to the discard pile.
        /// </summary>
        /// <param name="card">Card to discard</param>
        public void Discard(ICard card)
        {
            DiscardPile.Add(card);
        }

        public Deck Clone()
        {
            Deck newDeck = new Deck();
            newDeck.random = new Random();
            newDeck.DiscardPile = new List<ICard>(this.DiscardPile);
            newDeck.CardDeck = new Queue<ICard>(this.CardDeck);
            return newDeck;
        }
    }
}