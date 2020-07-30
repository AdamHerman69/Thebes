using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThebesCore;

namespace ThebesAI
{
    /// <summary>
    /// Criterion used by EvolutionA 
    /// </summary>
    [Serializable]
    public abstract class ComplexCriterion
    {
        public abstract int WeightsNeeded { get; }
        protected Weight[] mainWeights;
        protected Weight[] subWeights;

        public ComplexCriterion(int mainWeightsLength, int subWeightsCount)
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

        protected static double Normalize(double value, double originalMin, double originalMax, double newMin, double newMax)
        {
            return (newMax - newMin) / (originalMax - originalMin) * (value - originalMin) + newMin;
        }

        protected int TotalWeeks(IGame game)
        {
            if (game.Players.Count == 4)
            {
                return 2 * 52;
            }
            else if (game.Players.Count == 3)
            {
                return 2 * 52 + (52 - 16);
            }
            else if (game.Players.Count == 2)
            {
                return 3 * 52;
            }
            else
            {
                throw new ArgumentOutOfRangeException("Invalid number of players");
            }
        }
    }

    [Serializable]
    public class CSpecializedKnowledge : ComplexCriterion
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
                score += mainWeight * subWeights[0].Value * Normalize(Time.weeksInAYear - player.Time.CurrentWeek, 0, 52, 0, 1);

                // years left
                score += mainWeight * subWeights[1].Value * Normalize(Time.finalYear - player.Time.CurrentYear, 0, Time.finalYear - Time.firstYear + 1, 0, 1);

