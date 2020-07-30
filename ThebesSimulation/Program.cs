using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThebesCore;
using ThebesAI;
using System.IO;
using System.Web.Script.Serialization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace ThebesSimulation
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
             * This project is used to run any random snippet you should choose
             * 
             * 
             * 
             * Don't worry about the mess here, the mess here prevents mess in other places 
             * 
             * 
             * 
             * Some of these snippets are quite useful when testing AI or running evolution
             * 
             * 
             * 
             * 
             * 
             * 
             */
            
            
            
            //string[] filepaths = Directory.GetFiles(@"C:\Users\admhe\Desktop\Statistics\200728", "TEST*", SearchOption.TopDirectoryOnly);

            //string fileContents;
            //foreach (string fp in filepaths)
            //{
            //    fileContents = File.ReadAllText(fp);

            //    fileContents = fileContents.Replace("Generation ", "");
            //    fileContents = fileContents.Replace(" : Best avg: ", ",");
            //    fileContents = fileContents.Replace(", Total avg: ", ",");
            //    fileContents = fileContents.Replace(", avg similarity: ", ",");

            //    using (var tw = new StreamWriter(fp + ".csv", false))
            //    {
            //        tw.Write(fileContents);
            //    }
            //}


            //GameSettings.LoadFromFile(@"thebes_config.thc");

            //Individual[] individuals = new Individual[4];
            //for (int i = 0; i < 4; i++)
            //{
            //    individuals[i] = new Individual(4 ,new SimpleEvolutionAI(4));
            //}

            //int counter = 10;
            //for (int i = 0; i < 500; i++)
            //{

            //    if (i % 100 == 0)
            //    {
            //        Console.WriteLine(counter--);
            //    }
            //    Population.PlayMatch(individuals);
            //}
            //for (int i = 0; i < individuals.Length; i++)
            //{
            //    Console.WriteLine($"Average score for {i}: {individuals[i].AverageScore()}");
            //}
            //Console.ReadLine();


            //Population population = new Population(2, 100);
            //population.Evolve(1000, 5, 0.3, 2, 0.01, 0.05, 0);


            //Population population;
            //// number of parents
            //for (int i = 2; i < 5; i += 2)
            //{
            //    // survivor ratio
            //    for (double j = 0.1; j <= 0.4; j += 0.1
            //    {
            //        // mutation probability
            //        for (double k = 0.01; k < 0.2; k += 0.06)
            //        {
            //            // mutation range
            //            for (double l = 0.05; l < 0.5; l *= 2)
            //            {
            //                population = new Population(100);
            //                population.Evolve(4, 150, 5, j, i, k, l, 0);
            //            }
            //        }
            //    }
            //}

            //Individual individual = new Individual(new BetterEvolutionAI(null, null));
            //individual.ai.NormalizeValues(-1, 1);

            //foreach (var value in individual.ai.weights)
            //{
            //    Console.WriteLine(value.ToString() + ',');
            //}
            //Console.ReadLine();








            GameSettings.LoadFromFile(@"thebes_config.thc");
            Dictionary<string, Tester> testers = new Dictionary<string, Tester>();

            EvolutionA evoAI = new EvolutionA(2);
            testers.Add("mctsevo", new Tester("mctsevo", new MCTSIR(evoAI, 5000, 1.8)));


            testers.Add("simpleEvo", new Tester("simpleEvo", new EvolutionA(2)));

            //testers.Add("mcts1.4", new Tester("mcts1.4", new MCTSAI(2, 5000, 1.4)));
            //testers.Add("mcts1", new Tester("mcts1", new MCTSAI(2, 5000, 1)));
            //testers.Add("mcts1.8", new Tester("mcts1.8", new MCTSAI(3, 5000, 1.8)));
            //testers.Add("mcts0.7", new Tester("mcts0.7", new MCTSAI(2, 5000, 0.7)));

            //testers.Add("complexEvo", new Tester("complexEvo", new ComplexEvolutionAI(3)));

            //testers.Add("random", new Tester("random", new RandomAI()));
            //testers.Add("random1", new Tester("random1", new RandomAI()));
            //testers.Add("random2", new Tester("random2", new RandomAI()));
            //testers.Add("random3", new Tester("random3", new RandomAI()));



            //testers.Add("oldEvo", new Tester("oldEvo", new DumbEvolutionAI(4)));


            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine(i);
                Tester.PlayMatch(testers);
            }

            Console.WriteLine("Done\n");

            foreach (Tester tester in testers.Values.ToList())
            {
                Console.WriteLine($"{tester.name}: avg rank: {tester.AverageRank()}, avg score: {tester.AverageScore()}");
                tester.PrintPlayerStats();
            }

            Console.ReadLine();
        }
    }

    class Tester
    {
        public string name;
        public IAI ai;
        List<int> scores;
        List<int> rankings;
        List<IPlayer> players;

        public Tester(string name, IAI ai)
        {
            this.name = name;
            this.ai = ai;
            scores = new List<int>();
            rankings = new List<int>();
            players = new List<IPlayer>();
        }

        public void AddScore(int score)
        {
            this.scores.Add(score);
        }

        public void AddRank(int rank)
        {
            this.rankings.Add(rank);
        }

        public double AverageScore()
        {
            return scores.Sum() / (double)scores.Count();
        }

        public double AverageRank()
        {
            return rankings.Sum() / (double)rankings.Count();
        }

        public void PrintPlayerStats()
        {
            Console.WriteLine(this.name);
            Console.WriteLine($"Avg congress: {players.Average(p => p.Congresses)}");
            Console.WriteLine($"Avg general: {players.Average(p => p.GeneralKnowledge)}");

            // tokens
            int tokens = 0;
            foreach (var player in players)
            {
                var tokenLists = player.Tokens.Values.ToList();
                foreach (var list in tokenLists)
                {
                    tokens += list.Count();
                }
            }
            Console.WriteLine($"Avg tokens: { tokens / players.Count}");

            // specialized knowledge
            int knowledge = 0;
            foreach (var player in players)
            {
                foreach (var kvp in player.SpecializedKnowledge)
                {
                    knowledge += kvp.Value;
                }
            }
            Console.WriteLine($"Avg knowledge per site: {knowledge / (players.Count * 5)}");



        }

        public static void PlayMatch(Dictionary<string, Tester> testers)
        {
            if (testers.Count < 2 || testers.Count > 4)
            {
                throw new ArgumentOutOfRangeException("Game only supports 2 - 4 players");
            }

            SimulationGame game = new SimulationGame(testers.Count);

            // create players
            List<IPlayer> players = new List<IPlayer>();
            AIPlayer player;

            foreach (Tester tester in testers.Values.ToList())
            {
                player = new AIPlayer(
                            tester.name,
                            GameSettings.Places.OfType<IDigSite>().ToList(),
                            GameSettings.StartingPlace,
                            GameSettings.Places,
                            Console.WriteLine,
                            game.AvailableCards.ChangeDisplayedCards,
                            game.AvailableCards.GiveCard,
                            game.Deck.Discard,
                            game.ActiveExhibitions.GiveExhibition,
                            game.DrawTokens,
                            game.PlayersOnWeek
                            ); ;

                player.Init(tester.ai);
                players.Add(player);
            }

            game.Initialize(players);

            game.Play();

            players.Sort((x, y) => y.Points.CompareTo(x.Points));

            // save results
            for (int i = 0; i < players.Count; i++)
            {
                testers[players[i].Name].AddScore(players[i].Points);
                testers[players[i].Name].AddRank(i + 1);
                testers[players[i].Name].players.Add(players[i]);
            }
        }
    }


}
