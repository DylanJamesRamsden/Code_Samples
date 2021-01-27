using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class NormalLight : LightBase
{
    private Color LightColor1;

    public Color lightcolor1
    {
        get { return LightColor1; }
        set { LightColor1 = value; }
    }

    public NormalLight(Color c, Light lObject)
    {
        lightcolor1 = c;
        lightObject = lObject;
    }

    public override void colorToShow()
    {
        base.lightObject.color = lightcolor1;
    }
}
