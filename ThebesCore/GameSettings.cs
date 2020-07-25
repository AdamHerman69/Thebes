using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Linq.Expressions;

namespace ThebesCore
{
    /// <summary>
    /// Holds the information about the current version of the game played.
    /// </summary>
    public static class GameSettings
    {
        public static List<IPlace> Places { get; private set; }
        public static IPlace StartingPlace { get; private set; }
        public static int[,] Distances { get; private set; }
        public static Dictionary<int, Dictionary<int, int>> TimeWheel { get; private set; }
        public static List<ICard> Cards { get; private set; }

        /// <summary>
        /// Gets the distnce between two places.
        /// </summary>
        /// <param name="from">origin</param>
        /// <param name="to">destination</param>
        /// <returns>distance in weeks</returns>
        public static int GetDistance(IPlace from, IPlace to)
        {
            return Distances[from.Index, to.Index];
        }

        /// <summary>
        /// Returns the amout of tokens drawn for given knowledge and time 
        /// </summary>
        /// <param name="knowledge">knowledge amount</param>
        /// <param name="weeks">time to spend in weeks</param>
        /// <returns></returns>
        public static int DugTokenCount(int knowledge, int weeks)
        {
            if (weeks > 12) throw new InvalidOperationException("You can't dig for more than 12 weeks");
            if (weeks < 1 || knowledge < 1)
            {
                return 0;
            }
            if (knowledge >= 12)
            {
                knowledge = 12;
            }
            return TimeWheel[knowledge][weeks];
        }

        /// <summary>
        /// Returns a place with given name
        /// </summary>
        /// <param name="placeName"></param>
        /// <returns>Place object</returns>
        public static IPlace getPlaceByName(string placeName)
        {
            foreach (IPlace place in Places)
            {
                if (place.Name.Equals(placeName))
                {
                    return place;
                }
            }
            return null;
        }

