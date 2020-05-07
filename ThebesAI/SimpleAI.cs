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
        public IAction TakeAction(IUIGame gameState)
        {
            return new TakeCardAction(gameState.DisplayedCards[0]);
        }
    }

    
}
