using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[Serializable]
public class DialogueInteraction
{
    public int ID;

    public string Description;

    public int EasterEggCode;

    public string Speaker;

    public List<DialogueChoice> DialogueChoices;

    public DialogueInteraction()
    {
        DialogueChoices = new List<DialogueChoice>();
    }
}
