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
using System.Xml.Schema;
using System.Runtime.CompilerServices;

namespace ThebesAI
{
    /// <summary>
    /// Legacy class, works with 
    /// </summary>
    public class Weights : IEnumerable<double>
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

        public Weights(int random)
        {
            var enumerator = this.GetEnumerator();

        }

        /// <summary>
        /// Produces a child combining the weights of the supplied array. For every weight a random parent is chosen.
        /// </summary>
        /// <param name="parents">Array of parents</param>
        /// <returns>A set of new weights</returns>
        public static Weights Procreate(params Weights[] parents)
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
            WeightsEnum enumerator = (WeightsEnum)this.GetEnumerator();

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









































    public class Weight
    {
        public double Value { get; set; }

        public Weight(double weight)
        {
            this.Value = weight;
        }

        public static implicit operator double(Weight w) => w.Value;
        public static implicit operator Weight(double d) => new Weight(d);

        public override string ToString()
        {
            return Value.ToString();
        }
    }

    public abstract class SimpleCriterion
    {
        public abstract int WeightsNeeded { get; }
        protected Weight[,] mainWeights;
        protected static int yearsPlayed;

        public SimpleCriterion(int mainWeightsLength, int subWeightsCount, int yearsPlayed)
        {
            SimpleCriterion.yearsPlayed = yearsPlayed;
            mainWeights = new Weight[mainWeightsLength, yearsPlayed];
        }

        public void ReceiveWeights(Queue<Weight> allocatedWeights)
        {
            for (int i = 0; i < mainWeights.GetLength(0); i++)
            {
                for (int j = 0; j < mainWeights.GetLength(1); j++)
                {
                    mainWeights[i, j] = allocatedWeights.Dequeue();
                }
            }
        }

        public abstract double GetScore(ISimulationState gameState, IPlayer player);

        protected static int GetYearIndex(ITime time)
        {
            int index = time.CurrentYear - Time.firstYear;
            if (time.CurrentYear == 1904)
            {
                index--;
            }
            return index;
        }
    }

    public class SCSpecializedKnowledge : SimpleCriterion
    {
        public override int WeightsNeeded => 8 * yearsPlayed;

        public SCSpecializedKnowledge(int years) : base(8, 0, years) { }

        public override double GetScore(ISimulationState gameState, IPlayer player)
        {
            double score = 0;
            int weightIndex;

            foreach (IDigSite digSite in gameState.Game.DigsiteInventory.Keys)
            {
                if (player.SpecializedKnowledge[digSite] > 0)
                {
                    weightIndex = Math.Min(mainWeights.GetLength(0) - 1, player.SpecializedKnowledge[digSite] - 1);

                    score += mainWeights[weightIndex, GetYearIndex(player.Time)].Value;
                } 
            }

            return score;
        }
    }

    public class SCSingleUseKnowledge : SimpleCriterion
    {
        public override int WeightsNeeded => 1 * yearsPlayed;

        public SCSingleUseKnowledge(int years) : base(1, 0, years) { }

        public override double GetScore(ISimulationState gameState, IPlayer player)
        {
            double score = 0;

            foreach (IDigSite digSite in gameState.Game.DigsiteInventory.Keys)
            {
                // player can only have 2 or 0 singleUseKnowledge
                if (player.SingleUseKnowledge[digSite] == 2)
                {
                    score += mainWeights[0, GetYearIndex(player.Time)];
                }
            }

            return score;
        }
    }

    public class SCPermissions : SimpleCriterion
    {
        public override int WeightsNeeded => 1 * yearsPlayed;

        public SCPermissions(int years) : base(1, 0, years) { }

        public override double GetScore(ISimulationState gameState, IPlayer player)
        {
            double score = 0;
            double mainWeight;

            mainWeight = mainWeights[0, GetYearIndex(player.Time)].Value;
            foreach (IDigSite digSite in gameState.Game.DigsiteInventory.Keys)
            {
                if (player.Permissions[digSite])
                {
                    score += mainWeight;
                }
            }

            return score;
        }
    }

    public class SCGeneralKnowledge : SimpleCriterion
    {
        public override int WeightsNeeded => 8 * yearsPlayed;

        public SCGeneralKnowledge(int years) : base(8, 0, years) { }

        public override double GetScore(ISimulationState gameState, IPlayer player)
        {
            double score = 0;
            int weightIndex;

            if (player.GeneralKnowledge > 0)
            {
                weightIndex = Math.Min(mainWeights.GetLength(0) - 1, player.GeneralKnowledge - 1);
                score += mainWeights[weightIndex, GetYearIndex(player.Time)].Value;
            }


            return score;
        }
    }

    public class SCShovels : SimpleCriterion
    {
        public override int WeightsNeeded => 4 * yearsPlayed;

        public SCShovels(int years) : base(4, 0, years) { }

