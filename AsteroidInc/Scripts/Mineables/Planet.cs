using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Planet : Mineable
{
    public int PlanetDefence { get; set; }
    public int PlanetAttack { get; set; }

    public Planet(string name,int totalR, int RperT, int pDefence, int pAttack)
    {
        ResourcesAvailable = totalR;
        ResourcePerTurn = RperT;
        PlanetDefence = pDefence;
        PlanetAttack = pAttack;
        Name = name;
    }

    //public void PlanetDefenceAttempt(PlayerBase pBase)
    //{
    //    if (pBase.Attack > PlanetDefence && pBase.Shield > PlanetAttack)
    //    {
    //        pBase.MineablesOwned.Add(this);
    //        Owned = pBase;
    //    }
    //}

    public string getInfo()
    {
        return "Name: " + Name + "\nResources Available: " + ResourcesAvailable.ToString() + "\nResources/PT: " + ResourcePerTurn.ToString() + "\nInhabitaant Defence: " + PlanetDefence.ToString() + "\nInhabitant Attack: " + PlanetAttack.ToString();
    }
}
