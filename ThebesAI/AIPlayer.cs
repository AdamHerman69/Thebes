using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThebesCore;

namespace ThebesAI
{
    [Serializable]
    public class AIPlayer : Player, IAIPlayer
    {
        public IAI AI { get; private set; }

        public AIPlayer(string name, List<IDigSite> digSites, IPlace startingPlace, List<IPlace> places, Action<string> errorDialog, System.Action changeDisplayCards, Action<ICard> takeCard, Action<ICard> discardCard, Action<IExhibitionCard> executeExhibition, Func<ITime, int> playersOnWeek) : base(name, digSites, startingPlace, errorDialog, changeDisplayCards, takeCard, discardCard, executeExhibition, playersOnWeek)
        {
        }

        public void Init(IAI ai)
        {
            this.AI = ai;
        }
    }
}
