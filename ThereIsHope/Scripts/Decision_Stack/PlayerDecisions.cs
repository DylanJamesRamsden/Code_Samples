using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDecisions : MonoBehaviour
{
    LinkQueue<string> playerChoices;

    // Start is called before the first frame update
    void Start()
    {
        playerChoices = new LinkQueue<string>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    
    public void resetQueue()
    {
        playerChoices = new LinkQueue<string>();
    }

    public void addChoice(string choice)
    {
        playerChoices.Push(choice);
    }

    public List<string> returnAllPlayerChoices()
    {
        List<string> temp = new List<string>();

        while(playerChoices.returnQueSize() > 0)
        {
            temp.Add(playerChoices.Pop());
        }

        return temp;
    }
}
