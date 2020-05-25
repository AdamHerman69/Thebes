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

    public abstract class Action : IAction
    {
        protected static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public abstract void Execute(IPlayer player);
    }

    public class ChangeCardsAction : Action
    {
        ICardChangePlace cardChangePlace;

        public ChangeCardsAction(ICardChangePlace cardChangePlace)
        {
            this.cardChangePlace = cardChangePlace;
        }

        public override void Execute(IPlayer player)
        {
            log.Debug($"{player.Time}: Player {player.Name} Changing cards at {cardChangePlace}");
            player.MoveAndChangeDisplayCards(cardChangePlace);
        }
    }

    public class TakeCardAction : Action
    {
        ICard card;

        public TakeCardAction(ICard card)
        {
            this.card = card;
        }

        public override void Execute(IPlayer player)
        {
            log.Debug($"{player.Time}: Player {player.Name} taking {card}");
            player.MoveAndTakeCard(card);
        }
    }

    public class ExecuteExhibitionAction : Action
    {
        IExhibitionCard exhibition;

        public ExecuteExhibitionAction(IExhibitionCard exhibition)
        {
            this.exhibition = exhibition;
        }

        public override void Execute(IPlayer player)
        {
            log.Debug($"{player.Time}: Player {player.Name} executing exhibition: {exhibition}");
            player.MoveAndTakeCard(exhibition);
        }
    }

    public class EndYearAction : Action
    {
        public EndYearAction() {}

        public override void Execute(IPlayer player)
        {
            log.Debug($"{player.Time}: Player {player.Name} ending year");
            player.EndYear();
        }
    }

    public class ZeppelinAction : Action
    {
        bool use;
        public ZeppelinAction(bool use)
        {
            this.use = use;
        }

        public override void Execute(IPlayer player)
        {
            log.Debug($"{player.Time}: Player {player.Name} using a zeppelin");
            player.ToggleZeppelin(use);
        }
    }

    public class DigAction : Action
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

        public override void Execute(IPlayer player)
        {
            log.Debug($"{player.Time}: Player {player.Name} digging at {digSite} for {weeks} weeks");

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
