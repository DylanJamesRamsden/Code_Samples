using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class JackController : MonoBehaviour
{
    [Header ("NPC variables")]
    public bool dialogueTrigger = false;
    NavMeshAgent nmAgent;

    DialogueCanvasManager dcManager;

    [Header ("NPC dialogue variables:")]
    public string NPCname;
    public string DialogueTextfileName1;
    public string DialogueTextfileName2;

    [Header (" ")]
    public TextMesh playerWorldName;
    public GameObject bikeShedLocation;

    [Header ("World variables")]
    public GameObject player;

    Animator npcAnimator;

    [Header("Level Manager Varaibels")]
    public Area1LevelManager a1Manager;

    // Start is called before the first frame update
    void Start()
    {
        npcAnimator = GetComponent<Animator>();

        playerWorldName.text = NPCname;

        nmAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        npcBehaviour();
        runToBikeshed();
    }

    void npcBehaviour()
    {
        if (dialogueTrigger == false)
        {
            if (getDistanceFromPlayer() < 5)
            {
                npcAnimator.SetTrigger("Wave");
            }
            else
            {
                npcAnimator.SetTrigger("isIdle");
            }
        }
    }

    void runToBikeshed()
    {
        if (dialogueTrigger == true)
        {
            if (a1Manager.inDialogueInteraction == false)
            {
                if (Vector3.Distance(transform.position, bikeShedLocation.transform.position) > 2)
                {
                    nmAgent.SetDestination(bikeShedLocation.transform.position);
                    npcAnimator.SetTrigger("isWalking");
                }
                else
                {
                    npcAnimator.SetTrigger("isIdle");
                    dialogueTrigger = false;
                }
            }
        }
    }

    float getDistanceFromPlayer()
    {
        return Vector3.Distance(player.transform.position, transform.position);
    }

    void NameSymbolController() //Rotates the bully's detection symbol to the camera
    {
        playerWorldName.transform.LookAt(Camera.main.transform.position);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //Debug.Log("Colliding with player");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerWorldName.text = "E";

            if (Input.GetKey(KeyCode.E))
            {
                if (a1Manager.JackFirstInteraction == true)
                {
                    dcManager = GameObject.FindGameObjectWithTag("DialogueCanvasManager").GetComponent<DialogueCanvasManager>();

                    dcManager.startDialogueInteraction(DialogueTextfileName1, NPCname);

                    playerWorldName.gameObject.SetActive(false);

                    a1Manager.inDialogueInteraction = true;
                }
                else if (a1Manager.JackSecondInteraction == true)
                {
                    dcManager = GameObject.FindGameObjectWithTag("DialogueCanvasManager").GetComponent<DialogueCanvasManager>();

                    dcManager.startDialogueInteraction(DialogueTextfileName2, NPCname);

                    playerWorldName.gameObject.SetActive(false);

                    a1Manager.inDialogueInteraction = true;
                }
            }
            else
            {
                if (a1Manager.inDialogueInteraction == true)
                {
                    if (Input.GetKey(KeyCode.Escape))
                    {
                        a1Manager.inDialogueInteraction = false;
                        dcManager.deleteInteraction();
                    }
                }
            }

            if (dialogueTrigger == true)
            {
                a1Manager.inDialogueInteraction = false;
                dcManager.deleteInteraction();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerWorldName.text = NPCname;
            playerWorldName.gameObject.SetActive(true);
        }
    }
}
