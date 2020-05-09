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
        void Initialize(List<IPlayer> players);
        new ICardView[] DisplayedCards { get; }
        new ICardView[] DisplayedExhibitions { get; }
    }

    [Serializable]
    public class UIGame : Game, IUIGame
    {
        public IPlayer ActivePlayer { get; private set; }

        public ICardView[] DisplayedCards { get {return Array.ConvertAll(AvailableCards.AvailableCards, ToView); } }
        public ICardView[] DisplayedExhibitions { get { return Array.ConvertAll(ActiveExhibitions.Exhibitions, ToView); } }

        ICard[] IGame.DisplayedCards { get { return AvailableCards.AvailableCards; } }

        ICard[] IGame.DisplayedExhibitions { get { return ActiveExhibitions.Exhibitions; } }

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

        private ICardView ToView(ICard card)
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
