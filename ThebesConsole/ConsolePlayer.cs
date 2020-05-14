using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThebesCore;

namespace ThebesConsole
{
    public interface IConsolePlayer : IPlayer
    {
        void TakeActionWrapper(List<ICard> availableCards, List<IExhibitionCard> availableExhibitions);
    }

    [Serializable]
    public class ConsolePlayer : Player
    {
        public List<IPlace> Places { get; set; }
        public ConsolePlayer(string name, List<IDigSite> digSites, IPlace startingPlace, List<IPlace> places, Action<string> errorDialog, System.Action changeDisplayCards, Action<ICard> takeCard, Action<ICard> discardCard, Action<IExhibitionCard> executeExhibition, Func<ITime, int> playersOnWeek) : base(name, digSites, startingPlace, errorDialog, changeDisplayCards, takeCard, discardCard, executeExhibition, playersOnWeek)
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
                    if (!(place is IDigSite))
                    {
                        Console.WriteLine(place + "is not a digsite");
                        break;
                    }
                    if (Permissions[(IDigSite)place])
                    {
                        Console.WriteLine("you already have that permission");
                        break;
                    }
                    if (!UseSpecialPermission((IDigSite)place))
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
                    if (!(place is IDigSite))
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
                    List<ICard> singleUsedCards = GetUsableSingleUseCards((IDigSite)place);
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
                    if ((dugTokens = Dig((IDigSite)place, weeks, cardsToUse)) != null)
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
}
