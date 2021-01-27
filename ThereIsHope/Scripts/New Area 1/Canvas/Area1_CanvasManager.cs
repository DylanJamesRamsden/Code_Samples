using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Area1_CanvasManager : MonoBehaviour
{

    public Text HelpText;
    int time = 120;
    int timeCounter = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        helpTextController();
    }

    void helpTextController()
    {
        if (timeCounter < time)
        {
            timeCounter++;
        }
        else
        {
            HelpText.enabled = false;
        }
    }
}
