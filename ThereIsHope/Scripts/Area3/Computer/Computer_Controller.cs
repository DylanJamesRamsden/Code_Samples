using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Computer_Controller : MonoBehaviour
{

    public Area3_Manager a3manager;

    [Header ("Player Variables")]
    public Area3_PlayerController pController;

    [Header("Computer")]
    public GameObject computer;
    public Light computerLight;
    public GameObject computerContainer;

    [Header("UI Elements")]
    public Text Heading;
    public InputField codeInputField;
    public Button acceptButton;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (a3manager.inComputerInteraction == true)
        {
            showUI();

            userInput();
        }
        else
        {
            hideUI();
        }
    }

    void showUI()
    {
        computer.SetActive(true);

        Heading.enabled = true;
        codeInputField.gameObject.SetActive(true);
        acceptButton.gameObject.SetActive(true);

        computerLight.enabled = true;
    }

    void hideUI()
    {
        computer.SetActive(false);

        Heading.enabled = false;
        codeInputField.gameObject.SetActive(false);
        acceptButton.gameObject.SetActive(false);

        computerLight.enabled = false;
    }

    void cameraController()
    {
        if (a3manager.inComputerInteraction == true)
        {
            computerContainer.transform.position = new Vector3(transform.position.x, -0.77f, transform.position.z + 3f);
        }
        else
        {
            computerContainer.transform.position = new Vector3(transform.position.x, -3.59f, transform.position.z);
        }
    }

    void userInput()
    {
        if (a3manager.inComputerInteraction == true)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                a3manager.inComputerInteraction = false;
                cameraController();
            }
        }
    }

    public void acceptCode()
    {
        if (codeInputField.text == GameManager.instance.skeletonKeyCode)
        {
            GameManager.instance.skeletonKeyEnabled = true;
            codeInputField.text = "Skeleton Key enabled...";
        }
        else
        {
            codeInputField.text = "";
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (a3manager.inComputerInteraction == false)
            {
                pController.showInteractabledText = true;

                if (Input.GetKey(KeyCode.E))
                {
                    a3manager.inComputerInteraction = true;
                    cameraController();
                }
            }
            else
            {
                pController.showInteractabledText = false;
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
