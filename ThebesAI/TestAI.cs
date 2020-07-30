using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using ThebesCore;

namespace ThebesAI
{
    /// <summary>
    /// First AI developed, surprisingly not that bad
    /// </summary>
    [Serializable]
    public class TestAI : IAI
    {
        IPlayerData player;
        IGame game;

        public TestAI(int playerCount)
        {

        }
        
        public IAction TakeAction(IGame gameState)
        {
            this.game = gameState;
            this.player = gameState.ActivePlayer;
            
            IExhibitionCard exhibition;
            if (player.Time.RemainingWeeks() > 10 && (exhibition = CanIExhibit()) != null)
            {
                return new ExecuteExhibitionAction(exhibition);
            }
            
            IDigSite digSite;
            if (player.Time.RemainingWeeks() >= 11 && (digSite = CanIDig()) != null)
            {
                return new DigAction(digSite, 7, null, null);
            }

            ICard card = ChooseCard();
            if (player.Time.RemainingWeeks() >= card.Weeks + GameSettings.GetDistance(player.CurrentPlace, card.Place))
            {
                return new TakeCardAction(card);
            }
            return new EndYearAction();
            
        }

        private IDigSite CanIDig()
        {
            foreach (KeyValuePair<IDigSite, int> digSite_amount in player.SpecializedKnowledge)
            {
                if (digSite_amount.Value > 1 && digSite_amount.Value + player.GeneralKnowledge > 5 && player.Permissions[digSite_amount.Key])
                {
                    return digSite_amount.Key;
                }
            }
            return null;
        }

        private IExhibitionCard CanIExhibit()
        {
            foreach (IExhibitionCard exhibition in game.DisplayedExhibitions)
            {
                if (exhibition == null) continue;

                if (exhibition.CheckRequiredArtifacts(player.Tokens))
                {
                    return exhibition;
                }
            }
            return null;
        }

        private ICard ChooseCard()
        {
            IEnumerable<ICard> cards;
            if ((cards = game.DisplayedCards.Where(c => c?.Place == player.CurrentPlace)).Count() > 0)
            {
                return cards.First();
            }
            else if ((cards = game.DisplayedCards.Where(c => c is IGeneralKnowledgeCard)).Count() > 0)
            {
                return cards.First();
            }
            else if ((cards = game.DisplayedCards.Where(c => IsDesired(c))).Count() > 0)
            {
                return cards.First();
            }
            return ClosestCard();
        }

        private bool IsDesired(ICard card)
        {
            if (card is ISpecializedKnowledgeCard && player.SpecializedKnowledge[((ISpecializedKnowledgeCard)card).digSite] > 1 && player.SpecializedKnowledge[((ISpecializedKnowledgeCard)card).digSite] < 5)
            {
                return true;
            }
            return false;
        }

        private ICard ClosestCard()
        {
            int shortestDistance = int.MaxValue;
            ICard closestCard = null;
            foreach (ICard card in game.DisplayedCards)
            {
                if (shortestDistance > GameSettings.GetDistance(player.CurrentPlace, card.Place))
                {
                    shortestDistance = GameSettings.GetDistance(player.CurrentPlace, card.Place);
                    closestCard = card;
                }
            }
            return closestCard;
        }
    }

    
}
