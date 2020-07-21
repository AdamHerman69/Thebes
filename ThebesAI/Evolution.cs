using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using ThebesCore;
using System.Web.Script.Serialization;
using System.IO;

namespace ThebesAI
{
    /*
     * Zkusit zohlednit:
     * 
     * vemu knihu když jich můžu mít nejvíc
     * 
     * 
     * 
     */


    public struct Weights : IEnumerable<double>
    {
        static Random random = new Random();
        List<double[]> weightArrays;
        
        public double[] knowledgeCards;
        public double[] tKnowledgeCards;

        public double[] singleUseKnowledgeCards;

        public double[] generalKnowledgeCards;
        public double[] tGeneralKnowledgeCards;

        public double[] shovels;
        public double[] tShovels;

        public double[] assistants;
        public double[] tAssistants;

        public double[] car;
        public double[] tCar;

        public double[] specialPermissions;
        public double[] zeppelins;

        public double[] congresses;
        public double[] tCongresses;

        public double[] permission;
        public double[] tPermission;

        public double[] timeLeftMultiplier;
        public double[] pointsMultiplier;

        public Weights(string nothing) 
        {
            knowledgeCards = new double[13]
            {   0,
                1.1037508079887661E-05,
                4.423210424306365E-08,
                13.009785750785342,
                2.2142512362146583E-06,
                1.6210347428513657E-07,
                6.5597901272757263E-08,
                1.9837604204390725E-15,
                2.9296689817262733E-15,
                2.3100046398081926E-07,
                0.00014722574621158239,
                3.2182504734312679E-12,
                2.2143612949910419E-11,
            };

            tKnowledgeCards = new double[13] 
            {
                1.0368319512336537E-08,
                3.9386212264555813E-12,
                6.6790697654043283E-06,
                3.9068477712150828E-16,
                1.7690317948754689E-08,
                0.00064954904956271734,
                6.019300996498915E-10,
                2.2697604850393586E-08,
                2.3003829808627842E-15,
                7.0353141347936878E-14,
                2.4769334755451079E-13,
                8.9186031185078167E-09,
                2.1729795519247193E-13,
            };

            singleUseKnowledgeCards = new double[6] 
            {
               0,
                3.2976480072428905E-09,
                1.3194086132934448E-05,
                3.6244100063906858E-08,
                7.7331739624187129E-13,
                2.1433102880505741E-08,
            };

            generalKnowledgeCards = new double[13] 
            {
                0,
                2.1695869158524315E-10,
                946.59692377158808,
                2.4160757689717004E-07,
                2.9381931340014189E-05,
                1.660319913003155E-09,
                4.2273568635208688E-06,
                0.0388530128799772,
                1.2873427970030681E-14,
                2.6203687257285542E-05,
                8.7446441059509208E-12,
                1.2601777282966746E-09,
                1.1336155855371867E-11,
            };
            tGeneralKnowledgeCards = new double[13] 
            {
                1.1488553890876533E-08,
                1.2085783026623123E-09,
                1.6575173237691361E-06,
                5.2811835104650861E-13,
                7.9595695172966628E-08,
                6.1527371963869907E-07,
                5.0143269196907159E-06,
                9.90962724182828E-14,
                3.2351958444299958E-09,
                1.7243045651395564E-07,
                1.5345555940832E-07,
                3.1941304263110021E-11,
                0.0014233278594004983,
            };

            shovels = new double[5] 
            {
                0,
                2.5944420135446523E-06,
                1.5966353807529544E-12,
                5.4266953278127232,
                0,
            };

            tShovels = new double[5] 
            {
                1.3489069041461626E-11,
                1.3030632524437439E-10,
                8.8805675548175E-15,
                3.2651515934147844E-10,
                2.1331920813508376E-14,
            };

            assistants = new double[5] 
            {
                0,
                4.0791929713811216E-09,
                1.5710656676215277E-10,
                4.6001911910408103E-10,
                0,
            };
            tAssistants = new double[5] 
            {
                1.5870023582001166E-09,
                7.0431022582841274E-07,
                1.3966415306154843E-05,
                2.6744205389749509E-18,
                1.8329104522116111E-10,
            };

            specialPermissions = new double[4] 
            {
                0,
                9.78667358195109E-15,
                3.0184969834541402E-05,
                0.013682068960582895,
            };

            zeppelins = new double[6] 
            {
                0,
                0.010545284784401376,
                6.6621636014441717E-10,
                1.8758757307435428E-07,
                1.1761499876937767E-09,
                0.0011842056593770517,
            };

            congresses = new double[9] 
            {
                0,
                4.17734108212371E-09,
                2.8981074175019588E-10,
                5.1391002944486067E-07,
                1.7544299883335087E-10,
                0.090673938275772012,
                1.4389752400682561E-07,
                1.4241895187725792E-07,
                0,
            };
            tCongresses = new double[9] 
            {
                7.8834647914570634E-09,
                1.0519365856000629E-14,
                1.677355021501641E-10,
                1.3203067649884011E-18,
                1.9881558350931395E-11,
                6.0123055440694365E-10,
                8.9154463068929766E-13,
                2.7622597399386247E-06,
                6.04401847669993E-12,
            };

            car = new double[1] { 2.2683087269476137E-05, };
            tCar = new double[1] { 0.00056772673078374846 };

            permission = new double[1] { 1.362997680440333E-09, };
            tPermission = new double[1] { 1.2416096724153225E-10, };

            timeLeftMultiplier = new double[1] { 19.7974530090443, };
            pointsMultiplier = new double[1] { 39.36968916975745 };

            weightArrays = new List<double[]>
            {
                knowledgeCards,
                tKnowledgeCards,
                singleUseKnowledgeCards,
                generalKnowledgeCards,
                tGeneralKnowledgeCards,
                shovels,
                tShovels,
                assistants,
                tAssistants,
                specialPermissions,
                zeppelins,
                congresses,
                tCongresses,
                car,
                tCar,
                permission,
                tPermission,
                timeLeftMultiplier,
                pointsMultiplier
            };
        }

