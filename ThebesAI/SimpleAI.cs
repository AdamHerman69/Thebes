using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThebesCore;

namespace ThebesAI
{
    

    public class TestAI : IAI
    {
        IPlayerData player;
        IGame game;

        public TestAI(IPlayerData player, IGame game)
        {
            this.player = player;
            this.game = game;
        }
        
        public IAction TakeAction(IGame gameState)
        {
            IDigSiteSimpleView digSite;
            if ((digSite = CanIDig()) != null)
            {
                return new DigAction(digSite, 7, null, null);
            }

            return new TakeCardAction(ChooseCard());
        }

        private IDigSiteSimpleView CanIDig()
        {
            foreach (KeyValuePair<IDigSiteSimpleView, int> digSite_amount in player.SpecializedKnowledge)
            {
                if (digSite_amount.Value > 1 && digSite_amount.Value + player.GeneralKnowledge > 5 && player.Permissions[digSite_amount.Key])
                {
                    return digSite_amount.Key;
                }
            }
            return null;
        }

        private ICard ChooseCard()
        {
            ICard card;
            IEnumerable<ICard> cards;
            if ((cards = game.DisplayedCards.Where(c => c.Place == player.CurrentPlace)).Count() > 0)
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
            return game.DisplayedCards[0];
        }

        private bool IsDesired(ICard card)
        {
            if (card is ISpecializedKnowledgeCard && player.SpecializedKnowledge[((ISpecializedKnowledgeCard)card).digSite] > 1)
            {
                return true;
            }
            return false;
        }
    }

    
}