        public override double GetScore(ISimulationState gameState, IPlayer player)
        {
            double score = 0;
            int weightIndex;

            if (player.Shovels > 0)
            {
                weightIndex = Math.Min(mainWeights.GetLength(0) - 1, player.Shovels - 1);
                score += mainWeights[weightIndex, GetYearIndex(player.Time)];
            }

            return score;
        }
    }

    public class SCAssistants : SimpleCriterion
    {
        public override int WeightsNeeded => 4 * yearsPlayed;

        public SCAssistants(int years) : base(4, 0, years) { }

        public override double GetScore(ISimulationState gameState, IPlayer player)
        {
            double score = 0;
            int weightIndex;

            if (player.Assistants > 0)
            {
                weightIndex = Math.Min(mainWeights.GetLength(0) - 1, player.Assistants - 1);
                score += mainWeights[weightIndex, GetYearIndex(player.Time)];
            }

            return score;
        }
    }

    public class SCSpecialPermissions : SimpleCriterion
    {
        public override int WeightsNeeded => 2 * yearsPlayed;

        public SCSpecialPermissions(int years) : base(2, 0, years) { }

        public override double GetScore(ISimulationState gameState, IPlayer player)
        {
            double score = 0;
            double mainWeight;
            int weightIndex;

            if (player.SpecialPermissions > 0)
            {
                weightIndex = Math.Min(mainWeights.GetLength(0) - 1, player.SpecialPermissions - 1);
                score += mainWeights[weightIndex, GetYearIndex(player.Time)];
            }
            
            return score;
        }
    }

    public class SCZeppelins : SimpleCriterion
    {
        public override int WeightsNeeded => 2 * yearsPlayed;

        public SCZeppelins(int years) : base(2, 0, years) { }

        public override double GetScore(ISimulationState gameState, IPlayer player)
        {
            double score = 0;
            double mainWeight;
            int weightIndex;

            if (player.Zeppelins > 0)
            {
                weightIndex = Math.Min(mainWeights.GetLength(0) - 1, player.Zeppelins - 1);
                score += mainWeights[weightIndex, 0];
            }

            return score;
        }
    }

    public class SCCongresses : SimpleCriterion
    {
        public override int WeightsNeeded => 8 * yearsPlayed;

        public SCCongresses(int years) : base(8, 0, years) { }

        public override double GetScore(ISimulationState gameState, IPlayer player)
        {
            double score = 0;
            int weightIndex;

            if (player.Congresses > 0)
            {
                weightIndex = Math.Min(mainWeights.GetLength(0) - 1, player.Congresses - 1);

                score += mainWeights[weightIndex, GetYearIndex(player.Time)].Value;
            }

            return score;
        }
    }

    public class SCCar : SimpleCriterion
    {
        public override int WeightsNeeded => 1 * yearsPlayed;

        public SCCar(int years) : base(1, 0, years) { }

        public override double GetScore(ISimulationState gameState, IPlayer player)
        {
            double score = 0;

            if (player.Cars > 0)
            {
                score += mainWeights[0, GetYearIndex(player.Time)];
            }

            return score;
        }
    }

    public class SCPoints : SimpleCriterion
    {
        public override int WeightsNeeded => 2 * yearsPlayed;

        public SCPoints(int year) : base(2, 0, year) { }

        public override double GetScore(ISimulationState gameState, IPlayer player)
        {
            double score = 0;
            double mainWeight;
            int weightIndex;
            double points = player.Points;

            // add end game points for most knowledge

            List<IPlayer> sortedPlayers;
            foreach (IDigSite digSite in gameState.Game.DigsiteInventory.Keys)
            {
                sortedPlayers = gameState.Game.Players.OrderByDescending(p => p.SpecializedKnowledge[digSite]).ToList();

                // our player has the most of this knowledge (5 points)
                if (sortedPlayers[0] == player && sortedPlayers[0].SpecializedKnowledge[digSite] > 0 && sortedPlayers[0].SpecializedKnowledge[digSite] > sortedPlayers[1].SpecializedKnowledge[digSite])
                {
                    points += 5 * mainWeights[1, GetYearIndex(player.Time)];
                }

                // our player shares the first place with another (3 points)
                else if (sortedPlayers[0].SpecializedKnowledge[digSite] > 0)
                {
                    foreach (IPlayer p in sortedPlayers)
                    {
                        if (p == player && p.SpecializedKnowledge[digSite] == sortedPlayers[0].SpecializedKnowledge[digSite])
                        {
                            points += 3 * mainWeights[1, GetYearIndex(player.Time)];
                        }
                    }
                }
            }


            // points * point weight
            score += mainWeights[0, GetYearIndex(player.Time)] * points;

            return score;
        }
    }

    public class SCTime : SimpleCriterion
    {
        public override int WeightsNeeded => 1 * yearsPlayed;

        public SCTime(int years) : base(1, 0, years) { }

        public override double GetScore(ISimulationState gameState, IPlayer player)
        {
            // weeks left * weight
            return mainWeights[0, 0].Value * player.Time.RemainingWeeks();
        }
    }

