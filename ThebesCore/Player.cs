using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ThebesCore
{
    public interface IPlayerData
    {
        string Name { get; }
        ITime Time { get; }
        int Points { get; set; }

        Dictionary<IDigSiteSimpleView, bool> Permissions { get; set; }
        Dictionary<IDigSiteSimpleView, int> SpecializedKnowledge { get; set; }
        Dictionary<IDigSiteSimpleView, int> SingleUseKnowledge { get; set; }

        int GeneralKnowledge { get; set; }
        int Shovels { get; set; }
        int Assistants { get; set; }
        int SpecialPermissions { get; set; }
        int Congresses { get; set; }
        int Cars { get; set; }
        int Zeppelins { get; set; }
        int GetAssistantKnowledge();
        List<ICard> GetUsableSingleUseCards(IDigSiteSimpleView digSite);
        void GetDigStats(IDigSiteSimpleView digSite, List<ICard> singleUseCards, out int knowledge, out int tokenBonus);
        IPlace CurrentPlace { get; set; }
        Dictionary<IDigSiteSimpleView, List<IToken>> Tokens { get; }
    }

    public interface IPlayer : IPlayerData, IComparable<IPlayer>
    {
        List<IToken> Dig(IDigSiteFullView digSite, int weeks, List<ICard> singleUseCards);
        void ResetCardChnageInfo();
        void MoveAndTakeCard(ICard card);
        void EndYear();
        void MoveAndChangeDisplayCards(ICardChangePlace cardChangePlace);
        bool UseZeppelin();
    }

    [Serializable]
    public class Player : IComparable<IPlayer>, IPlayerData, IPlayer
    {
        Action<string> errorDialog;
        Action changeDisplayCards;
        Action<ICard> takeCard;
        Action<ICard> discardCard;
        Action<IExhibitionCard> executeExhibition;
        public string Name { get; private set; }
        public ITime Time { get; set; }
        public Dictionary<IDigSiteSimpleView, bool> Permissions { get; set; }
        public Dictionary<IDigSiteSimpleView, int> SpecializedKnowledge { get; set; }
        public Dictionary<IDigSiteSimpleView, int> SingleUseKnowledge { get; set; }
        public int GeneralKnowledge { get; set; }
        public int Zeppelins { get; set; }

        private bool useZappelin;
        public int SpecialPermissions { get; set; }
        public int Congresses { get; set; }
        public int Assistants { get; set; }
        public int Shovels { get; set; }
        public int Cars { get; set; }
        public int Points { get; set; }
        public List<ICard> Cards { get; set; }
        public Dictionary<IDigSiteSimpleView, List<IToken>> Tokens { get; set; }
        public IPlace CurrentPlace { get; set; }

        private int CardChangeCost { get; set; }
        protected bool LastRoundChange { get; set; }

        public override string ToString()
        {
            // general
            string str = $"{Name} is at {CurrentPlace} time: {Time} points: {Points}\n";

            // permissions
            str += "PERMISSIONS: ";
            foreach (KeyValuePair<IDigSiteSimpleView, bool> kvp in Permissions)
            {
                if (kvp.Value)
                {
                    str += kvp.Key + " ";
                }
            }
            str += "\n";

            // knowledge
            str += "KNOWLEDGE: ";
            foreach (KeyValuePair<IDigSiteSimpleView, int> kvp in SpecializedKnowledge)
            {
                str += $"{kvp.Key} {kvp.Value}({SingleUseKnowledge[kvp.Key]}) ";
            }

            // other cards
            str += $"OTHER: genknow: {GeneralKnowledge} zep: {Zeppelins} spPrmsns: {SpecialPermissions} cngrs: {Congresses} ass: {Assistants} shovels: {Shovels} car: {Cars}";

            // tokens
            str += "TOKENS: \n";
            foreach (KeyValuePair<IDigSiteSimpleView, List<IToken>> kvp in Tokens)
            {
                if (kvp.Value.Count > 0)
                {
                    str += kvp.Key.ToString() + ": ";
                    foreach (Token token in kvp.Value)
                    {
                        str += token.ToString() + ", ";
                    }
                    str += "\n";
                }
            }

            return str;
        }

        public Player() { }
        public Player(string name, List<IDigSiteSimpleView> digSites, IPlace startingPlace, Action<string> errorDialog, Action changeDisplayCards, Action<ICard> takeCard, Action<ICard> discardCard, Action<IExhibitionCard> executeExhibition, Func<ITime, int> playersOnWeek)
        {
            this.Name = name;
            this.CurrentPlace = startingPlace;

            this.errorDialog = errorDialog;
            this.changeDisplayCards = changeDisplayCards;
            this.takeCard = takeCard;
            this.discardCard = discardCard;
            this.executeExhibition = executeExhibition;

            Cards = new List<ICard>();
            Tokens = new Dictionary<IDigSiteSimpleView, List<IToken>>();
            Permissions = new Dictionary<IDigSiteSimpleView, bool>();
            SpecializedKnowledge = new Dictionary<IDigSiteSimpleView, int>();
            SingleUseKnowledge = new Dictionary<IDigSiteSimpleView, int>();

            this.Time = new Time(playersOnWeek, ResetPermissions);

            // add all valid permissions
            Permissions = new Dictionary<IDigSiteSimpleView, bool>();
            foreach (IDigSiteSimpleView digSite in digSites)
            {
                Permissions.Add(digSite, true);
            }

            // add specialized knowledge values (each player starts with 0)
            SpecializedKnowledge = new Dictionary<IDigSiteSimpleView, int>();
            foreach (IDigSiteSimpleView digSite in digSites)
            {
                SpecializedKnowledge.Add(digSite, 0);
            }

            // add single use knowledge values (each player starts with 0)
            SingleUseKnowledge = new Dictionary<IDigSiteSimpleView, int>();
            foreach (IDigSiteSimpleView digSite in digSites)
            {
                SingleUseKnowledge.Add(digSite, 0);
            }

            // create token bags for each dig site
            Tokens = new Dictionary<IDigSiteSimpleView, List<IToken>>();
            foreach (IDigSiteSimpleView digSite in digSites)
            {
                Tokens.Add(digSite, new List<IToken>());
            }

            CardChangeCost = CardDisplay.timeToChangeCards;

            // all other fields are 0
        }

        int IComparable<IPlayer>.CompareTo(IPlayer other)
        {
            return Time.CompareTo(other.Time);
        }

        public int GetAssistantKnowledge()
        {
            if (Assistants >= 3)
            {
                return 2;
            }
            else if (Assistants >= 2)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public int GetShovelBonus()
        {
            if (Shovels >= 3)
            {
                return 2;
            }
            else if (Shovels >= 2)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        private void AddSingleUseCardsStats(IDigSiteSimpleView digSite, List<ICard> singleUseCards, ref int knowledge, ref int tokenBonus)
        {
            if (singleUseCards == null)
            {
                return;
            }

            foreach (ICard card in singleUseCards)
            {
                if (card is IRumorsCard && ((IRumorsCard)card).digSite == digSite)
                {
                    knowledge += ((IRumorsCard)card).KnowledgeAmount;
                }
                if (card is IAssistantCard)
                {
                    knowledge += 1;
                }
                if (card is IShovelCard)
                {
                    tokenBonus += 1;
                }
            }

            return;
        }

        public void GetDigStats(IDigSiteSimpleView digSite, List<ICard> singleUseCards, out int knowledge, out int tokenBonus)
        {
            knowledge = 0;
            tokenBonus = 0;

            tokenBonus += GetShovelBonus();

            knowledge += SpecializedKnowledge[digSite];
            knowledge += GeneralKnowledge;
            knowledge += GetAssistantKnowledge();

            AddSingleUseCardsStats(digSite, singleUseCards, ref knowledge, ref tokenBonus);

            return;
        }

        /// <summary>
        /// Player will use zeppelin for his next move
        /// </summary>
        /// <returns>true if player has zeppelin available</returns>
        public bool UseZeppelin()
        {
            if (Zeppelins > 0)
            {
                useZappelin = true;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Use special permission card to renew one of his permissions
        /// </summary>
        /// <param name="digSite">digsite to renew permission at</param>
        /// <returns>false if player doesnt have any special permission card or if he already has permission for the digsite</returns>
        public bool UseSpecialPermission(IDigSiteSimpleView digSite)
        {
            if (SpecialPermissions > 0 && !Permissions[digSite])
            {
                Permissions[digSite] = true;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Moves a player to the desired place by spending weeks.
        /// </summary>
        /// <param name="destination"></param>
        public void MoveTo(IPlace destination)
        {
            if (useZappelin)
            {
                if (this.Zeppelins > 0)
                {
                    this.Zeppelins--;
                    useZappelin = false;
                }
                else
                {
                    throw new InvalidOperationException("Player doesn't have a zeppelin");
                }
            }
            else
            {
                Time.SpendWeeks(GameSettings.GetDistance(CurrentPlace, destination));
            }

            CurrentPlace = destination;
        }

        private void TakeCard(ICard card)
        {
            Time.SpendWeeks(card.Weeks);
            if (card is IExhibitionCard)
            {
                executeExhibition((IExhibitionCard)card);
            }
            else
            {
                takeCard(card);
            }

            Cards.Add(card);
            card.UpdateStats(this);
        }

        private void UpdateStats()
        {
            // zero all necessary fields 
            SingleUseKnowledge.Keys.ToList().ForEach(x => SingleUseKnowledge[x] = 0);
            this.Zeppelins = 0;
            this.Shovels = 0;
            this.Assistants = 0;

            foreach (ICard card in this.Cards.Where(c => c is IRumorsCard || c is IAssistantCard || c is IShovelCard || c is IZeppelinCard))
            {
                card.UpdateStats(this);
            }
        }

        public List<ICard> GetUsableSingleUseCards(IDigSiteSimpleView digSite)
        {
            List<ICard> singleUseCards = new List<ICard>();
            foreach (ICard card in Cards)
            {
                if ((card is IRumorsCard && ((IRumorsCard)card).digSite == digSite) ||
                    (Assistants == 1 && card is IAssistantCard) ||
                    (Shovels == 1 && card is IShovelCard)
                    )
                {
                    singleUseCards.Add(card);
                }
            }
            return singleUseCards;
        }

        /// <summary>
        /// Moves a player to the dig site and proceeds to dig according to the amount of knowledge and weeks spend.
        /// </summary>
        /// <param name="digSite">Where to dig</param>
        /// <param name="weeks">How long to dig</param>
        /// <param name="singleUseCards">Single use cards to use. NOT WORKING ATM</param>
        public List<IToken> Dig(IDigSiteFullView digSite, int weeks, List<ICard> singleUseCards)
        {
            // TODO different dialog for invalid permission and no specialized knowledge
            if (Time.RemainingWeeks() < weeks + GameSettings.GetDistance(CurrentPlace, digSite))
            {
                errorDialog("You don't have enough time!");
                return null;
            }
            if (!Permissions[digSite])
            {
                errorDialog("You don't have a valid permisssion!");
                return null;
            }
            if (SpecializedKnowledge[digSite] < 1)
            {
                errorDialog("You need at least one specialized knowledge!");
                return null;
            }


            MoveTo(digSite);
            Permissions[digSite] = false;
            Time.SpendWeeks(weeks);

            // get token amount
            GetDigStats(digSite, singleUseCards, out int knowledge, out int tokenBonus);
            int tokenAmount = GameSettings.DugTokenCount(knowledge, weeks) + tokenBonus;

            // draw tokens and give them to player
            List<IToken> dugTokens = digSite.DrawTokens(tokenAmount);
            foreach (IToken token in dugTokens)
            {
                token.UpdateStats(this);
                if (!(token is IDirtToken))
                {
                    Tokens[digSite].Add(token);
                }
            }

            // TODO change player stats
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

            return dugTokens;
        }

        /// <summary>
        /// Moves a player to the destination of the desired card and takes it. Spending weeks.
        /// </summary>
        /// <param name="card">Card to take</param>
        public void MoveAndTakeCard(ICard card)
        {
            if (card is IExhibitionCard && !((IExhibitionCard)card).CheckRequiredArtifacts(Tokens))
            {
                errorDialog("You don't have the required artifacts!"); // TODO can't execute exhibition dialog
                return;
            }
            if (Time.RemainingWeeks() < card.Weeks + GameSettings.GetDistance(CurrentPlace, card.Place))
            {
                errorDialog("You don't have enought time for that!");
                return;
            }

            MoveTo(card.Place);
            TakeCard(card);
        }

        /// <summary>
        /// Ends the year for a player by waiting for another. Used when he doesn't want to do anything.
        /// </summary>
        public void EndYear()
        {
            Time.EndYear();
        }

        /// <summary>
        /// Moves player to the desired card changing place and changes the cards
        /// </summary>
        /// <param name="cardChangePlace">Place where to change cards</param>
        public void MoveAndChangeDisplayCards(ICardChangePlace cardChangePlace)
        {
            if (Time.RemainingWeeks() < GameSettings.GetDistance(CurrentPlace, cardChangePlace) + CardDisplay.timeToChangeCards)
            {
                errorDialog("You don't have enough time to change the display cards");
                return;
            }



            MoveTo(cardChangePlace);
            Time.SpendWeeks(CardChangeCost);
            LastRoundChange = true;
            CardChangeCost++;

            changeDisplayCards();
        }

        public void ResetCardChnageInfo()
        {
            CardChangeCost = CardDisplay.timeToChangeCards;
            LastRoundChange = false;
        }

        /// <summary>
        /// Resets the players permissions (Call on new year).
        /// </summary>
        public void ResetPermissions()
        {
            foreach (var digsite in Permissions.Keys.ToList())
            {
                Permissions[digsite] = true;
            }
        }
    }

    public interface IConsolePlayer : IPlayer
    {
        void TakeActionWrapper(List<ICard> availableCards, List<IExhibitionCard> availableExhibitions);
    }

    [Serializable]
    public class ConsolePlayer : Player
    {
        public List<IPlace> Places { get; set; }
        public ConsolePlayer(string name, List<IDigSiteSimpleView> digSites, IPlace startingPlace, List<IPlace> places, Action<string> errorDialog, Action changeDisplayCards, Action<ICard> takeCard, Action<ICard> discardCard, Action<IExhibitionCard> executeExhibition, Func<ITime, int> playersOnWeek) : base(name, digSites, startingPlace, errorDialog, changeDisplayCards, takeCard, discardCard, executeExhibition, playersOnWeek)
        {
            Places = places;
        }

        public void TakeActionWrapper(List<ICard> availableCards, List<IExhibitionCard> availableExhibitions)
        {
            LastRoundChange = false;
            TakeAction(availableCards, availableExhibitions);
            if (!LastRoundChange)
            {
                ResetCardChnageInfo();
            }
        }

        protected void TakeAction(List<ICard> availableCards, List<IExhibitionCard> availableExhibitions)
        {
            string[] command = Console.ReadLine().Split();
            IPlace place;
            int cardNumber;
            switch (command[0])
            {
                case "help":
                    string helpstring =
                        @"Commands:
                        help: displays this help
                        card [index]: take specified card
                        exhibition [index]: execute specified exhibition
                        changecards [cardChangePlace]: travel to specified card change place and cange display cards
                        endyear: wait till the year ends
                        usezeppelin: use zeppelin for the next travel
                        usepermission [DigSite]: use special permission card for given digsite
                        dig [DigSite] [weeks]: dig at [DigSite] for [weeks]

                        all commands include the travel required, use 1-based indexing and full dig site names with capitalized first letter
                        ";
                    Console.Write(helpstring);
                    break;

                case "card":
                    if (command.Length != 2)
                    {
                        Console.WriteLine("invalid command format: card [Index]");
                        break;
                    }
                    if (!int.TryParse(command[1], out cardNumber))
                    {
                        Console.WriteLine("[Index] is not an integer");
                        break;
                    }
                    if (cardNumber < 1 || cardNumber > availableCards.Count)
                    {
                        Console.WriteLine("[Index] is out of bounds");
                        break;
                    }


                    MoveAndTakeCard(availableCards[cardNumber - 1]);
                    break;

                case "exhibition":
                    if (command.Length != 2)
                    {
                        Console.WriteLine("invalid command format: exhibition [Index]");
                        break;
                    }
                    if (!int.TryParse(command[1], out cardNumber))
                    {
                        Console.WriteLine("[Index] is not an integer");
                        break;
                    }
                    if (cardNumber < 1 || cardNumber > availableExhibitions.Count)
                    {
                        Console.WriteLine("[Index] is out of bounds");
                        break;
                    }

                    MoveAndTakeCard(availableExhibitions[cardNumber - 1]);
                    break;

                case "changecards":
                    if (command.Length != 2)
                    {
                        Console.WriteLine("invalid command format: changecards [cardChangePlace]");
                        break;
                    }
                    if ((place = GameSettings.getPlaceByName(command[1])) == null)
                    {
                        Console.WriteLine("place with that name doesn't exist");
                        break;
                    }
                    if (!(place is ICardChangePlace))
                    {
                        Console.WriteLine("you can't change cards at " + place);
                        break;
                    }

                    MoveAndChangeDisplayCards((ICardChangePlace)place);
                    break;

                case "endyear":
                    EndYear();
                    break;

                case "usezeppelin":
                    if (!UseZeppelin())
                    {
                        Console.WriteLine("you don't have any zeppelins");
                    }
                    break;

                case "usepermission":
                    if (command.Length != 2)
                    {
                        Console.WriteLine("invalid command format: usepermission [digSite]");
                        break;
                    }
                    if ((place = GameSettings.getPlaceByName(command[1])) == null)
                    {
                        Console.WriteLine("place with that name doesn't exist");
                        break;
                    }
                    if (!(place is IDigSiteSimpleView))
                    {
                        Console.WriteLine(place + "is not a digsite");
                        break;
                    }
                    if (Permissions[(IDigSiteSimpleView)place])
                    {
                        Console.WriteLine("you already have that permission");
                        break;
                    }
                    if (!UseSpecialPermission((IDigSiteSimpleView)place))
                    {
                        Console.WriteLine("You don't have any special permission card");
                        break;
                    }
                    break;

                case "dig":
                    if (command.Length != 3)
                    {
                        Console.WriteLine("invalid command format: usepermission [digSite]");
                        break;
                    }
                    if ((place = GameSettings.getPlaceByName(command[1])) == null)
                    {
                        Console.WriteLine("place with that name doesn't exist");
                        break;
                    }
                    if (!(place is IDigSiteFullView))
                    {
                        Console.WriteLine(place + "is not a digsite");
                        break;
                    }
                    int weeks;
                    if (!int.TryParse(command[2], out weeks))
                    {
                        Console.WriteLine("[weeks] is not an integer");
                        break;
                    }
                    List<ICard> singleUsedCards = GetUsableSingleUseCards((IDigSiteSimpleView)place);
                    List<ICard> cardsToUse = new List<ICard>();
                    if (singleUsedCards.Count > 0)
                    {
                        bool validResponse = false;
                        while (!validResponse)
                        {
                            Console.WriteLine("You have these single use cards available. Which do you want to use? (indexes separated by space or 'none')");
                            foreach (ICard card in singleUsedCards)
                            {
                                Console.WriteLine(card);
                            }

                            string[] response = Console.ReadLine().Split();

                            if (response.Length == 1 && response[0].Equals("none"))
                            {
                                validResponse = true;
                            }
                            else
                            {
                                validResponse = true;
                                int cardIndex;
                                foreach (string cardIndexString in response)
                                {
                                    if (int.TryParse(cardIndexString, out cardIndex) && cardIndex > 0 && cardIndex <= singleUsedCards.Count)
                                    {
                                        cardsToUse.Add(singleUsedCards[cardIndex]);
                                    }
                                    else
                                    {
                                        cardsToUse.Clear();
                                        validResponse = false;
                                        Console.WriteLine("Invalid response! Try again.");
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    List<IToken> dugTokens;
                    if ((dugTokens = Dig((IDigSiteFullView)place, weeks, cardsToUse)) != null)
                    {
                        Console.WriteLine("Congratulations! You dug these tokens:");
                        foreach (Token token in dugTokens)
                        {
                            Console.WriteLine(token);
                        }
                    }
                    Console.WriteLine("\nenter something to continue");
                    Console.ReadLine();
                    break;

                default:
                    Console.WriteLine("Unknown command, type 'help' to display options");
                    break;
            }
        }
    }

    public interface IAI
    {
        IAction TakeAction(IGame gameState);
    }

    public interface IAIPlayer : IPlayer
    {
        IAI AI { get; }
    }

    [Serializable]
    public class AIPlayer : Player, IAIPlayer
    {
        public IAI AI { get; private set; }

        public AIPlayer(string name, List<IDigSiteSimpleView> digSites, IPlace startingPlace, List<IPlace> places, Action<string> errorDialog, Action changeDisplayCards, Action<ICard> takeCard, Action<ICard> discardCard, Action<IExhibitionCard> executeExhibition, Func<ITime, int> playersOnWeek) : base(name, digSites, startingPlace, errorDialog, changeDisplayCards, takeCard, discardCard, executeExhibition, playersOnWeek)
        {
        }

        public void Init(IAI ai)
        {
            this.AI = ai;
        }
    }
}