using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

public class MiningShip : Ship_Base
{
    public MiningShip(string n, string type, int h, int a, int d, int actions, int hX, int hZ, GameObject sObject, GameObject selected)
    {
        name = n;
        shipType = type;
        health = h;
        attack = a;
        defence = d;

        numberOfActionsPerTurn = actions;
        actionsLeft = numberOfActionsPerTurn;

        hexX = hX;
        hexZ = hZ;

        shipObject = sObject;
        selectedObject = selected;
    }
}
