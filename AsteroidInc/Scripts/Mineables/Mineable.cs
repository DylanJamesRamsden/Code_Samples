using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Mineable
{
    public string Name { get; set; }

    public int ResourcesAvailable { get; set; }
    public int ResourcePerTurn { get; set; }

    public PlayerBase Owned { get; set; }

    public int GenerateResourcesPerTurn()
    {
        if (ResourcesAvailable > 0)
        {
            if (ResourcesAvailable > ResourcePerTurn)
            {
                ResourcesAvailable = ResourcesAvailable - ResourcePerTurn;
                return ResourcePerTurn;
            }
            else
            {
                int resourcesLeft = ResourcesAvailable;
                ResourcesAvailable = 0;
                return resourcesLeft;
            }
        }
        else return 0;
    }
}
