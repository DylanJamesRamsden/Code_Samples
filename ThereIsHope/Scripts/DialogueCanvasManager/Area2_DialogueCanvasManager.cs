﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Area2_DialogueCanvasManager : MonoBehaviour
{
    public Text Heading;

    public Text npcDialogue;

    public Text easterEggCodeText;

    public Button[] options = new Button[3];
    int optionCounter = 0;

    NarrativeEngine nEngine;

    List<DialogueChoice> currentDialogue;

    string npcName;

    public GameObject Diary;
    public Area2_DiaryController dController;

    Area2_Manager a2Manager;

    // Start is called before the first frame update
    void Start()
    {
        // a1Manager = GameObject.FindGameObjectWithTag("CameraController").GetComponent<Area1LevelManager>();
        a2Manager = GameObject.FindGameObjectWithTag("CameraController").GetComponent<Area2_Manager>();

        Heading.enabled = false;

        easterEggCodeText.enabled = false;

        npcDialogue.enabled = false;

        options[0].gameObject.SetActive(false);
        options[1].gameObject.SetActive(false);
        options[2].gameObject.SetActive(false);

        dController = Diary.GetComponent<Area2_DiaryController>();
        Diary.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void startDialogueInteraction(string jsonName, string NPCname)
    {
        nEngine = new NarrativeEngine();
        nEngine.getDialogueInteraction(jsonName);

        easterEggCodeText.text = nEngine.currentInteraction.EasterEggCode.ToString();

        currentDialogue = new List<DialogueChoice>();

        Diary.SetActive(true);
        dController.diaryType = "Dialogue";

        currentDialogue = getDialogue();

        npcName = NPCname;
    }

    public void showDialogueScreen()
    {
        Heading.enabled = true;
        Heading.text = nEngine.currentInteraction.Description;

        easterEggCodeText.enabled = true;

        npcDialogue.enabled = true;

        options[0].gameObject.SetActive(true);
        options[1].gameObject.SetActive(true);
        options[2].gameObject.SetActive(true);
    }

    void hideDialogueScreen()
    {
        Heading.enabled = false;

        easterEggCodeText.enabled = false;

        npcDialogue.enabled = false;

        options[0].gameObject.SetActive(false);
        options[1].gameObject.SetActive(false);
        options[2].gameObject.SetActive(false);

        dController.diaryType = "";

        Diary.SetActive(false);
    }

    List<DialogueChoice> getDialogue()
    {
        List<DialogueChoice> activeDialogue = new List<DialogueChoice>();

        foreach (DialogueChoice u in currentDialogue)
        {
            u.isActive = false;
        }

        foreach (DialogueChoice u in nEngine.currentInteraction.DialogueChoices)
        {
            if (u.isActive == true)
            {
                activeDialogue.Add(u);
            }
        }

        return activeDialogue;
    }

    public void chosenDialogue(int buttonID)
    {
        foreach (DialogueChoice u in nEngine.currentInteraction.DialogueChoices) //Controls player options
        {
            if (u.Dialogue == options[buttonID].GetComponentInChildren<Text>().text)
            {
                GameManager.instance.pDecisions.addChoice("Tod(Player): " + u.Dialogue);
                foreach (int nextID in u.nextDialogue)
                {
                    foreach (DialogueChoice nextChoice in nEngine.currentInteraction.DialogueChoices)
                    {
                        if (nextChoice.ID == nextID)
                        {
                            nextChoice.isActive = true;

                            if (nextChoice.TriggerTag != "")
                            {
                                switch (nextChoice.TriggerTag)
                                {
                                    case "O1":
                                        dController.objective1complete = true;
                                        dController.objective2Active = true;
                                        break;
                                    case "O2":
                                        dController.objective2complete = true;
                                        dController.objective3Active = true;
                                        break;
                                    case "O3":
                                        dController.objective3complete = true;
                                        dController.objective4Active = true;
                                        break;
                                    case "O4":
                                        dController.objective4complete = true;
                                        dController.objective5Active = true;
                                        break;
                                    case "O5":
                                        dController.objective5complete = true;
                                        dController.objective6Active = true;
                                        a2Manager.inDemonGame = true;
                                        break;
                                    case "O6":
                                        a2Manager.isComplete = true;
                                        break;

                                }
                            }
                        }
                    }
                }
            }
        }

        currentDialogue = getDialogue();

        loadDialogue();
    }

    public void loadDialogue()
    {
        int openButtonCounter = 0;

        foreach (DialogueChoice u in currentDialogue)
        {
            if (u.Speaker != "Tod")
            {
                npcDialogue.text = u.Dialogue;
                GameManager.instance.pDecisions.addChoice(npcName + ": " + u.Dialogue);
            }
            else
            {
                options[openButtonCounter].GetComponentInChildren<Text>().text = u.Dialogue;
                openButtonCounter++;
            }
        }

        for (int i = 2; i > openButtonCounter - 1; i--)
        {
            options[i].gameObject.SetActive(false);
        }

        for (int x = 0; x < openButtonCounter; x++)
        {
            options[x].gameObject.SetActive(true);
        }
    }

    public void deleteInteraction()
    {
        hideDialogueScreen();

        nEngine = null;
    }
}
