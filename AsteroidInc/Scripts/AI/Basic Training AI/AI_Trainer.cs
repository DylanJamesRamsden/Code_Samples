using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class AI_Trainer
{
    public Hexagon[,] Map { get; set; }
    public List<Mineable> MineablesInWorld { get; set; }

    public List<Ship_Base> p1Fleet { get; set; }
    public int p1Resources { get; set; }
    public int p1ResourcesPerTurn { get; set; }

    public List<Ship_Base> p2Fleet { get; set; }
    public int p2Resources { get; set; }
    public int p2ResourcesPerTurn { get; set; }

    public int turnCounter { get; set; }


    public AI_Trainer()
    {

    }

    //double MilitaryEvaluationFunction(int player)
    //{
    //    if (player == 1)
    //    {
    //        double tempEF = (p1Fleet.Count * 0.1) - (p2Fleet.Count * 0.1);
    //        return tempEF;
    //    }
    //    else
    //    {
    //        double tempEF = (p2Fleet.Count * 0.1) - (p1Fleet.Count * 0.1);
    //        return tempEF;
    //    }
    //}

    double ResourceEvaluationFunction(int player)
    {
        if (player == 1)
        {
            double tempEF = (p1Resources + (p1ResourcesPerTurn * (50 - turnCounter))) - (p2Resources + (p2ResourcesPerTurn * (50 - turnCounter)));
            return tempEF;
        }
        else
        {
            double tempEF = (p2Resources + (p2ResourcesPerTurn * (50 - turnCounter))) - (p1Resources + (p1ResourcesPerTurn * (50 - turnCounter)));
            return tempEF;
        }
    }
}
