using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThebesCore;
using System.Diagnostics;
using System.Runtime.InteropServices.ComTypes;

namespace ThebesAI
{
    class DeterministicGame : Game
    {
        public Dictionary<IDigSite, double> assumedArtifactSum;
        public Dictionary<IDigSite, double> assumedArtifactCount;

        public DeterministicGame(Game game)
        {
            this.Deck = game.Deck.Clone();
            this.AvailableCards = game.AvailableCards.Clone(this.DrawCard, this.Deck.Discard);
            this.ActiveExhibitions = game.ActiveExhibitions.Clone(this.Deck.Discard);

            this.DigsiteInventory = new Dictionary<IDigSite, List<IToken>>();
            foreach (KeyValuePair<IDigSite, List<IToken>> digsite_tokenList in game.DigsiteInventory)
            {
                this.DigsiteInventory[digsite_tokenList.Key] = new List<IToken>(game.DigsiteInventory[digsite_tokenList.Key]);
            }

            this.BonusTokens = new Dictionary<IDigSite, IToken>(game.BonusTokens);

            this.Players = new List<IPlayer>();
            foreach (IPlayer player in game.Players)
            {
                this.Players.Add(new DeterministicPlayer(this, (Player)player));
            }


            //this.Players = game.Players.Select(p => p.Clone(
            //    null,
            //    this.AvailableCards.ChangeDisplayedCards,
            //    this.AvailableCards.GiveCard,
            //    this.Deck.Discard,
            //    this.ActiveExhibitions.GiveExhibition,
            //    this.DrawTokens,
            //    this.PlayersOnWeek
            //    )).ToList();

            // SimGame Specifics
            assumedArtifactSum = new Dictionary<IDigSite, double>();
            assumedArtifactCount = new Dictionary<IDigSite, double>();

            if (game is DeterministicGame)
            {
                assumedArtifactCount = new Dictionary<IDigSite, double>(((DeterministicGame)game).assumedArtifactCount);
                assumedArtifactSum = new Dictionary<IDigSite, double>(((DeterministicGame)game).assumedArtifactSum);
            }
            else
            {   
                foreach (var digSite_tokenList in DigsiteInventory)
                {
                    assumedArtifactSum[digSite_tokenList.Key] = 0;
                    assumedArtifactCount[digSite_tokenList.Key] = 0;

                    foreach (IToken token in digSite_tokenList.Value)
                    {
                        if (token is IArtifactToken)
                        {
                            assumedArtifactSum[digSite_tokenList.Key] += ((IArtifactToken)token).Points;
                            assumedArtifactCount[digSite_tokenList.Key] += 1;
                        }
                    }
                }
            }
        }

        public double ExpectedValueOfToken(IDigSite digSite)
        {
            int bonusToken = 0;
            if (this.BonusTokens[digSite] != null)
            {
                bonusToken = 1;
            }
            return assumedArtifactSum[digSite] / (assumedArtifactCount[digSite] + 16) + bonusToken;
        }

        public double ExpectedNumberOfTokens(IDigSite digSite)
        {
            int bonusToken = 0;
            if (this.BonusTokens[digSite] != null)
            {
                bonusToken = 1;
            }
            return assumedArtifactCount[digSite] / (assumedArtifactCount[digSite] + 16) + bonusToken;
        }

        public override IGame Clone()
        {
            return new DeterministicGame(this);
        }

    }

    class DeterministicPlayer : Player
    {
        DeterministicGame game;
        double assumedPoints;
        Dictionary<IDigSite, double> assumedArtifacts;

        public DeterministicPlayer() { }
        
