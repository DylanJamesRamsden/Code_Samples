using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

public class Ship_Base
{
    public string name { get; set; }

    public string shipType { get; set; }

    public int health { get; set; }

    public int attack { get; set; }

    public int defence { get; set; }

    public int numberOfActionsPerTurn { get; set; }

    public int actionsLeft { get; set; }

    public int hexX { get; set; }

    public int hexZ { get; set; }

    public GameObject shipObject { get; set; }

    public GameObject selectedObject { get; set; }

    public string getInfo()
    {
        string shipInfo = "";

        shipInfo += "Name: " + name + "\n";
        shipInfo += "Type: " + shipType + "\n";
        shipInfo += "Health: " + health.ToString() + "\n";
        shipInfo += "Attack: " + attack.ToString() + "\n";
        shipInfo += "Defence: " + defence.ToString() + "\n";
        shipInfo += "Actions left: " + actionsLeft.ToString();

        return shipInfo;
    }
}
