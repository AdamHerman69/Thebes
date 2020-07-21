using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThebesCore;
using ThebesAI;
using System.IO;
using System.Web.Script.Serialization;
using System.Runtime.CompilerServices;

namespace ThebesSimulation
{
    class Program
    {
        static void Main(string[] args)
        {
            //GameSettings.LoadFromFile(@"thebes_config.thc");

            //Individual[] individuals = new Individual[4];
            //for (int i = 0; i < individuals.Length; i++)
            //{
            //    individuals[i] = new Individual(i);
            //}

            //int counter = 10;
            //for (int i = 0; i < 1000; i++)
            //{

            //    if (i % 100 == 0)
            //    {
            //        Console.WriteLine(counter--);
            //    }
            //    Population.PlayMatch(individuals);
            //}
            //for (int i = 0; i < individuals.Length; i++)
            //{
            //    Console.WriteLine($"Average score for {i}: {individuals[i].AverageScore()}");
            //}
            //Console.ReadLine();

            Population population = new Population(100);
            population.Evolve(2, 10000, 7);
            Console.ReadLine();
        }
    }


}
