using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[Serializable]
public class DialogueChoice
{
    public float ID;
    public string Speaker;
    public string TriggerTag;
    public string Dialogue;
    public bool isActive;

    public List<int> nextDialogue; //Holds the dialogue IDs for the next dialogue-multiple ints result in multiple branches

    public DialogueChoice()
    {
        //nextDialogue = new List<int>();
    }
}
