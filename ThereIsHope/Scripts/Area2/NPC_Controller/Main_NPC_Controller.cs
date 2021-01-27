using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Main_NPC_Controller : MonoBehaviour
{
    [Header("NPC variables")]
    public bool dialogueTrigger = false;
    public Slider npcHP;

    [Header("NPC dialogue variables:")]
    public string NPCname;
    public string DialogueTextfileName1;
    public string DialogueTextfileName2;

    [Header(" ")]
    public TextMesh playerWorldName;

    [Header("World variables")]
    public GameObject player;
    public Area2_DialogueCanvasManager dcManager;

    [Header("Level Manager Varaibels")]
    public Area2_Manager a2Manager;
    public Area2_DiaryController dManager;

    // Start is called before the first frame update
    void Start()
    {
        playerWorldName.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        NameSymbolController();

        inDemonGame();
    }

    float getDistanceFromPlayer()
    {
        return Vector3.Distance(player.transform.position, transform.position);
    }

    void NameSymbolController() //Rotates the bully's detection symbol to the camera
    {
        playerWorldName.transform.LookAt(Camera.main.transform.position);
    }

    void aliveCheck()
    {
        if (npcHP.value <= 0)
        {
            a2Manager.isAlive = false;
        }
    }

    void inDemonGame()
    {
        if (a2Manager.inDemonGame == true)
        {
            npcHP.gameObject.SetActive(true);

            npcHP.enabled = true;
            npcHP.value = a2Manager.todHealth;

            aliveCheck();
        }
        else
        {
            npcHP.gameObject.SetActive(false);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (a2Manager.inDemonGame == false)
            {
                playerWorldName.gameObject.SetActive(true);
                playerWorldName.text = "E";

                if (Input.GetKey(KeyCode.E))
                {
                    if (a2Manager.inDialogueInteraction == false)
                    {
                        if (dManager.objective5Active == true && dManager.objective5complete != true)
                        {
                            dcManager.startDialogueInteraction(DialogueTextfileName1, NPCname);

                            playerWorldName.gameObject.SetActive(false);

                            a2Manager.inDialogueInteraction = true;
                        }
                        else if (dManager.objective7Active == true)
                        {
                            dcManager.startDialogueInteraction(DialogueTextfileName2, NPCname);

                            playerWorldName.gameObject.SetActive(false);

                            a2Manager.inDialogueInteraction = true;
                        }
                    }
                }
                else
                {
                    if (Input.GetKey(KeyCode.Escape))
                    {
                        if (a2Manager.inDialogueInteraction == true)
                        {
                            a2Manager.inDialogueInteraction = false;
                            dcManager.deleteInteraction();
                        }
                    }
                }

            }
            else
            {
                playerWorldName.gameObject.SetActive(false);

                if (a2Manager.inDialogueInteraction == true)
                {
                    a2Manager.inDialogueInteraction = false;
                    dcManager.deleteInteraction();
                }
            }

            //if (dialogueTrigger == true)
            //{
            //    a2Manager.inDialogueInteraction = false;
            //    dcManager.deleteInteraction();
            //}
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerWorldName.text = "";
            playerWorldName.gameObject.SetActive(true);
        }
    }
}
