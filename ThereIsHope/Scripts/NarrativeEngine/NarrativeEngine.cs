using System.IO;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NarrativeEngine : MonoBehaviour
{

    public DialogueInteraction currentInteraction;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void getDialogueInteraction(string jsonName)
    {
        string jsonString = File.ReadAllText(jsonName);

        currentInteraction = JsonUtility.FromJson<DialogueInteraction>(jsonString);
    }
}
