using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class SM_Player : PlayerBase
{
    public float EFValue { get; set; }

    public SM_Player(int pID)
    {
        playerID = pID;

        MineablesOwned = new List<Mineable>();
        Fleet = new List<Ship_Base>();
    }

    public double EvaluationFunction(PlayerBase Opponent, double TurnsLeft)
    {
        double dTotalMoney = TotalMoney;
        double dIncomePerTurn = getIncomePerTurn();
        double dOTotalMoney = Opponent.TotalMoney;
        double dOIncomePerTurn = Opponent.getIncomePerTurn();
        double dTurnsLeft = TurnsLeft;

        double tResources = (dTotalMoney / 3000.0) * 0.08;
        double tResourcesPerTurn = ((dIncomePerTurn * dTurnsLeft) / 3000.0) * 0.02;

        double tOResources = (dOTotalMoney / 3000.0) * 0.08;
        double tOResourcesPerTurn = ((dOIncomePerTurn * dTurnsLeft) / 3000.0) * 0.02;

        double tempEF = (tResources + tResourcesPerTurn) - (tOResources + tOResourcesPerTurn);
        return tempEF;
    }
}
