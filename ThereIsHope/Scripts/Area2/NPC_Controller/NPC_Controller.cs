using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Controller : MonoBehaviour
{
    [Header("NPC variables")]
    public bool dialogueTrigger = false;

    //DialogueCanvasManager dcManager;

    [Header("NPC dialogue variables:")]
    public string NPCname;
    public string DialogueTextfileName1;

    [Header(" ")]
    public TextMesh playerWorldName;

    [Header("World variables")]
    public GameObject player;
    public Area2_DialogueCanvasManager dcManager;

    [Header("Level Manager Varaibels")]
    public Area2_Manager a2Manager;

    // Start is called before the first frame update
    void Start()
    {
        playerWorldName.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        NameSymbolController();
    }

    float getDistanceFromPlayer()
    {
        return Vector3.Distance(player.transform.position, transform.position);
    }

    void NameSymbolController() //Rotates the bully's detection symbol to the camera
    {
        playerWorldName.transform.LookAt(Camera.main.transform.position);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerWorldName.text = "E";

            if (Input.GetKey(KeyCode.E))
            {
                //dcManager = GameObject.FindGameObjectWithTag("DialogueCanvasManager").GetComponent<DialogueCanvasManager>();
                    a2Manager.inDialogueInteraction = true;

                    dcManager.startDialogueInteraction(DialogueTextfileName1, NPCname);

                    playerWorldName.gameObject.SetActive(false);
            }
            else
            {
                if (a2Manager.inDialogueInteraction == true)
                {
                    if (Input.GetKey(KeyCode.Escape))
                    {
                        a2Manager.inDialogueInteraction = false;
                        dcManager.deleteInteraction();
                    }
                }
            }

            if (dialogueTrigger == true)
            {
                a2Manager.inDialogueInteraction = false;
                dcManager.deleteInteraction();
            }
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
