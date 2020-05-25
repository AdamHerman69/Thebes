using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThebesCore;

namespace ThebesAI
{
    class BetterAI : IAI
    {
        IPlayerData player;
        IGame game;

        public BetterAI(IPlayerData player, IGame game)
        {
            this.player = player;
            this.game = game;
        }  

        public IAction TakeAction(IGame gameState)
        {
            throw new NotImplementedException();
        }
    }

    class StatePrediction
    {
        List<PlayerPrediction> players;
        // cards
        // exhibitions
        // digsites with expected distributions
    }

    class PlayerPrediction
    {
        // pro různý karty array pravděpodobností
        // nebo střední hodnota
    }

    abstract class Move
    {
        int weeks;


        public abstract StatePrediction Result(StatePrediction previousState);
    }

    //class CardMove : Move
    //{

    //}

    //class DigMove : Move
    //{

    //}

    //class ExhibitionMove : Move
    //{

    //}

    //class ChangeCardsMove : Move
    //{

    //}

    //class ZeppelinMove : Move
    //{

    //}

    //class EndYearMove : Move
    //{

    //}
}
