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
        public abstract override string ToString();
    }

    public class ChangeCardsAction : Action
    {
        public ICardChangePlace cardChangePlace;

        public ChangeCardsAction(ICardChangePlace cardChangePlace)
        {
            this.cardChangePlace = cardChangePlace;
        }

        public override void Execute(IPlayer player)
        {
            log.Debug($"{player.Time}: Player {player.Name} Changing cards at {cardChangePlace}");
            player.MoveAndChangeDisplayCards(cardChangePlace);
        }

        public override string ToString()
        {
            return $"ChangeCards";
        }
    }

    public class TakeCardAction : Action
    {
        public ICard card;

        public TakeCardAction(ICard card)
        {
            this.card = card;
        }

        public override void Execute(IPlayer player)
        {
            log.Debug($"{player.Time}: Player {player.Name} taking {card}");
            player.MoveAndTakeCard(card);
        }

        public override string ToString()
        {
            return $"Take {card}";
        }
    }

    public class ExecuteExhibitionAction : Action
    {
        public IExhibitionCard exhibition;

        public ExecuteExhibitionAction(IExhibitionCard exhibition)
        {
            this.exhibition = exhibition;
        }

        public override void Execute(IPlayer player)
        {
            log.Debug($"{player.Time}: Player {player.Name} executing exhibition: {exhibition}");
            player.MoveAndTakeCard(exhibition);
        }

        public override string ToString()
        {
            return $"Execute {exhibition}";
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

        public override string ToString()
        {
            return $"EndYear";
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

        public override string ToString()
        {
            return $"UseZeppelin";
        }
    }

    public class DigAction : Action
    {
        public IDigSite digSite;
        public int weeks;
        List<ICard> singleUseCards;
        System.Action<List<IToken>> displayDigResult;

        public DigAction(IDigSite digSite, int weeks, List<ICard> singleUseCards, System.Action<List<IToken>> displayDigResult)
        {
            this.digSite = digSite;
            this.weeks = weeks;
            this.singleUseCards = singleUseCards;
            this.displayDigResult = displayDigResult;
        }

        public override void Execute(IPlayer player)
        {
            log.Debug($"{player.Time}: Player {player.Name} digging at {digSite} for {weeks} weeks");

            List<IToken> dugTokens = player.Dig(digSite, weeks, singleUseCards);
            
            // only if you need to inspect the result
            if (dugTokens != null && displayDigResult != null)
            {
                List<IToken> tokens = new List<IToken>();
                foreach (IToken token in dugTokens)
                {
                    tokens.Add(token);
                }
                displayDigResult(tokens);
            }

            
        }

        public override string ToString()
        {
            return $"Dig {digSite}, {weeks} weeks";
        }
    }


}