        public DeterministicPlayer(DeterministicGame game, Player player)
        {
            this.Name = player.Name;
            this.CurrentPlace = player.CurrentPlace;

            // delegates
            this.errorDialog = null;
            this.changeDisplayCards = game.AvailableCards.ChangeDisplayedCards;
            this.takeCard = game.AvailableCards.GiveCard;
            this.discardCard = game.Deck.Discard;
            this.executeExhibition = game.ActiveExhibitions.GiveExhibition;
            this.drawTokens = null;

            this.Time = player.Time.Clone(game.PlayersOnWeek, this.ResetPermissions);

            this.GeneralKnowledge = player.GeneralKnowledge;
            this.Zeppelins = player.Zeppelins;
            this.useZeppelin = player.useZeppelin;
            this.SpecialPermissions = player.SpecialPermissions;
            this.Congresses = player.Congresses;
            this.Assistants = player.Assistants;
            this.Shovels = player.Shovels;
            this.Cars = player.Cars;
            this.Points = player.Points;

            this.CardChangeCost = player.CardChangeCost;
            this.LastRoundChange = player.LastRoundChange;

            // Collections 
            this.Permissions = new Dictionary<IDigSite, bool>(player.Permissions);
            this.SpecializedKnowledge = new Dictionary<IDigSite, int>(player.SpecializedKnowledge);
            this.SingleUseKnowledge = new Dictionary<IDigSite, int>(player.SingleUseKnowledge);
            this.Cards = new List<ICard>(player.Cards);

            this.Tokens = new Dictionary<IDigSite, List<IToken>>();
            foreach (KeyValuePair<IDigSite, List<IToken>> digSite_tokenList in player.Tokens)
            {
                this.Tokens[digSite_tokenList.Key] = new List<IToken>(player.Tokens[digSite_tokenList.Key]);
            }

            /////////////////////////////////////////////////

            this.game = game;
            this.assumedPoints = player.Points;

            assumedArtifacts = new Dictionary<IDigSite, double>();
            foreach (var digSite_tokenList in Tokens)
            {
                assumedArtifacts[digSite_tokenList.Key] = digSite_tokenList.Value.Count();
            }
        }


        public double AssumedArtifacts(IDigSite digSite)
        {
            return this.Tokens[digSite].Where(y => y is IArtifactToken).Count() + assumedArtifacts[digSite];
        }

        public double AssumedPoints { get => base.Points + assumedPoints; }
        public override int Points { get => base.Points + (int)assumedPoints; }

        public override void MoveAndTakeCard(ICard card)
        {
            if (card is IExhibitionCard && !((IExhibitionCard)card).CheckRequiredArtifacts(this.AssumedArtifacts))
            {
                errorDialog("You don't have the required artifacts!");
                return;
            }

            int travelTime = GameSettings.GetDistance(CurrentPlace, card.Place);
            if (useZeppelin) travelTime = 0;
            if (Time.RemainingWeeks() < card.Weeks + travelTime)
            {
                errorDialog("You don't have enough time for that!");
                return;
            }

            MoveTo(card.Place);
            TakeCard(card);
        }

        // TODO change getPossibleActions na exibice ^^^

        public override List<IToken> Dig(IDigSite digSite, int weeks, List<ICard> singleUseCards)
        {
            // don't have to check if valid here

            MoveTo(digSite);
            Permissions[digSite] = false;
            Time.SpendWeeks(weeks);

            // get token amount
            GetDigStats(digSite, singleUseCards, out int knowledge, out int tokenBonus);
            int tokenAmount = GameSettings.DugTokenCount(knowledge, weeks) + tokenBonus;

            // draw tokens and give them to player
            double valueDrawn = tokenAmount * game.ExpectedValueOfToken(digSite);
            double tokenCountDrawn = tokenAmount * game.ExpectedNumberOfTokens(digSite);
            assumedPoints += valueDrawn;
            assumedArtifacts[digSite] += tokenCountDrawn;

            // remove assumed tokens from digsite inventory
            game.assumedArtifactSum[digSite] -= valueDrawn;
            game.assumedArtifactCount[digSite] -= tokenCountDrawn;


            // discard single-use cards used
            if (singleUseCards != null)
            {
                foreach (ICard card in singleUseCards)
                {
                    Cards.Remove(card);
                    discardCard(card);
                }
            }
            UpdateStats();

            return null;
        }


        public DeterministicPlayer Clone(DeterministicGame game)
        {
            DeterministicPlayer clone = new DeterministicPlayer(game, this);

            clone.assumedPoints = this.assumedPoints;

            clone.assumedArtifacts = new Dictionary<IDigSite, double>();
            foreach (var digSite_double in this.assumedArtifacts)
            {
                clone.assumedArtifacts[digSite_double.Key] = digSite_double.Value;
            }

            return clone;
        }

    }

