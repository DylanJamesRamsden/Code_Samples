using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area3_npcController : MonoBehaviour
{
    [Header("NPC dialogue variables:")]
    public string NPCname;
    public string DialogueTextfileName1;

    [Header("World variables")]
    public GameObject player;
    Area3_PlayerController pController;
    public Area3_DialogueCanvasManager dcManager;
    public GameObject diaryObject;

    [Header("Level Manager Varaibels")]
    public Area3_Manager a3Manager;

    // Start is called before the first frame update
    void Start()
    {
        pController = player.GetComponent<Area3_PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            pController.showInteractabledText = true;

            if (Input.GetKey(KeyCode.E))
            {
                a3Manager.inDialogueInteraction = true;

                dcManager.startDialogueInteraction(DialogueTextfileName1, NPCname);

                diaryObject.transform.position = new Vector3(transform.position.x, -0.77f, transform.position.z + 3f);
            }
            else if (Input.GetKey(KeyCode.Escape))
            {
                if (a3Manager.inDialogueInteraction == true)
                {
                    a3Manager.inDialogueInteraction = false;
                    dcManager.deleteInteraction();

                    pController.showInteractabledText = false;

                    diaryObject.transform.position = new Vector3(transform.position.x, -3.59f, transform.position.z);
                }

            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            pController.showInteractabledText = true;
        }
    }
}
