using System;
using System.Collections.Generic;

public class SatSolver
{
    private List<List<int>> formula;  // SAT formula qe ka mu zgjedh
    private int numVars;              // numri i variablave ne formul
    private int maxIterations;        // iteracionet maksimale qe mu perserit GSAT algoritmi
    private Random rand;              

    public SatSolver(List<List<int>> formula, int numVars, int maxIterations)
    {
        this.formula = formula;
        this.numVars = numVars;
        this.maxIterations = maxIterations;
        this.rand = new Random();
    }

    public List<bool> Solve()
    {
        // Initialize a random truth assignment
        List<bool> assignment = new List<bool>();
      

        for (int iter = 0; iter < maxIterations; iter++)
        {
            for (int i = 0; i < numVars; i++)
            {
                assignment.Add(rand.Next(2) == 1);
            }
            // Evaluate the formula with the current assignment
            int numSatisfied = CountSatisfiedClauses(assignment);
            if (numSatisfied == formula.Count)
            {
                return assignment;
            }

            int bestVar = -1;
            int bestDelta = -1;
            for (int i = 0; i < numVars; i++)
            {
                if (!assignment[i])
                {
                    // Try flipping
                    int delta = CountSatisfiedClauses(assignment, i, true) - numSatisfied;
                    if (delta > bestDelta)
                    {
                        bestVar = i;
                        bestDelta = delta;
                    }
                }
                else
                {
                    // Try flipping variable i to false
                    int delta = CountSatisfiedClauses(assignment, i, false) - numSatisfied;
                    if (delta > bestDelta)
                    {
                        bestVar = i;
                        bestDelta = delta;
                    }
                }
            }

            if (bestVar == -1)
            {
                return null;
            }

            // Flip the truth value of the best variable
            assignment[bestVar] = !assignment[bestVar];
        }

        return null;
    }

    private int CountSatisfiedClauses(List<bool> assignment, int flippedVar = -1, bool flippedValue = false)
    {
        int numSatisfied = 0;
        foreach (List<int> clause in formula)
        {
            bool clauseSatisfied = false;
            foreach (int var in clause)
            {
                bool varValue = assignment[Math.Abs(var) - 1];
                //nese nje numer ne clause eshte negativ, atehere vlera korresponduese ne assignmet duhet te jete e kunderta,p.sh -3==not true
                if (var < 0) varValue = !varValue;
                if (var == flippedVar)
                {
                    varValue = flippedValue;
                }
                if (varValue)
                {
                    clauseSatisfied = true;
                    break;
                }
            }
            if (clauseSatisfied)
            {
                numSatisfied++;
            }
        }
        return numSatisfied;
    }
}
