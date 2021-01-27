using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDecisionsCanvasManager : MonoBehaviour
{

    [Header ("UI Elements")]
    public Text playerDecisionsText;

    // Start is called before the first frame update
    void Start()
    {
        populateDecisionText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void populateDecisionText()
    {
        string temp = "";

        foreach (string u in GameManager.instance.playerDecisionList)
        {
            temp = temp + u + "\n \n";
        }

        playerDecisionsText.text = temp;
    }
}
