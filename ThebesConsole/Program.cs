using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThebesCore;
using System.IO;
using System.Configuration;
using Newtonsoft.Json;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace ThebesConsole
{
    class Program
    {
        static void Main(string[] args)
        {



            //GameSettings.Initialize();
            GameSettings.LoadFromFile(@"C:\Users\admhe\source\repos\Thebes\ThebesConsole\bin\Debug\thebes_config_auto.txt");

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream("game_state.bn", FileMode.Open, FileAccess.Read);
            GameState gameState = (GameState)formatter.Deserialize(stream);
            ConsoleGame game = (ConsoleGame)gameState.game;
            GameSettings.LoadSerializedData(gameState.settings);
            Time.Configure(game.Players.Count);


            //ConsoleGame game = (ConsoleGame)JsonConvert.DeserializeObject<ConsoleGame>(File.ReadAllText("game_state.json"), new JsonSerializerSettings
            //{
            //    TypeNameHandling = TypeNameHandling.Auto,
            //    NullValueHandling = NullValueHandling.Ignore,
            //    ContractResolver = new DictionaryAsArrayResolver()
            //});

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

            game.Play();

            //File.WriteAllText("game_state2.json", game.Serialize());

            //IFormatter formatter = new BinaryFormatter();
            //Stream stream = new FileStream("game_state.bn", FileMode.Create, FileAccess.Write);
            //formatter.Serialize(stream, new GameState(game));
            //stream.Close();
        }

        public static void NotEnoughTimeDialog()
        {
            Console.WriteLine("You don't have enough time for that action");
        }
    }
}
