using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThebesCore;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Runtime.Remoting.Messaging;

namespace ThebesUI
{
    public interface IUIGame : IGame
    {
        IPlayer ActivePlayer { get; }
        void Initialize(Dictionary<IPlayer, PlayerColor> playerColors);
        new ICardView[] DisplayedCards { get; }
        new ICardView[] DisplayedExhibitions { get; }
        bool ExecuteAction(IAction action);
        Dictionary<IPlayer, PlayerColor> Colors { get; }
    }

    [Serializable]
    public class UIGame : Game, IUIGame
    {
        public IPlayer ActivePlayer { get; private set; }

        public ICardView[] DisplayedCards { get {return Array.ConvertAll(AvailableCards.AvailableCards, ToView); } }
        public ICardView[] DisplayedExhibitions { get { return Array.ConvertAll(ActiveExhibitions.Exhibitions, ToView); } }

        ICard[] IGame.DisplayedCards { get { return AvailableCards.AvailableCards; } }

        ICard[] IGame.DisplayedExhibitions { get { return ActiveExhibitions.Exhibitions; } }
        public Dictionary<IPlayer, PlayerColor> Colors { get; private set; }

        public UIGame(int playerCount) : base(playerCount) {}

        /// <summary>
        /// Initializes the game. Needs to be called before using.
        /// </summary>
        /// <param name="playerColors"></param>
        public void Initialize(Dictionary<IPlayer, PlayerColor> playerColors)
        {
            this.Players = playerColors.Keys.ToList();
            this.Colors = playerColors;
            Players.Sort();
            ActivePlayer = Players[0];
        }

        /// <summary>
        /// Sorts the players and selects the player who's next. Called after an executed action.
        /// </summary>
        private void NextMove()
        {
            ResetCardChangeInfos();
            Players.Sort();
            ActivePlayer = Players[0];
        }

        /// <summary>
        /// Executes the given action, lets the AI player playa and ends when it's a human player's turn.
        /// Also ends the game when all players are done.
        /// </summary>
        /// <param name="action">action to execute</param>
        /// <returns></returns>
        public bool ExecuteAction(IAction action)
        {
            if (action != null)
            {
                action.Execute(ActivePlayer);
            }

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
                return true;
            }
            return false;
        }

        public static ICardView ToView(ICard card)
        {
            if (card is null)
            {
                return null;
            }
            else if (card is ISpecializedKnowledgeCard)
            {
                return new SpecializedKnowledgeCardView((ISpecializedKnowledgeCard)card);
            }
            else if (card is IGeneralKnowledgeCard)
            {
                return new GeneralKnowledgeCardView((IGeneralKnowledgeCard)card);
            }
            else if (card is IRumorsCard)
            {
                return new RumorsCardView((IRumorsCard)card);
            }
            else if (card is IZeppelinCard)
            {
                return new ZeppelinCardView((IZeppelinCard)card);
            }
            else if (card is ICarCard)
            {
                return new CarCardView((ICarCard)card);
            }
            else if (card is IAssistantCard)
            {
                return new AssistantCardView((IAssistantCard)card);
            }
            else if (card is IShovelCard)
            {
                return new ShovelCardView((IShovelCard)card);
            }
            else if (card is ISpecialPermissionCard)
            {
                return new SpecialPermissionCardView((ISpecialPermissionCard)card);
            }
            else if (card is ICongressCard)
            {
                return new CongressCardView((ICongressCard)card);
            }
            else if (card is IExhibitionCard)
            {
                return new ExhibitionCardView((IExhibitionCard)card);
            }
            throw new InvalidCastException();
        }
        public static ITokenView ToView(IToken token)
        {
            if (token is IDirtToken)
            {
                return new DirtTokenView((IDirtToken)token);
            }
            else if (token is IArtifactToken)
            {
                return new ArtifactTokenView((IArtifactToken)token);
            }
            else if (token is ISpecializedKnowledgeToken)
            {
                return new SpecializedKnowledgeTokenView((ISpecializedKnowledgeToken)token);
            }
            else if (token is IGeneralKnowledgeToken)
            {
                return new GeneralKnowledgeTokenView((IGeneralKnowledgeToken)token);
            }
            throw new InvalidCastException();
        }
    }

    /// <summary>
    /// Used to serialize / deserialize the game state.
    /// </summary>
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

        /// <summary>
        /// Serializes the game state and saves it to filePath
        /// </summary>
        /// <param name="game">game object to serialize</param>
        /// <param name="filePath">where to save</param>
        public static void Serialize(IUIGame game, string filePath)
        {
            GameState state = new GameState(game);

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(filePath, FileMode.Create, FileAccess.Write);
            formatter.Serialize(stream, state);
            stream.Close();
        }

        /// <summary>
        /// Deserializes the game from a file. 
        /// </summary>
        /// <param name="filePath">file to deserialize</param>
        /// <returns>Game that is ready to run</returns>
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