    public abstract class EvolutionAI : IAI
    {
        protected static Random random = new Random();
        public Weight[] weights;

        public abstract IAction TakeAction(IGame gameState);

        protected static double RandomDouble(double lowerBound, double upperBound)
        {
            return random.NextDouble() * (upperBound - lowerBound) + lowerBound;
        }

        public void Mutate(double probability, double minRange, double minRangeProportion)
        {
            double range, mutation;
            for (int i = 0; i < weights.Length; i++)
            {
                if (random.NextDouble() <= probability)
                {
                    range = Math.Max(Math.Abs(weights[i].Value * minRangeProportion), minRange); // max (minimal range, proportional range)
                    mutation = RandomDouble(-1 * range, range);
                    weights[i].Value += mutation;
                }
            }
        }

        /// <summary>
        /// Mutates randomly selected weights. The weights remain in the <-1; 1> interval
        /// </summary>
        /// <param name="probability"></param>
        /// <param name="range"></param>
        public void MutateInRange(double probability, double range)
        {
            double mutation, minMutation, maxMutation;

            for (int i = 0; i < weights.Length; i++)
            {
                if (random.NextDouble() <= probability)
                {
                    //minMutation = Math.Max(-1 * Math.Abs(weights[i] - (- 1)), -1 * range);
                    //maxMutation = Math.Min(Math.Abs(weights[i] - 1), range);

                    //mutation = RandomDouble(minMutation, maxMutation);
                    //weights[i].Value += mutation;


                    if (random.NextDouble() <= probability)
                    {
                        if (random.Next(2) == 0)
                        {
                            // positive mutation
                            minMutation = 0;
                            maxMutation = Math.Min(Math.Abs(weights[i] - 1), range);
                        }
                        else
                        {
                            // negative mutation
                            minMutation = Math.Max(-1 * Math.Abs(weights[i] - (-1)), -1 * range);
                            maxMutation = 0;
                        }

                        mutation = RandomDouble(minMutation, maxMutation);
                        weights[i].Value += mutation;
                    }
                }
            }
        }

        /// <summary>
        /// Mutates randomly selected weights. The weights remain in the <0; 1> interval
        /// </summary>
        /// <param name="probability"></param>
        /// <param name="range"></param>
        public void MutateInRangePositive(double probability, double range)
        {
            double mutation, minMutation, maxMutation;

            for (int i = 0; i < weights.Length; i++)
            {
                if (random.NextDouble() <= probability)
                {
                    if (random.Next(2) == 0)
                    {
                        // positive mutation
                        minMutation = 0;
                        maxMutation = Math.Min(1 - weights[i], range);
                    }
                    else
                    {
                        // negative mutation
                        minMutation = Math.Max(-1 * weights[i], -1 * range);
                        maxMutation = 0;
                    }
                    //minMutation = Math.Min(weights[i], range);
                    //maxMutation = Math.Min(Math.Abs(weights[i] - 1), range);

                    mutation = RandomDouble(minMutation, maxMutation);
                    weights[i].Value += mutation;
                }
            }
        }
    }

    public class EvolutionB : EvolutionAI
    {
        

        public List<SimpleCriterion> criteria;

