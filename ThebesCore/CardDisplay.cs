using System;
using System.Collections.Generic;
using System.Text;

namespace ThebesCore
{
    public interface ICardDisplay
    {
        ICard[] AvailableCards { get; }

        void ChangeDisplayedCards();
        void GiveCard(ICard card);
    }

    public class CardDisplay : ICardDisplay
    {
        public ICard[] AvailableCards { get; private set; }
        Func<ICard> drawCardMethod;
        Action<ICard> DiscardCard;
        public static int timeToChangeCards = 1;

        public CardDisplay(Func<ICard> drawCard, Action<ICard> discardCard)
        {
            AvailableCards = new ICard[4];
            drawCardMethod = drawCard;
            DiscardCard = discardCard;

            for (int i = 0; i < AvailableCards.Length; i++)
            {
                AvailableCards[i] = drawCardMethod();
            }
        }

        public void ChangeDisplayedCards()
        {
            for (int i = 0; i < AvailableCards.Length; i++)
            {
                DiscardCard(AvailableCards[i]);
            }

            for (int i = 0; i < AvailableCards.Length; i++)
            {
                AvailableCards[i] = drawCardMethod();
            }
        }

        public void GiveCard(ICard card)
        {
            int cardIndex = Array.IndexOf(this.AvailableCards, card);
            if (cardIndex < 0)
            {
                throw new InvalidOperationException("Desired card is not on display.");
            }

            this.AvailableCards[cardIndex] = drawCardMethod();
        }
    }

    public interface IAvailableExhibitions
    {
        IExhibitionCard[] Exhibitions { get; set; }

        void DisplayExhibition(IExhibitionCard exhibition);
        void GiveExhibition(IExhibitionCard exhibition);
    }

    public class AvailableExhibitions : IAvailableExhibitions
    {
        public IExhibitionCard[] Exhibitions { get; set; }
        Action<ICard> DiscardCard;

        public AvailableExhibitions(Action<ICard> DiscardCardMethod)
        {
            Exhibitions = new IExhibitionCard[3];
            DiscardCard = DiscardCardMethod;
        }

        public void DisplayExhibition(IExhibitionCard exhibition)
        {
            if (Exhibitions[Exhibitions.Length - 1] != null)
            {
                DiscardCard(Exhibitions[Exhibitions.Length - 1]);
            }

            for (int i = Exhibitions.Length - 1; i > 0; i--)
            {
                Exhibitions[i] = Exhibitions[i - 1];
            }
            Exhibitions[0] = exhibition;
        }

        public void GiveExhibition(IExhibitionCard exhibition)
        {
            int cardIndex = Array.IndexOf(Exhibitions, exhibition);
            if (cardIndex < 0)
            {
                throw new InvalidOperationException("Exhibition is not active.");
            }

            Exhibitions[cardIndex] = null;
        }

    }
}