    public interface ISimulationState
    {
        ISimulationState NextState(IAction move);
        List<ISimulationState> GetAllChildStates();
        IPlayer ActivePlayer { get; }
        ISimulationState RandomChild();
        Dictionary<string, double> GetScores();
        Dictionary<string, double> GetExpectedScores();
        IAction Move { get; }
        IGame Game { get;}

    }

    public class SimulationState : ISimulationState
    {
        public IGame Game { get; private set; }
        public IAction Move { get; private set; }

        public SimulationState(IGame game, IAction move = null)
        {
            this.Game = game.Clone();
            if (move != null)
            {
                this.Move = move;
                this.Game.Move(move);
            }
        }

        public IPlayer ActivePlayer { get { return Game.ActivePlayer; } }

        public List<ISimulationState> GetAllChildStates()
        {
            List<ISimulationState> childStates = new List<ISimulationState>();
            List<IAction> possibleActions = GetAllPossibleActions();

            foreach (IAction action in possibleActions)
            {
                childStates.Add(new SimulationState(this.Game, action));
            }

            return childStates;
        }

        protected virtual List<IAction> GetAllPossibleActions()
        {
            List<IAction> actions = new List<IAction>();
            IAction action;
            IPlayer player = Game.ActivePlayer;

            //no time left
            if (player.Time.RemainingWeeks() == 0)
            {
                return actions;
            }

            // change cards
            foreach (IPlace place in GameSettings.Places)
            {
                if (place is ICardChangePlace)
                {
                    action = new ChangeCardsAction((ICardChangePlace)place);
                    if (player.IsEnoughTime(action))
                    {
                        actions.Add(action);
                    }
                }
            }


            // take card
            foreach (ICard card in Game.DisplayedCards)
            {
                if (card == null) continue;
                
                action = new TakeCardAction(card);
                if (player.IsEnoughTime(action))
                {
                    actions.Add(action);
                }
            }

            // execute exhibition
            foreach (IExhibitionCard exhibition in Game.DisplayedExhibitions)
            {
                if (exhibition == null) continue;

                action = new ExecuteExhibitionAction(exhibition);
                if (player.IsEnoughTime(action) && 
                    (
                        (player is DeterministicPlayer && exhibition.CheckRequiredArtifacts(((DeterministicPlayer)player).AssumedArtifacts))
                        || exhibition.CheckRequiredArtifacts(player.Tokens)
                    )
                )
                {
                    actions.Add(action);
                }
            }

            // dig
            foreach (DigSite digSite in Game.DigsiteInventory.Keys)
            {
                if (!player.CanIDig(digSite)) continue;

                for (int weeks = 1; weeks <= 12; weeks++)
                {
                    
                    // add all single use card options
                    List<ICard> singleUseCards = player.GetUsableSingleUseCards(digSite);
                    foreach (var subset in GameSettings.Subsets<ICard>(singleUseCards))
                    {
                        action = new DigAction(digSite, weeks, new List<ICard>(subset), null);
                        if (player.IsEnoughTime(action))
                        {
                            actions.Add(action);
                        }
                    } 
                }
            }


            // use zeppelin
            if (player.Zeppelins > 0 && !((Player)player).useZeppelin && actions.Count > 0)
            {
                actions.Add(new ZeppelinAction(true));
            }

            // end year
            // TODO upravit
            if (actions.Count < 3)
            {
                actions.Add(new EndYearAction());
            }
            

            return actions;
        }

        public Dictionary<string, double> GetScores()
        {
            Dictionary<string, double> scores = new Dictionary<string, double>();
            foreach (IPlayer player in Game.Players)
            {
                scores[player.Name] = player.Points;
            }

            return scores;
        }

