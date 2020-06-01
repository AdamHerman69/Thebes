using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThebesCore;
using System.Diagnostics;

namespace ThebesAI
{
    interface ISimulationState
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

    class SimulationState : ISimulationState
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

        private List<IAction> GetAllPossibleActions()
        {
            List<IAction> actions = new List<IAction>();
            IAction action;

            //no time left
            if (Game.ActivePlayer.Time.RemainingWeeks() == 0)
            {
                return actions;
            }

            // change cards
            foreach (IPlace place in GameSettings.Places)
            {
                if (place is ICardChangePlace)
                {
                    action = new ChangeCardsAction((ICardChangePlace)place);
                    if (Game.ActivePlayer.IsEnoughTime(action))
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
                if (Game.ActivePlayer.IsEnoughTime(action))
                {
                    actions.Add(action);
                }
            }

            // execute exhibition
            foreach (IExhibitionCard exhibition in Game.DisplayedExhibitions)
            {
                if (exhibition == null) continue;

                action = new ExecuteExhibitionAction(exhibition);
                if (Game.ActivePlayer.IsEnoughTime(action) && exhibition.CheckRequiredArtifacts(Game.ActivePlayer.Tokens))
                {
                    actions.Add(action);
                }
            }

            // dig
            foreach (DigSite digSite in Game.DigsiteInventory.Keys)
            {
                if (!Game.ActivePlayer.CanIDig(digSite)) continue;

                for (int weeks = 1; weeks <= 12; weeks++)
                {
                    // TODO single use cards
                    action = new DigAction(digSite, weeks, null, null);
                    if (Game.ActivePlayer.IsEnoughTime(action))
                    {
                        actions.Add(action);
                    }
                }
            }

            // use zeppelin
            if (Game.ActivePlayer.Zeppelins > 0 && actions.Count > 0)
            {
                actions.Add(new ZeppelinAction(true));
            }

            // end year
            actions.Add(new EndYearAction());

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
                    scores[player.Name] += PossibleExcavations(player, digSite) * ExpectedDigValue(player, digSite, 8); // 8 weeks seams reasonable
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
        public CheaterAI(IPlayerData player, IGame game) { }
        public IAction TakeAction(IGame gameState)
        {
            MCTSNode mctsNode = new MCTSNode(new SimulationState(gameState), null);
            return mctsNode.Run(5000);
        }
    }

    public class HeuristicCheaterAI : IAI
    {
        public HeuristicCheaterAI(IPlayerData player, IGame game) { }
        public IAction TakeAction(IGame gameState)
        {
            MCTSNodeCutoff mctsNode = new MCTSNodeCutoff(new SimulationState(gameState), null);
            return mctsNode.Run(5000);
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
            return $"{state.Move}, UCB: {UCB1("cheater", 0)}, visits: {visits}";
        }

        private void UpdateScore(Dictionary<string, double> newScores)
        {
            foreach (KeyValuePair<string, double> player_score in newScores)
            {
                scores[player_score.Key] += newScores[player_score.Key];
            }
        }

        public double UCB1(string playerName, double explorationConstant)
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
        
        private void Traverse()
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
            double maxUCB1 = double.MinValue;
            double score;
            foreach (MCTSNode node in children)
            {
                if ((score = node.UCB1(this.state.ActivePlayer.Name, explorationConstant)) > maxUCB1)
                {
                    bestChild = node;
                    maxUCB1 = score;

                    if (maxUCB1 == double.MaxValue) break; // no need to go through the others
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
}
