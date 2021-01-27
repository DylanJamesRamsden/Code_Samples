using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Asteroid : Mineable
{
    public Asteroid(string name,int totalR, int RperT)
    {
        Name = name;
        ResourcesAvailable = totalR;
        ResourcePerTurn = RperT;
    }

    public string getInfo()
    {
        return "Name: " + Name + "\nResources Available: " + ResourcesAvailable.ToString() + "\nResources/pt:" + ResourcePerTurn.ToString();
    }
}
