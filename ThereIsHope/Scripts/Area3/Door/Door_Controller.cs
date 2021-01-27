using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_Controller : MonoBehaviour
{
    [Header ("Door Variables")]
    public Animator doorAnimator;
    public BoxCollider doorCollider;
    public string doorTag;

    public bool isOpen = false;
    public bool isLocked = false;

    public int doorCloseTime;
    int doorCloseTimer = 0;

    [Header("Door Sounds:")]
    public AudioSource doorOpenSound;
    public AudioSource doorLockedSound;

    Area3_PlayerController pController;

    // Start is called before the first frame update
    void Start()
    {
        pController = GameObject.FindGameObjectWithTag("Player").GetComponent<Area3_PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        autoCloseDoor();
    }

    void autoCloseDoor()
    {
        if (isOpen == true)
        {
            if (doorCloseTimer < doorCloseTime)
            {
                doorCloseTimer++;
            }
            else
            {
                doorAnimator.SetTrigger("ClosedDoor");
                doorCloseTimer = 0;

                isOpen = false;

                doorCollider.enabled = true;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {

            if (pController.showLockedText == false)
            {
                pController.showInteractabledText = true;
            }
            else
            {
                pController.showInteractabledText = false;
            }

            if (Input.GetKey(KeyCode.E))
            {
                if (isLocked == false) //UnlockedDoor
                {
                    if (isOpen == false)
                    {
                        doorAnimator.SetTrigger("OpenDoor");

                        isOpen = true;

                        doorOpenSound.Play();

                        doorCollider.enabled = false;
                    }
                }
                else //LockedDoor
                {
                    if (pController.keyTag == doorTag)
                    {
                        isLocked = false;
                        //UnlockedDoor
                    }
                    else if (GameManager.instance.skeletonKeyEnabled == true)
                    {
                        isLocked = false;
                        //UnlockedDoor
                    }
                    else
                    {
                        pController.showLockedText = true;
                        doorLockedSound.Play();
                    }
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            pController.showInteractabledText = false;
        }
    }

}