        /// <summary>
        /// Produces a child combining the weights of the supplied array. For every weight a random parent is chosen.
        /// </summary>
        /// <param name="parents">Array of parents</param>
        /// <returns>A set of new weights</returns>
        public static Weights Crossover(params Weights[] parents)
        {
            Weights child = new Weights("useless parameter");
            
            // get an enumerator for each parent
            IEnumerator<double>[] enums = new IEnumerator<double>[parents.Length];
            for (int i = 0; i < parents.Length; i++)
            {
                enums[i] = parents[i].GetEnumerator();
            }

            // set all child's weights, every weight is inherited from a randomly chosen parent
            WeightsEnum childEnum = (WeightsEnum)child.GetEnumerator();
            while (childEnum.MoveNext())
            {
                
                foreach (IEnumerator enumerator in enums)
                {
                    enumerator.MoveNext();
                }

                child.weightArrays[childEnum.arrayPosition][childEnum.doublePosition] = enums[random.Next(enums.Length)].Current;
            }

            return child;
        }

        /// <summary>
        /// Alteres the weights with according to the provided parameters 
        /// </summary>
        /// <param name="probability">probability [0, 1] that a single weight is going to get altered</param>
        /// <param name="rangeProportion">the 'severity' of the mutation, the max possible change that can occur, represented as a proportion of the weight</param>
        public void Mutate(double probability, double rangeProportion)
        {
            WeightsEnum enumerator = (WeightsEnum) this.GetEnumerator();

            while (enumerator.MoveNext())
            {
                // is chosen for mutation?
                if (random.NextDouble() <= probability)
                {
                    // mutation
                    double range = Math.Abs(weightArrays[enumerator.arrayPosition][enumerator.doublePosition] * rangeProportion); // compute the modification range
                    double modification = random.NextDouble() * 2 * range - range; // randomly choose a number whithin the range [-range, range]
                    weightArrays[enumerator.arrayPosition][enumerator.doublePosition] = enumerator.Current + modification;
                }
            }
        }

        /// <summary>
        /// Returns the specified weight. Returns the last weight in the array if index is out of bounds
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public double GetWeight(double[] array, int index)
        {
            if (index < array.Length)
            {
                return array[index];
            }
            return array[array.Length - 1];
        }