        /// <summary>
        /// Loads all data from a .thc file. The format is specified in the documentation
        /// </summary>
        /// <param name="path"></param>
        public static void LoadFromFile(string path)
        {
            Places = new List<IPlace>();
            Cards = new List<ICard>();


            Queue<string[]> lines = new Queue<string[]>();

            // load lines
            StreamReader sr = new StreamReader(path);
            string lineString;
            string[] line;
            while ((lineString = sr.ReadLine()) != null)
            {
                line = lineString.Split((char[])null, options: StringSplitOptions.RemoveEmptyEntries);
                if (line.Length != 0 && !line[0].Equals("") && line[0][0] != '#') // is not comment or empty
                {
                    lines.Enqueue(line);
                }
            }
            sr.Close();


            line = lines.Dequeue();

            // UNIVERSITIES
            if (!line[0].Equals("UNIVERSITIES")) throw new FormatException("invalid config file format");
            int index = 0;
            while (lines.Count > 0 && !(line = lines.Dequeue())[0].Equals("DIGSITES"))
            {
                Places.Add(new University(line[0], index++));
            }

            // DIGSITES
            if (!line[0].Equals("DIGSITES")) throw new FormatException("invalid config file format");
            while (lines.Count > 0 && !(line = lines.Dequeue())[0].Equals("CARDCHANGEPLACES"))
            {
                Places.Add(new DigSite(line[0], index++));
            }

            // CARDCHANGEPLACES
            if (!line[0].Equals("CARDCHANGEPLACES")) throw new FormatException("invalid config file format");
            while (lines.Count > 0 && !(line = lines.Dequeue())[0].Equals("STARTINGPLACE"))
            {
                Places.Add(new CardChangePlace(line[0], index++));
            }

            if (!line[0].Equals("STARTINGPLACE")) throw new FormatException("invalid config file format");
            StartingPlace = getPlaceByName(line[1]);


            int itemID = 0;

            // DISTRIBUTIONS
            line = lines.Dequeue();
            if (!line[0].Equals("DISTRIBUTIONS")) throw new FormatException("invalid config file format");

            line = lines.Dequeue();
            for (int i = 0; i < Places.Where(x => x is IDigSite).Count(); i++)
            {
                if (!line[0].Equals("TOKENS")) throw new FormatException("invalid config file format");
                IDigSite digSite = (IDigSite)getPlaceByName(line[1]);

                while (lines.Count > 0 && !(line = lines.Dequeue())[0].Equals("TOKENS") && !line[0].Equals("DISTANCEMATRIX"))
                {
                    switch (line[0])
                    {
                        case "dirt":
                            digSite.Tokens.Add(new DirtToken(itemID++.ToString("0000"), digSite));
                            break;

                        case "specializedKnowledge":
                            digSite.Tokens.Add(new SpecializedKnowledgeToken(itemID++.ToString("0000"), digSite, int.Parse(line[1]), (IDigSite)getPlaceByName(line[2])));
                            break;

                        case "generalKnowledge":
                            digSite.Tokens.Add(new GeneralKnowledgeToken(itemID++.ToString("0000"), digSite, int.Parse(line[1])));
                            break;

                        case "artifact":
                            digSite.Tokens.Add(new ArtifactToken(itemID++.ToString("0000"), digSite, int.Parse(line[1]), line[2]));
                            break;

                        default:
                            throw new FormatException("invalid config file format");
                    }
                }
            }

            // DISTANCE MATRIX
            if (!line[0].Equals("DISTANCEMATRIX")) throw new FormatException("invalid config file format");

            if (!(line = lines.Dequeue())[0].Equals("PLACES")) throw new FormatException("invalid config file format");
            int[] indexMapping = new int[Places.Count + 1];
            for (int i = 1; i < indexMapping.Length; i++)
            {
                indexMapping[i] = getPlaceByName(line[i]).Index;
            }

            Distances = new int[Places.Count, Places.Count];
            for (int i = 0; i < Places.Count; i++)
            {
                line = lines.Dequeue();
                IPlace place = getPlaceByName(line[0]);
                for (int j = 1; j < indexMapping.Length; j++)
                {
                    Distances[place.Index, indexMapping[j]] = int.Parse(line[j]);
                }
            }

            // CARDS
            if (!(line = lines.Dequeue())[0].Equals("CARDS")) throw new FormatException("invalid config file format");

            line = lines.Dequeue();
            while (lines.Count > 0 && !line[0].Equals("TIMEWHEEL"))
            {
                switch (line[0])
                {
                    case "specializedKnowledge":
                        Cards.Add(new SpecializedKnowledgeCard(itemID++.ToString("0000"), (IUniversity)getPlaceByName(line[1]), int.Parse(line[2]), int.Parse(line[3]), (IDigSite)getPlaceByName(line[4])));
                        break;

                    case "generalKnowledge":
                        Cards.Add(new GeneralKnowledgeCard(itemID++.ToString("0000"), (IUniversity)getPlaceByName(line[1]), int.Parse(line[2]), int.Parse(line[3])));
                        break;

                    case "rumors":
                        Cards.Add(new RumorsCard(itemID++.ToString("0000"), (IUniversity)getPlaceByName(line[1]), int.Parse(line[2]), int.Parse(line[3]), (IDigSite)getPlaceByName(line[4])));
                        break;

                    case "zeppelin":
                        Cards.Add(new ZeppelinCard(itemID++.ToString("0000"), (IUniversity)getPlaceByName(line[1]), int.Parse(line[2])));
                        break;

                    case "car":
                        Cards.Add(new CarCard(itemID++.ToString("0000"), (IUniversity)getPlaceByName(line[1]), int.Parse(line[2])));
                        break;

                    case "assistant":
                        Cards.Add(new AssistantCard(itemID++.ToString("0000"), (IUniversity)getPlaceByName(line[1]), int.Parse(line[2])));
                        break;

                    case "shovel":
                        Cards.Add(new ShovelCard(itemID++.ToString("0000"), (IUniversity)getPlaceByName(line[1]), int.Parse(line[2])));
                        break;

                    case "specialPermission":
                        Cards.Add(new SpecialPermissionCard(itemID++.ToString("0000"), (IUniversity)getPlaceByName(line[1]), int.Parse(line[2])));
                        break;

                    case "congress":
                        Cards.Add(new CongressCard(itemID++.ToString("0000"), (IUniversity)getPlaceByName(line[1]), int.Parse(line[2])));
                        break;

                    case "exhibition":
                        List<IDigSite> artifactsRequired = new List<IDigSite>();
                        for (int i = 4; i < line.Length; i++)
                        {
                            artifactsRequired.Add((IDigSite)getPlaceByName(line[i]));
                        }
                        Cards.Add(new ExhibitionCard(itemID++.ToString("0000"), (IUniversity)getPlaceByName(line[1]), int.Parse(line[2]), int.Parse(line[3]), artifactsRequired));
                        break;

                    default:
                        throw new FormatException("invalid config file format");
                }
                line = lines.Dequeue();
            }

            // TIMEWHEEL
            if (!line[0].Equals("TIMEWHEEL")) throw new FormatException("invalid config file format");
            TimeWheel = new Dictionary<int, Dictionary<int, int>>();
            int knowledge;
            while (lines.Count > 0)
            {
                line = lines.Dequeue();
                knowledge = int.Parse(line[0]);
                TimeWheel.Add(knowledge, new Dictionary<int, int>());
                for (int i = 1; i < line.Length; i++)
                {
                    TimeWheel[knowledge].Add(i, int.Parse(line[i]));
                }
            }
        }

        /// <summary>
        /// Saves all the current data to a .thc file
        /// </summary>
        /// <param name="path"></param>
        public static void SaveToFile(string path)
        {
            File.WriteAllText(path, getSettingsString());
        }