        public EvolutionB(int playerCount)
        {
            int yearsPlayed = 3;
            if (playerCount == 4)
            {
                yearsPlayed = 2;
            }
            criteria = new List<SimpleCriterion>()
            {
                new SCSpecializedKnowledge(yearsPlayed),
                new SCSingleUseKnowledge(yearsPlayed),
                new SCPermissions(yearsPlayed),
                new SCGeneralKnowledge(yearsPlayed),
                new SCShovels(yearsPlayed),
                new SCAssistants(yearsPlayed),
                new SCSpecialPermissions(yearsPlayed),
                new SCZeppelins(yearsPlayed),
                new SCCongresses(yearsPlayed),
                new SCCar(yearsPlayed),
                new SCPoints(yearsPlayed),
                new SCTime(yearsPlayed)
            };

            if (playerCount == 4)
            {
                // weights for 4 players
                this.weights = new Weight[]
                {
                    0.16386060932776517,
                    0.20306987203800567,
                    0.0030143131268541641,
                    0.11042700578012829,
                    0.64895792191334,
                    0.59872672588039466,
                    0.58312370741419672,
                    0.64127919508203846,
                    0.0910220714011333,
                    0.45888388420403187,
                    0.093351691904432432,
                    0.26252489507315913,
                    0.66712724332524753,
                    0.71767350326649548,
                    0.58472637512475545,
                    0.82853642468272548,
                    0.73196238632405286,
                    0.09039282121923084,
                    0.66000869290903619,
                    0.673887286299787,
                    0.48818674189419803,
                    0.66173535019050833,
                    0.73984211382448772,
                    0.55893560387610253,
                    0.12875431099848556,
                    0.67362661281303793,
                    0.041211182810907784,
                    0.79047894093257232,
                    0.00777092581721461,
                    0.69680630490035089,
                    0.87306500585892477,
                    0.68147991217276072,
                    0.99580782067109241,
                    0.77477228830977285,
                    0.7552557108482606,
                    0.17450057085292289,
                    0.05951313863485732,
                    0.48166995715893363,
                    0.41177598555189382,
                    0.40195991314589574,
                    0.015574676817043297,
                    0.20253476996558475,
                    0.35264992506832343,
                    0.71071119767646862,
                    0.92336300344678357,
                    0.26755731049010589,
                    0.79758384871155164,
                    0.24617274647866039,
                    0.47000594850164168,
                    0.27085260389805832,
                    0.99863335214321358,
                    0.30719928802326285,
                    0.4414100541926036,
                    0.03198764194558909,
                    0.17764855761902809,
                    0.12304834380071159,
                    0.24953554333166017,
                    0.69314245079724446,
                    0.1643351044245526,
                    0.4445872250220681,
                    0.35663378506742111,
                    0.46050733484351386,
                    0.90183319670699247,
                    0.25580304621523409,
                    0.829046250307621,
                    0.17289941524860536,
                    0.50057434332583772,
                    0.23708211515894262,
                    0.726563513547444,
                    0.99916803617837524,
                    0.13307556409066337,
                    0.63541280805850986,
                    0.2010761700342765,
                    0.87007265153201052,
                    0.01537503745156915,
                    0.29928317021245376,
                    0.34814485565207193,
                    0.54662416924099633,
                    0.0926606122036842,
                    0.88235844910195382,
                    0.90200667726894357,
                    0.084878455551966214,
                    0.045493253404194994,
                    0.96825536317493022
                };
            }
            else if (playerCount == 3)
            {
                // weights for 3 players
                this.weights = new Weight[]
                {
                    0.10118056815172573,
                    0.076279876118309276,
                    0.91207603917553814,
                    0.6099335625813963,
                    0.024810186224577244,
                    0.94084573691461459,
                    0.8113607464178284,
                    0.31200293680280583,
                    0.580410538185579,
                    0.75654880511070277,
                    0.8557360625509477,
                    0.1603315491743067,
                    0.75012418616103194,
                    0.30733956420204583,
                    0.32993886367415021,
                    0.61602847008780981,
                    0.2765620317433784,
                    0.54656514450747751,
                    0.44113742548094004,
                    0.52666598664441433,
                    0.86775039286247946,
                    0.56646331405102424,
                    0.42028397131724471,
                    0.57723306078800607,
                    0.10618762746741403,
                    0.86481057329279254,
                    0.47811340199695579,
                    0.65976975462854393,
                    0.67866076125235364,
                    0.27358074131122828,
                    0.98575983992171423,
                    0.8112100118590565,
                    0.59853652594542894,
                    0.023855774421937904,
                    0.43580397899998546,
                    0.8495092194991708,
                    0.82970018044642113,
                    0.68614319848648453,
                    0.24285086460544297,
                    0.88247367119321518,
                    0.90350251071892906,
                    0.66933477438023992,
                    0.73456855247941222,
                    0.36393446953506314,
                    0.089211643336359114,
                    0.54592733464479781,
                    0.18802237139323375,
                    0.13038877888084832,
                    0.094029995493604773,
                    0.55985291984856733,
                    0.46026028674573655,
                    0.84398010507733146,
                    0.82800507534214152,
                    0.35954542113912547,
                    0.90203445629880041,
                    0.975157132724463,
                    0.57567308634783732,
                    0.85721523287110746,
                    0.0057602503805794722,
                    0.32202831160837242,
                    0.27322121158857882,
                    0.33947775412326558,
                    0.32803337102664332,
                    0.268818362438501,
                    0.67411683191271321,
                    0.14665491420154225,
                    0.00054531159975068882,
                    0.37018045043581194,
                    2.3821262482845837E-05,
                    0.91887692544533051,
                    0.02395499764433244,
                    0.25138218971033688,
                    0.19033100932851949,
                    0.41365171848500687,
                    0.87419805546486662,
                    0.7276841324184482,
                    0.082179688769003292,
                    0.2987174944480499,
                    0.88986294649856124,
                    0.87000412519796866,
                    0.3448250144695979,
                    0.11333475618778477,
                    0.0095614898121942557,
                    0.56112815565947816,
                    0.87715060006694479,
                    0.51950071976496881,
                    0.91958260918948,
                    0.64330986649417732,
                    0.72720816271715261,
                    0.024153320757076741,
                    0.90514270525346974,
                    0.74772298321953179,
                    0.88579617670938415,
                    0.87864781647190859,
                    0.99445370214254925,
                    0.45851652794914161,
                    0.9436072435281605,
                    0.43146166032248245,
                    0.53568969046961945,
                    0.780824629265193,
                    0.84949258288810614,
                    0.70726513651072287,
                    0.29645138815811445,
                    0.41377216880851064,
                    0.13481097446512941,
                    0.491194523634014,
                    0.19554269158663853,
                    0.74477747491723134,
                    0.38590918406187991,
                    0.99991224937485312,
                    0.028646499960446908,
                    0.64815908931110022,
                    0.799974111118342,
                    0.7429124575308117,
                    0.59402978608572365,
                    0.61389362170076633,
                    0.76444407563809857,
                    0.43525005364103708,
                    0.25955641989575529,
                    0.48507183098004758,
                    0.72212006794947936,
                    0.785746054416325,
                    0.29540107159195517,
                    0.3705373846090107,
                    0.93213791366308008,
                    0.74664244602743646
                };
            }
            else if (playerCount == 2)
            {
                // weights for 2 players
                this.weights = new Weight[]
                {
                    0.10772292665565521,
                    0.7568847753139607,
                    0.74631516339551429,
                    0.48422212767611361,
                    0.6904421048892857,
                    0.5638831437630033,
                    0.92751435948420058,
                    0.11517178503105967,
                    0.73141940153735663,
                    0.080500113025808756,
                    0.2792686570106393,
                    0.48644287387674812,
                    0.82613329180801909,
                    0.43936299797583511,
                    0.60968136177849086,
                    0.57248039474826329,
                    0.18924283037858214,
                    0.68709403464435315,
                    0.16412713765824544,
                    0.922583390852708,
                    0.64309565205736807,
                    0.81671887543830957,
                    0.91617429795031169,
                    0.61241034453847,
                    0.35780636517228853,
                    0.6238802657806688,
                    0.79255218822162221,
                    0.70989934867708915,
                    0.18803045874835481,
                    0.286052742477531,
                    0.76064842909604713,
                    0.45001016936731064,
                    0.73109669330580929,
                    0.42193991386887614,
                    0.61249398419749623,
                    0.3697377791021661,
                    0.96211453610961661,
                    0.95373598390659375,
                    0.45749338234658044,
                    0.51007718758661169,
                    0.53072096040971628,
                    0.88570951581267132,
                    0.54078058085440683,
                    0.032582360861624193,
                    0.99860538550352518,
                    0.925730366667607,
                    0.46704319839693753,
                    0.9907799637584267,
                    0.47184547291688878,
                    0.60509634900609788,
                    0.45023372108127624,
                    0.33474675865599274,
                    0.4644525567602612,
                    0.27093972043178033,
                    0.25944109550185557,
                    0.31924239677341765,
                    0.62114454362129068,
                    0.9655833078727486,
                    0.38164135607967209,
                    0.26135486413787812,
                    0.04766920122675096,
                    0.9876389942510051,
                    0.8705903114614032,
                    0.79465070592456055,
                    0.24544539721470579,
                    0.26200166359171351,
                    0.063534421378567379,
                    0.68054673787231867,
                    0.52852707848815572,
                    0.83093081946993752,
                    0.37600639507919853,
                    0.16639153564646447,
                    0.52078435934185252,
                    0.5111772827390475,
                    0.53798279817587824,
                    0.712534388579677,
                    0.81813607605553051,
                    0.61135987532854075,
                    0.13833448984116992,
                    0.17292112692860939,
                    0.43032710779473515,
                    0.630235472777968,
                    0.64270482491362135,
                    0.83622870453923415,
                    0.55135714430425187,
                    0.083735655123146444,
                    0.26680164177287446,
                    0.24129558491580913,
                    0.343688098175306,
                    0.46714263414831025,
                    0.31457978601780712,
                    0.22724254982417569,
                    0.18147895793965035,
                    0.64586532553465348,
                    0.0215015263396788,
                    0.57306267915901832,
                    0.29812336410308415,
                    0.091270815181206361,
                    0.74228235142877441,
                    0.96545397670262212,
                    0.46566309268384376,
                    0.77102167882072814,
                    0.44907003492073627,
                    0.36989022778342018,
                    0.57970048469943025,
                    0.4983978745752935,
                    0.55170745020811784,
                    0.922489740570686,
                    0.41556133062837708,
                    0.729099832046358,
                    0.31756847978922004,
                    0.65190549248452556,
                    0.0018197459325073,
                    0.41450251984153991,
                    0.38987789151253088,
                    0.63939463076619185,
                    0.43057403752141354,
                    0.0076865458800822357,
                    0.17509300227979807,
                    0.28830572836487822,
                    0.71150372666842476,
                    0.88695861843272994,
                    0.067309790159254243,
                    0.27861013434762605,
                    0.21176778493065748,
                    0.50546630467542741
                };
            }
            else
            {
                throw new ArgumentOutOfRangeException("Invalid number of players");
            }

            

            // Provide the criteria with the weights
            Queue<Weight> weightsQueue = new Queue<Weight>(weights);
            foreach (SimpleCriterion criterion in criteria)
            {
                criterion.ReceiveWeights(weightsQueue);
            }
        }


