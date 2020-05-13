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

        Dictionary<IDigSite, bool> Permissions { get; set; }
        Dictionary<IDigSite, int> SpecializedKnowledge { get; set; }
        Dictionary<IDigSite, int> SingleUseKnowledge { get; set; }

        int GeneralKnowledge { get; set; }
        int Shovels { get; set; }
        int Assistants { get; set; }
        int SpecialPermissions { get; set; }
        int Congresses { get; set; }
        int Cars { get; set; }
        int Zeppelins { get; set; }
        int GetAssistantKnowledge();
        List<ICard> GetUsableSingleUseCards(IDigSite digSite);
        void GetDigStats(IDigSite digSite, List<ICard> singleUseCards, out int knowledge, out int tokenBonus);
        IPlace CurrentPlace { get; set; }
        Dictionary<IDigSite, List<IToken>> Tokens { get; }
    }

    public interface IPlayer : IPlayerData, IComparable<IPlayer>
    {
        List<IToken> Dig(IDigSite digSite, int weeks, List<ICard> singleUseCards);
        void ResetCardChnageInfo();
        void MoveAndTakeCard(ICard card);
        void EndYear();
        void MoveAndChangeDisplayCards(ICardChangePlace cardChangePlace);
        bool UseZeppelin();
        bool UseSpecialPermission(IDigSite digSite);
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
        public Dictionary<IDigSite, bool> Permissions { get; set; }
        public Dictionary<IDigSite, int> SpecializedKnowledge { get; set; }
        public Dictionary<IDigSite, int> SingleUseKnowledge { get; set; }
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
        public Dictionary<IDigSite, List<IToken>> Tokens { get; set; }
        public IPlace CurrentPlace { get; set; }

        private int CardChangeCost { get; set; }
        protected bool LastRoundChange { get; set; }

        public override string ToString()
        {
            // general
            string str = $"{Name} is at {CurrentPlace} time: {Time} points: {Points}\n";

            // permissions
            str += "PERMISSIONS: ";
            foreach (KeyValuePair<IDigSite, bool> kvp in Permissions)
            {
                if (kvp.Value)
                {
                    str += kvp.Key + " ";
                }
            }
            str += "\n";

            // knowledge
            str += "KNOWLEDGE: ";
            foreach (KeyValuePair<IDigSite, int> kvp in SpecializedKnowledge)
            {
                str += $"{kvp.Key} {kvp.Value}({SingleUseKnowledge[kvp.Key]}) ";
            }

            // other cards
            str += $"OTHER: genknow: {GeneralKnowledge} zep: {Zeppelins} spPrmsns: {SpecialPermissions} cngrs: {Congresses} ass: {Assistants} shovels: {Shovels} car: {Cars}";

            // tokens
            str += "TOKENS: \n";
            foreach (KeyValuePair<IDigSite, List<IToken>> kvp in Tokens)
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
        public Player(string name, List<IDigSite> digSites, IPlace startingPlace, Action<string> errorDialog, Action changeDisplayCards, Action<ICard> takeCard, Action<ICard> discardCard, Action<IExhibitionCard> executeExhibition, Func<ITime, int> playersOnWeek)
        {
            this.Name = name;
            this.CurrentPlace = startingPlace;

            this.errorDialog = errorDialog;
            this.changeDisplayCards = changeDisplayCards;
            this.takeCard = takeCard;
            this.discardCard = discardCard;
            this.executeExhibition = executeExhibition;

            Cards = new List<ICard>();
            Tokens = new Dictionary<IDigSite, List<IToken>>();
            Permissions = new Dictionary<IDigSite, bool>();
            SpecializedKnowledge = new Dictionary<IDigSite, int>();
            SingleUseKnowledge = new Dictionary<IDigSite, int>();

            this.Time = new Time(playersOnWeek, ResetPermissions);

            // add all valid permissions
            Permissions = new Dictionary<IDigSite, bool>();
            foreach (IDigSite digSite in digSites)
            {
                Permissions.Add(digSite, true);
            }

            // add specialized knowledge values (each player starts with 0)
            SpecializedKnowledge = new Dictionary<IDigSite, int>();
            foreach (IDigSite digSite in digSites)
            {
                SpecializedKnowledge.Add(digSite, 0);
            }

            // add single use knowledge values (each player starts with 0)
            SingleUseKnowledge = new Dictionary<IDigSite, int>();
            foreach (IDigSite digSite in digSites)
            {
                SingleUseKnowledge.Add(digSite, 0);
            }

            // create token bags for each dig site
            Tokens = new Dictionary<IDigSite, List<IToken>>();
            foreach (IDigSite digSite in digSites)
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

        /// <summary>
        /// Computes knowledge gained from assistants
        /// </summary>
        /// <returns>amount of knowledge from assistants</returns>
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

        /// <summary>
        /// Computes token bonus from shovels
        /// </summary>
        /// <returns>token bonus</returns>
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

        /// <summary>
        /// Returns knowledge and token bonus from a list of single-use cards.
        /// </summary>
        /// <param name="digSite">dig site to dig at</param>
        /// <param name="singleUseCards">list of cards to use</param>
        /// <param name="knowledge">knowledge gained</param>
        /// <param name="tokenBonus">tokens gained</param>
        private void AddSingleUseCardsStats(IDigSite digSite, List<ICard> singleUseCards, ref int knowledge, ref int tokenBonus)
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

        /// <summary>
        /// Computes total knowledge and token bonus for this player at given digsite.
        /// </summary>
        /// <param name="digSite">dig site to dig at</param>
        /// <param name="singleUseCards">single-use cards to use</param>
        /// <param name="knowledge">total knowledge</param>
        /// <param name="tokenBonus">token bonus</param>
        public void GetDigStats(IDigSite digSite, List<ICard> singleUseCards, out int knowledge, out int tokenBonus)
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
        public bool UseSpecialPermission(IDigSite digSite)
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

        /// <summary>
        /// Is used to update player's stats, whenever he uses a single-use card
        /// </summary>
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

        /// <summary>
        /// Gets single-use cards usable at given dig site
        /// </summary>
        /// <param name="digSite"></param>
        /// <returns>List of usable single-use cards</returns>
        public List<ICard> GetUsableSingleUseCards(IDigSite digSite)
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
        public List<IToken> Dig(IDigSite digSite, int weeks, List<ICard> singleUseCards)
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

        /// <summary>
        /// Called when there has been an action after changing displayed cards so that player doesn't spend bigger price next time (see game rules)
        /// </summary>
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

    public interface IAI
    {
        IAction TakeAction(IGame gameState);
    }

    public interface IAIPlayer : IPlayer
    {
        IAI AI { get; }
    }
}