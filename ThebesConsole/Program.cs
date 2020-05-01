using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThebesCore;
using System.IO;
using System.Configuration;

namespace ThebesConsole
{
    class Program
    {
        static void Main(string[] args)
        {



            //GameSettings.Initialize();
            GameSettings.LoadFromFile(@"C:\Users\admhe\source\repos\Thebes\ThebesConsole\bin\Debug\thebes_config_auto_test.txt");
            GameSettings.SaveToFile("thebes_config_auto.txt");

            //ConsoleGame game = new ConsoleGame(2);

            //IPlayer adam = new ConsolePlayer(
            //    "Adam",
            //    GameSettings.Places.OfType<IDigSiteSimpleView>().ToList(),
            //    GameSettings.StartingPlace,
            //    GameSettings.Places,
            //    NotEnoughTimeDialog,
            //    game.AvailableCards.ChangeDisplayedCards,
            //    game.AvailableCards.GiveCard,
            //    game.Deck.Discard,
            //    game.ActiveExhibitions.GiveExhibition,
            //    game.PlayersOnWeek);
            //IPlayer vitek = new ConsolePlayer(
            //    "Vitek",
            //    GameSettings.Places.OfType<IDigSiteSimpleView>().ToList(),
            //    GameSettings.StartingPlace,
            //    GameSettings.Places,
            //    NotEnoughTimeDialog,
            //    game.AvailableCards.ChangeDisplayedCards,
            //    game.AvailableCards.GiveCard,
            //    game.Deck.Discard,
            //    game.ActiveExhibitions.GiveExhibition,
            //    game.PlayersOnWeek);

            //game.Players = new List<IPlayer>() { adam, vitek };

            //game.Play();
        }

        public static void NotEnoughTimeDialog()
        {
            Console.WriteLine("You don't have enough time for that action");
        }
    }
}
