using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAT_Problem_GSAT
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<List<int>> formula = new List<List<int>> { new List<int> { 1, 2 }, new List<int> { 2, 3, -4 } };
            int numVars = 4;
            int maxIterations = 1000;

            SatSolver solver = new SatSolver(formula, numVars, maxIterations);
            List<bool> solution = solver.Solve();

            if (solution != null)
            {
                Console.WriteLine("SATISFIABLE:");
                for (int i = 0; i < numVars; i++)
                {
                    Console.WriteLine("x{0} = {1}", i + 1, solution[i]);
                }
            }
            else
            {
                Console.WriteLine("UNSATISFIABLE");
            }
            Console.ReadLine();
        }
    }
}
