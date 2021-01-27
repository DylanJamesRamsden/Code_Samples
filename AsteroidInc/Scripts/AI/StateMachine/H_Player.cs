using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class H_Player : PlayerBase
{
    public H_Player(int pID)
    {
        playerID = pID;

        MineablesOwned = new List<Mineable>();
        Fleet = new List<Ship_Base>();
    }
}
