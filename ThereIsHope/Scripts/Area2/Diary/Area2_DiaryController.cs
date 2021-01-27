using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Area2_DiaryController : MonoBehaviour
{
    public Area2_DialogueCanvasManager dcmanager;

    [Header("Objective UI elements")]
    public Text Heading;
    public Text objective1;
    public bool objective1Active;
    public bool objective1complete;

    public Text objective2;
    public bool objective2Active;
    public bool objective2complete;

    public Text objective3;
    public bool objective3Active;
    public bool objective3complete;

    public Text objective4;
    public bool objective4Active;
    public bool objective4complete;

    public Text objective5;
    public bool objective5Active;
    public bool objective5complete;

    public Text objective6;
    public bool objective6Active;
    public bool objective6complete;

    public Text objective7;
    public bool objective7Active;
    public bool objective7complete;

    public Stack<Text> objectiveText = new Stack<Text>(6);

    public string diaryType;

    public bool isOpen = false;

    // Start is called before the first frame update
    private void Awake()
    {

    }

    void Start()
    {
        objectiveText.Push(objective1);
        objectiveText.Push(objective2);
        objectiveText.Push(objective3);
        objectiveText.Push(objective4);
        objectiveText.Push(objective5);
        objectiveText.Push(objective6);
        objectiveText.Push(objective7);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void hideObjectiveUI()
    {
        Heading.enabled = false;

        objectiveText[0].enabled = false;
        objectiveText[1].enabled = false;
        objectiveText[2].enabled = false;
        objectiveText[3].enabled = false;
        objectiveText[4].enabled = false;
        objectiveText[5].enabled = false;
        objectiveText[6].enabled = false;
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
        }

        if (objective3Active)
        {
            objectiveText[2].enabled = true;
            if (objective3complete)
            {
                objectiveText[2].color = Color.green;
            }
        }

        if (objective4Active)
        {
            objectiveText[3].enabled = true;
            if (objective4complete)
            {
                objectiveText[3].color = Color.green;
            }
        }

        if (objective5Active)
        {
            objectiveText[4].enabled = true;
            if (objective5complete)
            {
                objectiveText[4].color = Color.green;
            }
        }

        if (objective6Active)
        {
            objectiveText[5].enabled = true;
            if (objective6complete)
            {
                objectiveText[5].color = Color.green;
            }
        }

        if (objective7Active)
        {
            objectiveText[6].enabled = true;
            if (objective7complete)
            {
                objectiveText[6].color = Color.green;
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
