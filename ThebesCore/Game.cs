using System;
using System.Collections.Generic;
using System.Linq;
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
        public IAvailableExhibitions ActiveExhibitions { get; set; }
                
        public Game(int playerCount)
        {
            this.Deck = new Deck(GameSettings.Cards, playerCount);

            AvailableCards = new CardDisplay(DrawCard, Deck.Discard);
            ActiveExhibitions = new AvailableExhibitions(Deck.Discard);

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

    [Serializable]
    public class UIGame : Game
    {
        public IPlayer activePlayer;

        public UIGame(int playerCount) : base(playerCount) { }

        public void Initialize(List<IPlayer> players)
        {
            this.Players = players;
            Players.Sort();
            activePlayer = Players[0];
        }

        public void NextMove()
        {
            if (!AreAllPlayersDone())
            {
                ResetCardChangeInfos();
                Players.Sort();
                activePlayer = Players[0];
                // TODO if active player is AI do his turn and NextMove
            }
            else
            {
                //end game
                throw new NotImplementedException();
            }
            
        }

        public string GetImgFilePath (IItem item)
        {
            // TODO actual file path
            if (item is IToken)
            {
                return @"C:\Users\admhe\source\repos\Thebes\img\one_token_sample.png";
            }
            if (item is ICard)
            {
                return @"C:\Users\admhe\source\repos\Thebes\img\card-46.png";
            }
            throw new InvalidOperationException();
        }
    }

    [Serializable]
    public class GameState
    {
        public Game game { get; set; }
        public GameSettingsSerializable settings { get; set; }

        public GameState(Game game)
        {
            this.game = game;
            settings = new GameSettingsSerializable();
        }
    }
}