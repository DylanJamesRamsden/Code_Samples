using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area3_PlayerController : MonoBehaviour
{
    [Header("Movement Stats")]
    public float walkSpeed;
    public float runSpeed;

    public Animator playerAnimator;

    [Header("Global State Variables")]
    public bool IsPlayerAlive = true;
    public Area3_Manager a3Manager;

    [Header("Key Variables")]
    public string keyTag = "";

    public GameObject interactableText;
    public bool showInteractabledText;

    public GameObject lockedText;
    public bool showLockedText = false;
    public int lockedShowTimer;
    int lockedShowCounter;

    [Header("Diary:")]
    public GameObject Diary;
    public Area3_DiaryController pDiary;
    public GameObject DiaryContainer;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        DiaryControls();

        lockedDoor();
        interactableTextActive();

        if (pDiary.isOpen == false && a3Manager.inComputerInteraction == false)
        {
            MovementControls();
            AnimationControls();
        }
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
            }
            else
            {
                transform.Translate(Vector3.forward * walkSpeed * Time.deltaTime);
            }
        }
        else if (Input.GetKey(KeyCode.S))
        {
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
            }
            else
            {
                transform.Translate(Vector3.forward * walkSpeed * Time.deltaTime);
            }
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.localRotation = Quaternion.Euler(0, 90, 0);

            if (Input.GetKey(KeyCode.LeftShift))
            {
                transform.Translate(Vector3.forward * runSpeed * Time.deltaTime);
            }
            else
            {
                transform.Translate(Vector3.forward * walkSpeed * Time.deltaTime);
            }
        }
        else if (Input.GetKey(KeyCode.A))
        {
            transform.localRotation = Quaternion.Euler(0, 270, 0);

            if (Input.GetKey(KeyCode.LeftShift))
            {
                transform.Translate(Vector3.forward * runSpeed * Time.deltaTime);
            }
            else
            {
                transform.Translate(Vector3.forward * walkSpeed * Time.deltaTime);
            }
        }
    }

    void AnimationControls()
    {
        if (Input.GetKey(KeyCode.W))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                playerAnimator.SetTrigger("Run");
            }
            else
            {
                playerAnimator.SetTrigger("Walk");
            }
        }
        else if (Input.GetKey(KeyCode.S))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                playerAnimator.SetTrigger("Run");
            }
            else
            {
                playerAnimator.SetTrigger("Walk");
            }
        }
        else if (Input.GetKey(KeyCode.D))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                playerAnimator.SetTrigger("Run");
            }
            else
            {
                playerAnimator.SetTrigger("Walk");
            }
        }
        else if (Input.GetKey(KeyCode.A))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                playerAnimator.SetTrigger("Run");
            }
            else
            {
                playerAnimator.SetTrigger("Walk");
            }
        }
        else
        {
            playerAnimator.SetTrigger("Idle");
        }
    }

    void DiaryControls()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (a3Manager.inComputerInteraction == false)
            {
                if (Diary.activeSelf == true)
                {
                    if (pDiary.diaryType == "")
                    {
                        DiaryContainer.transform.position = new Vector3(transform.position.x, -3.59f, transform.position.z);
                        pDiary.hideObjectiveUI();
                        Diary.SetActive(false);

                        pDiary.isOpen = false;
                    }
                }
                else
                {
                    DiaryContainer.transform.position = new Vector3(transform.position.x, -0.77f, transform.position.z + 3f);
                    Diary.SetActive(true);
                    playerAnimator.SetTrigger("Idle");

                    pDiary.isOpen = true;
                }
            }
        }
    }

    void lockedDoor()
    {
        if (showLockedText == true)
        {
            lockedText.gameObject.SetActive(true);
            lockedSymbolController();

            if (lockedShowCounter < lockedShowTimer)
            {
                lockedShowCounter++;
            }
            else
            {
                lockedShowCounter = 0;
                showLockedText = false;
            }
        }
        else
        {
            lockedText.gameObject.SetActive(false);
        }
    }

    void lockedSymbolController() //Rotates the bully's detection symbol to the camera
    {
        lockedText.transform.LookAt(Camera.main.transform.position);
    }

    void interactableTextActive()
    {
        if (a3Manager.inDialogueInteraction == false)
        {
            if (showInteractabledText == true)
            {
                interactableText.SetActive(true);
                interactableSymbolController();
            }
            else
            {
                interactableText.SetActive(false);
            }
        }
        else
        {
            showInteractabledText = false;
            interactableText.SetActive(false);
        }
    }

    void interactableSymbolController() //Rotates the bully's detection symbol to the camera
    {
        interactableText.transform.LookAt(Camera.main.transform.position);
        //interactableText.transform.Translate(0, 1.907f, -0.277f);
    }
}