        /// <summary>
        /// Creates AI with provided weights if not null. If null the weights will be random
        /// </summary>
        /// <param name="weights">weights to create the AI with</param>
        public EvolutionB(int playerCount, Weight[] assignedWeights = null)
        {
            int yearsPlayed = 3;
            if (playerCount == 4)
            {
                yearsPlayed = 2;
            }
            criteria = new List<SimpleCriterion>()
            {
                new SCSpecializedKnowledge(yearsPlayed),
                new SCSingleUseKnowledge(yearsPlayed),
                new SCPermissions(yearsPlayed),
                new SCGeneralKnowledge(yearsPlayed),
                new SCShovels(yearsPlayed),
                new SCAssistants(yearsPlayed),
                new SCSpecialPermissions(yearsPlayed),
                new SCZeppelins(yearsPlayed),
                new SCCongresses(yearsPlayed),
                new SCCar(yearsPlayed),
                new SCPoints(yearsPlayed),
                new SCTime(yearsPlayed)
            };

            if (assignedWeights == null)
            {
                int weightCount = criteria.Sum(c => c.WeightsNeeded);
                weights = new Weight[weightCount];

                for (int i = 0; i < weightCount; i++)
                {
                    weights[i] = new Weight(RandomDouble(0, 1));
                }
            }
            else
            {
                this.weights = assignedWeights;
            }

            // Provide the criteria with the weights
            Queue<Weight> weightsQueue = new Queue<Weight>(weights);
            foreach (SimpleCriterion criterion in criteria)
            {
                criterion.ReceiveWeights(weightsQueue);
            }
        }


