using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Hexagon
{
    public String HexagonName { get; set; }

    public GameObject HexagonObject { get; set; }

    public List<Hexagon> surroundingHex { get; set; }

    public Mineable MineableInHex { get; set; }

    public float centerX { get; set; }
    public float centerZ { get; set; }

    public Hexagon(string hName, GameObject hObject, float cX, float cZ)
    {
        HexagonName = hName;
        HexagonObject = hObject;

        MineableInHex = null;

        centerX = cX;
        centerZ = cZ;
    }
}
