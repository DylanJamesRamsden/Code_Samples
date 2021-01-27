using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipyardController : MonoBehaviour
{

    public GameObject shipyardObject;

    [Header("UI Elements:")]
    public Button CancelButton;
    public Button PurchaseButton;

    public Button selectMiner;
    public Button selectDestroyer;

    public Text noInfoText;
    public Text InfoText;

    public InputField ShipNameInput;
    public Text shipPrice;
    public Text currentFunds;

    [Header("Ship prices:")]
    public int MinerPrice;
    public int DestroyerPrice;

    [Header("Ship Statistics:")]
    public int MinerHP;
    public int MinerAttack;
    public int MinerDefence;
    public int DestroyerHP;
    public int DestroyerAttack;
    public int DestroyerDefence;

    bool MinerChosen = false;
    bool DestroyerChosen = false;

    PlayManager pManager;

    // Start is called before the first frame update
    void Start()
    {
        pManager = GameObject.FindGameObjectWithTag("PlayManager").GetComponent<PlayManager>();

        HideShipYard();
    }

    // Update is called once per frame
    void Update()
    {
        if (shipyardObject.active == true)
        {
            EnablePurchaseButton();
        }
    }

    public void ShowShipYard()
    {
        shipyardObject.SetActive(true);

        noInfoText.enabled = true;
        InfoText.enabled = false;

        ShipNameInput.placeholder.enabled = true;
        ShipNameInput.text = "";

        shipPrice.text = "$000000";

        if (pManager.PlayersTurn == 1)
        {
            currentFunds.text = "$" + pManager.p1Base.TotalMoney.ToString();
        }
        else if (pManager.PlayersTurn == 2)
        {
            currentFunds.text = "$" + pManager.p2Base.TotalMoney.ToString();
        }
    }

    public void HideShipYard()
    {
        shipyardObject.SetActive(false);
    }

    public void MinerClick()
    {
        shipPrice.text = "$" + MinerPrice.ToString();

        noInfoText.enabled = false;
        InfoText.enabled = true;
        string infoString = "";
        infoString += "Type: Miner \n";
        infoString += "Attack: " + MinerAttack + "\n";
        infoString += "Defence: " + MinerDefence + "\n";
        infoString += "Info: Can establish mining facilities at planets and asteroids \n";
        infoString += "Moves/turn:  2 \n";

        InfoText.text = infoString;

        MinerChosen = true;
        DestroyerChosen = false;
    }

    public void DestroyerClick()
    {
        shipPrice.text = "$" + DestroyerPrice.ToString();

        noInfoText.enabled = false;
        InfoText.enabled = true;
        string infoString = "";
        infoString += "Type: Destroyer \n";
        infoString += "Attack: " + DestroyerAttack + "\n";
        infoString += "Defence: " + DestroyerDefence + "\n";
        infoString += "Info: Can attack and destroy enemy ships and their HQ \n";
        infoString += "Moves/turn:  2 \n";

        InfoText.text = infoString;

        MinerChosen = false;
        DestroyerChosen = true;
    }

    void EnablePurchaseButton()
    {
        if (pManager.PlayersTurn == 1)
        {
            if (MinerChosen == true)
            {
                if (pManager.p1Base.TotalMoney > MinerPrice)
                {
                    PurchaseButton.interactable = true;
                }
                else PurchaseButton.interactable = false;
            }
            else if (DestroyerChosen == true)
            {
                if (pManager.p1Base.TotalMoney > DestroyerPrice)
                {
                    PurchaseButton.interactable = true;
                }
                else PurchaseButton.interactable = false;
            }
            else PurchaseButton.interactable = false;
        }
        else if (pManager.PlayersTurn == 2)
        {
            if (MinerChosen == true)
            {
                if (pManager.p2Base.TotalMoney > MinerPrice)
                {
                    PurchaseButton.interactable = true;
                }
                else PurchaseButton.interactable = false;
            }
            else if (DestroyerChosen == true)
            {
                if (pManager.p2Base.TotalMoney > DestroyerPrice)
                {
                    PurchaseButton.interactable = true;
                }
                else PurchaseButton.interactable = false;
            }
            else PurchaseButton.interactable = false;
        }
    }

    public void PurchaseClick()
    {
        if (pManager.PlayersTurn == 1)
        {
            if (MinerChosen == true)
            {
                pManager.fleetController.CreateNewShip("Miner",ShipNameInput.text, MinerAttack, MinerDefence, pManager.PlayersTurn);
                pManager.p1Base.TotalMoney -= MinerPrice;
                MinerChosen = false;
            }
            else if (DestroyerChosen == true)
            {
                pManager.fleetController.CreateNewShip("Destroyer",ShipNameInput.text, MinerAttack, MinerDefence, pManager.PlayersTurn);
                pManager.p1Base.TotalMoney -= DestroyerPrice;
                DestroyerChosen = false;
            }
            shipyardObject.SetActive(false);
        }
        else if (pManager.PlayersTurn == 2)
        {
            if (MinerChosen == true)
            {
                pManager.fleetController.CreateNewShip("Miner",ShipNameInput.text, MinerAttack, MinerDefence, pManager.PlayersTurn);
                pManager.p2Base.TotalMoney -= MinerPrice;
                MinerChosen = false;
            }
            else if (DestroyerChosen == true)
            {
                pManager.fleetController.CreateNewShip("Destroyer",ShipNameInput.text, MinerAttack, MinerDefence, pManager.PlayersTurn);
                pManager.p2Base.TotalMoney -= DestroyerPrice;
                DestroyerChosen = false;
            }
            shipyardObject.SetActive(false);
        }
    }

    public void CancelClick()
    {
        HideShipYard();
    }

    //public void 
}