        public Dictionary<string, double> GetExpectedScores()
        {
            Dictionary<string, double> scores = GetScores();

            // points from digging
            foreach (IPlayer player in Game.Players)
            {
                foreach (IDigSite digSite in Game.DigsiteInventory.Keys)
                {
                    scores[player.Name] += PossibleExcavations(player, digSite) * ExpectedDigValue(player, digSite, 10); // 8 weeks seams reasonable
                }
            }

            // points from knowledge bonus at the end
            List<IPlayer> sortedPlayers;
            foreach (IDigSite digSite in Game.DigsiteInventory.Keys)
            {
                sortedPlayers = Game.Players.OrderByDescending(p => p.SpecializedKnowledge[digSite]).ToList();

                if (sortedPlayers[0].SpecializedKnowledge[digSite] > 0 && sortedPlayers[0].SpecializedKnowledge[digSite] > sortedPlayers[1].SpecializedKnowledge[digSite])
                {
                    scores[sortedPlayers[0].Name] += 5;
                }
                else if (sortedPlayers[0].SpecializedKnowledge[digSite] > 0)
                {
                    foreach (IPlayer player in sortedPlayers)
                    {
                        if (player.SpecializedKnowledge[digSite] == sortedPlayers[0].SpecializedKnowledge[digSite])
                        {
                            scores[player.Name] += 3;
                        }
                    }
                }
            }

            return scores;
        }
        
        private int PossibleExcavations(IPlayer player, IDigSite digSite)
        {
            int excavations = 0;
            for (int i = player.Time.CurrentYear; i <= Time.finalYear; i++)
            {
                excavations++;
            }
            if (!player.Permissions[digSite] && excavations > 0)
            {
                excavations--;
            }
            return excavations;
        }

        private double ExpectedDigValue(IPlayer player, IDigSite digSite, int weeks)
        {
            // get token amount
            player.GetDigStats(digSite, null, out int knowledge, out int tokenBonus);
            int tokenAmount = GameSettings.DugTokenCount(knowledge, weeks) + tokenBonus;

            return ExpectedValueOfToken(digSite) * tokenAmount;


        }

        // TODO what about knowledge tokens?
        private double ExpectedValueOfToken(IDigSite digSite)
        {
            int tokenValueSum = 0;
            foreach (IToken token in Game.DigsiteInventory[digSite])
            {
                if (token is IArtifactToken)
                {
                    tokenValueSum += ((IArtifactToken)token).Points;
                }
            }
            return tokenValueSum / Game.DigsiteInventory[digSite].Count;
        }

        public ISimulationState NextState(IAction move)
        {
            return new SimulationState(this.Game, move);
        }

        public ISimulationState RandomChild()
        {
            Random random = new Random();
            List<IAction> possibleActions = GetAllPossibleActions();    

            if (possibleActions.Count == 0)
            {
                return null;
            }

            return new SimulationState(Game, possibleActions[random.Next(0, possibleActions.Count)]);
        }   
    }


    public class CheaterAI : IAI
    {
        public CheaterAI(int playerCount) { }
        public IAction TakeAction(IGame gameState)
        {
            MCTSNode mctsNode = new MCTSNode(new SimulationState(gameState), null);
            return mctsNode.Run(5000);
        }
    }

    public class HeuristicCheaterAI : IAI
    {
        public HeuristicCheaterAI(int playerCount) { }
        public IAction TakeAction(IGame gameState)
        {
            MCTSNodeCutoff mctsNode = new MCTSNodeCutoff(new SimulationState(gameState), null);
            return mctsNode.Run(5000);
        }
    }

    public class SimCheaterAI : IAI
    {
        public SimCheaterAI(int playerCount) { }
        public IAction TakeAction(IGame gameState)
        {
            MCTSNode mctsNode = new MCTSNodeCutoff(new SimulationState(new DeterministicGame((Game)gameState)), null);
            return mctsNode.Run(5000);
        }
    }

    public class FirstYearDFSAI : IAI
    {
        public FirstYearDFSAI(int playerCount) { }
        DFSNode head;
        public IAction TakeAction(IGame gameState)
        {
            DFSNode dfsNode = new DFSNode(new SimulationState(new DeterministicGame((Game)gameState)), null);
            head = dfsNode;
            return dfsNode.Run();
        }
    }


    class MCTSNode
    {
        protected Dictionary<string, double> scores; // string is player name
        protected int visits;
        static double explorationConstant = 150;

        protected ISimulationState state;
        protected List<MCTSNode> children;
        protected MCTSNode parent;

