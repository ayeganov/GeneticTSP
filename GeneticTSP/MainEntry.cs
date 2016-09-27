using System;
using System.Windows.Forms;

namespace GeneticTSP
{
    static class MainEntry
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new TSPForm());
/*
            const int NUM_CITIES = 20;
            const int POP_SIZE = 40;

            TSPMap map = new TSPMap(1000, 1000, NUM_CITIES);
            TSPGenAlg gen_alg = new TSPGenAlg(NUM_CITIES, POP_SIZE, map);
            while (!gen_alg.Done)
            {
                Console.WriteLine($"Generation {gen_alg.GenerationNumber}");
                var start = DateTime.Now.Millisecond;
                gen_alg.Epoch();
                var end = DateTime.Now.Millisecond;
                Console.WriteLine($"Took {end - start} ms");
            }
            Console.WriteLine($"And the winner is {gen_alg.FittestGenome.Data}");
*/
        }
    }
}