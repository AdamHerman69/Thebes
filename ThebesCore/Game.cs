using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;

namespace ThebesCore
{  
    public interface IGame
    {
        IPlayer ActivePlayer { get; }
        List<IPlayer> Players { get; }
        ICard[] DisplayedCards { get; }
        ICard[] DisplayedExhibitions { get; }
        void Move(IAction action);
        Dictionary<IDigSite, List<IToken>> DigsiteInventory { get; }
        IGame Clone();
    }
    
    [Serializable]
    public class Game : IGame
    {
        Random random;
        public IPlayer ActivePlayer { get { return Players[0]; } }
        public List<IPlayer> Players { get; set; }
        public IDeck Deck { get; set; }
        public ICardDisplay AvailableCards { get; set; }
        public IExhibitionDisplay ActiveExhibitions { get; set; }

        ICard[] IGame.DisplayedCards { get { return AvailableCards.AvailableCards; } }
        ICard[] IGame.DisplayedExhibitions { get { return ActiveExhibitions.Exhibitions; } }

        public Dictionary<IDigSite, List<IToken>> DigsiteInventory { get; private set; }
        private bool pointsFromKnowledgeAdded = false;


        public Game() { }
        public Game(int playerCount)
        {
            random = new Random();
            this.Deck = new Deck(GameSettings.Cards, playerCount);

            AvailableCards = new CardDisplay(DrawCard, Deck.Discard);
            ActiveExhibitions = new ExhibitionDisplay(Deck.Discard);

            DigsiteInventory = new Dictionary<IDigSite, List<IToken>>();
            foreach (IPlace place in GameSettings.Places)
            {
                if (place is IDigSite)
                {
                    IDigSite digSite = (IDigSite)place;
                    DigsiteInventory.Add(digSite, new List<IToken>());

                    foreach (IToken token in digSite.Tokens)
                    {
                        DigsiteInventory[digSite].Add(token);
                    }
                }
            }

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
        /// Randomly draws the requested amount of tokens. Keeping just the dirt.
        /// </summary>
        /// <param name="tokenAmount"></param>
        /// <returns>List of drawn tokens</returns>
        public List<IToken> DrawTokens(IDigSite digSite, int tokenAmount)
        {
            List<IToken> tokensDrawn = new List<IToken>();
            List<IToken> tokens = DigsiteInventory[digSite];
            for (int i = 0; i < tokenAmount; i++)
            {
                IToken tokenDrawn = tokens[random.Next(0, tokens.Count)];
                if (!(tokenDrawn is IDirtToken))
                {
                    tokens.Remove(tokenDrawn);
                }
                tokensDrawn.Add(tokenDrawn);
            }
            return tokensDrawn;
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

            if (AreAllPlayersDone() && !pointsFromKnowledgeAdded)
            {
                AddPointsFromKnowledge();
                pointsFromKnowledgeAdded = true;
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
            List<IPlayer> sortedPlayers;
            foreach (IDigSite digSite in GameSettings.Places.Where(p => p is IDigSite))
            {
                sortedPlayers = Players.OrderByDescending(p => p.SpecializedKnowledge[digSite]).ToList();

                if (sortedPlayers[0].SpecializedKnowledge[digSite] > 0 && sortedPlayers[0].SpecializedKnowledge[digSite] > sortedPlayers[1].SpecializedKnowledge[digSite])
                {
                    sortedPlayers[0].Points += 5;
                }
                else if (sortedPlayers[0].SpecializedKnowledge[digSite] > 0)
                {
                    foreach (IPlayer player in sortedPlayers)
                    {
                        if (player.SpecializedKnowledge[digSite] == sortedPlayers[0].SpecializedKnowledge[digSite])
                        {
                            player.Points += 3;
                        }
                    }
                }
            }

        }

        public IGame Clone()
        {
            Game newGame = new Game();

            newGame.random = new Random();
            newGame.Deck = this.Deck.Clone();
            newGame.AvailableCards = this.AvailableCards.Clone(newGame.DrawCard, newGame.Deck.Discard);
            newGame.ActiveExhibitions = this.ActiveExhibitions.Clone(newGame.Deck.Discard);
            
            newGame.DigsiteInventory = new Dictionary<IDigSite, List<IToken>>();
            foreach (KeyValuePair<IDigSite, List<IToken>> digsite_tokenList in this.DigsiteInventory)
            {
                newGame.DigsiteInventory[digsite_tokenList.Key] = new List<IToken>(this.DigsiteInventory[digsite_tokenList.Key]);
            }


            newGame.Players = this.Players.Select(p => p.Clone(
                null,
                newGame.AvailableCards.ChangeDisplayedCards,
                newGame.AvailableCards.GiveCard,
                newGame.Deck.Discard,
                newGame.ActiveExhibitions.GiveExhibition,
                newGame.DrawTokens,
                newGame.PlayersOnWeek
                )).ToList();

            return newGame;
        }
    }
}