using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room_Controller : MonoBehaviour
{

    [Header("Room Variables")]
    public string roomType;

    public Light[] RoomLights;
    LightBase[] lightTemplates;

    // Start is called before the first frame update
    void Start()
    {
        lightTemplates = new LightBase[RoomLights.Length];

        for (int i = 0; i < RoomLights.Length; i++)
        {
            if (roomType == "Normal")
            {
                lightTemplates[i] = new NormalLight(new Color32(180, 177, 139, 225), RoomLights[i]);
                //lightTemplates[i] = new NormalLight(new Color32(225, 0, 0, 225), RoomLights[i]);
            }
            else
            {
                lightTemplates[i] = new EasterEggLight(new Color32(225, 0, 0, 225), new Color32(0, 225, 0, 225), RoomLights[i]); //Add in lightobject in parameter
            }
        }
        turnOffLights();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void turnOffLights()
    {
        foreach (LightBase u in lightTemplates)
        {
            u.lightOff();
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.transform.gameObject.tag == "Player")
        {
            if (roomType != "Normal")
            {
                foreach (LightBase u in lightTemplates)
                {
                    EasterEggLight tempEggLight = (EasterEggLight)u;

                    if (tempEggLight.lerpO == false)
                    {
                        tempEggLight.lerpS += 0.025f;
                    }
                    else tempEggLight.lerpS -= 0.025f;

                    Debug.Log(tempEggLight.lerpS);

                    tempEggLight.colorToShow();
                }
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.gameObject.tag == "Player")
        {
            foreach (LightBase u in lightTemplates)
            {
                u.lightOn();

                if (u is NormalLight)
                {
                    NormalLight tempNormalLight = (NormalLight)u;
                    tempNormalLight.colorToShow();
                }
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.gameObject.tag == "Player")
        {
            foreach (LightBase u in lightTemplates)
            {
                u.lightOff();
            }
        }
    }
}