        public MCTSNode(ISimulationState state, MCTSNode parent)
        {
            scores = null;
            visits = 0;
            children = new List<MCTSNode>();
            this.state = state;
            this.parent = parent;
            
            this.scores = new Dictionary<string, double>();
            foreach (IPlayer player in state.Game.Players)
            {
                scores.Add(player.Name, 0);
            }
        }

        public override string ToString()
        {
            return $"{state.Move}, UCT: {UCT("cheater", 0)}, visits: {visits}";
        }

        private void UpdateScore(Dictionary<string, double> newScores)
        {
            foreach (KeyValuePair<string, double> player_score in newScores)
            {
                scores[player_score.Key] += newScores[player_score.Key];
            }
        }

        public double UCT(string playerName, double explorationConstant)
        {
            if (visits == 0)
            {
                return double.MaxValue;
            }
            else
            {
                return scores[playerName] / visits + explorationConstant * Math.Sqrt(Math.Log(parent.visits) / this.visits);
            }
        }

        public IAction Run(int miliseconds)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            while (stopwatch.ElapsedMilliseconds < miliseconds)
            {
                this.Traverse();
            }
            return pickBestChild(0).state.Move;
        }

        private void Expand()
        {
            foreach (ISimulationState state in state.GetAllChildStates())
            {
                children.Add(new MCTSNode(state, this));
            }
        }
        
        private bool IsLeaf()
        {
            return this.children.Count == 0;
        }
        
        protected virtual void Traverse() 
        {
            if (IsLeaf())
            {
                if (visits == 0)
                {
                    Backpropagate(Rollout());
                }
                else
                {
                    Expand();
                    if (children.Count == 0)
                    {
                        Backpropagate(state.GetScores());
                    }
                    else
                    {
                        Backpropagate(children[0].Rollout());
                    }
                   
                }
            }
            else
            {
                this.pickBestChild(explorationConstant).Traverse();
            }
        }

        protected virtual MCTSNode pickBestChild(double explorationConstant)
        {
            MCTSNode bestChild = null;
            double maxUCT = double.MinValue;
            double score;
            foreach (MCTSNode node in children)
            {
                if ((score = node.UCT(this.state.ActivePlayer.Name, explorationConstant)) > maxUCT)
                {
                    bestChild = node;
                    maxUCT = score;

                    if (maxUCT == double.MaxValue) break; // no need to go through the others
                }
            }
            return bestChild;
        }

        protected virtual Dictionary<string, double> Rollout()
        {
            ISimulationState randomChild, currentState = this.state;
            while ((randomChild = currentState.RandomChild()) != null)
            {
                currentState = randomChild;
            }
            return currentState.GetScores();
        }

        private void Backpropagate(Dictionary<string, double> newScores)
        {
            MCTSNode current = this;
            while (current != null)
            {
                current.UpdateScore(newScores);
                current.visits++;

                current = current.parent;
            }
        }
    }

    class MCTSNodeCutoff : MCTSNode
    {
        public MCTSNodeCutoff(ISimulationState state, MCTSNode parent) : base(state, parent)
        {

        }

        protected override Dictionary<string, double> Rollout()
        {
            return this.state.GetExpectedScores();
        }

    }


    class DFSNode
    {
        protected ISimulationState state;
        //protected List<DFSNode> children;
        protected DFSNode parent;
        protected DFSNode bestChild;
        protected int score;

        public DFSNode(ISimulationState state, DFSNode parent)
        {
            score = 0;
            //children = new List<DFSNode>();
            this.state = state;
            this.parent = parent;
            
        }

        public double Eval()
        {
            if (this.IsTerminal())
            {
                return this.state.ActivePlayer.Points;
            }
            
            double maxScore = double.MinValue;
            double childScore;
            foreach (ISimulationState state in state.GetAllChildStates())
            {
                DFSNode child = new DFSNode(state, this);
                if ((childScore = child.Eval()) > maxScore)
                {
                    maxScore = childScore;
                    bestChild = child;
                }
            }
            return maxScore;
        }

        public IAction Run()
        {
            this.Eval();
            return this.bestChild.state.Move;
        }

        protected bool IsTerminal()
        {
            // TODO needs an actual end condition this won't work past the first year
            return this.state.ActivePlayer.Time.CurrentYear > Time.firstYear;
        }

    }
}
