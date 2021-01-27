using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InformationPanelController : MonoBehaviour
{
    public GameObject infObject;

    [Header("Player 1 Variables")]
    public Text p1Money;
    public Text p1MineablesOwned;
    public Text p1ResourcesPerTurn;
    public Button p1TurnIndicator;

    [Header("Player 2 Variables")]
    public Text p2Money;
    public Text p2MineablesOwned;
    public Text p2ResourcesPerTurn;
    public Button p2TurnIndicator;

    [Header("Gameplay Variables:")]
    public Text gameTime;
    public Text noOfTurns;

    public Button nextTurn;

    PlayManager pManager;

    // Start is called before the first frame update
    void Start()
    {
        pManager = GameObject.FindGameObjectWithTag("PlayManager").GetComponent<PlayManager>();

        nextTurn.interactable = false;
    }

    // Update is called once per frame
    void Update()
    {
        updatePlayer1Values();
        updatePlayer2Values();

        TurnIndicator();
    }

    void updatePlayer1Values()
    {
        if (pManager.p1Base.TotalMoney > 0)
        {
            p1Money.text = "$" + pManager.p1Base.TotalMoney.ToString();
        }
        else p1Money.text = "$00000";

        p1MineablesOwned.text = pManager.p1Base.MineablesOwned.Count.ToString();

        p1ResourcesPerTurn.text = "(+" + pManager.p1Base.getIncomePerTurn().ToString() + ")"; 
    }

    void updatePlayer2Values()
    {
        if (pManager.p2Base.TotalMoney > 0)
        {
            p2Money.text = "$" + pManager.p2Base.TotalMoney.ToString();
        }
        else p2Money.text = "$00000";

        p2MineablesOwned.text = pManager.p2Base.MineablesOwned.Count.ToString();

        p2ResourcesPerTurn.text = "(+" + pManager.p2Base.getIncomePerTurn().ToString() + ")";
    }

    public void endTurnClick()
    {
        pManager.EndTurn();
        nextTurn.interactable = false;
    }

    public void TimeUpdater(float minutes, float seconds)
    {
        string secondsString;
        string minutesString;

        if (minutes < 10)
        {
            minutesString = "0" + minutes.ToString();
        }
        else
        {
            minutesString = minutes.ToString();
        }

        if (seconds < 10)
        {
            secondsString = "0" + seconds.ToString();
        }
        else
        {
            secondsString = seconds.ToString();
        }

        gameTime.text = minutesString + ":" + secondsString;
    }

    public void TurnCounterUpdate(int turns)
    {
        noOfTurns.text = turns.ToString();
    }

    public void HideUI()
    {
        infObject.SetActive(false);
    }

    void TurnIndicator()
    {
        if (pManager.PlayersTurn == 1)
        {
            p1TurnIndicator.GetComponent<Image>().color = Color.green;
            p2TurnIndicator.GetComponent<Image>().color = Color.red;
        }
        else if (pManager.PlayersTurn == 2)
        {
            p2TurnIndicator.GetComponent<Image>().color = Color.green;
            p1TurnIndicator.GetComponent<Image>().color = Color.red;
        }
    }
}
