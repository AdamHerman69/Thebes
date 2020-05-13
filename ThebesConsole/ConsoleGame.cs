using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThebesCore;

namespace ThebesConsole
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class ConsoleGame : Game, IGame
    {
        public ConsoleGame(int playerCount) : base(playerCount) { }

        public ICard[] DisplayedCards => throw new NotImplementedException();

        public ICard[] DisplayedExhibitions => throw new NotImplementedException();

        public void Play()
        {
            while (!AreAllPlayersDone())
            {
                Players.Sort();
                PrintState();
                ((ConsolePlayer)Players[0]).TakeActionWrapper(new List<ICard>(AvailableCards.AvailableCards), ActiveExhibitions.Exhibitions.ToList<IExhibitionCard>().Where(x => x != null).ToList());
                ResetCardChangeInfos();
            }

            Console.WriteLine("---- GAME ENDED ----");
        }

        private void PrintState()
        {
            Console.WriteLine("--------------------------------------------------------------------------------------------");

            // Player stats
            foreach (IPlayer player in Players)
            {
                Console.Write(player.ToString() + "\n\n");
            }

            // Cards Available
            Console.WriteLine("Available Cards:");
            foreach (ICard card in AvailableCards.AvailableCards)
            {
                Console.WriteLine(card);
            }
            Console.WriteLine();
            foreach (IExhibitionCard exhibition in this.ActiveExhibitions.Exhibitions)
            {
                Console.WriteLine(exhibition);
            }

            Console.WriteLine($"\n{Players[0].Name}'s turn");
        }
    }
}
