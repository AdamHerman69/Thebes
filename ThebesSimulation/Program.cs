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
            //for (int i = 0; i < 2; i++)
            //{
            //    individuals[i] = new Individual(new BetterEvolutionAI(null, null));
            //}
            //for (int i = 2; i < 4; i++)
            //{
            //    individuals[i] = new Individual(new EvolutionAI(null, null));
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




            Population population;
            // number of parents
            for (int i = 2; i < 5; i += 2)
            {
                // survivor ratio
                for (double j = 0.1; j <= 0.4; j += 0.1)
                {
                    // mutation probability
                    for (double k = 0.01; k < 0.2; k += 0.06)
                    {
                        // mutation range
                        for (double l = 0.05; l < 0.5; l *= 2)
                        {
                            population = new Population(100);
                            population.Evolve(4, 150, 5, j, i, k, l, 0);
                        }
                    }
                }
            }

            //Population population = new Population(100);
            //population.Evolve(4, 10000, 10, 0.2, 2, 0.04, 0.1, 0);
            //Console.ReadLine();

            //Individual individual = new Individual(new BetterEvolutionAI(null, null));
            //individual.ai.NormalizeValues(-1, 1);

            //foreach (var value in individual.ai.weights)
            //{
            //    Console.WriteLine(value.ToString() + ',');
            //}
            //Console.ReadLine();



        }
    }


}
