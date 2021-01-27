using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{

    [Header("Movement Stats")]
    public float walkSpeed;
    public float runSpeed;
    public float sneakSpeed;

    Animator playerAnimator;

    public bool isSneaking = false;

    [Header("Detetction Objects")]
    public GameObject detectionCircle;

    [Header("Player Sound")]
    public AudioSource stepSound;
    public AudioSource heartbeatSound;

    [Header ("Global State Variables")]
    public bool IsPlayerAlive = true;

    bool canPickUp = false;
    bool isPickingUp = false;

    [Header("Pick-up variables")]
    public GameObject throwingRock;
    public GameObject throwPosition;
    public GameObject Aimer;

    bool hasRock = false;
    GameObject currentRock;

    [Header("Diary:")]
    public GameObject Diary;

    Area1LevelManager a1Manager;

    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = GetComponent<Animator>();

        a1Manager = GameObject.FindGameObjectWithTag("CameraController").GetComponent<Area1LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {
        MovementControls();
        SneakingController();
    }

    void MovementControls()
    {
        if (Input.GetKey(KeyCode.W))
        {
            if (Input.GetKey(KeyCode.D))
            {
                transform.localRotation = Quaternion.Euler(0, 45, 0);
            }
            else if (Input.GetKey(KeyCode.A))
            {
                transform.localRotation = Quaternion.Euler(0, 315, 0);
            }
            else
            {
                transform.localRotation = Quaternion.Euler(0, 0, 0);
            }

            if (Input.GetKey(KeyCode.LeftShift))
            {
                transform.Translate(Vector3.forward * runSpeed * Time.deltaTime);

                playerAnimator.SetTrigger("isRunning");

                isSneaking = false;

                heartbeatSound.Stop();
            }
            else
            {
                if (isSneaking == false)
                {
                    transform.Translate(Vector3.forward * walkSpeed * Time.deltaTime);

                    playerAnimator.SetTrigger("isWalking");
                }
                else
                {
                    transform.Translate(Vector3.forward * sneakSpeed * Time.deltaTime);

                    playerAnimator.SetTrigger("isCrouchedWalk");
                }
            }
        }
        else if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.forward * walkSpeed * Time.deltaTime);

            if (Input.GetKey(KeyCode.D))
            {
                transform.localRotation = Quaternion.Euler(0, 135, 0);
            }
            else if (Input.GetKey(KeyCode.A))
            {
                transform.localRotation = Quaternion.Euler(0, 225, 0);
            }
            else
            {
                transform.localRotation = Quaternion.Euler(0, 180, 0);
            }

            if (Input.GetKey(KeyCode.LeftShift))
            {
                transform.Translate(Vector3.forward * runSpeed * Time.deltaTime);

                playerAnimator.SetTrigger("isRunning");

                isSneaking = false;

                heartbeatSound.Stop();
            }
            else
            {
                if (isSneaking == false)
                {
                    transform.Translate(Vector3.forward * walkSpeed * Time.deltaTime);

                    playerAnimator.SetTrigger("isWalking");
                }
                else
                {
                    transform.Translate(Vector3.forward * sneakSpeed * Time.deltaTime);

                    playerAnimator.SetTrigger("isCrouchedWalk");
                }
            }
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.localRotation = Quaternion.Euler(0, 90, 0);

            if (Input.GetKey(KeyCode.LeftShift))
            {
                transform.Translate(Vector3.forward * runSpeed * Time.deltaTime);

                playerAnimator.SetTrigger("isRunning");

                isSneaking = false;

                heartbeatSound.Stop();
            }
            else
            {
                if (isSneaking == false)
                {
                    transform.Translate(Vector3.forward * walkSpeed * Time.deltaTime);

                    playerAnimator.SetTrigger("isWalking");
                }
                else
                {
                    transform.Translate(Vector3.forward * sneakSpeed * Time.deltaTime);

                    playerAnimator.SetTrigger("isCrouchedWalk");
                }
            }
        }
        else if (Input.GetKey(KeyCode.A))
        {
            transform.localRotation = Quaternion.Euler(0, 270, 0);

            if (Input.GetKey(KeyCode.LeftShift))
            {
                transform.Translate(Vector3.forward * runSpeed * Time.deltaTime);

                playerAnimator.SetTrigger("isRunning");

                isSneaking = false;

                heartbeatSound.Stop(); heartbeatSound.Stop();
            }
            else
            {
                if (isSneaking == false)
                {
                    transform.Translate(Vector3.forward * walkSpeed * Time.deltaTime);

                    playerAnimator.SetTrigger("isWalking");
                }
                else
                {
                    transform.Translate(Vector3.forward * sneakSpeed * Time.deltaTime);

                    playerAnimator.SetTrigger("isCrouchedWalk");
                }
            }
        }
        else if (Input.GetKey(KeyCode.E))
        {
            if (canPickUp == true)
            {
                isPickingUp = true;
                playerAnimator.SetTrigger("isPickingUp");
            }

            if (hasRock == true)
            {
                playerAnimator.SetTrigger("isThrowing");

                isSneaking = false;
                SneakingController();
            }
        }
        else if (Input.GetKey(KeyCode.P))
        {
            NarrativeEngine nEngine = GameObject.FindGameObjectWithTag("NarrativeEngine").GetComponent<NarrativeEngine>();
            nEngine.getDialogueInteraction("Tod_Interaction1.json");
        }
        else
        {
            if (isPickingUp == false)
            {
                if (isSneaking == true)
                {
                    playerAnimator.SetTrigger("isCrouched");
                }
                else
                {
                    playerAnimator.SetTrigger("isIdle");
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Diary_Controller dController = Diary.GetComponent<Diary_Controller>();

            if (Diary.activeSelf == true)
            {
                if (dController.diaryType == "")
                {
                    dController.hideObjectiveUI();
                    Diary.SetActive(false);
                }
            }
            else
            {
                Diary.SetActive(true);
            }
        }
    }

    void SneakingController()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            if (isSneaking == false)
            {
                isSneaking = true;
                stepSound.volume = 0.1f;

                heartbeatSound.Play();

                detectionCircle.transform.localPosition = new Vector3(0, 0.45f, -0.14f);
            }
            else
            {
                isSneaking = false;
                stepSound.volume = 0.3f;

                heartbeatSound.Stop();

                detectionCircle.transform.localPosition = new Vector3(0, 0.87f, -0.14f);
            }
        }

        if (isSneaking == false)
        {
            detectionCircle.transform.localPosition = new Vector3(0, 0.87f, -0.14f);
        }
        else
        {
            detectionCircle.transform.localPosition = new Vector3(0, 0.45f, -0.14f);
        }
    }

    void throwController()
    {
        playerAnimator.Play("Idle1");

        hasRock = false;

        GameObject thrownRock = Instantiate(throwingRock, throwPosition.transform.position, Quaternion.identity);
        //thrownRock.transform.LookAt(Aimer.transform.position);

        Rigidbody thrownRockRB = thrownRock.GetComponent<Rigidbody>();
        thrownRockRB.AddForce(transform.forward * 200f);
        thrownRockRB.AddForce(transform.up * 300f);
    }

    void playStep()
    {
        stepSound.Play();
    }

    //pICK-UP ANIMATION METHODS
    public void rockPickedUp()
    {
        playerAnimator.SetTrigger("isIdle");

        canPickUp = false; //MIGHT NOT HAVE TO CHANGE HERE
        isPickingUp = false;

        hasRock = true;
    }

    public void deleteRock()
    {
        Destroy(currentRock);
    }


    //COLLISION METHODS
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bully")
        {
            a1Manager.isAlive = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Rock")
        {
            canPickUp = true;
            currentRock = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Rock")
        {
            canPickUp = false;
        }
    }
}
