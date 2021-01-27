using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopController : MonoBehaviour
{

    [Header("UI Variables:")]
    public RawImage ShopPanel;

    public Text fuelText;
    int fuelCounter = 0;
    public Text attackText;
    int attackCounter = 0;
    public Text defenceText;
    int defenceCounter = 0;

    public Text totalText;
    int Total = 0;

    public Button ConfirmButton;

    PlayManager pManager;

    // Start is called before the first frame update
    void Start()
    {
        pManager = GameObject.FindGameObjectWithTag("PlayManager").GetComponent<PlayManager>();

        HideShop();
    }

    // Update is called once per frame
    void Update()
    {
        checkConfirm();
    }

    void checkConfirm()
    {
        if (ShopPanel.IsActive() == true)
        {
            if (Total > 0)
            {
                if (pManager.PlayersTurn == 1)
                {
                    if (pManager.p1Base.TotalMoney >= Total)
                    {
                        ConfirmButton.interactable = true;
                    }
                    else ConfirmButton.interactable = false;
                }
                else
                {
                    if (pManager.p2Base.TotalMoney >= Total)
                    {
                        ConfirmButton.interactable = true;
                    }
                    else ConfirmButton.interactable = false;
                }
            }
            else ConfirmButton.interactable = false;
        }
    }

    void HideShop()
    {
        ShopPanel.gameObject.SetActive(false);

        fuelCounter = 0;
        attackCounter = 0;
        defenceCounter = 0;

        fuelText.text = "0";
        attackText.text = "0";
        defenceText.text = "0";

        totalText.text = "$00000";
        Total = 0;
    }

    void ShowShop()
    {
        ShopPanel.gameObject.SetActive(true);
    }

    public void incFuel()
    {
        fuelCounter += 1;
        fuelText.text = fuelCounter.ToString();

        Total += 50;
        totalText.text = "$" + Total.ToString();
    }

    public void decFuel()
    {
        if (fuelCounter > 0)
        {
            fuelCounter -= 1;
            fuelText.text = fuelCounter.ToString();

            Total -= 50;
            totalText.text = "$" + Total.ToString();
        }
    }

    public void incAttack()
    {
        attackCounter += 1;
        attackText.text = attackCounter.ToString();

        Total += 100;
        totalText.text = "$" + Total.ToString();
    }

    public void decAttack()
    {
        if (attackCounter > 0)
        {
            attackCounter -= 1;
            attackText.text = attackCounter.ToString();

            Total -= 100;
            totalText.text = "$" + Total.ToString();
        }
    }

    public void incShield()
    {
        defenceCounter += 1;
        defenceText.text = defenceCounter.ToString();

        Total += 100;
        totalText.text = "$" + Total.ToString();
    }

    public void decShield()
    {
        if (defenceCounter > 0)
        {
            defenceCounter -= 1;
            defenceText.text = defenceCounter.ToString();

            Total -= 100;
            totalText.text = "$" + Total.ToString();
        }
    }

    public void ShopButtonClick()
    {
        ShowShop();
    }

    public void CancelClick()
    {
        HideShop();
    }

    public void ConfirmClick()
    {
        //if (pManager.PlayersTurn == 1)
        //{
        //    pManager.p1Controller.pBase.Fuel += fuelCounter;
        //    pManager.p1Controller.pBase.Attack += attackCounter;
        //    pManager.p1Controller.pBase.Shield += defenceCounter;

        //    pManager.p1Controller.pBase.TotalMoney -= Total;
        //}
        //else
        //{
        //    pManager.p2Controller.pBase.Fuel += fuelCounter;
        //    pManager.p2Controller.pBase.Attack += attackCounter;
        //    pManager.p2Controller.pBase.Shield += defenceCounter;

        //    pManager.p2Controller.pBase.TotalMoney -= Total;
        //}
    }
}
