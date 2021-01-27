using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

    class EasterEggLight : LightBase
    {

    private Color LightColor1;

    public Color lightcolor1
    {
        get { return LightColor1; }
        set { LightColor1 = value; }
    }

    private Color LightColor2;

    public Color lightcolor2
    {
        get { return LightColor2; }
        set { LightColor2 = value; }
    }

    private float LerpStep;

    public float lerpS
    {
        get { return LerpStep; }
        set { LerpStep = value; }
    }

    private bool LerpOpposite;

    public bool lerpO
    {
        get { return LerpOpposite; }
        set { LerpOpposite = value; }
    }



    public EasterEggLight(Color c1, Color c2, Light lObject)
    {
        lightcolor1 = c1;
        lightcolor2 = c2;
        lightObject = lObject;

        lerpS = 0.025f;
        lerpO = false;
    }

    public override void colorToShow()
    {
        lightObject.color = Color.Lerp(lightcolor1, lightcolor2, lerpS);

        if (lerpS >= 1)
        {
            lerpO = true;
        }
        else if (lerpS <= 0)
        {
            lerpO = false;
        }
    }

}
