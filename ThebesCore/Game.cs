using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.AccessControl;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace ThebesCore
{   
    [Serializable]
    public class Game
    {
        public List<IPlayer> Players { get; set; }
        public IDeck Deck { get; set; }
        public ICardDisplay AvailableCards { get; set; }
        public IExhibitionDisplay ActiveExhibitions { get; set; }
                
        public Game(int playerCount)
        {
            this.Deck = new Deck(GameSettings.Cards, playerCount);

            AvailableCards = new CardDisplay(DrawCard, Deck.Discard);
            ActiveExhibitions = new ExhibitionDisplay(Deck.Discard);

            Time.Configure(playerCount);
        }

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

        protected void ResetCardChangeInfos()
        {
            for (int i = 1; i < Players.Count; i++)
            {
                Players[i].ResetCardChnageInfo();
            }
        }
    }

    [Serializable]
    public class ConsoleGame : Game
    {
        public ConsoleGame(int playerCount) : base(playerCount) { }
        public void Play()
        {
            while (!AreAllPlayersDone())
            {
                Players.Sort();
                PrintState();
                ((ConsolePlayer)Players[0]).TakeActionWrapper(new List<ICard>(AvailableCards.AvailableCards), ActiveExhibitions.Exhibitions.ToList<IExhibitionCard>().Where(x => x != null).ToList());
                ResetCardChangeInfos();
            }

            Console.WriteLine("---- GAME ENDED ----");
        }

        private void PrintState()
        {
            Console.WriteLine("--------------------------------------------------------------------------------------------");

            // Player stats
            foreach (IPlayer player in Players)
            {
                Console.Write(player.ToString() + "\n\n");
            }

            // Cards Available
            Console.WriteLine("Available Cards:");
            foreach (ICard card in AvailableCards.AvailableCards)
            {
                Console.WriteLine(card);
            }
            Console.WriteLine();
            foreach (IExhibitionCard exhibition in this.ActiveExhibitions.Exhibitions)
            {
                Console.WriteLine(exhibition);
            }

            Console.WriteLine($"\n{Players[0].Name}'s turn");
        }
    }

    public interface IUIGame
    {
        List<IPlayer> Players { get; }
        IPlayer ActivePlayer { get; }
        ICard[] DisplayedCards { get; }
        ICard[] DisplayedExhibitions { get; }
        void Initialize(List<IPlayer> players);
    }

    [Serializable]
    public class UIGame : Game, IUIGame
    {
        public IPlayer ActivePlayer { get; private set; }

        public ICard[] DisplayedCards { get { return AvailableCards.AvailableCards; } }
        public ICard[] DisplayedExhibitions { get { return ActiveExhibitions.Exhibitions; } }

        public UIGame(int playerCount) : base(playerCount) { }

        public void Initialize(List<IPlayer> players)
        {
            this.Players = players;
            Players.Sort();
            ActivePlayer = Players[0];
        }

        private void NextMove()
        {
            ResetCardChangeInfos();
            Players.Sort();
            ActivePlayer = Players[0];
        }
        
        public void ExecuteAction(IAction action)
        {
            action.Execute(ActivePlayer);

            if (!AreAllPlayersDone())
            {
                NextMove();

                while (!AreAllPlayersDone() && ActivePlayer is IAIPlayer)
                {
                    action = ((IAIPlayer)ActivePlayer).AI.TakeAction(this);
                    action.Execute(ActivePlayer);
                    NextMove();
                }
            }

            if (AreAllPlayersDone())
            {
                //end game
                throw new NotImplementedException();
            }
            
        }
    }

    [Serializable]
    public class GameState
    {
        public IUIGame game { get; set; }
        public GameSettingsSerializable settings { get; set; }

        public GameState(IUIGame game)
        {
            this.game = game;
            settings = new GameSettingsSerializable();
        }

        public static void Serialize(IUIGame game, string filePath)
        {
            GameState state = new GameState(game);

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(filePath, FileMode.Create, FileAccess.Write);
            formatter.Serialize(stream, state);
            stream.Close();
        }

        public static IUIGame Deserialize(string filePath)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            GameState gameState = (GameState)formatter.Deserialize(stream);
            IUIGame game = gameState.game;
            GameSettings.LoadSerializedData(gameState.settings);
            Time.Configure(game.Players.Count);
            return game;
        }
    }
}