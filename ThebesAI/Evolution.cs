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
    /*
     * Zkusit zohlednit:
     * 
     * vemu knihu když jich můžu mít nejvíc
     * 
     * 
     * 
     */

    /*
     * POZNAMKY DO BP
     * 
     * lineárnost a nelineárnost různejch proměnejch (počet knih - nelineární, čas - lineární)
     * 
     * zmínit normalizace vs nenormalizace (v pocketu odkaz na paper v něm jsou odkazy hned v úvodu)
     * 
     * náš model nám neumožňuje použít zeppelin správně
     * 
     */


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

    public abstract class Criterion
    {
        public abstract int WeightsNeeded { get; }
        protected Weight[] mainWeights;
        protected Weight[] subWeights;

        public Criterion(int mainWeightsLength, int subWeightsCount)
        {
            mainWeights = new Weight[mainWeightsLength];
            subWeights = new Weight[subWeightsCount];
        }

        public void ReceiveWeights(Queue<Weight> allocatedWeights)
        {
            for (int i = 0; i < mainWeights.Length; i++)
            {
                mainWeights[i] = allocatedWeights.Dequeue();
            }

            for (int i = 0; i < subWeights.Length; i++)
            {
                subWeights[i] = allocatedWeights.Dequeue();
                //for (int j = 0; j < subWeights.GetLength(1); j++)
                //{
                //    subWeights[i, j] = allocatedWeights.Dequeue();
                //}
            }
        }

        public abstract double GetScore(ISimulationState gameState, IPlayer player);
    }

    public class CSpecializedKnowledge : Criterion
    {
        public override int WeightsNeeded => 9 + 3;

        public CSpecializedKnowledge() : base(9, 3) { }

        public override double GetScore(ISimulationState gameState, IPlayer player)
        {
            double score = 0;
            double mainWeight;
            int weightIndex;

            foreach (IDigSite digSite in gameState.Game.DigsiteInventory.Keys)
            {
                weightIndex = Math.Min(mainWeights.Length - 1, player.SpecializedKnowledge[digSite]);

                mainWeight = mainWeights[weightIndex].Value;

                score += mainWeight;

                // remaining weeks in a year
                score += mainWeight * subWeights[0].Value * (Time.weeksInAYear - player.Time.CurrentWeek);

                // years left
                score += mainWeight * subWeights[1].Value * (Time.finalYear - player.Time.CurrentYear);

                // artifact value sum at the dig site
                score += mainWeight * subWeights[2].Value * (gameState.Game.ArtifactSum(digSite));
            }

            return score;
        }
    }

    public class CSingleUseKnowledge : Criterion
    {
        public override int WeightsNeeded => 9 + 3;

        public CSingleUseKnowledge() : base(9, 3) { }

        public override double GetScore(ISimulationState gameState, IPlayer player)
        {
            double score = 0;
            double mainWeight;
            int weightIndex;

            foreach (IDigSite digSite in gameState.Game.DigsiteInventory.Keys)
            {
                weightIndex = Math.Min(mainWeights.Length - 1, player.SingleUseKnowledge[digSite]);

                mainWeight = mainWeights[weightIndex].Value;

                score += mainWeight;

                // remaining weeks in a year
                score += mainWeight * subWeights[0].Value * (Time.weeksInAYear - player.Time.CurrentWeek);

                // years left
                score += mainWeight * subWeights[1].Value * (Time.finalYear - player.Time.CurrentYear);

                // artifact value sum at the dig site
                score += mainWeight * subWeights[2].Value * (gameState.Game.ArtifactSum(digSite));
            }

            return score;
        }
    }

    public class CPermissions : Criterion
    {
        public override int WeightsNeeded => 1 + 3;

        public CPermissions() : base(1, 3) { }

        public override double GetScore(ISimulationState gameState, IPlayer player)
        {
            double score = 0;
            double mainWeight;
            int weightIndex;

            foreach (IDigSite digSite in gameState.Game.DigsiteInventory.Keys)
            {
                weightIndex = 0;

                mainWeight = mainWeights[weightIndex].Value;

                if (player.Permissions[digSite])
                {
                    score += mainWeight;

                    // remaining weeks in a year
                    score += mainWeight * subWeights[0].Value * (Time.weeksInAYear - player.Time.CurrentWeek);

                    // years left
                    score += mainWeight * subWeights[1].Value * (Time.finalYear - player.Time.CurrentYear);

                    // artifact value sum at the dig site
                    score += mainWeight * subWeights[2].Value * (gameState.Game.ArtifactSum(digSite));
                }
            }

            return score;
        }
    }

    public class CGeneralKnowledge : Criterion
    {
        public override int WeightsNeeded => 9 + 4;

        public CGeneralKnowledge() : base(9, 4) { }

        public override double GetScore(ISimulationState gameState, IPlayer player)
        {
            double score = 0;
            double mainWeight;
            int weightIndex;


            weightIndex = Math.Min(mainWeights.Length - 1, player.GeneralKnowledge);

            mainWeight = mainWeights[weightIndex].Value;

            score += mainWeight;

            // remaining weeks in a year
            score += mainWeight * subWeights[0].Value * (Time.weeksInAYear - player.Time.CurrentWeek);

            // years left
            score += mainWeight * subWeights[1].Value * (Time.finalYear - player.Time.CurrentYear);

            // consider number of assistants
            score += mainWeight * subWeights[2].Value * player.Assistants;

            // consider number of shovels
            score += mainWeight * subWeights[3].Value * player.Shovels;


            return score;
        }
    }

    public class CShovels : Criterion
    {
        public override int WeightsNeeded => 5 + 4;

        public CShovels() : base(5, 4) { }

        public override double GetScore(ISimulationState gameState, IPlayer player)
        {
            double score = 0;
            double mainWeight;
            int weightIndex;


            weightIndex = Math.Min(mainWeights.Length - 1, player.Shovels);

            mainWeight = mainWeights[weightIndex].Value;

            score += mainWeight;

            // remaining weeks in a year
            score += mainWeight * subWeights[0].Value * (Time.weeksInAYear - player.Time.CurrentWeek);

            // years left
            score += mainWeight * subWeights[1].Value * (Time.finalYear - player.Time.CurrentYear);

            // consider number of assistants
            score += mainWeight * subWeights[2].Value * player.Assistants;

            // consider number of general knowledge
            score += mainWeight * subWeights[3].Value * player.GeneralKnowledge;

            return score;
        }
    }

    public class CAssistants : Criterion
    {
        public override int WeightsNeeded => 5 + 4;

        public CAssistants() : base(5, 4) { }

        public override double GetScore(ISimulationState gameState, IPlayer player)
        {
            double score = 0;
            double mainWeight;
            int weightIndex;


            weightIndex = Math.Min(mainWeights.Length - 1, player.Assistants);

            mainWeight = mainWeights[weightIndex].Value;

            score += mainWeight;

            // remaining weeks in a year
            score += mainWeight * subWeights[0].Value * (Time.weeksInAYear - player.Time.CurrentWeek);

            // years left
            score += mainWeight * subWeights[1].Value * (Time.finalYear - player.Time.CurrentYear);

            // consider number of general knowledge
            score += mainWeight * subWeights[2].Value * player.GeneralKnowledge;

            // consider number of shovels
            score += mainWeight * subWeights[3].Value * player.Shovels;

            return score;
        }
    }

    public class CSpecialPermissions : Criterion
    {
        public override int WeightsNeeded => 3 + 2;

        public CSpecialPermissions() : base(3, 2) { }

        public override double GetScore(ISimulationState gameState, IPlayer player)
        {
            double score = 0;
            double mainWeight;
            int weightIndex;


            weightIndex = Math.Min(mainWeights.Length - 1, player.SpecialPermissions);

            mainWeight = mainWeights[weightIndex].Value;

            score += mainWeight;

            // remaining weeks in a year
            score += mainWeight * subWeights[0].Value * (Time.weeksInAYear - player.Time.CurrentWeek);

            // years left
            score += mainWeight * subWeights[1].Value * (Time.finalYear - player.Time.CurrentYear);

            return score;
        }
    }

    public class CZeppelins : Criterion
    {
        public override int WeightsNeeded => 3 + 2;

        public CZeppelins() : base(3, 2) { }

        public override double GetScore(ISimulationState gameState, IPlayer player)
        {
            double score = 0;
            double mainWeight;
            int weightIndex;


            weightIndex = Math.Min(mainWeights.Length - 1, player.Zeppelins);

            mainWeight = mainWeights[weightIndex].Value;

            score += mainWeight;

            // remaining weeks in a year
            score += mainWeight * subWeights[0].Value * (Time.weeksInAYear - player.Time.CurrentWeek);

            // years left
            score += mainWeight * subWeights[1].Value * (Time.finalYear - player.Time.CurrentYear);

            return score;
        }
    }

    public class CCongresses : Criterion
    {
        public override int WeightsNeeded => 9 + 2;

        public CCongresses() : base(9, 2) { }

        public override double GetScore(ISimulationState gameState, IPlayer player)
        {
            double score = 0;
            double mainWeight;
            int weightIndex;


            weightIndex = Math.Min(mainWeights.Length - 1, player.Congresses);

            mainWeight = mainWeights[weightIndex].Value;

            score += mainWeight;

            // remaining weeks in a year
            score += mainWeight * subWeights[0].Value * (Time.weeksInAYear - player.Time.CurrentWeek);

            // years left
            score += mainWeight * subWeights[1].Value * (Time.finalYear - player.Time.CurrentYear);

            return score;
        }
    }

    public class CCar : Criterion
    {
        public override int WeightsNeeded => 1 + 2;

        public CCar() : base(1, 2) { }

        public override double GetScore(ISimulationState gameState, IPlayer player)
        {
            double score = 0;
            double mainWeight;
            int weightIndex;


            weightIndex = 0;

            mainWeight = mainWeights[weightIndex].Value;

            if (player.Cars > 0)
            {
                score += mainWeight;

                // remaining weeks in a year
                score += mainWeight * subWeights[0].Value * (Time.weeksInAYear - player.Time.CurrentWeek);

                // years left
                score += mainWeight * subWeights[1].Value * (Time.finalYear - player.Time.CurrentYear);
            }

            return score;
        }
    }

    public class CPoints : Criterion
    {
        public override int WeightsNeeded => 1 + 1;

        public CPoints() : base(1, 1) { }

        public override double GetScore(ISimulationState gameState, IPlayer player)
        {
            double score = 0;
            double mainWeight;
            int weightIndex;
            int points = player.Points;

            // add end game points for most knowledge

            List<IPlayer> sortedPlayers;
            foreach (IDigSite digSite in gameState.Game.DigsiteInventory.Keys)
            {
                sortedPlayers = gameState.Game.Players.OrderByDescending(p => p.SpecializedKnowledge[digSite]).ToList();

                // our player has the most of this knowledge (5 points)
                if (sortedPlayers[0] == player && sortedPlayers[0].SpecializedKnowledge[digSite] > 0 && sortedPlayers[0].SpecializedKnowledge[digSite] > sortedPlayers[1].SpecializedKnowledge[digSite])
                {
                    points += 5;
                }

                // our player shares the first place with another (3 points)
                else if (sortedPlayers[0].SpecializedKnowledge[digSite] > 0)
                {
                    foreach (IPlayer p in sortedPlayers)
                    {
                        if (p == player && p.SpecializedKnowledge[digSite] == sortedPlayers[0].SpecializedKnowledge[digSite])
                        {
                            points += 3;
                        }
                    }
                }
            }


            weightIndex = 0;

            mainWeight = mainWeights[weightIndex].Value;

            // points * point weight
            score += mainWeight * points;

            // points * point weight * time (adjusted for importance of points as game progresses)
            score += mainWeight * subWeights[0].Value * player.Time.RemainingWeeks();

            return score;
        }
    }

    public class CTime : Criterion
    {
        public override int WeightsNeeded => 1;

        public CTime() : base(1, 0) { }

        public override double GetScore(ISimulationState gameState, IPlayer player)
        {
            // weeks left * weight
            return mainWeights[0].Value * player.Time.RemainingWeeks();
        }
    }

    public class BetterEvolutionAI : IAI
    {
        protected static Random random = new Random();

        public List<Criterion> criteria;
        public Weight[] weights;

        public BetterEvolutionAI(IPlayerData player, IGame game)
        {
            criteria = new List<Criterion>()
            {
                new CSpecializedKnowledge(),
                new CSingleUseKnowledge(),
                new CPermissions(),
                new CGeneralKnowledge(),
                new CShovels(),
                new CAssistants(),
                new CSpecialPermissions(),
                new CZeppelins(),
                new CCongresses(),
                new CCar(),
                new CPoints(),
                new CTime()
            };

            this.weights = new Weight[]
            {
                1.2243084497991237e-71,
                -3.530436853005161e-50,
                -8.208852961510335e-82,
                1.2953884364553836e-91,
                -4.111744622225419e-86,
                -3.521632634965802e-44,
                3.639760356501169e-72,
                -1.227065307537201e-35,
                -7.333411945532716e-82,
                -1.5200341552835515e-70,
                -1.3900588525232612e-90,
                7.167940872157079e-88,
                2.8889238572627056e-70,
                -2.348278738977676e-61,
                -2.7309483172827976e-81,
                5.326159634958654e-71,
                -2.8491833736882204e-79,
                -1.18050386644939e-80,
                -8.644509706740843e-50,
                1.2224828108642408e-87,
                -2.677858182840602e-47,
                2.1190143640728934e-72,
                -2.398769245534746e-87,
                -3.459525132851405e-10,
                9.033685106589897e-82,
                1.3276268883801298e-89,
                7.778349229008317e-73,
                -7.177797998518349e-63,
                1.5454710949619094e-64,
                -1.1178362786983964e-79,
                -1.2544271017304697e-75,
                7.578275619632956e-60,
                -2.0531220417869518e-78,
                -4.334187719334211e-54,
                -4.945287780620139e-68,
                -5.396271243462965e-78,
                1.2063803228737208e-91,
                -1.0776294009751257e-77,
                3.9004246444748936e-79,
                5.819604398734575e-17,
                -2.3430234803642405e-73,
                -1.3987506448227614e-77,
                9.113516994198352e-79,
                -7.32169595028998e-71,
                -2.1400371588395845e-55,
                -3.804296843041148e-22,
                -9.34289341456395e-87,
                3.045932704960982e-55,
                7.778458696316704e-36,
                1.7415529966839343e-92,
                -9.01356646967168e-11,
                1.3548168982616625e-53,
                -1.1696749848247006e-74,
                -3.3129230204262105e-78,
                4.709095482614014e-52,
                5.015252720735729e-79,
                -5.573052180087497e-90,
                -5.617351116200448e-78,
                2.2703043931460284e-69,
                -6.953878716679261e-70,
                9.384087458257445e-58,
                3.152476336925059e-74,
                -1.7646455876768747e-55,
                -1.877788759867031e-72,
                -1.932499938807948e-58,
                3.8910133253105e-82,
                -8.753098001159325e-77,
                4.019561970478813e-73,
                -7.29428816401581e-80,
                3.1313563103435717e-80,
                -8.945379758004847e-58,
                -1.0414507213886696e-73,
                1.776643909731047e-82,
                -7.909806169497701e-69,
                8.604429201931234e-83,
                -5.30588236645631e-88,
                -6.0174410716735625e-37,
                -1.3388461609206404e-37,
                4.135406908294625e-45,
                9.961264848453039e-87,
                3.690427952651561e-70,
                -0.0000039281173175389075,
                2.5750185201902195e-66,
                -8.701742624441452e-29,
                7.1971695477966715e-84,
                -9.056043028032425e-69,
                8.675241363522308e-71,
                -1.5490447664647754e-73,
                -8.927568868029489e-78,
                -3.350882474559033e-73,
                2.436753267309452e-85,
                -0.000966551556260041,
                -8.286331828535277e-65,
                -8.217369934529178e-43,
                2.4041198999253315e-68,
                -1.2516281519351828e-77,
                -8.526580936770401e-70,
                1.817382823144913e-55,
                1.1482133825570278e-92,
                1.4710514970917792e-88,
                -2.5821882499297065e-87,
                -5.705783169700445e-68,
                -4.623259948517564e-83,
                -2.148285250251597e-79,
                1.6798900057138798e-48,
                -2.2620002344323243e-71,
                1.4987325849367441e-90,
                -3.19398560853742e-66,
                4.2579691795752584e-79,
                6.01077080723869e-71,
                -1.5102006383858075e-60,
                1.0380087233752108e-86,
                1.2115138945935349e-61,
                8.052998755074214e-65,
                -3.232253970646156e-68,
                -5.371197301301626e-27,
                -3.7445378986705926e-78,
                -1.2897130944446438e-60,
                -1.0823945537876856e-75,
                2.5152494946436868e-65,
                5.429648533544136e-58,
                7.037223975270087e-87,
                3.5390661316177025e-88,
                3.378095579027258e-78,
                1.6798841555189277e-85,
                3.323235085734913e-74,
                -1.6316953977728203e-82,
                6.505457097386954e-62,
                -3.585596973213483e-67,
                2.398746836665616e-69,
                -3.0064340643921375e-78,
                7.13672976245117e-52,
                8.523961434647718e-89,
                -5.305027074109728e-78,
                7.160683001324848e-75,
                3.6663894010208925e-86,
                6.076074859161301e-70,
                3.593238413415383e-68,
                2.7938652682509055e-82,
                -3.573131792407269e-56,
                -6.400362412735803e-59,
                4.2159683469225124e-61,
                -8.521321780487519e-45,
                8.022161241279014e-88,
                -5.792869524268589e-73,
                -8.707339230651963e-81,
                -1.8291474502783196e-51,
                -7.612334999888518e-8,
                3.3613670675664953e-78,
                9.087679381191283e-68,
                -1.5953741338195885e-75,
                7.279888411768805e-65,
                -8.534745620311444e-82,
                7.472235117401435e-65,
                1.0417853071121063e-50,
                -2.1757932773345105e-82,
                6.367505801334348e-87,
                7.098346414087372e-68,
                2.0056018934763585e-87,
                -4.9756812966934605e-98,
                -2.2127683910825345e-80,
                -1.5015260999242632e-72,
                3.235306115059654e-45,
                -1.4675554867610345e-9,
                -3.048289664624658e-72,
                -0.000003358411238723024,
                -9.412561528032283e-66,
                1.8413961482212935e-66,
                4.387494593079372e-62,
                -3.4823329405161405e-44,
                -9.712290624655452e-88,
                6.159019339033206e-79,
                1.5951933951624834e-60,
                8.113708775016778e-84,
                9.28938040763493e-70,
                8.670284782470976e-88,
                3.7511867613227223e-81,
                -1.563622810039548e-67,
                -1.1550797100320747e-69,
                6.232724329279303e-84,
                -6.837823875087702e-83,
                -1.534449811674169e-35,
                1.9246994415858514e-77,
                1.1034482002072507e-54,
                -4.015817698595511e-59,
                7.624780446596304e-77,
                6.890181966776087e-80,
                2.4163093500461633e-48,
                3.455115748625997e-82,
                -1.9624763255263236e-76,
                -8.825153922049766e-82,
                4.876314545323031e-70,
                4.575611743793934e-75,
                -1.32428891661484e-74,
                -7.3263711664671674e-62,
                -4.734482660136632e-75,
                1.7749281749653538e-99,
                -5.0017698590531916e-71,
                1.7278755692964047e-64,
                -2.8930765529686636e-73,
                2.2608736957698019e-69,
                -0.00044604339140172583,
                -2.357925468642044e-89,
                -4.286216665439247e-58,
                1.4978828359311542e-40,
                1.7695523969001554e-31,
                2.0840184249526874e-61,
                -1.9217479424199257e-74,
                -4.1938375782580685e-57,
                2.6501988751676535e-73,
                -1.1612470965304571e-79,
                -3.1538658895682697e-74,
                -1.936322628214626e-30,
                3.918563134120961e-61,
                1.2318915445888919e-79,
                -8.22704951828408e-80,
                -1.1163218397153994e-84,
                3.345397911129507e-75,
                -1.9590321780699372e-88,
                2.7339166593131477e-60,
                2.1723250587590266e-73,
                2.7349226979052956e-71
            };

            // Provide the criteria with the weights
            Queue<Weight> weightsQueue = new Queue<Weight>(weights);
            foreach (Criterion criterion in criteria)
            {
                criterion.ReceiveWeights(weightsQueue);
            }
        }


        /// <summary>
        /// Creates AI with provided weights if not null. If null the weights will be random
        /// </summary>
        /// <param name="weights">weights to create the AI with</param>
        public BetterEvolutionAI(Weight[] assignedWeights = null)
        {
            criteria = new List<Criterion>()
            {
                new CSpecializedKnowledge(),
                new CSingleUseKnowledge(),
                new CPermissions(),
                new CGeneralKnowledge(),
                new CShovels(),
                new CAssistants(),
                new CSpecialPermissions(),
                new CZeppelins(),
                new CCongresses(),
                new CCar(),
                new CPoints(),
                new CTime()
            };

            if (assignedWeights == null)
            {
                int weightCount = criteria.Sum(c => c.WeightsNeeded);
                weights = new Weight[weightCount];

                for (int i = 0; i < weightCount; i++)
                {
                    weights[i] = new Weight(RandomDouble(-1, 1));
                }
            }
            else
            {
                this.weights = assignedWeights;
            }

            // Provide the criteria with the weights
            Queue<Weight> weightsQueue = new Queue<Weight>(weights);
            foreach (Criterion criterion in criteria)
            {
                criterion.ReceiveWeights(weightsQueue);
            }
        }


        protected virtual double EvalScore(ISimulationState gameState, IPlayer player)
        {
            double score = 0;

            foreach (Criterion criterion in criteria)
            {
                score += criterion.GetScore(gameState, player);
            }

            return score;
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

        protected double RandomDouble(double lowerBound, double upperBound)
        {
            return random.NextDouble() * (upperBound - lowerBound) + lowerBound;
        }

        public static BetterEvolutionAI Procreate(params BetterEvolutionAI[] parents)
        {
            // assuming all parents have the same number of weights
            Weight[] newWeights = new Weight[parents[0].weights.Length];

            for (int i = 0; i < newWeights.Length; i++)
            {
                newWeights[i] = parents[random.Next(parents.Length)].weights[i];
            }

            return new BetterEvolutionAI(newWeights);
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
        public BetterEvolutionAI ai;
        List<int> scores;

        public Individual() { }

        public Individual(BetterEvolutionAI ai = null, int id = -1, int generation = -1)
        {
            this.id = id;
            this.generation = generation;
            scores = new List<int>();

            if (ai == null)
            {
                this.ai = new BetterEvolutionAI();
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
        BetterEvolutionAI currentBest;
        double currentBestScore = double.MinValue;
        List<double> averageScores = new List<double>();
        double mutationProbability;
        StreamWriter sw;

        public Population(int count, Weight[] seed = null)
        {
            Individual individual;
            for (int i = 0; i < count; i++)
            {
                individual = new Individual(new BetterEvolutionAI(seed), currentID++, currentGen);
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

                using (var tw = new StreamWriter($"BEST_4_150_5_{survivorRatio}_{parentAmount}_{mutationProbability}_{minMutationRange}.txt", true))
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
            BetterEvolutionAI[] parents = new BetterEvolutionAI[parentAmount];
            Individual newIndividual;
            for (int i = 0; i < childrenCount; i++)
            {
                // choose parents randomly
                for (int j = 0; j < parentAmount; j++)
                {
                    parents[j] = newPopulation[random.Next(newPopulation.Count)].ai;
                }

                // add newly created indiviudal to the new generation
                newIndividual = new Individual(BetterEvolutionAI.Procreate(parents), currentID++, currentGen);
                newIndividual.ai.MutateInRange(mutationProbability, minMutationRange);
                newGeneration.Add(newIndividual);
            }

            // generate random individuals
            for (int i = 0; i < randomIndividualsCount; i++)
            {
                newGeneration.Add(new Individual(new BetterEvolutionAI(null), currentID++, currentGen));
            }

            // generate childer of the best individual so far
            BetterEvolutionAI bestAICopy;
            for (int i = 0; i < bestChildrenCount; i++)
            {
                bestAICopy = new BetterEvolutionAI(currentBest.weights);
                bestAICopy.MutateInRange(mutationProbability, minMutationRange);
                newGeneration.Add(new Individual(bestAICopy, currentID++, currentGen));
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

        public void Evolve(int playersPerGame, int generations, int gamesPerPlayer, double survivorRatio = 0.3, int parentAmount = 4, double mutationProbability = 0.1, double minMutationRange = 0.05, double mutationRelativeRange = 0.3)
        {
            GameSettings.LoadFromFile(@"thebes_config.thc");

            sw = new StreamWriter($"TEST_{playersPerGame}_{generations}_{gamesPerPlayer}_{survivorRatio}_{parentAmount}_{mutationProbability}_{minMutationRange}.txt", true);

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

                    sum += CosineSimilarity(individuals[i].ai.weights, individuals[random.Next(individuals.Count)].ai.weights);
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

            return possibleStates[random.Next(possibleStates.Count)].Move;
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