        protected virtual double EvalScore(ISimulationState gameState, IPlayer player)
        {
            double score = 0;

            foreach (SimpleCriterion criterion in criteria)
            {
                score += criterion.GetScore(gameState, player);
            }

            return score;
        }

        public override IAction TakeAction(IGame gameState)
        {
            string playerName = gameState.ActivePlayer.Name;

            // generate all possible moves and their resulting game states
            // TODO can be changed to deterministic, but not for evolution
            List<ISimulationState> possibleStates = new SimulationState(new DeterministicGame((Game)gameState)).GetAllChildStates();

            //List<ISimulationState> possibleStates = new SimulationState(gameState).GetAllChildStates();

            double bestScore = double.MinValue;
            SimulationState bestState = null;

            // TODO debugging
            Dictionary<IAction, double> moveScores = new Dictionary<IAction, double>();


            // Evaluate all child states and pick the best one
            double score;
            foreach (SimulationState state in possibleStates)   
            {
                score = EvalScore(state, state.Game.Players.First(p => p.Name.Equals(playerName)));
                // TODO debugging
                moveScores[state.Move] = score;

                if (score > bestScore)
                {
                    bestScore = score;
                    bestState = state;
                }
            }

            return bestState.Move;
        }

        public static EvolutionB Procreate(int playerCount, params EvolutionAI[] parents)
        {
            // assuming all parents have the same number of weights
            Weight[] newWeights = new Weight[parents[0].weights.Length];

            for (int i = 0; i < newWeights.Length; i++)
            {
                newWeights[i] = parents[random.Next(parents.Length)].weights[i];
            }

            return new EvolutionB(playerCount, newWeights);
        }



        public void NormalizeValues(double min, double max)
        {
            double[] doubleWeights = new double[this.weights.Length];
            for (int i = 0; i < doubleWeights.Length; i++)
            {
                doubleWeights[i] = this.weights[i];
            }

            double actualMin = doubleWeights.Min();
            double actualMax = doubleWeights.Max();

            for (int i = 0; i < weights.Length; i++)
            {
                weights[i] = (max - min) / (actualMax - actualMin) * (weights[i] - actualMin) + min;
            }
        }
    }









    public class DumbEvolutionAI : IAI
    {
        public Weights weights;

        public DumbEvolutionAI() { }

        public DumbEvolutionAI(int playerCount)
        {
            weights = new Weights("useless parameter");
        }

        public DumbEvolutionAI(Weights weights)
        {
            this.weights = weights;
        }

