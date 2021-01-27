using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayManager : MonoBehaviour
{

    [Header("Game Variables")]
    [Range (1,2)]
    public int PlayerToStart;
    public int MaxTurns;
    public int winner = 0;
    public bool gameOver = false;

    public int PlayersTurn = 1;
    int turnCounter = 1;

    [Header ("Player Objects:")]
    public PlayerBase p1Base;
    public bool p1SM;
    public PlayerBase p2Base;
    public bool p2SM;
    public FleetController fleetController;
    public StateMachineController smController;

    [Header("Other Objects:")]
    public InteractingCanvasManager icManager;
    public InformationPanelController infManager;
    public CameraController cController;
    public HexGridController hexGridController;
    public ShipUIController sUIController;
    public ValuePopupManager vpManager;
    public ConsoleController conController;
    public EndGameCanvasController egcController;
    public GameSoundController gsManager;

    float minutes;
    float seconds;
    float secondCounter = 0;

    // Start is called before the first frame update
    void Start()
    {
        PlayersTurn = PlayerToStart;

        assignPlayers();

        icManager = GameObject.FindGameObjectWithTag("InteractiveCanvasManager").GetComponent<InteractingCanvasManager>();
        infManager = GameObject.FindGameObjectWithTag("InformationPanelController").GetComponent<InformationPanelController>();
        cController = GameObject.FindGameObjectWithTag("CameraController").GetComponent<CameraController>();

        fleetController = GameObject.FindGameObjectWithTag("FleetController").GetComponent<FleetController>();
        smController = GameObject.FindGameObjectWithTag("StateMachineController").GetComponent<StateMachineController>();

        hexGridController = GameObject.FindGameObjectWithTag("HexaGridController").GetComponent<HexGridController>();
        sUIController = GameObject.FindGameObjectWithTag("ShipUIController").GetComponent<ShipUIController>();

        conController = GameObject.FindGameObjectWithTag("ConsoleController").GetComponent<ConsoleController>();
        egcController = GameObject.FindGameObjectWithTag("EndGameCanvasController").GetComponent<EndGameCanvasController>();

        gsManager = GameObject.FindGameObjectWithTag("GameSoundManager").GetComponent<GameSoundController>();

        vpManager = GameObject.FindGameObjectWithTag("PopUpManager").GetComponent<ValuePopupManager>();

        hexGridController.startProtocal();

        smController.InitiateStateMachineTurn(p1Base, p2Base, MaxTurns - turnCounter);
    }

    // Update is called once per frame
    void Update()
    {
        canEndTurn();

        TurnCounterController();
    }

    private void FixedUpdate()
    {
        TimeController();
    }

    void assignPlayers()
    {
        if (GameManager.instance.isAIgame == false)
        {
            p1Base = new H_Player(1);
            p2Base = new H_Player(2);
        }
        else
        {
            p1Base = new H_Player(1);
            p2Base = new SM_Player(2);
        }
    }

    public void EndTurn()
    {
        if (turnCounter < MaxTurns)
        {
            switch (PlayersTurn)
            {
                case 1:
                    PlayersTurn = 2;
                    turnCounter++;

                    if (p2Base.GetType() == typeof(SM_Player))
                    {
                        smController.isStateMachineTurn = true;
                        smController.InitiateStateMachineTurn(p2Base, p1Base, MaxTurns - turnCounter);
                    }

                    p2Base.GenerateIncomePerTurn();

                    break;
                case 2:
                    PlayersTurn = 1;
                    turnCounter++;

                    if (p1Base.GetType() == typeof(SM_Player))
                    {
                        smController.isStateMachineTurn = true;
                        smController.InitiateStateMachineTurn(p1Base, p2Base, MaxTurns - turnCounter);
                    }

                    p1Base.GenerateIncomePerTurn();

                    break;
            }

            fleetController.ResetShipActions();
        }
        else
        {
            if (p1Base.TotalMoney > p2Base.TotalMoney)
            {
                winner = 1;
            }
            else if (p2Base.TotalMoney > p1Base.TotalMoney)
            {
                winner = 2;
            }
            else
            {
                winner = 3;
            }

            gameOver = true;
            endGameProtocal();
        }
    } //REDO

    bool turnEndCheck()
    {
        if (PlayersTurn == 1)
        {
            int shipsThatNeedToMove = 0;

            foreach (Ship_Base sB in p1Base.Fleet)
            {
                if (sB.actionsLeft != 0)
                    shipsThatNeedToMove++;
            }

            if (shipsThatNeedToMove == 0)
            {
                return true;
            }
            else return false;
        }
        else
        {
            int shipsThatNeedToMove = 0;

            foreach (Ship_Base sB in p2Base.Fleet)
            {
                if (sB.actionsLeft != 0)
                    shipsThatNeedToMove++;
            }

            if (shipsThatNeedToMove == 0)
            {
                return true;
            }
            else return false;
        }
    }
    
    void TimeController()
    {
        secondCounter += Time.deltaTime;

        if (secondCounter > 1)
        {
            if (seconds < 59)
            {
                seconds++;
            }
            else
            {
                minutes++;
                seconds = 0;
            }

            secondCounter = 0;
        }

        infManager.TimeUpdater(minutes, seconds);
    }

    void TurnCounterController()
    {
        infManager.TurnCounterUpdate(turnCounter);
    }

    public void canEndTurn()
    {
        if (turnEndCheck() == true)
        {
            if (PlayersTurn == 1)
            {
                if (p1Base.GetType() != typeof(SM_Player))
                {
                    infManager.nextTurn.interactable = true;
                }
            }
            else if (PlayersTurn == 2)
            {
                if (p2Base.GetType() != typeof(SM_Player))
                {
                    infManager.nextTurn.interactable = true;
                }
            }
        }
    }

    void endGameProtocal()
    {
        //if (GameManager.instance.isAIgame == true)
        //{
        //    if (GameManager.instance.AIgameType == "Evo")
        //    {
        //        smController.EvolutionaryLearning();
        //    }
        //}

        icManager.HideUI();
        infManager.HideUI();
        sUIController.hideShipUI();
        conController.HideConsole();

        egcController.showUI();
    }

    public void Start2v2StateMachine()
    {
        smController.InitiateStateMachineTurn(p1Base, p2Base, MaxTurns - turnCounter);
    }
}