        /// <summary>
        /// Returns all current setting in a string with a .thc format
        /// </summary>
        /// <returns>.thc string</returns>
        private static string getSettingsString()
        {
            string str = "UNIVERSITIES\n";

            foreach (IPlace place in Places)
            {
                if (place is IUniversity)
                {
                    str += place.Name + "\n";
                }
            }

            str += "\nDIGSITES\n";
            foreach (IPlace place in Places)
            {
                if (place is IDigSite)
                {
                    str += place.Name + "\n";
                }
            }

            str += "\nCARDCHANGEPLACES\n";
            foreach (IPlace place in Places)
            {
                if (place is ICardChangePlace)
                {
                    str += place.Name + "\n";
                }
            }

            str += "\nSTARTINGPLACE " + StartingPlace.Name + "\n";

            str += "\nDISTRIBUTIONS\n";

            foreach (IPlace place in Places)
            {
                if (place is IDigSite)
                {
                    str += $"\nTOKENS {place.Name}\n";
                    foreach (IToken token in ((IDigSite)place).Tokens)
                    {
                        if (token is IDirtToken)
                        {
                            str += "dirt\n";
                        }
                        if (token is ISpecializedKnowledgeToken)
                        {
                            str += $"specializedKnowledge {((ISpecializedKnowledgeToken)token).KnowledgeAmount} {((ISpecializedKnowledgeToken)token).KnowledgeDigSite.Name}\n";
                        }
                        if (token is IGeneralKnowledgeToken)
                        {
                            str += $"generalKnowledge {((IGeneralKnowledgeToken)token).KnowledgeAmount}\n";
                        }
                        if (token is IArtifactToken)
                        {
                            str += $"artifact {((IArtifactToken)token).Points} {((IArtifactToken)token).Name}\n";
                        }
                    }
                }
            }

            str += "\nDISTANCEMATRIX\n";
            str += "PLACES ";
            foreach (IPlace place in Places)
            {
                str += place.Name + " ";
            }

            foreach (IPlace place in Places)
            {
                str += "\n" + place.Name + "\t";
                foreach (IPlace otherPlace in Places)
                {
                    str += GetDistance(place, otherPlace) + "\t";
                }
            }

            str += "\n\nCARDS\n";
            foreach (ISpecializedKnowledgeCard card in Cards.Where(x => x is ISpecializedKnowledgeCard))
            {
                str += $"specializedKnowledge {card.Place} {card.Weeks} {card.KnowledgeAmount} {card.digSite.Name}\n";
            }

            foreach (IGeneralKnowledgeCard card in Cards.Where(x => x is IGeneralKnowledgeCard))
            {
                str += $"generalKnowledge {card.Place} {card.Weeks} {card.KnowledgeAmount}\n";
            }

            foreach (IRumorsCard card in Cards.Where(x => x is IRumorsCard))
            {
                str += $"rumors {card.Place} {card.Weeks} {card.KnowledgeAmount} {card.digSite.Name}\n";
            }

            foreach (IZeppelinCard card in Cards.Where(x => x is IZeppelinCard))
            {
                str += $"zeppelin {card.Place} {card.Weeks}\n";
            }

            foreach (ICarCard card in Cards.Where(x => x is ICarCard))
            {
                str += $"car {card.Place} {card.Weeks}\n";
            }

            foreach (IAssistantCard card in Cards.Where(x => x is IAssistantCard))
            {
                str += $"assistant {card.Place} {card.Weeks}\n";
            }

            foreach (IShovelCard card in Cards.Where(x => x is IShovelCard))
            {
                str += $"shovel {card.Place} {card.Weeks}\n";
            }

            foreach (ISpecialPermissionCard card in Cards.Where(x => x is ISpecialPermissionCard))
            {
                str += $"specialPermission {card.Place} {card.Weeks}\n";
            }

            foreach (ICongressCard card in Cards.Where(x => x is ICongressCard))
            {
                str += $"congress {card.Place} {card.Weeks}\n";
            }

            foreach (IExhibitionCard card in Cards.Where(x => x is IExhibitionCard))
            {
                str += $"exhibition {card.Place} {card.Weeks} {card.Points} ";
                foreach (IDigSite digSite in card.ArtifactsRequired)
                {
                    str += digSite.Name + " ";
                }
                str += "\n";
            }

            str += "\nTIMEWHEEL\n";
            str += "# [knowledge] [1 week] ... [n week]\n";

            foreach (KeyValuePair<int, Dictionary<int, int>> kvp in TimeWheel)
            {
                str += $"{kvp.Key} ";
                foreach (KeyValuePair<int, int> weeksTokens in kvp.Value)
                {
                    str += weeksTokens.Value + " ";
                }
                str += "\n";
            }

            return str;
        }

        /// <summary>
        /// Loads data from GameSettingsSerializable object
        /// </summary>
        /// <param name="data">data to load</param>
        public static void LoadSerializedData(GameSettingsSerializable data)
        {
            Places = data.Places;
            StartingPlace = data.StartingPlace;
            Distances = data.Distances;
            TimeWheel = data.TimeWheel;
            Cards = data.Cards;
        }

