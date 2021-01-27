using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Diary_Controller : MonoBehaviour
{

    public DialogueCanvasManager dcmanager;

    [Header("Objective UI elements")]
    public Text Heading;
    public Text objective1;
    public bool objective1Active;
    public bool objective1complete;

    public Text objective2;
    public bool objective2Active;
    public bool objective2complete;

    public RawImage[] unfoundLunch;
    public RawImage[] foundLunch;
    public bool[] isLunchFound = new bool[5] { false, false, false, false, false };

    public Text objective3;
    public bool objective3Active;
    public bool objective3complete;

    public Stack<Text> objectiveText = new Stack<Text>(4);

    public string diaryType;

    // Start is called before the first frame update
    private void Awake()
    {
        
    }

    void Start()
    {
        objectiveText.Push(objective1);
        objectiveText.Push(objective2);
        objectiveText.Push(objective3);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void hideObjectiveUI()
    {
        Heading.enabled = false;

        //objective1.enabled = false;
        //objective2.enabled = false;
        //objective3.enabled = false;

        objectiveText[0].enabled = false;
        objectiveText[1].enabled = false;
        objectiveText[2].enabled = false;

        foreach (RawImage u in unfoundLunch)
        {
            u.enabled = false;
        }

        foreach (RawImage u in foundLunch)
        {
            u.enabled = false;
        }
    }

    void showObjectiveUI()
    {
        Heading.enabled = true;

        if (objective1Active)
        {
            objectiveText[0].enabled = true;
            if (objective1complete)
            {
                objectiveText[0].color = Color.green;
            }
        }

        if (objective2Active)
        {
            objectiveText[1].enabled = true;
            if (objective2complete)
            {
                objectiveText[1].color = Color.green;
            }

            for (int i = 0; i < 5; i++)
            {
                if (isLunchFound[i] == true)
                {
                    foundLunch[i].enabled = true;
                }
                else
                {
                    unfoundLunch[i].enabled = true;
                }
            }
        }

        if (objective3Active)
        {
            objectiveText[2].enabled = true;
            if (objective3complete)
            {
                objectiveText[2].color = Color.green;
            }
        }
    }

    public void foundLunchBox()
    {
        for (int i = 0; i < 5; i++)
        {
            if (isLunchFound[i] == false)
            {
                isLunchFound[i] = true;
                break;
            }
        }
    }

    public void showUI()
    {
        if (diaryType == "Dialogue")
        {
            dcmanager.showDialogueScreen();
            dcmanager.loadDialogue();
        }
        else
        {
            showObjectiveUI();
        }
    }
}