        public IAction TakeAction(IGame gameState)
        {
            string playerName = gameState.ActivePlayer.Name;

            // generate all possible moves and their resulting game states
            //List<ISimulationState> possibleStates = new SimulationState(gameState).GetAllChildStates();
            List<ISimulationState> possibleStates = new SimulationState(new DeterministicGame((Game)gameState)).GetAllChildStates();

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


            // vahaKarty * čas

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

        public Individual(int playerCount, EvolutionAI ai = null, int id = -1, int generation = -1)
        {
            this.id = id;
            this.generation = generation;
            scores = new List<int>();

            if (ai == null)
            {
                this.ai = new EvolutionB(playerCount);
            }
            else
            {
                this.ai = ai;
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

        public void ResetScores()
        {
            this.scores.Clear();
        }
    }

    public class Population
    {
        static Random random = new Random();
        List<Individual> individuals = new List<Individual>();
        int currentGen = 0;
        int currentID = 0;
        int playersPerGame;

        EvolutionAI currentBest;
        double currentBestScore = double.MinValue;

        List<double> averageScores = new List<double>();
        double mutationProbability;

        StreamWriter sw;

        public Population(int playersPerGame, int count, Weight[] seed = null)
        {
            this.playersPerGame = playersPerGame;

            Individual individual;
            for (int i = 0; i < count; i++)
            {
                individual = new Individual(playersPerGame, new EvolutionA(seed), currentID++, currentGen);
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
                throw new ArgumentOutOfRangeException("Number of individuals in a population must be divisible by playersPerGame");
            }

            // play required number of matches for every player
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

        public void CreateNewGeneration(double survivorRatio, int parentAmount, double minMutationRange, double mutationProportionalRange)
        {
            currentGen++;

            // sort by average score descending
            individuals.Sort((x, y) => y.AverageScore().CompareTo(x.AverageScore()));

            // report a new best score
            double bestScore = individuals[0].AverageScore();
            if (bestScore > currentBestScore)
            {
                currentBestScore = bestScore;
                currentBest = individuals[0].ai;
                Console.WriteLine($"-------------------- new best score: {bestScore}");

                using (var tw = new StreamWriter($"ComplexBEST_{playersPerGame}_150_5_{survivorRatio}_{parentAmount}_{mutationProbability}_{minMutationRange}.txt", true))
                {
                    tw.Write(new JavaScriptSerializer().Serialize(currentBest));
                }
            }

            int survivorCount = (int)Math.Round(individuals.Count * survivorRatio);

            // select survivors
            List<Individual> newPopulation = new List<Individual>();
            for (int i = 0; i < survivorCount; i++)
            {
                individuals[i].ResetScores();
                newPopulation.Add(individuals[i]);
            }
            int childrenCount = individuals.Count - survivorCount;

            // adjust if population is too similar
            int randomIndividualsCount = 0;
            if (AverageSimilarity(this.individuals, 20) > 0.95)
            {
                childrenCount = (int)Math.Round(childrenCount * 0.6);
                randomIndividualsCount = individuals.Count - survivorCount - childrenCount;

                // raise mutation probability
                //mutationProbability += 0.01;
                //mutationProbability = 0.05;
            }

            // adjust if population average fell off
            int bestChildrenCount = 0;
            if (currentBestScore > bestScore + 12)
            {
                childrenCount = (int)Math.Round(childrenCount * 0.6);
                bestChildrenCount = individuals.Count - survivorCount - childrenCount - randomIndividualsCount;
            }


            // generate the rest of the population
            List<Individual> newGeneration = new List<Individual>();
            EvolutionAI[] parents = new EvolutionAI[parentAmount];
            Individual newIndividual;
            for (int i = 0; i < childrenCount; i++)
            {
                // choose parents randomly
                for (int j = 0; j < parentAmount; j++)
                {
                    parents[j] = newPopulation[random.Next(newPopulation.Count)].ai;
                }

                // add newly created indiviudal to the new generation
                newIndividual = new Individual(playersPerGame, EvolutionA.Procreate(parents), currentID++, currentGen);
                newIndividual.ai.MutateInRange(mutationProbability, minMutationRange);
                newGeneration.Add(newIndividual);
            }

            // generate random individuals
            for (int i = 0; i < randomIndividualsCount; i++)
            {
                newGeneration.Add(new Individual(playersPerGame, new EvolutionA(null), currentID++, currentGen));
            }

            // generate childer of the best individual so far
            EvolutionA bestAICopy;
            for (int i = 0; i < bestChildrenCount; i++)
            {
                bestAICopy = new EvolutionA(currentBest.weights);
                bestAICopy.MutateInRange(mutationProbability, minMutationRange);
                newGeneration.Add(new Individual(playersPerGame, bestAICopy, currentID++, currentGen));
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

        public void Evolve(int generations, int gamesPerPlayer, double survivorRatio = 0.3, int parentAmount = 4, double mutationProbability = 0.1, double minMutationRange = 0.05, double mutationRelativeRange = 0.3)
        {
            GameSettings.LoadFromFile(@"thebes_config.thc");

            sw = new StreamWriter($"Complex_TEST{playersPerGame}_{generations}_{gamesPerPlayer}_{survivorRatio}_{parentAmount}_{mutationProbability}_{minMutationRange}.txt", true);

            this.mutationProbability = mutationProbability;
            for (int i = 0; i < generations; i++)
            {
                TestGeneration(gamesPerPlayer, playersPerGame);
                ReportProgress();
                CreateNewGeneration(survivorRatio, parentAmount, minMutationRange, mutationRelativeRange);
            }

        }

        public void ReportProgress()
        {
            double averageScore = individuals.Average(x => x.AverageScore());
            double maxAverageScore = individuals.Max(x => x.AverageScore());
            averageScores.Add(averageScore);
            double averageSimilarity = AverageSimilarity(individuals, 20);
            Console.WriteLine($"Generation {this.currentGen} : Best avg: {maxAverageScore}, Total avg: {averageScore}, avg similarity: {averageSimilarity}");

            sw.Write($"Generation {this.currentGen} : Best avg: {maxAverageScore}, Total avg: {averageScore}, avg similarity: {averageSimilarity}\n");

            //if (this.currentGen % 20 == 0)
            //{
            //    using (var tw = new StreamWriter($"gen_{currentGen}_betterAI.txt", true))
            //    {
            //        tw.Write(new JavaScriptSerializer().Serialize(this.averageScores));
            //    }
            //}
        }

        /// <summary>
        /// Computes the cosine similarity of two vectors
        /// </summary>
        /// <param name="a">first vecotr</param>
        /// <param name="b">second vector</param>
        /// <returns>double in range <-1; 1> where 1 is most similar, -1 least similar </returns>
        private static double CosineSimilarity(Weight[] a, Weight[] b)
        {
            double ab = 0.0;
            double aa = 0.0;
            double bb = 0.0;

            // compute formula parts
            for (int i = 0; i < a.Length; ++i)
            {
                aa += a[i].Value * a[i].Value;
                ab += a[i].Value * b[i].Value;
                bb += b[i].Value * b[i].Value;
            }

            // edge cases
            if (aa == 0)
                return bb == 0 ? 1.0 : 0.0;
            else if (bb == 0)
                return 0.0;
            else
                return ab / Math.Sqrt(aa) / Math.Sqrt(bb);
        }

        private static double AverageSimilarity(List<Individual> individuals, int comparisonCount)
        {
            int count = 0;
            double sum = 0;
            while (true)
            {
                for (int i = 0; i < individuals.Count; i++)
                {
                    if (count >= comparisonCount)
                    {
                        return sum / count;
                    }

                    Individual randomIndividual = individuals[random.Next(individuals.Count)];
                    if (randomIndividual.ai.weights.Length != individuals[i].ai.weights.Length)
                    {
                        continue;
                    }
                    sum += CosineSimilarity(individuals[i].ai.weights, randomIndividual.ai.weights);
                    count++;
                }
            }
        }
    }



    /// <summary>
    /// AI that plays a random move on every turn
    /// </summary>
    public class RandomAI : IAI
    {
        Random random = new Random();
            
        public IAction TakeAction(IGame gameState)
        {
            List<ISimulationState> possibleStates = new SimulationState(gameState).GetAllChildStates();

            List<ISimulationState> filteredStates;

            if (possibleStates.Count < 2)
            {
                return possibleStates[random.Next(possibleStates.Count)].Move;
            }
            while (true)
            {
                switch (random.Next(5))
                {
                    // take a card
                    case 0:
                        filteredStates = possibleStates.Where(x => x.Move is TakeCardAction).ToList();
                        if (filteredStates.Count != 0)
                        {
                            return filteredStates[random.Next(filteredStates.Count)].Move;
                        }
                        break;

                    // dig
                    case 1:
                        filteredStates = possibleStates.Where(x => x.Move is DigAction).ToList();
                        if (filteredStates.Count != 0)
                        {
                            return filteredStates[random.Next(filteredStates.Count)].Move;
                        }
                        break;

                    // exhibition
                    case 2:
                        filteredStates = possibleStates.Where(x => x.Move is ExecuteExhibitionAction).ToList();
                        if (filteredStates.Count != 0)
                        {
                            return filteredStates[random.Next(filteredStates.Count)].Move;
                        }
                        break;

                    // change cards
                    case 3:
                        filteredStates = possibleStates.Where(x => x.Move is ChangeCardsAction).ToList();
                        if (filteredStates.Count != 0)
                        {
                            return filteredStates[random.Next(filteredStates.Count)].Move;
                        }
                        break;

                    // use zeppelin
                    case 4:
                        filteredStates = possibleStates.Where(x => x.Move is ZeppelinAction).ToList();
                        if (filteredStates.Count != 0)
                        {
                            return filteredStates[random.Next(filteredStates.Count)].Move;
                        }
                        break;

                    default:
                        break;
                } 
            }

            

            
        }
    }


    public class SimulationGame : Game
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
            newGame.BonusTokens = new Dictionary<IDigSite, IToken>();
            foreach (KeyValuePair<IDigSite, List<IToken>> digsite_tokenList in this.DigsiteInventory)
            {
                newGame.DigsiteInventory[digsite_tokenList.Key] = new List<IToken>(this.DigsiteInventory[digsite_tokenList.Key]);
                newGame.BonusTokens[digsite_tokenList.Key] = this.BonusTokens[digsite_tokenList.Key];
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