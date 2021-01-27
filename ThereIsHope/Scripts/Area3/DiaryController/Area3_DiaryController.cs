using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Area3_DiaryController : MonoBehaviour
{
    public Area3_DialogueCanvasManager dcmanager;
    public Area3_Manager a3Manager;

    [Header("Lights")]
    public Light diaryLight;

    [Header("Objective UI elements")]
    public Text Heading;
    public Text Hint;

    public Text LevelTimer;
    int minutes = 0;
    int seconds = 0;

    public Text objective1;
    public bool objective1Active;
    public bool objective1complete;

    public Text objective2;
    public bool objective2Active;
    public bool objective2complete;

    public Text objective3;
    public bool objective3Active;
    public bool objective3complete;

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
    }

    // Update is called once per frame
    void Update()
    {
        diaryLightController();
        timerValueSet();
    }

    void timerValueSet()
    {
        minutes = a3Manager.levelTimer / 60;
        seconds = a3Manager.levelTimer % 60;

        string minString = "";
        string minSeconds = "";

        if (minutes < 10 && minutes > 0)
        {
            minString = "0" + minutes.ToString();
        }
        else if (minutes == 0)
        {
            minString = "00";
        }
        else
        {
            minString = minutes.ToString();
        }

        if (seconds < 10 && seconds > 0)
        {
            minSeconds = "0" + seconds.ToString();
        }
        else if (seconds == 0)
        {
            minSeconds = "00";
        }
        else
        {
            minSeconds = seconds.ToString();
        }

        LevelTimer.text = minString + ":" + minSeconds;
    }

    void diaryLightController()
    {
        if (gameObject.activeSelf == true)
        {
            diaryLight.enabled = true;
        }
        else
        {
            diaryLight.enabled = false;
        }
    }

    public void hideObjectiveUI()
    {
        Heading.enabled = false;
        Hint.enabled = false;
        LevelTimer.enabled = false;

        objectiveText[0].enabled = false;
        objectiveText[1].enabled = false;
        objectiveText[2].enabled = false;
    }

    void showObjectiveUI()
    {
        Heading.enabled = true;
        Hint.enabled = true;
        LevelTimer.enabled = true;

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