        /// <summary>
        /// Legacy function initializing the game with standart Thebes rules
        /// </summary>
        public static void Initialize()
        {
            List<IToken> CreateTokenList(IDigSite digSite, int one, int two, int three, int four, int five, int six, int seven, IDigSite knowledgeDigSite)
            {
                List<IToken> tokenList = new List<IToken>();

                // Specialized Knowledge
                tokenList.Add(new SpecializedKnowledgeToken("TODO", digSite, 1, knowledgeDigSite));

                // General Knowledge
                tokenList.Add(new GeneralKnowledgeToken("TODO", digSite, 1));

                // Dirt
                for (int i = 0; i < 16; i++)
                {
                    tokenList.Add(new DirtToken("TODO", digSite));
                }

                // Artifacts
                for (int i = 0; i < one; i++)
                {
                    tokenList.Add(new ArtifactToken("TODO", digSite, 1, "TODO"));
                }

                for (int i = 0; i < two; i++)
                {
                    tokenList.Add(new ArtifactToken("TODO", digSite, 2, "TODO"));
                }

                for (int i = 0; i < three; i++)
                {
                    tokenList.Add(new ArtifactToken("TODO", digSite, 3, "TODO"));
                }

                for (int i = 0; i < four; i++)
                {
                    tokenList.Add(new ArtifactToken("TODO", digSite, 4, "TODO"));
                }

                for (int i = 0; i < five; i++)
                {
                    tokenList.Add(new ArtifactToken("TODO", digSite, 5, "TODO"));
                }

                for (int i = 0; i < six; i++)
                {
                    tokenList.Add(new ArtifactToken("TODO", digSite, 6, "TODO"));
                }

                for (int i = 0; i < seven; i++)
                {
                    tokenList.Add(new ArtifactToken("TODO", digSite, 7, "TODO"));
                }

                if (tokenList.Count != 31)
                {
                    throw new InvalidOperationException();
                }
                return tokenList;
            }


            // PLACES
            // Universities
            Places = new List<IPlace>();
            University london = new University("London", 0);
            Places.Add(london);

            University paris = new University("Paris", 1);
            Places.Add(paris);

            University berlin = new University("Berlin", 2);
            Places.Add(berlin);

            University vienna = new University("Vienna", 3);
            Places.Add(vienna);

            University moscow = new University("Moscow", 4);
            Places.Add(moscow);

            University rome = new University("Rome", 5);
            Places.Add(rome);

            // Card chagne places
            CardChangePlace warsaw = new CardChangePlace("Warsaw", 6);
            Places.Add(warsaw);

            // Digsites
            DigSite greece = new DigSite("Greece", 7);
            DigSite mesopotamia = new DigSite("Mesopotamia", 8);
            DigSite egypt = new DigSite("Egypt", 9);
            DigSite crete = new DigSite("Crete", 10);
            DigSite palestine = new DigSite("Palestine", 11);

            // Adding tokens to digsites
            greece.Tokens = CreateTokenList(greece, 4, 3, 2, 1, 2, 1, 0, crete);
            Places.Add(greece);

            mesopotamia.Tokens = CreateTokenList(mesopotamia, 5, 0, 3, 3, 2, 0, 0, palestine);
            Places.Add(mesopotamia);

            egypt.Tokens = CreateTokenList(egypt, 4, 2, 3, 2, 1, 1, 0, mesopotamia);
            Places.Add(egypt);

            crete.Tokens = CreateTokenList(crete, 3, 2, 4, 3, 1, 0, 0, greece);
            Places.Add(crete);

            palestine.Tokens = CreateTokenList(palestine, 5, 3, 1, 1, 1, 1, 1, egypt);
            Places.Add(palestine);


            StartingPlace = warsaw;

            // DISTANCES
            Distances = new int[12, 12];

            Distances[0, 0] = 0;
            Distances[0, 1] = 1;
            Distances[0, 2] = 1;
            Distances[0, 3] = 2;
            Distances[0, 4] = 3;
            Distances[0, 5] = 2;
            Distances[0, 6] = 2;
            Distances[0, 7] = 3;
            Distances[0, 8] = 4;
            Distances[0, 9] = 4;
            Distances[0, 10] = 3;
            Distances[0, 11] = 4;

            Distances[1, 0] = 1;
            Distances[1, 1] = 0;
            Distances[1, 2] = 1;
            Distances[1, 3] = 1;
            Distances[1, 4] = 3;
            Distances[1, 5] = 1;
            Distances[1, 6] = 2;
            Distances[1, 7] = 2;
            Distances[1, 8] = 3;
            Distances[1, 9] = 3;
            Distances[1, 10] = 2;
            Distances[1, 11] = 3;

            Distances[2, 0] = 1;
            Distances[2, 1] = 1;
            Distances[2, 2] = 0;
            Distances[2, 3] = 2;
            Distances[2, 4] = 2;
            Distances[2, 5] = 2;
            Distances[2, 6] = 1;
            Distances[2, 7] = 2;
            Distances[2, 8] = 3;
            Distances[2, 9] = 4;
            Distances[2, 10] = 3;
            Distances[2, 11] = 3;

            Distances[3, 0] = 2;
            Distances[3, 1] = 1;
            Distances[3, 2] = 2;
            Distances[3, 3] = 0;
            Distances[3, 4] = 2;
            Distances[3, 5] = 1;
            Distances[3, 6] = 1;
            Distances[3, 7] = 2;
            Distances[3, 8] = 3;
            Distances[3, 9] = 3;
            Distances[3, 10] = 2;
            Distances[3, 11] = 3;

            Distances[4, 0] = 3;
            Distances[4, 1] = 3;
            Distances[4, 2] = 2;
            Distances[4, 3] = 2;
            Distances[4, 4] = 0;
            Distances[4, 5] = 3;
            Distances[4, 6] = 1;
            Distances[4, 7] = 2;
            Distances[4, 8] = 3;
            Distances[4, 9] = 4;
            Distances[4, 10] = 3;
            Distances[4, 11] = 4;

            Distances[5, 0] = 1;
            Distances[5, 1] = 1;
            Distances[5, 2] = 2;
            Distances[5, 3] = 1;
            Distances[5, 4] = 3;
            Distances[5, 5] = 0;
            Distances[5, 6] = 2;
            Distances[5, 7] = 1;
            Distances[5, 8] = 2;
            Distances[5, 9] = 2;
            Distances[5, 10] = 1;
            Distances[5, 11] = 2;

            Distances[6, 0] = 2;
            Distances[6, 1] = 2;
            Distances[6, 2] = 1;
            Distances[6, 3] = 1;
            Distances[6, 4] = 1;
            Distances[6, 5] = 2;
            Distances[6, 6] = 0;
            Distances[6, 7] = 1;
            Distances[6, 8] = 2;
            Distances[6, 9] = 3;
            Distances[6, 10] = 2;
            Distances[6, 11] = 3;

            Distances[7, 0] = 3;
            Distances[7, 1] = 2;
            Distances[7, 2] = 2;
            Distances[7, 3] = 2;
            Distances[7, 4] = 2;
            Distances[7, 5] = 1;
            Distances[7, 6] = 1;
            Distances[7, 7] = 0;
            Distances[7, 8] = 1;
            Distances[7, 9] = 2;
            Distances[7, 10] = 1;
            Distances[7, 11] = 2;

            Distances[8, 0] = 4;
            Distances[8, 1] = 3;
            Distances[8, 2] = 3;
            Distances[8, 3] = 3;
            Distances[8, 4] = 3;
            Distances[8, 5] = 2;
            Distances[8, 6] = 2;
            Distances[8, 7] = 1;
            Distances[8, 8] = 0;
            Distances[8, 9] = 2;
            Distances[8, 10] = 2;
            Distances[8, 11] = 1;

            Distances[9, 0] = 4;
            Distances[9, 1] = 3;
            Distances[9, 2] = 4;
            Distances[9, 3] = 3;
            Distances[9, 4] = 4;
            Distances[9, 5] = 2;
            Distances[9, 6] = 3;
            Distances[9, 7] = 2;
            Distances[9, 8] = 2;
            Distances[9, 9] = 0;
            Distances[9, 10] = 1;
            Distances[9, 11] = 1;

            Distances[10, 0] = 3;
            Distances[10, 1] = 2;
            Distances[10, 2] = 3;
            Distances[10, 3] = 2;
            Distances[10, 4] = 3;
            Distances[10, 5] = 1;
            Distances[10, 6] = 2;
            Distances[10, 7] = 1;
            Distances[10, 8] = 2;
            Distances[10, 9] = 1;
            Distances[10, 10] = 0;
            Distances[10, 11] = 1;

            Distances[11, 0] = 4;
            Distances[11, 1] = 3;
            Distances[11, 2] = 3;
            Distances[11, 3] = 3;
            Distances[11, 4] = 4;
            Distances[11, 5] = 2;
            Distances[11, 6] = 3;
            Distances[11, 7] = 2;
            Distances[11, 8] = 2;
            Distances[11, 9] = 1;
            Distances[11, 10] = 1;
            Distances[11, 11] = 0;

            // TIMEWHEEL
            TimeWheel = new Dictionary<int, Dictionary<int, int>>();

            TimeWheel.Add(1, new Dictionary<int, int>
            {
                {1, 0},
                {2, 0},
                {3, 0},
                {4, 0},
                {5, 0},
                {6, 0},
                {7, 0},
                {8, 1},
                {9, 1},
                {10, 1},
                {11, 1},
                {12, 2},
            });

            TimeWheel.Add(2, new Dictionary<int, int>
            {
                {1, 0},
                {2, 0},
                {3, 0},
                {4, 0},
                {5, 0},
                {6, 1},
                {7, 1},
                {8, 1},
                {9, 1},
                {10, 2},
                {11, 2},
                {12, 2},
            });

            TimeWheel.Add(3, new Dictionary<int, int>
            {
                {1, 0},
                {2, 0},
                {3, 1},
                {4, 1},
                {5, 1},
                {6, 2},
                {7, 2},
                {8, 2},
                {9, 3},
                {10, 3},
                {11, 3},
                {12, 4},
            });

            TimeWheel.Add(4, new Dictionary<int, int>
            {
                {1, 0},
                {2, 1},
                {3, 1},
                {4, 2},
                {5, 2},
                {6, 3},
                {7, 3},
                {8, 4},
                {9, 4},
                {10, 4},
                {11, 5},
                {12, 5},
            });

            TimeWheel.Add(5, new Dictionary<int, int>
            {
                {1, 1},
                {2, 1},
                {3, 2},
                {4, 3},
                {5, 3},
                {6, 4},
                {7, 4},
                {8, 5},
                {9, 5},
                {10, 6},
                {11, 7},
                {12, 8},
            });

            TimeWheel.Add(6, new Dictionary<int, int>
            {
                {1, 1},
                {2, 2},
                {3, 3},
                {4, 4},
                {5, 4},
                {6, 5},
                {7, 5},
                {8, 5},
                {9, 6},
                {10, 6},
                {11, 7},
                {12, 8},
            });

            TimeWheel.Add(7, new Dictionary<int, int>
            {
                {1, 1},
                {2, 2},
                {3, 4},
                {4, 4},
                {5, 5},
                {6, 5},
                {7, 6},
                {8, 6},
                {9, 7},
                {10, 8},
                {11, 8},
                {12, 9},
            });

            TimeWheel.Add(8, new Dictionary<int, int>
            {
                {1, 1},
                {2, 3},
                {3, 4},
                {4, 5},
                {5, 5},
                {6, 6},
                {7, 6},
                {8, 7},
                {9, 8},
                {10, 9},
                {11, 9},
                {12, 10},
            });

            TimeWheel.Add(9, new Dictionary<int, int>
            {
                {1, 2},
                {2, 3},
                {3, 5},
                {4, 5},
                {5, 6},
                {6, 6},
                {7, 7},
                {8, 8},
                {9, 9},
                {10, 9},
                {11, 10},
                {12, 10},
            });

            TimeWheel.Add(10, new Dictionary<int, int>
            {
                {1, 2},
                {2, 3},
                {3, 5},
                {4, 5},
                {5, 6},
                {6, 7},
                {7, 8},
                {8, 9},
                {9, 9},
                {10, 10},
                {11, 10},
                {12, 10},
            });

            TimeWheel.Add(11, new Dictionary<int, int>
            {
                {1, 2},
                {2, 4},
                {3, 5},
                {4, 6},
                {5, 6},
                {6, 7},
                {7, 8},
                {8, 9},
                {9, 10},
                {10, 10},
                {11, 10},
                {12, 10},
            });

            TimeWheel.Add(12, new Dictionary<int, int>
            {
                {1, 3},
                {2, 4},
                {3, 5},
                {4, 6},
                {5, 7},
                {6, 8},
                {7, 9},
                {8, 10},
                {9, 10},
                {10, 10},
                {11, 10},
                {12, 10},
            });

            //
            //
            // CARDS
            //
            //

            Cards = new List<ICard>();

            // EXHIBITIONS
            // small

            Cards.Add(new ExhibitionCard("TODO", london, 3, 4, new List<IDigSite>{
                greece, egypt, egypt
            }));

            Cards.Add(new ExhibitionCard("TODO", moscow, 3, 4, new List<IDigSite>{
                mesopotamia, crete, crete
            }));

            Cards.Add(new ExhibitionCard("TODO", vienna, 3, 4, new List<IDigSite>{
                egypt, palestine, palestine
            }));

            Cards.Add(new ExhibitionCard("TODO", paris, 3, 4, new List<IDigSite>{
                palestine, mesopotamia, mesopotamia
            }));

            Cards.Add(new ExhibitionCard("TODO", berlin, 3, 4, new List<IDigSite>{
                crete, greece, greece
            }));

            // large

            Cards.Add(new ExhibitionCard("TODO", berlin, 4, 5, new List<IDigSite>{
                palestine, mesopotamia, mesopotamia, crete, crete, crete
            }));

            Cards.Add(new ExhibitionCard("TODO", paris, 4, 5, new List<IDigSite>{
                greece, egypt, egypt, palestine, palestine, palestine
            }));

            Cards.Add(new ExhibitionCard("TODO", london, 4, 5, new List<IDigSite>{
                crete, greece, greece, egypt, egypt, egypt
            }));

            Cards.Add(new ExhibitionCard("TODO", vienna, 4, 5, new List<IDigSite>{
                mesopotamia, crete, crete, greece, greece, greece
            }));

            Cards.Add(new ExhibitionCard("TODO", moscow, 4, 5, new List<IDigSite>{
                egypt, palestine, palestine, mesopotamia, mesopotamia, mesopotamia
            }));


            //
            // Special cards
            //

            // Assistants
            Cards.Add(new AssistantCard("TODO", berlin, 2));
            Cards.Add(new AssistantCard("TODO", paris, 2));
            Cards.Add(new AssistantCard("TODO", paris, 2));
            Cards.Add(new AssistantCard("TODO", rome, 2));
            Cards.Add(new AssistantCard("TODO", vienna, 2));

            // Shovels
            Cards.Add(new ShovelCard("TODO", london, 3));
            Cards.Add(new ShovelCard("TODO", london, 3));
            Cards.Add(new ShovelCard("TODO", moscow, 3));
            Cards.Add(new ShovelCard("TODO", moscow, 3));
            Cards.Add(new ShovelCard("TODO", rome, 3));
            Cards.Add(new ShovelCard("TODO", rome, 3));

            // zeppelins
            Cards.Add(new ZeppelinCard("TODO", rome, 1));
            Cards.Add(new ZeppelinCard("TODO", vienna, 1));

            // cars
            Cards.Add(new CarCard("TODO", rome, 1));
            Cards.Add(new CarCard("TODO", moscow, 1));

            // permission
            Cards.Add(new SpecialPermissionCard("TODO", moscow, 3));
            Cards.Add(new SpecialPermissionCard("TODO", london, 3));

            // congress cards
            Cards.Add(new CongressCard("TODO", london, 2));
            Cards.Add(new CongressCard("TODO", moscow, 2));
            Cards.Add(new CongressCard("TODO", moscow, 2));
            Cards.Add(new CongressCard("TODO", paris, 2));
            Cards.Add(new CongressCard("TODO", paris, 2));
            Cards.Add(new CongressCard("TODO", vienna, 2));
            Cards.Add(new CongressCard("TODO", vienna, 2));
            Cards.Add(new CongressCard("TODO", berlin, 2));
            Cards.Add(new CongressCard("TODO", berlin, 2));

            // general knowledge

            Cards.Add(new GeneralKnowledgeCard("TODO", berlin, 6, 2));
            Cards.Add(new GeneralKnowledgeCard("TODO", moscow, 6, 2));
            Cards.Add(new GeneralKnowledgeCard("TODO", london, 6, 2));
            Cards.Add(new GeneralKnowledgeCard("TODO", paris, 6, 2));

            Cards.Add(new GeneralKnowledgeCard("TODO", berlin, 3, 1));
            Cards.Add(new GeneralKnowledgeCard("TODO", rome, 3, 1));
            Cards.Add(new GeneralKnowledgeCard("TODO", vienna, 3, 1));
            Cards.Add(new GeneralKnowledgeCard("TODO", paris, 3, 1));

            // special knowledge mesopotamy
            Cards.Add(new SpecializedKnowledgeCard("TODO", paris, 1, 1, mesopotamia));
            Cards.Add(new SpecializedKnowledgeCard("TODO", moscow, 1, 1, mesopotamia));
            Cards.Add(new SpecializedKnowledgeCard("TODO", moscow, 1, 1, mesopotamia));
            Cards.Add(new SpecializedKnowledgeCard("TODO", rome, 1, 1, mesopotamia));

            Cards.Add(new SpecializedKnowledgeCard("TODO", vienna, 2, 2, mesopotamia));
            Cards.Add(new SpecializedKnowledgeCard("TODO", vienna, 2, 2, mesopotamia));
            Cards.Add(new SpecializedKnowledgeCard("TODO", london, 2, 2, mesopotamia));

            Cards.Add(new SpecializedKnowledgeCard("TODO", moscow, 4, 3, mesopotamia));
            Cards.Add(new SpecializedKnowledgeCard("TODO", london, 4, 3, mesopotamia));

            Cards.Add(new RumorsCard("TODO", berlin, 1, 2, mesopotamia));

            // special knowledge greece
            Cards.Add(new SpecializedKnowledgeCard("TODO", berlin, 1, 1, greece));
            Cards.Add(new SpecializedKnowledgeCard("TODO", vienna, 1, 1, greece));
            Cards.Add(new SpecializedKnowledgeCard("TODO", moscow, 1, 1, greece));
            Cards.Add(new SpecializedKnowledgeCard("TODO", rome, 1, 1, greece));

            Cards.Add(new SpecializedKnowledgeCard("TODO", rome, 2, 2, greece));
            Cards.Add(new SpecializedKnowledgeCard("TODO", rome, 2, 2, greece));
            Cards.Add(new SpecializedKnowledgeCard("TODO", london, 2, 2, greece));

            Cards.Add(new SpecializedKnowledgeCard("TODO", berlin, 4, 3, greece));
            Cards.Add(new SpecializedKnowledgeCard("TODO", london, 4, 3, greece));

            Cards.Add(new RumorsCard("TODO", moscow, 1, 2, greece));

            // special knowledge crete
            Cards.Add(new SpecializedKnowledgeCard("TODO", berlin, 1, 1, crete));
            Cards.Add(new SpecializedKnowledgeCard("TODO", vienna, 1, 1, crete));
            Cards.Add(new SpecializedKnowledgeCard("TODO", berlin, 1, 1, crete));
            Cards.Add(new SpecializedKnowledgeCard("TODO", paris, 1, 1, crete));

            Cards.Add(new SpecializedKnowledgeCard("TODO", rome, 2, 2, crete));
            Cards.Add(new SpecializedKnowledgeCard("TODO", paris, 2, 2, crete));
            Cards.Add(new SpecializedKnowledgeCard("TODO", vienna, 2, 2, crete));

            Cards.Add(new SpecializedKnowledgeCard("TODO", moscow, 4, 3, crete));
            Cards.Add(new SpecializedKnowledgeCard("TODO", moscow, 4, 3, crete));

            Cards.Add(new RumorsCard("TODO", paris, 1, 2, crete));

            // special knowledge egypt
            Cards.Add(new SpecializedKnowledgeCard("TODO", rome, 1, 1, egypt));
            Cards.Add(new SpecializedKnowledgeCard("TODO", moscow, 1, 1, egypt));
            Cards.Add(new SpecializedKnowledgeCard("TODO", paris, 1, 1, egypt));
            Cards.Add(new SpecializedKnowledgeCard("TODO", paris, 1, 1, egypt));

            Cards.Add(new SpecializedKnowledgeCard("TODO", berlin, 2, 2, egypt));
            Cards.Add(new SpecializedKnowledgeCard("TODO", berlin, 2, 2, egypt));
            Cards.Add(new SpecializedKnowledgeCard("TODO", london, 2, 2, egypt));

            Cards.Add(new SpecializedKnowledgeCard("TODO", moscow, 4, 3, egypt));
            Cards.Add(new SpecializedKnowledgeCard("TODO", london, 4, 3, egypt));

            Cards.Add(new RumorsCard("TODO", rome, 1, 2, egypt));

            // special knowledge palestine
            Cards.Add(new SpecializedKnowledgeCard("TODO", rome, 1, 1, palestine));
            Cards.Add(new SpecializedKnowledgeCard("TODO", vienna, 1, 1, palestine));
            Cards.Add(new SpecializedKnowledgeCard("TODO", vienna, 1, 1, palestine));
            Cards.Add(new SpecializedKnowledgeCard("TODO", vienna, 1, 1, palestine));

            Cards.Add(new SpecializedKnowledgeCard("TODO", berlin, 2, 2, palestine));
            Cards.Add(new SpecializedKnowledgeCard("TODO", paris, 2, 2, palestine));
            Cards.Add(new SpecializedKnowledgeCard("TODO", london, 2, 2, palestine));

            Cards.Add(new SpecializedKnowledgeCard("TODO", paris, 4, 3, palestine));
            Cards.Add(new SpecializedKnowledgeCard("TODO", london, 4, 3, palestine));

            Cards.Add(new RumorsCard("TODO", vienna, 1, 2, palestine));
        }


