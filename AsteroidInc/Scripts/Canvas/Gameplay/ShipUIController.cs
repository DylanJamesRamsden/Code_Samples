using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipUIController : MonoBehaviour
{

    PlayManager pManager;

    [Header("Canvas Variables")]
    public GameObject shipUICanvas;

    [Header("UI Text:")]
    public Text shipNameValue;
    public Text shipTypeValue;
    public Text shipHealthValue;
    public Text shipAttackValue;
    public Text shipDefenceValue;
    public Text shipActionsValue;

    [Header("UI Buttons:")]
    public Button skipButton;
    public Button moveButton;
    public Button actionButton;

    // Start is called before the first frame update
    void Start()
    {
        pManager = GameObject.FindGameObjectWithTag("PlayManager").GetComponent<PlayManager>();

        hideShipUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void showShipUI(Ship_Base selectedShip)
    {
        shipUICanvas.SetActive(true);

        updateShipUI(selectedShip);

        if (selectedShip.GetType() == typeof(MiningShip))
        {
            actionButton.GetComponentInChildren<Text>().text = "Mine";
        }
        else if (selectedShip.GetType() == typeof(DestroyerShip))
        {
            actionButton.GetComponentInChildren<Text>().text = "Attack";
        }

        disableSkipButton();
        disableMoveButton();
        disableActionButton();
    }

    void updateShipUI(Ship_Base selectedShip)
    {
        shipNameValue.text = selectedShip.name;

        switch (selectedShip.shipType)
        {
            case "Miner":
                shipTypeValue.text = "Miner";
                break;
            case "Destroyer":
                shipTypeValue.text = "Destroyer";
                break;
        }

        shipHealthValue.text = selectedShip.health.ToString();
        shipAttackValue.text = selectedShip.attack.ToString();
        shipDefenceValue.text = selectedShip.defence.ToString();
        shipActionsValue.text = selectedShip.actionsLeft.ToString();
    }

    public void hideShipUI()
    {
        shipUICanvas.SetActive(false);
    }

    public void enableMoveButton()
    {
        moveButton.interactable = true;
    }

    public void enableSkipButton()
    {
        skipButton.interactable = true;
    }

    public void enableActionButton()
    {
        actionButton.interactable = true;
    }

    public void disableMoveButton()
    {
        moveButton.interactable = false;
    }

    public void disableSkipButton()
    {
        skipButton.interactable = false;
    }

    public void disableActionButton()
    {
        actionButton.interactable = false;
    }

    public void MoveClick()
    {
        pManager.fleetController.canMoveSelected = true;
    }

    public void ActionClick()
    {
        if (pManager.fleetController.selectedUnit.GetType() == typeof(MiningShip))
        {
            pManager.fleetController.ShipMine();
        }
        else if (pManager.fleetController.selectedUnit.GetType() == typeof(DestroyerShip))
        {
            pManager.fleetController.canAttack = true;
        }
    }

    public void skipClick()
    {
        pManager.fleetController.SkipShipActions();
    }
}
