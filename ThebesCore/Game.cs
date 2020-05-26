using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;

namespace ThebesCore
{  
    public interface IGame : ICloneable
    {
        IPlayer ActivePlayer { get; }
        List<IPlayer> Players { get; }
        ICard[] DisplayedCards { get; }
        ICard[] DisplayedExhibitions { get; }
        void Move(IAction action);
    }
    
    [Serializable]
    public class Game : IGame
    {
        public IPlayer ActivePlayer { get { return Players[0]; } }
        public List<IPlayer> Players { get; set; }
        public IDeck Deck { get; set; }
        public ICardDisplay AvailableCards { get; set; }
        public IExhibitionDisplay ActiveExhibitions { get; set; }

        ICard[] IGame.DisplayedCards { get { return AvailableCards.AvailableCards; } }
        ICard[] IGame.DisplayedExhibitions { get { return ActiveExhibitions.Exhibitions; } }



        public Game() { }
        public Game(int playerCount)
        {
            this.Deck = new Deck(GameSettings.Cards, playerCount);

            AvailableCards = new CardDisplay(DrawCard, Deck.Discard);
            ActiveExhibitions = new ExhibitionDisplay(Deck.Discard);

            Time.Configure(playerCount);
        }

        /// <summary>
        /// Returns the number of players who are on the specified week
        /// </summary>
        /// <param name="time">ITime object specifying the week</param>
        /// <returns></returns>
        public int PlayersOnWeek(ITime time)
        {
            int count = 0;
            foreach (IPlayer player in Players)
            {
                if (player.Time.Equals(time))
                {
                    count++;
                }
            }
            return count;
        }
        
        /// <summary>
        /// Check if there's at least one player who has some time left
        /// </summary>
        /// <returns></returns>
        protected bool AreAllPlayersDone()
        {
            foreach (IPlayer player in Players)
            {
                if (player.Time.RemainingWeeks() > 0)
                {
                    return false;
                }
            }
            return true;
        }

        public void Move(IAction action)
        {
            if (action != null)
            {
                action.Execute(ActivePlayer);
                ResetCardChangeInfos();
                Players.Sort();
            }
        }


        /// <summary>
        /// Draws cards from <see cref="Deck"/> until a non-exhibition card is found. Exhibitions drawn are added using <see cref="AddNewExhibitionCard(ExhibitionCard)"/> method.
        /// </summary>
        /// <returns>First non-exhibition card drawn from <see cref="Deck"/></returns>
        public ICard DrawCard()
        {
            ICard drawnCard;
            while ((drawnCard = Deck.DrawCard()) is IExhibitionCard)
            {
                ActiveExhibitions.DisplayExhibition((IExhibitionCard)drawnCard);
            }
            return drawnCard;
        }

        /// <summary>
        /// Resets the card change info for every player (it's used to implement the changing-cards-in-a-row rule)
        /// </summary>
        protected void ResetCardChangeInfos()
        {
            for (int i = 1; i < Players.Count; i++)
            {
                Players[i].ResetCardChnageInfo();
            }
        }

        /// <summary>
        /// Awards each player who has the highest amount of specialized knowledge about one dig site 5 points. 3 points if there's two or more player with the most knowledge.
        /// Should be called at the very end of the game
        /// </summary>
        public void AddPointsFromKnowledge()
        {
            foreach (IDigSite digSite in GameSettings.Places.Where(p => p is IDigSite))
            {
                Players.OrderByDescending(p => p.SpecializedKnowledge[digSite]);

                if (Players[0].SpecializedKnowledge[digSite] > 0 && Players[0].SpecializedKnowledge[digSite] > Players[1].SpecializedKnowledge[digSite])
                {
                    Players[0].Points += 5;
                }
                else if (Players[0].SpecializedKnowledge[digSite] > 0)
                {
                    foreach (IPlayer player in Players)
                    {
                        if (player.SpecializedKnowledge[digSite] == Players[0].SpecializedKnowledge[digSite])
                        {
                            player.Points += 3;
                        }
                    }
                }
            }

        }
        public object Clone()
        {
            new Game();
        }
    }
}