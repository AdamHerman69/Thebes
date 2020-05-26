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
        Dictionary<IPlayer, int> GetScores();
        IAction Move { get; }
    }

    class SimulationState : ISimulationState
    {
        IGame game;

        public IPlayer ActivePlayer => throw new NotImplementedException();

        public IAction Move => throw new NotImplementedException();

        public List<ISimulationState> GetAllChildStates()
        {
            throw new NotImplementedException();
        }

        public Dictionary<IPlayer, int> GetScores()
        {
            throw new NotImplementedException();
        }

        public ISimulationState NextState(IAction move)
        {
            
        }

        public ISimulationState RandomChild()
        {
            throw new NotImplementedException();
        }
    }


    
    class CheaterAI : IAI
    {

    }

    class MCTSNode
    {
        Dictionary<IPlayer, int> scores;
        int visits;
        static double explorationConstant = 2;

        ISimulationState state;
        List<MCTSNode> children;
        MCTSNode parent;

        private void UpdateScore(Dictionary<IPlayer, int> newScores)
        {
            foreach (KeyValuePair<IPlayer, int> player_score in scores)
            {
                scores[player_score.Key] += newScores[player_score.Key];
            }
        }

        public double UCB1(IPlayer player, double explorationConstant)
        {
            if (visits == 0)
            {
                return double.MaxValue;
            }
            else
            {
                return scores[player] / visits + explorationConstant * Math.Sqrt(Math.Log(parent.visits) / this.visits);
            }
        }

        public MCTSNode(ISimulationState state, MCTSNode parent)
        {
            scores = null;
            visits = 0;
            children = new List<MCTSNode>();
            this.state = state;
            this.parent = parent;
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
            if (children.Count == 0) throw new InvalidOperationException("at least one action should be possible");
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
                    Backpropagate(RandomRollout());
                }
                else
                {
                    Expand();
                    Backpropagate(children[0].RandomRollout());
                }
            }
            else
            {
                this.pickBestChild(explorationConstant).Traverse();
            }
        }

        private MCTSNode pickBestChild(double explorationConstant)
        {
            MCTSNode bestChild = null;
            double maxUCB1 = double.MinValue;
            double score;
            foreach (MCTSNode node in children)
            {
                if ((score = node.UCB1(this.state.ActivePlayer, explorationConstant)) > maxUCB1)
                {
                    bestChild = node;
                    maxUCB1 = score;

                    if (maxUCB1 == double.MaxValue) break; // no need to go through the others
                }
            }
            return bestChild;
        }

        private Dictionary<IPlayer, int> RandomRollout()
        {
            ISimulationState randomChild, currentState = this.state;
            while ((randomChild = state.RandomChild()) != null)
            {
                currentState = randomChild;
            }
            return currentState.GetScores();
        }

        private void Backpropagate(Dictionary<IPlayer, int> newScores)
        {
            MCTSNode current = this;
            while (current != null )
            {
                current.UpdateScore(newScores);
                current.visits++;

                current = current.parent;
            }
        }
    }

    
}
