using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase
{
    public int playerID { get; set; }

    public int TotalMoney { get; set; }

    public HQShip HQ;
    public List<Ship_Base> Fleet { get; set; }

    public List<Mineable> MineablesOwned { get; set; }

    public void GenerateIncomePerTurn()
    {
        foreach (Mineable m in MineablesOwned)
        {
            TotalMoney += m.GenerateResourcesPerTurn();
        }
    }

    public int getIncomePerTurn()
    {
        int tempIncomePerTurn = 0;

        foreach (Mineable m in MineablesOwned)
        {
            if (m != null)
            {
                tempIncomePerTurn += m.ResourcePerTurn;
            }
        }

        return tempIncomePerTurn;
    }
}
