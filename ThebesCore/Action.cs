using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThebesCore
{
    /// <summary>
    /// Each instance of a class implementing this interface represents one action a player can do.
    /// It's used to send information between the UI and the Core.
    /// </summary>
    public interface IAction
    {
        /// <summary>
        /// Executes the action for the specified <paramref name="player"/>
        /// </summary>
        /// <param name="player">Player who is executing the action</param>
        void Execute(IPlayer player);
    }

    public class ChangeCardsAction : IAction
    {
        ICardChangePlace cardChangePlace;

        public ChangeCardsAction(ICardChangePlace cardChangePlace)
        {
            this.cardChangePlace = cardChangePlace;
        }

        public void Execute(IPlayer player)
        {
            player.MoveAndChangeDisplayCards(cardChangePlace);
        }
    }

    public class TakeCardAction : IAction
    {
        ICard card;

        public TakeCardAction(ICard card)
        {
            this.card = card;
        }

        public void Execute(IPlayer player)
        {
            player.MoveAndTakeCard(card);
        }
    }

    public class ExecuteExhibitionAction : IAction
    {
        IExhibitionCard exhibition;

        public ExecuteExhibitionAction(IExhibitionCard exhibition)
        {
            this.exhibition = exhibition;
        }

        public void Execute(IPlayer player)
        {
            player.MoveAndTakeCard(exhibition);
        }
    }

    public class EndYearAction : IAction
    {
        public EndYearAction() {}

        public void Execute(IPlayer player)
        {
            player.EndYear();
        }
    }

    public class ZeppelinAction : IAction
    {
        public ZeppelinAction() { }

        public void Execute(IPlayer player)
        {
            player.UseZeppelin();
        }
    }

    public class DigAction : IAction
    {
        IDigSite digSite;
        int weeks;
        List<ICard> singleUseCards;
        List<IToken> tokens;

        public DigAction(IDigSite digSite, int weeks, List<ICard> singleUseCards, List<IToken> tokens)
        {
            this.digSite = digSite;
            this.weeks = weeks;
            this.singleUseCards = singleUseCards;
            this.tokens = tokens;
        }

        public void Execute(IPlayer player)
        {
            List<IToken> dugTokens = player.Dig(digSite, weeks, singleUseCards);
            if (dugTokens != null && tokens != null)
            {
                foreach (IToken token in dugTokens)
                {
                    tokens.Add(token);
                }
            }
            
        }
    }


}
