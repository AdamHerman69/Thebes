using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThebesCore;
using ThebesAI;
using System.IO;
using System.Web.Script.Serialization;

namespace ThebesSimulation
{
    class Program
    {
        static void Main(string[] args)
        {
            //using (var sr = new StreamReader($"gen_1060.txt", true))
            //{
            //    string json = sr.ReadToEnd();
            //    List<Individual> individuals = new JavaScriptSerializer().Deserialize<List<Individual>>(json);

            //    Console.ReadLine();
            //}

            Population population = new Population();
            population.Evolve();
            Console.ReadLine();
        }
    }


}
