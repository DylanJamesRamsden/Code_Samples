using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public abstract class LightBase
{
    public Light lightObject { get; set; }

    public void lightOn()
    {
        lightObject.enabled = true;
    }

    public void lightOff()
    {
        lightObject.enabled = false;
    }

    public abstract void colorToShow();

}
