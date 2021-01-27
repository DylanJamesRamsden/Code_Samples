using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_Controller : MonoBehaviour
{
    public bool dialogueTrigger = false;

    [Header("Aware")]
    [Header ("Enemy Detection Values")]
    public float awareDistance;
    bool isAware = false;
    public float awareSpeed;

    [Header ("Detected")]
    public float detectedDistance;
    public float detectedSpeed;

    bool playerHidden = false;

    [Header("")]
    public Material[] enemyDetectionSymbols;
    public GameObject enemyDetection1;
    public GameObject enemyDetection2;
    public GameObject enemyDetectionCircle;
    MeshRenderer ed1MeshRenderer;
    MeshRenderer ed2MeshRenderer;

    public Material[] enemyDetectionBoundryMaterials;
    public GameObject enemyDetectionBoundry;
    MeshRenderer detectionBoundryMesh;

    [Header("Player Objects:")]
    public GameObject player;
    public Player_Controller pController;

    [Header(" ")]
    Vector3 playersLastPosition;
    public GameObject move2LocationObject;

    Vector3 initialLocation;
    bool move2Initial = false;

    GameObject move2SetObject = null;
    bool Moving2LastSeen = false;

    NavMeshAgent nmEnemy;
    Animator enemyAnimator;

    [Header("Area Manager:")]
    public Area1LevelManager a1Manager;

    //GameObject object2MoveTo;

    // Start is called before the first frame update
    void Start()
    {
        ed1MeshRenderer = enemyDetection1.GetComponent<MeshRenderer>();
        ed2MeshRenderer = enemyDetection2.GetComponent<MeshRenderer>();

        detectionBoundryMesh = enemyDetectionBoundry.GetComponent<MeshRenderer>();
        enemyDetectionBoundry.SetActive(false);

        nmEnemy = GetComponent<NavMeshAgent>();

        enemyAnimator = GetComponent<Animator>();

        enemyDetection1.SetActive(false);
        enemyDetection2.SetActive(false);

        initialLocation = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (dialogueTrigger)
        {
            if (playerDistance() < awareDistance && playerDistance() > detectedDistance)
            {             
                awarePlayer();
            }
            else if (playerDistance() < detectedDistance)
            {
                detectedPlayer();
            }
            else
            {
                nuetralPlayer();
            }

            detectedSymbolController();

            enemyDetectionBoundry.SetActive(true);

            a1Manager.FindingLunchTriggered = true;
        }
    }

    float playerDistance()
    {
        return Vector3.Distance(transform.position, player.transform.position);
    }

    void nuetralPlayer()
    {
        if (Moving2LastSeen == true)
        {
            if (Vector3.Distance(transform.position, playersLastPosition) > 1)
            {
                if (move2SetObject == null)
                {
                    move2SetObject = Instantiate(move2LocationObject, new Vector3(playersLastPosition.x, -4.1f, playersLastPosition.z), Quaternion.Euler(90f,0,0));
                }

                nmEnemy.SetDestination(playersLastPosition);

                isAware = false;
            }
            else
            {
                Moving2LastSeen = false;

                Destroy(move2SetObject.gameObject);
                move2SetObject = null;

                detectionBoundryMesh.material = enemyDetectionBoundryMaterials[0];

                enemyDetection1.SetActive(false);
                enemyDetection2.SetActive(false);

                move2Initial = true;
            }
        }
        else
        {
            if (move2Initial == true)
            {
                if (Vector3.Distance(transform.position, initialLocation) > 1)
                {
                    nmEnemy.SetDestination(initialLocation);
                }
                else
                {
                    move2Initial = false;
                }

                //move2Initial = true;
            }
        }

        if (Moving2LastSeen == false && move2Initial == false)
        {
            enemyAnimator.SetTrigger("isIdle");
        }
    }

    void awarePlayer()
    {
        RaycastHit hit;
        Vector3 direction = pController.detectionCircle.transform.position - enemyDetectionCircle.transform.position;

        if (Physics.Raycast(enemyDetectionCircle.transform.position, direction, out hit))
        {
            if (hit.transform.gameObject.tag == "Player")
            {
                ed1MeshRenderer.material = enemyDetectionSymbols[1];
                enemyDetection1.SetActive(true);

                ed2MeshRenderer.material = enemyDetectionSymbols[1];
                enemyDetection2.SetActive(true);

                detectionBoundryMesh.material = enemyDetectionBoundryMaterials[1];

                nmEnemy.SetDestination(player.transform.position);
                nmEnemy.speed = awareSpeed;

                playersLastPosition = player.transform.position;

                Moving2LastSeen = true;

                if (move2SetObject != null)
                {
                    Destroy(move2SetObject.gameObject);

                    move2SetObject = null;
                }

                enemyAnimator.SetTrigger("isWalking");

                isAware = true;
            }
        }
    }

    void detectedPlayer()
    {
        RaycastHit hit;
        Vector3 direction = pController.detectionCircle.transform.position - enemyDetectionCircle.transform.position;

        if (Physics.Raycast(enemyDetectionCircle.transform.position, direction, out hit))
        {
            if (hit.transform.gameObject.tag == "Player")
            {
                ed1MeshRenderer.material = enemyDetectionSymbols[2];
                enemyDetection1.SetActive(true);

                ed2MeshRenderer.material = enemyDetectionSymbols[2];
                enemyDetection2.SetActive(true);

                detectionBoundryMesh.material = enemyDetectionBoundryMaterials[2];

                nmEnemy.SetDestination(player.transform.position);
                nmEnemy.speed = detectedSpeed;

                playersLastPosition = player.transform.position;

                enemyAnimator.SetTrigger("isRunning");
            }
        }
    }

    void detectedSymbolController() //Rotates the bully's detection symbol to the camera
    {
        enemyDetection1.transform.LookAt(Camera.main.transform.position);
    }
}