                // artifact value sum at the dig site
                score += mainWeight * subWeights[2].Value * Normalize(gameState.Game.ArtifactSum(digSite), 0, 36, 0, 1);
            }

            return score;
        }
    }

    [Serializable]
    public class CSingleUseKnowledge : ComplexCriterion
    {
        public override int WeightsNeeded => 9 + 3; // Could be changed to 1 instead of 9 since there is only one card in the game

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
                score += mainWeight * subWeights[0].Value * Normalize(Time.weeksInAYear - player.Time.CurrentWeek, 0, 52, 0, 1);

                // years left
                score += mainWeight * subWeights[1].Value * Normalize(Time.finalYear - player.Time.CurrentYear, 0, Time.finalYear - Time.firstYear + 1, 0, 1);

                // artifact value sum at the dig site
                score += mainWeight * subWeights[2].Value * Normalize(gameState.Game.ArtifactSum(digSite), 0, 36, 0, 1);
            }

            return score;
        }
    }

    [Serializable]
    public class CPermissions : ComplexCriterion
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
                    score += mainWeight * subWeights[0].Value * Normalize(Time.weeksInAYear - player.Time.CurrentWeek, 0, 52, 0, 1);

                    // years left
                    score += mainWeight * subWeights[1].Value * Normalize(Time.finalYear - player.Time.CurrentYear, 0, Time.finalYear - Time.firstYear + 1, 0, 1);

                    // artifact value sum at the dig site
                    score += mainWeight * subWeights[2].Value * Normalize(gameState.Game.ArtifactSum(digSite), 0, 36, 0, 1); ;
                }
            }

            return score;
        }
    }

    [Serializable]
    public class CGeneralKnowledge : ComplexCriterion
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
            score += mainWeight * subWeights[0].Value * Normalize(Time.weeksInAYear - player.Time.CurrentWeek, 0, 52, 0, 1);

            // years left
            score += mainWeight * subWeights[1].Value * Normalize(Time.finalYear - player.Time.CurrentYear, 0, Time.finalYear - Time.firstYear + 1, 0, 1);

            // consider number of assistants
            score += mainWeight * subWeights[2].Value * Normalize(player.Assistants, 0, 3, 0, 1);

            // consider number of shovels
            score += mainWeight * subWeights[3].Value * Normalize(player.Shovels, 0, 3, 0, 1);


            return score;
        }
    }

    [Serializable]
    public class CShovels : ComplexCriterion
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
            score += mainWeight * subWeights[0].Value * Normalize(Time.weeksInAYear - player.Time.CurrentWeek, 0, 52, 0, 1);

            // years left
            score += mainWeight * subWeights[1].Value * Normalize(Time.finalYear - player.Time.CurrentYear, 0, Time.finalYear - Time.firstYear + 1, 0, 1);

            // consider number of assistants
            score += mainWeight * subWeights[2].Value * Normalize(player.Assistants, 0, 3, 0, 1);

            // consider number of general knowledge
            score += mainWeight * subWeights[3].Value * Normalize(player.GeneralKnowledge, 0, 8, 0, 1);

            return score;
        }
    }

    [Serializable]
    public class CAssistants : ComplexCriterion
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
            score += mainWeight * subWeights[0].Value * Normalize(Time.weeksInAYear - player.Time.CurrentWeek, 0, 52, 0, 1);

            // years left
            score += mainWeight * subWeights[1].Value * Normalize(Time.finalYear - player.Time.CurrentYear, 0, Time.finalYear - Time.firstYear + 1, 0, 1);

            // consider number of general knowledge
            score += mainWeight * subWeights[2].Value * Normalize(player.GeneralKnowledge, 0, 8, 0, 1);

            // consider number of shovels
            score += mainWeight * subWeights[3].Value * Normalize(player.Shovels, 0, 3, 0, 1);

            return score;
        }
    }

    [Serializable]
    public class CSpecialPermissions : ComplexCriterion
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
            score += mainWeight * subWeights[0].Value * Normalize(Time.weeksInAYear - player.Time.CurrentWeek, 0, 52, 0, 1);

            // years left
            score += mainWeight * subWeights[1].Value * Normalize(Time.finalYear - player.Time.CurrentYear, 0, Time.finalYear - Time.firstYear + 1, 0, 1);

            return score;
        }
    }

    [Serializable]
    public class CZeppelins : ComplexCriterion
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
            score += mainWeight * subWeights[0].Value * Normalize(Time.weeksInAYear - player.Time.CurrentWeek, 0, 52, 0, 1);

            // years left
            score += mainWeight * subWeights[1].Value * Normalize(Time.finalYear - player.Time.CurrentYear, 0, Time.finalYear - Time.firstYear + 1, 0, 1);

            return score;
        }
    }

    [Serializable]
    public class CCongresses : ComplexCriterion
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
            score += mainWeight * subWeights[0].Value * Normalize(Time.weeksInAYear - player.Time.CurrentWeek, 0, 52, 0, 1);

            // years left
            score += mainWeight * subWeights[1].Value * Normalize(Time.finalYear - player.Time.CurrentYear, 0, Time.finalYear - Time.firstYear + 1, 0, 1);

            return score;
        }
    }

    [Serializable]
    public class CCar : ComplexCriterion
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
                score += mainWeight * subWeights[0].Value * Normalize(Time.weeksInAYear - player.Time.CurrentWeek, 0, 52, 0, 1);

                // years left
                score += mainWeight * subWeights[1].Value * Normalize(Time.finalYear - player.Time.CurrentYear, 0, Time.finalYear - Time.firstYear + 1, 0, 1);
            }

            return score;
        }
    }

    [Serializable]
    public class CPoints : ComplexCriterion
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
            score += mainWeight * subWeights[0].Value * Normalize(player.Time.RemainingWeeks(), 0, TotalWeeks(gameState.Game), 0, 1);

            return score;
        }
    }

    [Serializable]
    public class CTime : ComplexCriterion
    {
        public override int WeightsNeeded => 1;

        public CTime() : base(1, 0) { }

        public override double GetScore(ISimulationState gameState, IPlayer player)
        {
            // weeks left * weight
            return mainWeights[0].Value * Normalize(player.Time.RemainingWeeks(), 0, TotalWeeks(gameState.Game), 0, 1);
        }
    }

    /// <summary>
    /// The AI using our A version of the rating function
    /// </summary>

    [Serializable]
    public class EvolutionA : EvolutionAI
    {
        public List<ComplexCriterion> criteria;

        public EvolutionA(int playerCount)
        {
            criteria = new List<ComplexCriterion>()
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

            if (playerCount == 4)
            {
                // weights for 4 players
                this.weights = new Weight[]
                {
                    -0.69095640056345442,
                    0.070225413455732744,
                    0.21102992601740642,
                    0.14979084727810266,
                    0.21327308994404648,
                    0.39258809173134535,
                    0.28003458035180095,
                    0.84742893564860744,
                    0.783342028168655,
                    -0.760221486054464,
                    0.513374222215905,
                    0.26912279020488383,
                    -0.0067365346274973932,
                    0.062475843849813417,
                    0.23480432475209434,
                    0.2630758554037082,
                    -0.13348356768651098,
                    0.27732539702082315,
                    -0.114252531628242,
                    -0.93891200234131511,
                    -0.7674106636864183,
                    -0.11192362569827755,
                    -0.61349855648050955,
                    0.37853909257265705,
                    0.47397350586204484,
                    0.037921856501103378,
                    0.98829442727765748,
                    0.76781291180188438,
                    -0.83126276537369137,
                    0.10719354010987721,
                    0.1545947152909799,
                    0.84740066055553065,
                    -0.08865159935720808,
                    0.19657419863928771,
                    -0.556122067177725,
                    -0.56168631816361392,
                    -0.95957559893785294,
                    -0.17463739070791634,
                    -0.48034347569585945,
                    0.0043186373097443553,
                    0.820557092209606,
                    0.77943846945624728,
                    -0.46650931260851641,
                    -0.554591006391957,
                    0.79879548200350037,
                    -0.32294733883950266,
                    0.38803186891043184,
                    -0.53775733529485648,
                    0.47769258705791673,
                    0.33925140997825715,
                    -0.19581693373425724,
                    0.53521198191922714,
                    0.60904690926384508,
                    -0.71546053619378269,
                    0.0057469563818290208,
                    0.087248888070345323,
                    0.44797081567718222,
                    0.091188443401450492,
                    -0.87244853743931672,
                    -0.4688203425467109,
                    0.72402237901651412,
                    -0.45584107379235378,
                    -0.48573558846755682,
                    -0.25057006871819965,
                    0.12208828941084837,
                    -0.98380038177456441,
                    0.70066445546721312,
                    -0.701006271737165,
                    -0.25852906483157023,
                    0.39350389102171346,
                    0.12822046835358281,
                    -0.13935346302546259,
                    0.37722806324121927,
                    0.85307651937616824,
                    -0.78887304467562258,
                    0.86912193515762781,
                    -0.513703260344315,
                    -0.4337476130033599,
                    0.066504953366939373,
                    0.081396951424608371,
                    -0.36042220860739338,
                    0.35617944153779163,
                    -0.15114003345423385,
                    0.092978365832464077,
                    0.600859083980722,
                    0.39000990751199888
                };
            }
            else if (playerCount == 3)
            {
                // weights for 3 players
                this.weights = new Weight[]
                {
                    0.40580581566589213,
                    -0.462793017487411,
                    -0.91856565881453722,
                    0.39383331909488584,
                    -0.37129121374864654,
                    0.624126179900079,
                    0.21125500989670629,
                    -0.77192233510870589,
                    0.81402500409820355,
                    -0.70099264322826293,
                    0.57800125609058961,
                    0.99491845129372281,
                    0.75441792337429625,
                    0.83179993218826132,
                    -0.355819284615954,
                    0.51250967914821088,
                    0.751105609234006,
                    0.7621189902593003,
                    0.3353549713899171,
                    -0.50221245805835935,
                    0.95679334677055694,
                    -0.094600525263045188,
                    -0.8420064441822499,
                    -0.85474884968937781,
                    0.770438462388906,
                    -0.3722932782826448,
                    -0.53791147383763982,
                    0.014863644849911064,
                    -0.79241201888882185,
                    -0.78632895720020357,
                    0.75222480192418439,
                    -0.65453478398478349,
                    0.38244884618672015,
                    0.14793024079312112,
                    -0.1028800746905059,
                    -0.66757635582125574,
                    0.784218977267956,
                    0.21318577658999049,
                    -0.011914608539973615,
                    0.96015252161778164,
                    0.95841779112432957,
                    0.81757454984708444,
                    -0.29820331381550208,
                    0.47132762410739781,
                    -0.72172486231276989,
                    0.962132196919315,
                    0.042429354527233842,
                    -0.91277847248724586,
                    0.08820768366018665,
                    0.95479498356804016,
                    0.41072600470423981,
                    -0.785033315320049,
                    -0.349783342704076,
                    0.14050778985978463,
                    -0.016709777999999842,
                    0.78432753374070274,
                    -0.46210327612334073,
                    0.11842377773412681,
                    0.52343058843325374,
                    0.85434708825980643,
                    -0.30476637738978785,
                    0.963364001067059,
                    0.16064688615065387,
                    0.80939093677298679,
                    -0.91129808731437578,
                    0.33580457295095756,
                    0.60136522487800792,
                    0.53671327034789751,
                    -0.17559259204454375,
                    -0.12112662050459847,
                    0.14441589647644004,
                    -0.11621551987073181,
                    -0.39068419180842312,
                    0.96137186044890988,
                    0.437002804333811,
                    -0.13564572473319511,
                    0.526380105654886,
                    0.877460714372555,
                    -0.23019386743204381,
                    0.54079777164887544,
                    -0.80195648807657727,
                    0.65821384671526684,
                    0.16536037305619589,
                    0.28031996697202333,
                    0.40164222172537922,
                    -0.1154454020389567
                };
            }
            else if (playerCount == 2)
            {
                // weights for 2 players
                this.weights = new Weight[]
                {
                    -0.70743528087969643,
                    -0.73992875951338966,
                    0.57976142593182689,
                    0.043953856939428437,
                    -0.33561445283033625,
                    -0.39841987350882024,
                    -0.88129798224256284,
                    -0.892902487839061,
                    0.68420271223140994,
                    -0.41201055022515853,
                    -0.47662568347371448,
                    -0.66868095457026777,
                    0.31865383094113975,
                    0.41622066656882906,
                    0.32706816183732279,
                    0.81626515268174238,
                    -0.709380774623426,
                    -0.58344133909951956,
                    0.58364456593228708,
                    -0.13921714720279776,
                    -0.28765153949505251,
                    0.16662417592789236,
                    0.016845424294865331,
                    -0.11364110331686261,
                    0.56792736778404906,
                    -0.021702718465450577,
                    0.83156589969599892,
                    0.44251379172434746,
                    -0.83108238551350422,
                    -0.40935625387791374,
                    -0.19620346147390244,
                    0.70969934841138294,
                    0.86846544308050788,
                    0.98906165267762813,
                    0.057735660605940708,
                    -0.90704732614431871,
                    -0.891146294721936,
                    0.19805865697472291,
                    0.17198304670023876,
                    -0.4964681302646492,
                    -0.51876125369163284,
                    -0.81775986581005156,
                    -0.70228519120825694,
                    -0.79883747806252792,
                    0.43016352664221236,
                    0.94377913602803787,
                    -0.78763643176650466,
                    -0.9354743351859387,
                    -0.92467063103088676,
                    0.755814853010613,
                    0.46387373072275606,
                    -0.88152869785275711,
                    0.2249844880890961,
                    -0.59567437488384289,
                    -0.3064288614813373,
                    -0.43493309311332795,
                    -0.025987853773863923,
                    0.38109319099741679,
                    -0.52162269294337493,
                    0.27825722996064339,
                    -0.267268902001562,
                    0.40716095706781408,
                    0.13383804873276417,
                    0.34151940948400616,
                    0.75536799605720129,
                    -0.059079820783380366,
                    -0.012560426729060881,
                    -0.58241537100747942,
                    0.2403693772574744,
                    0.27450152825308094,
                    -0.35630087897009255,
                    -0.90603797971552147,
                    -0.60971601661747132,
                    0.080112696196936284,
                    -0.12877565022966619,
                    -0.34821348420726761,
                    -0.2327493799304354,
                    0.74806653510223442,
                    -0.94917459411042493,
                    -0.92257564325005548,
                    -0.90583573370512371,
                    -0.82469310696455334,
                    -0.87215809145577161,
                    0.081921862010807756,
                    0.53407115374415692,
                    0.97761922789268962
                };
            }
            else
            {
                throw new ArgumentOutOfRangeException("Invalid number of players");
            }
            

            // Provide the criteria with the weights
            Queue<Weight> weightsQueue = new Queue<Weight>(weights);
            foreach (ComplexCriterion criterion in criteria)
            {
                criterion.ReceiveWeights(weightsQueue);
            }
        }


        /// <summary>
        /// Creates AI with provided weights if not null. If null the weights will be random
        /// </summary>
        /// <param name="weights">weights to create the AI with</param>
        public EvolutionA(Weight[] assignedWeights = null)
        {
            criteria = new List<ComplexCriterion>()
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
            foreach (ComplexCriterion criterion in criteria)
            {
                criterion.ReceiveWeights(weightsQueue);
            }
        }


        /// <summary>
        /// Evaluates the score of the given game state from the perspective of the given player
        /// </summary>
        /// <param name="gameState"></param>
        /// <param name="player"></param>
        /// <returns></returns>
        protected virtual double EvalScore(ISimulationState gameState, IPlayer player)
        {
            double score = 0;

            foreach (ComplexCriterion criterion in criteria)
            {
                score += criterion.GetScore(gameState, player);
            }

            return score;
        }

        /// <summary>
        /// Returns the best possible action to play from the specifeid state
        /// </summary>
        /// <param name="gameState"></param>
        /// <returns></returns>
        public override IAction TakeAction(IGame gameState)
        {
            string playerName = gameState.ActivePlayer.Name;

            // generate all possible moves and their resulting game states
            List<ISimulationState> possibleStates = new SimulationState(gameState).GetAllChildStates();
            //List<ISimulationState> possibleStates = new SimulationState(new DeterministicGame((Game)gameState)).GetAllChildStates();


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

        /// <summary>
        /// Performs a crossover from the parents specified
        /// </summary>
        /// <param name="parents"></param>
        /// <returns></returns>
        public static EvolutionA Procreate(params EvolutionAI[] parents)
        {
            // assuming all parents have the same number of weights
            Weight[] newWeights = new Weight[parents[0].weights.Length];

            for (int i = 0; i < newWeights.Length; i++)
            {
                newWeights[i] = parents[random.Next(parents.Length)].weights[i];
            }

            return new EvolutionA(newWeights);
        }
    }

}
