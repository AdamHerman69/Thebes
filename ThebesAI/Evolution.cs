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
     * true / false traits
     * 
     * mít x nějakejch karet
     * mít povolení pro digsity
     * přidat bodovej multiplier
     * mít x tokenů od y barvy
     * time left weight
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
                0.0020496833269402375,
                0.000044458309937917996,
                9.133551588167725,
                4.351315812735974e-7,
                1.1859396624744787e-7,
                0.000008584659058245017,
                1.591448143400574e-12,
                5.319418177139304e-15,
                5.885749995694802e-8,
                0.00862858477580439,
                1.0826110037125302e-10,
                4.415288970119904e-9
            };

            tKnowledgeCards = new double[13] 
            {
                2.0005671306800305e-8,
                1.1667670853189579e-11,
                0.000013097582308064662,
                2.924896927339194e-12,
                9.121746080189497e-9,
                0.0022623000348201906,
                7.335724939859327e-7,
                4.4581795841987085e-8,
                8.825945100432129e-14,
                2.9827427080007036e-13,
                3.150850949469863e-13,
                3.4803837360361654e-8,
                3.748230953619839e-14
            };

            singleUseKnowledgeCards = new double[6] 
            {
                0,
                7.252396296476999e-8,
                0.000006220968750865157,
                2.1879225874508627e-8,
                7.858436280796917e-10,
                9.393732354653443e-9
            };

            generalKnowledgeCards = new double[13] 
            {
                0,
                3.96585354391474e-8,
                1358.970971158819,
                1.1974080436642822e-7,
                0.000016569118584056017,
                5.693700044270805e-7,
                0.0000018184248666728766,
                0.04354505284782457,
                5.424853198230491e-14,
                0.07357875297763782,
                1.6203482200613113e-10,
                2.381412737491374e-9,
                1.372560296512603e-10
            };
            tGeneralKnowledgeCards = new double[13] 
            {
                1.6429781038386988e-7,
                8.143390398517959e-10,
                0.00004470620959790173,
                3.4775521746963103e-13,
                8.020745603146505e-7,
                4.902383882145659e-7,
                0.000007928507356784314,
                1.6159707111801825e-12,
                0.00000169315035140683,
                2.746237003401887e-7,
                5.879828846450945e-8,
                1.1542845084407851e-9,
                0.0008712092322610341,
            };

            shovels = new double[5] 
            {
                0,
                0.000001729289002888193,
                1.0462460891350776e-9,
                2.2795139446438006,
                0,
            };

            tShovels = new double[5] 
            {
                2.6456808027864073e-10,
                2.572355254877213e-9,
                5.138996927353314e-13,
                4.30173322132337e-7,
                4.093091136023641e-15,
            };

            assistants = new double[5] 
            {
                0,
                2.645475823043279e-9,
                1.054649039582463e-7,
                8.299328391956445e-9,
                0,
            };
            tAssistants = new double[5] 
            {
                1.4124057856528376e-9,
                0.000007576705658507248,
                0.000019190668164534102,
                6.435802150992901e-18,
                3.608318014747887e-7
            };

            specialPermissions = new double[4] 
            {
                0,
                5.0462725848473885e-11,
                0.00001953549745918159,
                0.014993422620390261
            };

            zeppelins = new double[6] 
            {
                0,
                0.017784555905542565,
                1.5622114595609596e-9,
                0.0000014077552628243986,
                2.999850940123164e-8,
                0.0009303468366914499,
            };

            congresses = new double[9] 
            {
                0,
                7.679629770923098e-7,
                1.3522959486658074e-9,
                8.888617463850518e-7,
                6.809307116616667e-10,
                0.052929490232342356,
                0.000002760673833028447,
                4.048603424572042e-7,
                0,
            };
            tCongresses = new double[9] 
            {
                3.5723595442180424e-9,
                1.247079578259405e-13,
                5.009612692060049e-11,
                2.1124793070372387e-18,
                2.3502528597885245e-10,
                2.1918584841218303e-7,
                2.0857947036581566e-12,
                0.0000037058812558783226,
                2.811031930396176e-10,
            };

            car = new double[1] { 0.000604475996634021};
            tCar = new double[1] { 0.0009957692217319666 };

            permission = new double[1] { 1.5005055795291187e-9};
            tPermission = new double[1] { 2.47595953991525e-9};

            timeLeftMultiplier = new double[1] { 20.755333815059412};
            pointsMultiplier = new double[1] { 24.343590052840856};

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

        public Weights(int nothing)
        {
            knowledgeCards = new double[13] {0.0, 2.0, 2.5, 3.0, 3.5, 4.0, 3.5, 2.8, 2.5, 1.8, 1.0, 0.3, 0.1};
            tKnowledgeCards = new double[13] { 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0 };

            singleUseKnowledgeCards = new double[6] { 0.0, 0.5, 1.0, 0.7, 0.5, 0.3 };

            generalKnowledgeCards = new double[13] { 0.0, 5.0, 6.0, 7.0, 6.0, 5.0, 3.0, 2.8, 2.5, 1.8, 1.0, 0.3, 0.1 };
            tGeneralKnowledgeCards = new double[13] { 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0 };

            shovels = new double[5] {0.0, 1.0, 3.0, 4.0, 0.0};
            tShovels = new double[5] { 1.0, 1.0, 1.0, 1.0, 1.0};

            assistants = new double[5] { 0.0, 0.8, 2.5, 3.5, 0.0 };
            tAssistants = new double[5] { 1.0, 1.0, 1.0, 1.0, 1.0 };
                
            specialPermissions = new double[4] { 0.0, 4.0, 3.0, 1.0};
            zeppelins = new double[6] { 0.0, 2.0, 2.0, 2.0, 2.0, 2.0 };

            congresses = new double[9] {0.0, 2.0, 3.0, 2.0, 1.0, 1.0, 1.0, 1.0, 0.0};
            tCongresses = new double[9] { 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0 };

            car = new double[1] { 5.0 };
            tCar = new double[1] { 1.0 };

            permission = new double[1] { 4.0 };
            tPermission = new double[1] { 1.0 };

            timeLeftMultiplier = new double[1] { 0.3 };
            pointsMultiplier = new double[1] { 1 };

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

        public static Weights Crossover(params Weights[] parents)
        {
            Weights child = new Weights(69);
            IEnumerator<double>[] enums = new IEnumerator<double>[parents.Length];
            for (int i = 0; i < parents.Length; i++)
            {
                enums[i] = parents[i].GetEnumerator();
            }

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

        public void Mutate(double probability, double rangePercentage)
        {
            WeightsEnum enumerator = (WeightsEnum) this.GetEnumerator();

            while (enumerator.MoveNext())
            {
                if (random.NextDouble() <= probability)
                {
                    // mutation
                    double range = Math.Abs(weightArrays[enumerator.arrayPosition][enumerator.doublePosition] * rangePercentage);
                    double modification = random.NextDouble() * 2 * range - range;
                    weightArrays[enumerator.arrayPosition][enumerator.doublePosition] = enumerator.Current + modification;
                }
            }
        }

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

        public EvolutionAI(IPlayerData player, IGame game)
        {
            weights = new Weights("adam");
        }

        public EvolutionAI(Weights weights)
        {
            this.weights = weights;
        }

        public IAction TakeAction(IGame gameState)
        {
            //SimGame simGame = new SimGame((Game)gameState);

            string playerName = gameState.ActivePlayer.Name;
            List<ISimulationState> possibleStates = new SimulationState(gameState).GetAllChildStates();

            double bestScore = double.MinValue;
            SimulationState bestState = null;

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

        private double EvalScore(ISimulationState state, IPlayer player)
        {
            double score = 0;
            
            // knowledge
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

        public Individual(int id, int generation)
        {
            this.id = id;
            this.ai = new EvolutionAI(null, null);
            ai.weights.Mutate(1, 0.9);
            scores = new List<int>();
        }

        public Individual(int id, int generation, Weights weights)
        {
            this.id = id;
            this.ai = new EvolutionAI(weights);
            scores = new List<int>();
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

        public void GenerateIndividuals(int count)
        {
            for (int i = 0; i < count; i++)
            {
                individuals.Add(new Individual(currentID++, currentGen));
            }
        }

        public void TestGeneration(int gamesPerPlayer)
        {
            int populationIndex = 0, playersIndex = 0;
            Individual[] players = new Individual[4];
            for (int i = 0; i < gamesPerPlayer; i++)
            {
                while (populationIndex < individuals.Count)
                {
                    players[playersIndex] = individuals[populationIndex];
                    if (playersIndex == 3)
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

        public void CreateNewGeneration()
        {
            currentGen++;

            // sort by average score
            individuals.Sort((x, y) => y.AverageScore().CompareTo(x.AverageScore()));

            // add first 20 to new population
            List<Individual> newPopulation = new List<Individual>();
            for (int i = 0; i < 30; i++)
            {
                newPopulation.Add(individuals[i]);
            }

            // generate 80 new ones from the last top 20
            List<Individual> newGeneration = new List<Individual>();
            Weights[] parents = new Weights[4];
            Individual newIndividual;
            for (int i = 0; i < 70; i++)
            {
                // choose 4 parents randomly
                for (int j = 0; j < 4; j++)
                {
                    parents[j] = newPopulation[random.Next(newPopulation.Count)].ai.weights;
                }
                // add newly created indiviudal to the new generation
                newIndividual = new Individual(currentID++, currentGen, Weights.Crossover(parents));
                newIndividual.ai.weights.Mutate(0.1, 1);
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

        private void PlayMatch(Individual[] individuals)
        {
            if (individuals.Length != 4)
            {
                Console.WriteLine("wrong number of players!!!!!!");
                return;
            }

            //GameSettings.LoadFromFile(@"thebes_config.thc");
            SimulationGame game = new SimulationGame(4);

            // create players
            List<IPlayer> players = new List<IPlayer>();
            AIPlayer player;

            for (int i = 0; i < 4; i++)
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

        public void Evolve()
        {
            GameSettings.LoadFromFile(@"thebes_config.thc");
            GenerateIndividuals(100);
            for (int i = 0; i < 10000; i++)
            {
                TestGeneration(5);

                Console.WriteLine($"Generation {this.currentGen} tested: Best average score: {individuals.Max(x => x.AverageScore())},");
                if (currentGen % 20 == 0)
                {
                    using (var tw = new StreamWriter($"gen_{currentGen}.txt", true))
                    {
                        tw.Write(new JavaScriptSerializer().Serialize(individuals));
                    }
                }

                CreateNewGeneration();
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

        //public int ArtifactSum(IDigSite digSite)
        //{
        //    int sum = 0;
        //    foreach (IToken token in digSite.Tokens)
        //    {
        //        if (token is IArtifactToken)
        //        {
        //            sum += ((IArtifactToken)token).Points;
        //        }
        //    }
        //    return sum;
        //}

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