        public IEnumerator<double> GetEnumerator()
        {
            return new WeightsEnum(weightArrays);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class WeightsEnum : IEnumerator<double>
    {
        List<double[]> weightArrays;
        public int arrayPosition = -1;
        public int doublePosition = -1;

        public WeightsEnum(List<double[]> weightArrays)
        {
            this.weightArrays = weightArrays;
        }

        public double Current => weightArrays[arrayPosition][doublePosition];

        object IEnumerator.Current => this.Current;

        public void Dispose()
        {

        }

        public bool MoveNext()
        {
            if (arrayPosition < 0 || doublePosition == weightArrays[arrayPosition].Length - 1)
            {
                arrayPosition++;
                doublePosition = -1;
            }
            doublePosition++;

            return arrayPosition < weightArrays.Count();
        }

        public void Reset()
        {
            arrayPosition = -1;
            doublePosition = -1;
        }
    }


    public class EvolutionAI : IAI
    {
        public Weights weights;

        public EvolutionAI() { }

        public EvolutionAI(IPlayerData player, IGame game) // UI calls all AI constructors with these parameters
        {
            weights = new Weights("useless parameter");
        }

        public EvolutionAI(Weights weights)
        {
            this.weights = weights;
        }

        public IAction TakeAction(IGame gameState)
        {
            string playerName = gameState.ActivePlayer.Name;

            // generate all possible moves and their resulting game states
            List<ISimulationState> possibleStates = new SimulationState(gameState).GetAllChildStates();

            double bestScore = double.MinValue;
            SimulationState bestState = null;

            // Evaluate all child states and pick the best one
            double score;
            foreach (SimulationState state in possibleStates)
            {
                score = EvalScore(state, state.Game.Players.First(p => p.Name.Equals(playerName)));
                if (score > bestScore)
                {
                    bestScore = score;
                    bestState = state;
                }
            }

            return bestState.Move;
        }

        protected virtual double EvalScore(ISimulationState state, IPlayer player)
        {
            double score = 0;
            
            // digsite dependent points
            foreach (IDigSite digSite in state.Game.DigsiteInventory.Keys)
            {
                // specialized knowledge
                score += weights.GetWeight(weights.knowledgeCards, player.SpecializedKnowledge[digSite])
                    * state.Game.ArtifactSum(digSite)
                    + weights.GetWeight(weights.tKnowledgeCards, player.SpecializedKnowledge[digSite]) * (52 - player.Time.CurrentWeek);

                //single use knowledge
                score += weights.GetWeight(weights.singleUseKnowledgeCards, player.SingleUseKnowledge[digSite])
                    * state.Game.ArtifactSum(digSite);
                
                // permissions
                if (player.Permissions[digSite])
                {
                    score += weights.permission[0] + (52 - player.Time.CurrentWeek) * weights.tPermission[0];
                }
            }

            score += weights.GetWeight(weights.generalKnowledgeCards, player.GeneralKnowledge)
                + (52 - player.Time.CurrentWeek) * weights.GetWeight(weights.tGeneralKnowledgeCards, player.GeneralKnowledge);
            score += weights.GetWeight(weights.shovels, player.Shovels)
                + (52 - player.Time.CurrentWeek) * weights.GetWeight(weights.tShovels, player.Shovels);
            score += weights.GetWeight(weights.assistants, player.Assistants)
                + (52 - player.Time.CurrentWeek) * weights.GetWeight(weights.tAssistants, player.Assistants);
            score += weights.GetWeight(weights.specialPermissions, player.SpecialPermissions);
            score += weights.GetWeight(weights.zeppelins, player.Zeppelins);
            score += weights.GetWeight(weights.congresses, player.Congresses)
                + (52 - player.Time.CurrentWeek) * weights.GetWeight(weights.tCongresses, player.Congresses);

            if (player.Cars > 0)
            {
                score += weights.car[0]
                    + (52 - player.Time.CurrentWeek) * weights.GetWeight(weights.tCar, player.Cars);
            }

            // points
            score += player.Points * weights.pointsMultiplier[0];

            // time left
            score += player.Time.RemainingWeeks() * weights.timeLeftMultiplier[0];


            return score;
        }
    }

    public class Individual
    {
        public int id;
        public int generation;
        public EvolutionAI ai;
        List<int> scores;

        public Individual() { }

        public Individual(Weights? weights = null, int id = -1, int generation = -1)
        {
            this.id = id;
            this.generation = generation;
            scores = new List<int>();

            if (weights == null)
            {
                this.ai = new EvolutionAI(null, null);
            }
            else
            {
                this.ai = new EvolutionAI(weights.Value);
            }
        }

        public void AddScore(int score)
        {
            scores.Add(score);
        }

        public double AverageScore()
        {
            return scores.Average();
        }
    }

    public class Population
    {
        static Random random = new Random();
        List<Individual> individuals = new List<Individual>();
        int currentGen = 0;
        int currentID = 0;
        Weights currentBest;
        double currentBestScore = double.MinValue;

        public Population(int count, Weights? seed = null, double mutationProbability = 1, double mutationRange = 1)
        {
            Individual individual;
            for (int i = 0; i < count; i++)
            {
                individual = new Individual(seed, currentID++, currentGen);
                individual.ai.weights.Mutate(mutationProbability, mutationRange);
                individuals.Add(individual);
            }
        }
            
        public void TestGeneration(int gamesPerPlayer, int playerPerGame)
        {
            if (playerPerGame < 2 || playerPerGame > 4)
            {
                throw new ArgumentOutOfRangeException("Game only supports 2 - 4 players");
            }
            if (gamesPerPlayer < 1)
            {
                throw new ArgumentOutOfRangeException("You have to play at least one game to test the players");
            }
            if (individuals.Count % playerPerGame != 0)
            {
                throw new ArgumentOutOfRangeException("Number of individual in a population must be divisible by playersPerGame");
            }

            int populationIndex = 0, playersIndex = 0;
            Individual[] players = new Individual[playerPerGame];
            for (int i = 0; i < gamesPerPlayer; i++)
            {
                while (populationIndex < individuals.Count)
                {
                    players[playersIndex] = individuals[populationIndex];
                    if (playersIndex == playerPerGame - 1)
                    {
                        PlayMatch(players);
                        playersIndex = -1;
                    }

                    playersIndex++;
                    populationIndex++;
                }

                populationIndex = 0;
                playersIndex = 0;

                ShufflePopulation();
            }
        }

        public void CreateNewGeneration(double survivorRatio, int parentAmount, double mutationProbability = 0.15, double mutationRange = 1)
        {
            currentGen++;

            // sort by average score descending
            individuals.Sort((x, y) => y.AverageScore().CompareTo(x.AverageScore()));

            // report a new best score
            double bestScore = individuals[0].AverageScore();
            if (bestScore > currentBestScore)
            {
                currentBestScore = bestScore;
                currentBest = individuals[0].ai.weights;
                Console.WriteLine($"-------------------- new best score: {bestScore}");
                
                using(var tw = new StreamWriter($"currentBest.txt", true))
                    {
                    tw.Write(new JavaScriptSerializer().Serialize(currentBest));
                }
            }

            int survivorCount = (int)Math.Round(individuals.Count * survivorRatio);

            // select survivors
            List<Individual> newPopulation = new List<Individual>();
            for (int i = 0; i < survivorCount; i++)
            {
                newPopulation.Add(individuals[i]);
            }

            // generate the rest of the population
            List<Individual> newGeneration = new List<Individual>();
            Weights[] parents = new Weights[parentAmount];
            Individual newIndividual;
            for (int i = 0; i < individuals.Count - survivorCount; i++)
            {
                // choose parents randomly
                for (int j = 0; j < parentAmount; j++)
                {
                    parents[j] = newPopulation[random.Next(newPopulation.Count)].ai.weights;
                }

                // add newly created indiviudal to the new generation
                newIndividual = new Individual(Weights.Crossover(parents), currentID++, currentGen);
                newIndividual.ai.weights.Mutate(mutationProbability, mutationRange);
                newGeneration.Add(newIndividual);
            }

            // merge survirors with children
            newPopulation.AddRange(newGeneration);
            this.individuals = newPopulation;
        }

        private void ShufflePopulation()
        {
            int index = individuals.Count;
            while (index > 1)
            {
                index--;
                int swapPosition = random.Next(index + 1);
                Individual i = individuals[swapPosition];
                individuals[swapPosition] = individuals[index];
                individuals[index] = i;
            }
        }

        public static void PlayMatch(Individual[] individuals)
        {
            if (individuals.Length < 2 || individuals.Length > 4)
            {
                throw new ArgumentOutOfRangeException("Game only supports 2 - 4 players");
            }

            SimulationGame game = new SimulationGame(individuals.Length);

            // create players
            List<IPlayer> players = new List<IPlayer>();
            AIPlayer player;

            for (int i = 0; i < individuals.Length; i++)
            {
                player = new AIPlayer(
                            i.ToString(),
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

                player.Init(individuals[i].ai);
                players.Add(player);
            }

            game.Initialize(players);

            game.Play();


            // save results
            for (int i = 0; i < individuals.Length; i++)
            {
                individuals[i].AddScore(players[i].Points);
            }
        }

        public void Evolve(int playersPerGame , int generations, int gamesPerPlayer, double survivorRatio = 0.3, int parentAmount = 4, double mutationProbability = 0.15, double mutationRange = 1)
        {
            GameSettings.LoadFromFile(@"thebes_config.thc");
            for (int i = 0; i < generations; i++)
            {
                TestGeneration(gamesPerPlayer, playersPerGame);

                Console.WriteLine($"Generation {this.currentGen} tested: Best average score: {individuals.Max(x => x.AverageScore())},");
                // print generation
                if (false)
                {
                    using (var tw = new StreamWriter($"gen_{currentGen}.txt", true))
                    {
                        tw.Write(new JavaScriptSerializer().Serialize(individuals));
                    }
                }

                CreateNewGeneration(survivorRatio, parentAmount, mutationProbability, mutationRange);
            }
            
        }

    }


    class SimulationGame : Game
    {
        public SimulationGame(int playerCount) : base(playerCount) { }

        public SimulationGame() { }

        public void Initialize(List<IPlayer> players)
        {
            this.Players = players;
            this.Players.Sort();
        }

        public void Play()
        {
            IAction action;
            while (!AreAllPlayersDone())
            {
                action = ((IAIPlayer)ActivePlayer).AI.TakeAction(this);
                Move(action);
            }
        }

        public override IGame Clone()
        {
            SimulationGame newGame = new SimulationGame();

            // newGame.random = new Random();
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