        /// <summary>
        /// Generic method to generate all subsets of a set
        /// 
        /// Courtesy of Stack overflow user Servy:
        /// https://stackoverflow.com/a/14820995/13426274
        /// </summary>
        /// <typeparam name="T">type of item in the set</typeparam>
        /// <param name="source">source set</param>
        /// <returns>iterator of all subsets</returns>
        public static IEnumerable<IEnumerable<T>> Subsets<T>(IEnumerable<T> source)
        {
            List<T> list = source.ToList();
            int length = list.Count;
            int max = (int)Math.Pow(2, list.Count);

            for (int count = 0; count < max; count++)
            {
                List<T> subset = new List<T>();
                uint rs = 0;
                while (rs < length)
                {
                    if ((count & (1u << (int)rs)) > 0)
                    {
                        subset.Add(list[(int)rs]);
                    }
                    rs++;
                }
                yield return subset;
            }
        }

    }

    /// <summary>
    /// Used to serialize the current settings, holds same data as <see cref="GameSettings"/>
    /// </summary>
    [Serializable]
    public class GameSettingsSerializable
    {
        public List<IPlace> Places { get; set; }
        public IPlace StartingPlace { get; set; }
        public int[,] Distances { get; set; }
        public Dictionary<int, Dictionary<int, int>> TimeWheel { get; set; }
        public List<ICard> Cards { get; set; }

        public GameSettingsSerializable()
        {
            Places = GameSettings.Places;
            StartingPlace = GameSettings.StartingPlace;
            Distances = GameSettings.Distances;
            TimeWheel = GameSettings.TimeWheel;
            Cards = GameSettings.Cards;
        }
    }
}