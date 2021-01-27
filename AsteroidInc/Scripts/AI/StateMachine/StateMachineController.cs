using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class StateMachineController : MonoBehaviour
{
    [Header("State Player")]
    SM_Player smPlayer;
    PlayerBase smOpponent;

    [Header("State Machine Booleans:")]
    public bool isStateMachineTurn = false;
    int state = 0;

    [Header ("Ship movement variables:")]
    Ship_Base selectedUnit = null;
    bool hasAssignedPath = false;
    public List<Hexagon> shipPath;
    int shipPathIndex = 1;
    //bool stateMachineMove = false;

    [Header("State Sections:")]
    //MOVES AND PREOFRMS ALL ACTIONS FOR SHIPS
    bool preformedShipActions = false;
    //PREFORMANCES UTILITY FUNCTIONS THAT A PLAYER WOULD,AN EXAMPLE BEING TRAINING NEW SHIPS
    bool preformedAIPlayerActions = false;

    [Header("SM UI:")]
    public Text efText;

    double gameNumber = 0;
    double s1Min = 0;
    double s1Max = 0;
    double s2Min = 0;
    double s2Max = 0;
    double s3Min = 0;
    double s3Max = 0;
    double s4Min = 0;
    double s4Max = 0;

    int s1Resources = 0;
    int s2Resources = 0;
    int s3Resources = 0;
    int s4Resources = 0;

    PlayManager pManager;
    // Start is called before the first frame update
    void Start()
    {
        pManager = GameObject.FindGameObjectWithTag("PlayManager").GetComponent<PlayManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //INITIATES A STATE MACHINE PLAYERS TURN
    public void InitiateStateMachineTurn(PlayerBase pObject, PlayerBase opponent, int turnsLeft)
    {
        smPlayer = (SM_Player)pObject;
        smOpponent = opponent;

        if (GameManager.instance.AIgameType == "Evo")
        {
            if (pManager.PlayersTurn == 2)
            {
                ObtainEvolutionaryWeights(); //ADD IN ONLY IF CHOSEN
            }
        }

        state = AssignState(smPlayer.EvaluationFunction(opponent, turnsLeft));
        efText.text = "EF: " +  smPlayer.EvaluationFunction(opponent, turnsLeft).ToString();

        isStateMachineTurn = true;
    }

    int AssignState(double efValue)
    {
        if (GameManager.instance.AIgameType == "SM" || GameManager.instance.AIgameType == "Evo" && pManager.PlayersTurn == 1)
        {
            if (GameManager.instance.AIDifficulty == "Easy")
            {
                if (efValue <= 1 && efValue > 0.2)
                {
                    //BEST STATE TO BE IN
                    return 1;
                }
                else if (efValue <= 0.2 && efValue > -0.6)
                {
                    //GOOD STATE TO BE IN
                    return 2;
                }
                else if (efValue <= -0.6 && efValue > -0.8)
                {
                    //BAD STATE TO BE IN
                    return 3;
                }
                else if (efValue <= -0.8 && efValue > -1)
                {
                    //WORST STATE TO BE IN
                    return 4;
                }
            }
            else if (GameManager.instance.AIDifficulty == "Intermediate")
            {
                if (efValue <= 1 && efValue > 0.4)
                {
                    //BEST STATE TO BE IN
                    return 1;
                }
                else if (efValue <= 0.4 && efValue > -0.1)
                {
                    //GOOD STATE TO BE IN
                    return 2;
                }
                else if (efValue <= -0.1 && efValue > -0.6)
                {
                    //BAD STATE TO BE IN
                    return 3;
                }
                else if (efValue <= -0.6 && efValue > -1)
                {
                    //WORST STATE TO BE IN
                    return 4;
                }
            }
            else if (GameManager.instance.AIDifficulty == "Hard")
            {
                if (efValue <= 1 && efValue > 0.6)
                {
                    //BEST STATE TO BE IN
                    return 1;
                }
                else if (efValue <= 0.6 && efValue > 0.1)
                {
                    //GOOD STATE TO BE IN
                    return 2;
                }
                else if (efValue <= 0.1 && efValue > -0.6)
                {
                    //BAD STATE TO BE IN
                    return 3;
                }
                else if (efValue <= -0.6 && efValue > -1)
                {
                    //WORST STATE TO BE IN
                    return 4;
                }
            }
        }
        else if (GameManager.instance.AIgameType == "Evo" && pManager.PlayersTurn == 2)
        {
            if (efValue <= s1Min && efValue > s1Max)
            {
                //BEST STATE TO BE IN
                return 1;
            }
            else if (efValue <= s2Min && efValue > s2Max)
            {
                //GOOD STATE TO BE IN
                Debug.Log("State 2");
                return 2;
            }
            else if (efValue <= s3Min && efValue > s3Max)
            {
                //BAD STATE TO BE IN
                return 3;
            }
            else if (efValue <= s4Min && efValue > s4Max)
            {
                //WORST STATE TO BE IN
                return 4;
            }
        }
        return 0;
    }

    private void FixedUpdate()
    {
        if (isStateMachineTurn == true)
        {
            stateMachine();
        }
        else
        {
            efText.text = "";
        }
    }

    void ObtainEvolutionaryWeights()
    {
        string path = Application.dataPath + "/Training/" + "EvolutionTraining.txt";
        string[] data = File.ReadAllLines(path);

        gameNumber = double.Parse(data[data.Length - 14].Substring(6));

        List<string> LastGamesData = new List<string>();
        for (int i = data.Length-13; i < data.Length - 1; i++)
        {
            LastGamesData.Add(data[i]);
        }

        s1Min = double.Parse(LastGamesData[4].Substring(6));
        s1Max = double.Parse(LastGamesData[5].Substring(6));
        s2Min = double.Parse(LastGamesData[6].Substring(6));
        s2Max = double.Parse(LastGamesData[7].Substring(6));
        s3Min = double.Parse(LastGamesData[8].Substring(6));
        s3Max = double.Parse(LastGamesData[9].Substring(6));
        s4Min = double.Parse(LastGamesData[10].Substring(6));
        s4Max = double.Parse(LastGamesData[11].Substring(6));

        string debugString = "Obtained Weights: \n" + s1Min + "\n" + s1Max + "\n" + s2Min + "\n" + s2Max + "\n" + s3Min + "\n" + s3Max + "\n" + s4Min + "\n" + s4Max;
        Debug.Log(debugString);
    } //PROBLEM NOT HERE

    void WriteTrainedData()
    {
        string[] newData = new string[14];

        gameNumber++;
        newData[0] = "**GAME " + gameNumber.ToString(); 
        newData[1] = "S1R " + s1Resources.ToString();
        newData[2] = "S2R " + s2Resources.ToString();
        newData[3] = "S3R " + s3Resources.ToString();
        newData[4] = "S4R " + s4Resources.ToString();

        newData[5] = "S1Min " + s1Min.ToString();
        newData[6] = "S1Max " + s1Max.ToString();
        newData[7] = "S2Min " + s2Min.ToString();
        newData[8] = "S2Max " + s2Max.ToString();
        newData[9] = "S3Min " + s3Min.ToString();
        newData[10] = "S3Max " + s3Max.ToString();
        newData[11] = "S4Min " + s4Min.ToString();
        newData[12] = "S4Max " + s4Max.ToString();
        newData[13] = "*****************";

        for (int i = 0; i < 14; i++)
        {
            using (StreamWriter writer = new StreamWriter("Assets/EvolutionTraining.txt", true)) //// true to append data to the file
            {
                writer.WriteLine(newData[i]);
            }
        }

        //File.WriteAllLines("Assets/EvolutionTraining.txt", newData);
    }

    public void EvolutionaryLearning()
    {
        int[] Resources = new int[4];
        int[] stateReference = new int[4];

        Resources[0] = s1Resources;
        stateReference[0] = 1;
        Resources[1] = s2Resources;
        stateReference[1] = 2;
        Resources[2] = s3Resources;
        stateReference[2] = 3;
        Resources[3] = s4Resources;
        stateReference[3] = 4;

        int tempResources;
        int tempReference;
        for (int x = 0; x < Resources.Length - 1; x++)
        {
            for (int y = x + 1; y < Resources.Length; y++)
            {
                if (Resources[x] < Resources[y])
                {
                    tempResources = Resources[x];
                    Resources[x] = Resources[y];
                    Resources[y] = tempResources;

                    tempReference = stateReference[x];
                    stateReference[x] = stateReference[y];
                    stateReference[y] = tempReference;
                }
            }
        }

        //BEST STATE
        if (stateReference[0] == 1) //STATE 1
        {
            if (stateReference[3] == 2)
            {
                s1Max -= 0.005;
                s2Min = s1Max;
            }
            else if (stateReference[3] == 3)
            {
                s1Max -= 0.005;
                s2Min = s1Max;
                s2Max -= 0.005;
                s3Min = s2Max;
            }
            else if (stateReference[3] == 4)
            {
                s1Max -= 0.005;
                s2Min = s1Max;
                s2Max -= 0.005;
                s3Min = s2Max;
                s3Max -= 0.005;
                s4Min = s3Max;
            }
        }
        else if (stateReference[0] == 2) //STATE 1
        {
            if (stateReference[3] == 1)
            {
                s1Max += 0.005;
                s2Min = s1Max;
            }
            else if (stateReference[3] == 3)
            { 
                s2Max -= 0.005;
                s3Min = s2Max;
            }
            else if (stateReference[3] == 4)
            {
                s2Max -= 0.005;
                s3Min = s2Max;
                s3Max -= 0.005;
                s4Min = s3Max;
            }
        }
        else if (stateReference[0] == 3) //STATE 1
        {
            if (stateReference[3] == 1)
            {
                s3Min += 0.005;
                s2Max = s3Min;
                s2Min += 0.005;
                s1Max = s2Min;
            }
            else if (stateReference[3] == 2)
            {
                s3Min += 0.005;
                s2Max = s3Min;
            }
            else if (stateReference[3] == 4)
            {
                s3Max -= 0.005;
                s4Min = s3Max;
            }
        }
        else if (stateReference[0] == 4) //STATE 1
        {
            if (stateReference[3] == 1)
            {
                s4Min += 0.005;
                s3Max = s4Min;
                s3Min += 0.005;
                s2Max = s3Min;
                s2Min += 0.005;
                s1Max = s2Min;
            }
            else if (stateReference[3] == 2)
            {
                s4Min += 0.005;
                s3Max = s4Min;
                s3Min += 0.005;
                s2Max = s3Min;
            }
            else if (stateReference[3] == 3)
            {
                s4Min += 0.005;
                s3Max = s4Min;
            }
        }

        WriteTrainedData();
        Debug.Log("#############");
    }

    void stateMachine()
    {
        switch(state)
        {
            case 1:
                state1();
                break;
            case 2:
                state2();
                break;
            case 3:
                state3();
                break;
            case 4:
                state4();
                break;
        }
    }

    //BETWEEN 0.5-1
    void state1()
    {
        if (preformedShipActions == false)
        {
            if (smShipsToMove() > 0)
            {
                //SELECTS A SHIP THAT STILL HAS ACTIONS
                if (selectedUnit == null)
                {
                    smShipSelector();
                }
                else
                {
                    if (selectedUnit.actionsLeft != 0)
                    {
                        if (selectedUnit.GetType() == typeof(MiningShip))
                        {
                            sm1MinerLogic();
                        }
                        else if (selectedUnit.GetType() == typeof(DestroyerShip))
                        {
                            sm1DestroyerLogic();
                        }
                    }
                    else
                    {
                        smShipUnselector();
                    }
                }
            }
            else
            {
                preformedShipActions = true;
                smShipUnselector();
                pManager.hexGridController.resetBoard();
            }
        }
        else if (preformedAIPlayerActions == false)
        {
            smS1PlayerLogic();
        }
        else
        {
            isStateMachineTurn = false;

            preformedShipActions = false;
            preformedAIPlayerActions = false;

            pManager.EndTurn();
        }
    }

    //BETWEEN 0-0.5
    void state2()
    {
        if (preformedShipActions == false)
        {
            if (smShipsToMove() > 0)
            {
                //SELECTS A SHIP THAT STILL HAS ACTIONS
                if (selectedUnit == null)
                {
                    smShipSelector();
                }
                else
                {
                    if (selectedUnit.actionsLeft != 0)
                    {
                        if (selectedUnit.GetType() == typeof(MiningShip))
                        {
                            sm2MinerLogic();
                        }
                        else if (selectedUnit.GetType() == typeof(DestroyerShip))
                        {
                            sm2DestroyerLogic();
                        }
                    }
                    else
                    {
                        smShipUnselector();
                    }
                }
            }
            else
            {
                preformedShipActions = true;
                smShipUnselector();
                pManager.hexGridController.resetBoard();
            }
        }
        else if (preformedAIPlayerActions == false)
        {
            smS2PlayerLogic();
        }
        else
        {
            isStateMachineTurn = false;

            preformedShipActions = false;
            preformedAIPlayerActions = false;

            pManager.EndTurn();
        }
    }

    //BETWEEN 0-(-0.5)
    void state3()
    {
        if (preformedShipActions == false)
        {
            if (smShipsToMove() > 0)
            {
                //SELECTS A SHIP THAT STILL HAS ACTIONS
                if (selectedUnit == null)
                {
                    smShipSelector();
                }
                else
                {
                    if (selectedUnit.actionsLeft != 0)
                    {
                        if (selectedUnit.GetType() == typeof(MiningShip))
                        {
                            sm3TempMinerLogic();
                        }
                        else if (selectedUnit.GetType() == typeof(DestroyerShip))
                        {
                            sm3DestroyerLogic();
                        }
                    }
                    else
                    {
                        smShipUnselector();
                    }
                }
            }
            else
            {
                preformedShipActions = true;
                smShipUnselector();
                pManager.hexGridController.resetBoard();
            }
        }
        else if (preformedAIPlayerActions == false)
        {
            smS3PlayerLogic();
        }
        else
        {
            isStateMachineTurn = false;

            preformedShipActions = false;
            preformedAIPlayerActions = false;

            pManager.EndTurn();
        }
    }

    //BETWEEN (0.5)-(-1)
    void state4()
    {
        if (preformedShipActions == false)
        {
            if (smShipsToMove() > 0)
            {
                //SELECTS A SHIP THAT STILL HAS ACTIONS
                if (selectedUnit == null)
                {
                    smShipSelector();
                }
                else
                {
                    if (selectedUnit.actionsLeft != 0)
                    {
                        if (selectedUnit.GetType() == typeof(MiningShip))
                        {
                            //sm4MinerLogic();
                            sm4TempMinerLogic();
                        }
                        else if (selectedUnit.GetType() == typeof(DestroyerShip))
                        {
                            sm4DestroyerLogic();
                        }
                    }
                    else
                    {
                        smShipUnselector();
                    }
                }
            }
            else
            {
                preformedShipActions = true;
                smShipUnselector();
                pManager.hexGridController.resetBoard();
            }
        }
        else if (preformedAIPlayerActions == false)
        {
            smS4PlayerLogic();
        }
        else
        {
            isStateMachineTurn = false;

            preformedShipActions = false;
            preformedAIPlayerActions = false;

            pManager.EndTurn();
        }
    }

    //SHIP SELECTION
    //SELECTS A SHIP FOR THE STATE MACHINE TO WORK WITH
    void smShipSelector()
    {
        foreach (Ship_Base ship in smPlayer.Fleet)
        {
            if (ship.actionsLeft != 0)
            {
                selectedUnit = ship;
                selectedUnit.selectedObject.SetActive(true);
                break;
            }
        }
    }

    //UNSELECTS A SHIP FOR THE STATE MACHINE
    void smShipUnselector()
    {
        if (selectedUnit != null)
        {
            selectedUnit.selectedObject.SetActive(false);
            selectedUnit = null;

            hasAssignedPath = false;
            shipPathIndex = 1;
        }
    }

    int smShipsToMove()
    {
        int numberOfShipsLeft = 0;

        foreach (Ship_Base ship in smPlayer.Fleet)
        {
            if (ship.actionsLeft > 0)
            {
                numberOfShipsLeft++;
            }
        }

        return numberOfShipsLeft;
    }

    //SHIP MOVEMENT
    void getPath(GameObject destinationObject)
    {
        pManager.hexGridController.resetBoard();

        GameObject start = pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].HexagonObject;
        GameObject destination = destinationObject;

        shipPath = new List<Hexagon>();
        shipPath = pManager.hexGridController.getShortestPath(start, destination);
        pManager.hexGridController.changeSelectedMaterial(shipPath, selectedUnit.actionsLeft);
    }

    void smMoveShip()
    {
        if (selectedUnit.actionsLeft > 0 && shipPathIndex != shipPath.Count)
        {
            if (shipPath[shipPathIndex].MineableInHex != null)
            {
                if (selectedUnit.GetType() == typeof(MiningShip))
                {
                    if (Vector3.Distance(selectedUnit.selectedObject.transform.position, new Vector3(shipPath[shipPathIndex].HexagonObject.transform.position.x, 10.5f, shipPath[shipPathIndex].HexagonObject.transform.position.z)) > 0.5f)
                    {
                        selectedUnit.shipObject.transform.position = Vector3.MoveTowards(selectedUnit.shipObject.transform.position, new Vector3(shipPath[shipPathIndex].HexagonObject.transform.position.x, 10.5f, shipPath[shipPathIndex].HexagonObject.transform.position.z), 0.5f);
                    }
                    else
                    {
                        shipPathIndex += 1;
                        selectedUnit.actionsLeft--;

                        selectedUnit.hexX = pManager.hexGridController.getHexIndex(shipPath[shipPathIndex - 1])[0];
                        selectedUnit.hexZ = pManager.hexGridController.getHexIndex(shipPath[shipPathIndex - 1])[1];
                    }
                }
                else if (selectedUnit.GetType() == typeof(DestroyerShip))
                {
                    if (Vector3.Distance(selectedUnit.selectedObject.transform.position, new Vector3(shipPath[shipPathIndex].HexagonObject.transform.position.x, 10.5f, shipPath[shipPathIndex].HexagonObject.transform.position.z)) > 1.2f)
                    {
                        selectedUnit.shipObject.transform.position = Vector3.MoveTowards(selectedUnit.shipObject.transform.position, new Vector3(shipPath[shipPathIndex].HexagonObject.transform.position.x, 10.5f, shipPath[shipPathIndex].HexagonObject.transform.position.z), 0.5f);
                    }
                    else
                    {
                        shipPathIndex += 1;
                        selectedUnit.actionsLeft--;

                        selectedUnit.hexX = pManager.hexGridController.getHexIndex(shipPath[shipPathIndex - 1])[0];
                        selectedUnit.hexZ = pManager.hexGridController.getHexIndex(shipPath[shipPathIndex - 1])[1];
                    }
                }

                selectedUnit.shipObject.transform.LookAt(new Vector3(shipPath[shipPathIndex].HexagonObject.transform.position.x, selectedUnit.shipObject.transform.position.y, shipPath[shipPathIndex].HexagonObject.transform.position.z));
            }
            else
            {
                if (selectedUnit.GetType() == typeof(MiningShip))
                {
                    if (Vector3.Distance(selectedUnit.selectedObject.transform.position, shipPath[shipPathIndex].HexagonObject.transform.position) > 0.5f)
                    {
                        selectedUnit.shipObject.transform.position = Vector3.MoveTowards(selectedUnit.shipObject.transform.position, shipPath[shipPathIndex].HexagonObject.transform.position, 0.5f);
                    }
                    else
                    {
                        shipPathIndex += 1;
                        selectedUnit.actionsLeft--;

                        selectedUnit.hexX = pManager.hexGridController.getHexIndex(shipPath[shipPathIndex - 1])[0];
                        selectedUnit.hexZ = pManager.hexGridController.getHexIndex(shipPath[shipPathIndex - 1])[1];
                    }
                }
                else if (selectedUnit.GetType() == typeof(DestroyerShip))
                {
                    if (Vector3.Distance(selectedUnit.selectedObject.transform.position, shipPath[shipPathIndex].HexagonObject.transform.position) > 1.2f)
                    {
                        selectedUnit.shipObject.transform.position = Vector3.MoveTowards(selectedUnit.shipObject.transform.position, shipPath[shipPathIndex].HexagonObject.transform.position, 0.5f);
                    }
                    else
                    {
                        shipPathIndex += 1;
                        selectedUnit.actionsLeft--;

                        selectedUnit.hexX = pManager.hexGridController.getHexIndex(shipPath[shipPathIndex - 1])[0];
                        selectedUnit.hexZ = pManager.hexGridController.getHexIndex(shipPath[shipPathIndex - 1])[1];
                    }
                }

                selectedUnit.shipObject.transform.LookAt(new Vector3(shipPath[shipPathIndex].HexagonObject.transform.position.x, selectedUnit.shipObject.transform.position.y, shipPath[shipPathIndex].HexagonObject.transform.position.z));
            }
        }
        else
        {
            selectedUnit.hexX = pManager.hexGridController.getHexIndex(shipPath[shipPathIndex - 1])[0];
            selectedUnit.hexZ = pManager.hexGridController.getHexIndex(shipPath[shipPathIndex - 1])[1];

            shipPathIndex = 1;
            hasAssignedPath = false;
            pManager.hexGridController.resetBoard();
        }
    }

    //SHIP ACTIONS
    void smEstablishMine()
    {
        if (pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].MineableInHex != null)
        {
            smPlayer.MineablesOwned.Add(pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].MineableInHex);

            if (GameManager.instance.AIgameType == "Evo")
            {
                if (pManager.PlayersTurn == 2)
                {
                    switch (state)
                    {
                        case 1:
                            s1Resources += pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].MineableInHex.ResourcesAvailable;
                            break;
                        case 2:
                            s2Resources += pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].MineableInHex.ResourcesAvailable;
                            break;
                        case 3:
                            s3Resources += pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].MineableInHex.ResourcesAvailable;
                            break;
                        case 4:
                            s4Resources += pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].MineableInHex.ResourcesAvailable;
                            break;
                    }
                }
            }
            pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].MineableInHex.Owned = smPlayer;
            Debug.Log(pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].MineableInHex.Owned.playerID);
        }
        selectedUnit.actionsLeft--;
    }

    void smAttack(GameObject hexObject)
    {
        Hexagon enemyHex = pManager.hexGridController.FindHexagonBasedOnGameobject(hexObject);
        //Retuns a ship based on a hexagon, we can assume the ship returned is always an enemy ship
        Ship_Base enemyShip = pManager.hexGridController.getAnyShipInHexagon(enemyHex);

        enemyShip.health -= selectedUnit.attack;
        pManager.gsManager.FightPlay();
        pManager.vpManager.CreateDamagePopUp(enemyShip.shipObject.transform.position, selectedUnit.attack);
        if (enemyShip.health <= 0)
        {
            smOpponent.Fleet.Remove(enemyShip);
            Destroy(enemyShip.shipObject);
            pManager.gsManager.ExplosionPlay();
        }

        selectedUnit.health -= (selectedUnit.defence - enemyShip.attack);
        pManager.vpManager.CreateDamagePopUp(selectedUnit.shipObject.transform.position, enemyShip.attack - selectedUnit.defence);
        if (selectedUnit.health <= 0)
        {
            selectedUnit.actionsLeft = 0;
            smPlayer.Fleet.Remove(selectedUnit);
            Destroy(selectedUnit.shipObject);
            selectedUnit = null;
        }
        else
        {
            selectedUnit.actionsLeft--;
        }
    }

    //SHIP LOGIC
    //LOOK FOR ANY TYPE OF MINEABLE THAT ISNT OWNED
    void sm1MinerLogic()
    {
        float closestMineableDistance = 0;
        Hexagon closestHexWithMineable = null;

        foreach (Hexagon hex in pManager.hexGridController.GridObjects)
        {
            if (hex != null)
            {
                if (hex.MineableInHex != null)
                {
                    if (hex.MineableInHex.Owned == null)
                    {
                        if (pManager.hexGridController.isClosestToHex(hex, selectedUnit, smPlayer) == true)
                        {
                            if (closestHexWithMineable == null)
                            {
                                closestHexWithMineable = hex;
                                closestMineableDistance = Mathf.Abs(pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerX - hex.centerX) + Mathf.Abs(pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerZ - hex.centerZ);
                            }
                            else
                            {
                                if (closestMineableDistance > Mathf.Abs(pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerX - hex.centerX) + Mathf.Abs(pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerZ - hex.centerZ))
                                {
                                    closestHexWithMineable = hex;
                                    closestMineableDistance = Mathf.Abs(pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerX - hex.centerX) + Mathf.Abs(pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerZ - hex.centerZ);
                                }
                            }
                        }
                    }
                }
            }
        }

        if (closestMineableDistance > 5 && selectedUnit != null)
        {
            if (hasAssignedPath == false)
            {
                getPath(closestHexWithMineable.HexagonObject);
                hasAssignedPath = true;
            }

            smMoveShip();
        }
        else
        {
            smEstablishMine();
        }
    }

    //PRIOROTIZE TAKING OVER PLANETS OVER ASTEROIDS
    void sm2MinerLogic()
    {
        float closestMineableDistance = 0;
        Hexagon closestHexWithMineable = null;

        //foreach (Hexagon hex in pManager.hexGridController.GridObjects)
        //{
        //    if (hex != null)
        //    {
        //        if (hex.MineableInHex != null)
        //        {
        //            //if (pManager.hexGridController.closestMineableInRange(smPlayer, selectedUnit, 60) != null && pManager.hexGridController.closestMineableInRange(smPlayer, selectedUnit, 25).MineableInHex.Owned != smPlayer)
        //            //{
        //            //if (closestHexWithMineable == null)
        //            //{
        //            //    closestHexWithMineable = hex;
        //            //    closestMineableDistance = Mathf.Abs(pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerX - hex.centerX) + Mathf.Abs(pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerZ - hex.centerZ);
        //            //}
        //            //else
        //            //{
        //            //    if (closestMineableDistance > Mathf.Abs(pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerX - hex.centerX) + Mathf.Abs(pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerZ - hex.centerZ))
        //            //    {
        //            //        closestHexWithMineable = hex;
        //            //        closestMineableDistance = Mathf.Abs(pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerX - hex.centerX) + Mathf.Abs(pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerZ - hex.centerZ);

        //            //    }
        //            //}
        //            //}
        //            //else 
        //            if (pManager.hexGridController.GetNumberOfUnownedPlanets() > 0)
        //            {
        //                if (hex.MineableInHex.GetType() == typeof(Planet))
        //                {
        //                    if (hex.MineableInHex.Owned == null)
        //                    {
        //                        if (pManager.hexGridController.isClosestToHex(hex, selectedUnit, smPlayer) == true)
        //                        {
        //                            if (closestHexWithMineable == null)
        //                            {
        //                                closestHexWithMineable = hex;
        //                                closestMineableDistance = Mathf.Abs(pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerX - hex.centerX) + Mathf.Abs(pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerZ - hex.centerZ);
        //                            }
        //                            else
        //                            {
        //                                if (closestMineableDistance > Mathf.Abs(pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerX - hex.centerX) + Mathf.Abs(pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerZ - hex.centerZ))
        //                                {
        //                                    closestHexWithMineable = hex;
        //                                    closestMineableDistance = Mathf.Abs(pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerX - hex.centerX) + Mathf.Abs(pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerZ - hex.centerZ);
        //                                }
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                if (pManager.hexGridController.isClosestToHex(hex, selectedUnit, smPlayer) == true)
        //                {
        //                    if (hex.MineableInHex.Owned == null)
        //                    {
        //                        if (closestHexWithMineable == null)
        //                        {
        //                            closestHexWithMineable = hex;
        //                            closestMineableDistance = Mathf.Abs(pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerX - hex.centerX) + Mathf.Abs(pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerZ - hex.centerZ);
        //                        }
        //                        else
        //                        {
        //                            if (closestMineableDistance > Mathf.Abs(pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerX - hex.centerX) + Mathf.Abs(pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerZ - hex.centerZ))
        //                            {
        //                                closestHexWithMineable = hex;
        //                                closestMineableDistance = Mathf.Abs(pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerX - hex.centerX) + Mathf.Abs(pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerZ - hex.centerZ);
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}

        foreach (Hexagon hex in pManager.hexGridController.GridObjects)
        {
            if (hex != null)
            {
                if (hex.MineableInHex != null)
                {
                    if (hex.MineableInHex.Owned == null)
                    {
                        if (pManager.hexGridController.isClosestToHex(hex, selectedUnit, smPlayer) == true)
                        {
                            if (closestHexWithMineable == null)
                            {
                                closestHexWithMineable = hex;
                                closestMineableDistance = Mathf.Abs(pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerX - hex.centerX) + Mathf.Abs(pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerZ - hex.centerZ);
                            }
                            else
                            {
                                if (closestMineableDistance > Mathf.Abs(pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerX - hex.centerX) + Mathf.Abs(pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerZ - hex.centerZ))
                                {
                                    closestHexWithMineable = hex;
                                    closestMineableDistance = Mathf.Abs(pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerX - hex.centerX) + Mathf.Abs(pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerZ - hex.centerZ);
                                }
                            }
                        }
                    }
                }
            }
        }

        if (closestMineableDistance > 5 && selectedUnit != null)
        {
            if (hasAssignedPath == false)
            {
                getPath(closestHexWithMineable.HexagonObject);
                hasAssignedPath = true;
            }

            smMoveShip();
        }
        else
        {
            smEstablishMine();
        }
    }

    //PRIOROTIZE TAKING OVER ENEMY OWNED MINEABLES
    void sm3MinerLogic()
    {
        float closestMineableDistance = 0;
        Hexagon closestHexWithMineable = null;

        foreach (Hexagon hex in pManager.hexGridController.GridObjects)
        {
            if (hex != null)
            {
                if (hex.MineableInHex != null)
                {
                    if (pManager.hexGridController.GetNumberOfEnemyOwnedMinables(smOpponent) > 0 )
                    {
                        if (pManager.hexGridController.closestMineableInRange(smPlayer, selectedUnit, 60) != null)
                        {
                            if (hex == pManager.hexGridController.closestMineableInRange(smPlayer, selectedUnit, 25))
                            {
                                if (closestHexWithMineable == null)
                                {
                                    closestHexWithMineable = hex;
                                    closestMineableDistance = Mathf.Abs(pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerX - hex.centerX) + Mathf.Abs(pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerZ - hex.centerZ);
                                }
                                else
                                {
                                    if (closestMineableDistance > Mathf.Abs(pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerX - hex.centerX) + Mathf.Abs(pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerZ - hex.centerZ))
                                    {
                                        closestHexWithMineable = hex;
                                        closestMineableDistance = Mathf.Abs(pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerX - hex.centerX) + Mathf.Abs(pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerZ - hex.centerZ);

                                    }
                                }
                            }
                        }
                        else
                        {
                            if (pManager.hexGridController.isClosestToHex(hex, selectedUnit, smPlayer) == true)
                            {
                                if (hex.MineableInHex.Owned == smOpponent)
                                {
                                    if (closestHexWithMineable == null)
                                    {
                                        closestHexWithMineable = hex;
                                        closestMineableDistance = Mathf.Abs(pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerX - hex.centerX) + Mathf.Abs(pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerZ - hex.centerZ);
                                    }
                                    else
                                    {
                                        if (closestMineableDistance > Mathf.Abs(pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerX - hex.centerX) + Mathf.Abs(pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerZ - hex.centerZ))
                                        {
                                            closestHexWithMineable = hex;
                                            closestMineableDistance = Mathf.Abs(pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerX - hex.centerX) + Mathf.Abs(pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerZ - hex.centerZ);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (pManager.hexGridController.GetNumberOfUnownedPlanets() > 0)
                        {
                            if (hex.MineableInHex.Owned == null)
                            {
                                if (hex.MineableInHex.GetType() == typeof(Planet))
                                {
                                    if (pManager.hexGridController.isClosestToHex(hex, selectedUnit, smPlayer) == true)
                                    {
                                        if (closestHexWithMineable == null)
                                        {
                                            closestHexWithMineable = hex;
                                            closestMineableDistance = Mathf.Abs(pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerX - hex.centerX) + Mathf.Abs(pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerZ - hex.centerZ);
                                        }
                                        else
                                        {
                                            if (closestMineableDistance > Mathf.Abs(pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerX - hex.centerX) + Mathf.Abs(pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerZ - hex.centerZ))
                                            {
                                                closestHexWithMineable = hex;
                                                closestMineableDistance = Mathf.Abs(pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerX - hex.centerX) + Mathf.Abs(pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerZ - hex.centerZ);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (hex.MineableInHex.Owned == null)
                            {
                                if (pManager.hexGridController.isClosestToHex(hex, selectedUnit, smPlayer) == true)
                                {
                                    if (closestHexWithMineable == null)
                                    {
                                        closestHexWithMineable = hex;
                                        closestMineableDistance = Mathf.Abs(pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerX - hex.centerX) + Mathf.Abs(pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerZ - hex.centerZ);
                                    }
                                    else
                                    {
                                        if (closestMineableDistance > Mathf.Abs(pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerX - hex.centerX) + Mathf.Abs(pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerZ - hex.centerZ))
                                        {
                                            closestHexWithMineable = hex;
                                            closestMineableDistance = Mathf.Abs(pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerX - hex.centerX) + Mathf.Abs(pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerZ - hex.centerZ);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        if (closestMineableDistance > 5 && selectedUnit != null)
        {
            if (hasAssignedPath == false)
            {
                getPath(closestHexWithMineable.HexagonObject);
                hasAssignedPath = true;
            }

            smMoveShip();
        }
        else
        {
            smEstablishMine();
        }
    }

    void sm3TempMinerLogic()
    {
        float closestMineableDistance = 0;
        Hexagon closestHexWithMineable = null;

        foreach (Hexagon hex in pManager.hexGridController.GridObjects)
        {
            if (hex != null)
            {
                if (hex.MineableInHex != null)
                {
                    //CHECKS TO SEE IF THERE IS A MINEABLE IN CLOSE PROXIMITY
                    if (pManager.hexGridController.closestMineableInRange(smPlayer, selectedUnit, 120) != null && pManager.hexGridController.closestMineableInRange(smPlayer, selectedUnit, 120).MineableInHex.Owned != smPlayer)
                    {
                        //CHECKS TO SEE IF MINEABLE FALLS INTO THAT PROXIMITY
                        if (Mathf.Abs(pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerX - hex.centerX) + Mathf.Abs(pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerZ - hex.centerZ) < 120)
                        {
                            if (hex.MineableInHex.Owned != smPlayer)
                            {
                                if (closestHexWithMineable == null)
                                {
                                    closestHexWithMineable = hex;
                                    closestMineableDistance = Mathf.Abs(pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerX - hex.centerX) + Mathf.Abs(pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerZ - hex.centerZ);
                                }
                                else
                                {
                                    if (closestMineableDistance > Mathf.Abs(pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerX - hex.centerX) + Mathf.Abs(pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerZ - hex.centerZ))
                                    {
                                        closestHexWithMineable = hex;
                                        closestMineableDistance = Mathf.Abs(pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerX - hex.centerX) + Mathf.Abs(pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerZ - hex.centerZ);
                                    }
                                }
                            }
                        }
                    }
                    else if (pManager.hexGridController.GetNumberOfEnemyOwnedMinables(smOpponent) > 0)
                    {
                        if (hex.MineableInHex.Owned == smOpponent)
                        {
                            if (closestHexWithMineable == null)
                            {
                                closestHexWithMineable = hex;
                                closestMineableDistance = Mathf.Abs(pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerX - hex.centerX) + Mathf.Abs(pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerZ - hex.centerZ);
                            }
                            else
                            {
                                if (closestMineableDistance > Mathf.Abs(pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerX - hex.centerX) + Mathf.Abs(pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerZ - hex.centerZ))
                                {
                                    closestHexWithMineable = hex;
                                    closestMineableDistance = Mathf.Abs(pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerX - hex.centerX) + Mathf.Abs(pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerZ - hex.centerZ);
                                }
                            }
                        }
                    }
                    else
                    {
                        if (hex.MineableInHex.Owned != smPlayer)
                        {
                            if (closestHexWithMineable == null)
                            {
                                closestHexWithMineable = hex;
                                closestMineableDistance = Mathf.Abs(pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerX - hex.centerX) + Mathf.Abs(pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerZ - hex.centerZ);
                            }
                            else
                            {
                                if (closestMineableDistance > Mathf.Abs(pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerX - hex.centerX) + Mathf.Abs(pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerZ - hex.centerZ))
                                {
                                    closestHexWithMineable = hex;
                                    closestMineableDistance = Mathf.Abs(pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerX - hex.centerX) + Mathf.Abs(pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerZ - hex.centerZ);
                                }
                            }
                        }
                    }
                }
            }
        }

        if (closestMineableDistance > 4 && selectedUnit != null)
        {
            if (hasAssignedPath == false)
            {
                getPath(closestHexWithMineable.HexagonObject);
                hasAssignedPath = true;
            }

            smMoveShip();
        }
        else
        {
            smEstablishMine();
        }
    }

    //PRIOROTIZE TAKING OVER ENEMY OWNED PLANETS
    void sm4MinerLogic()
    {
        float closestMineableDistance = 0;
        Hexagon closestHexWithMineable = null;

        foreach (Hexagon hex in pManager.hexGridController.GridObjects)
        {
            if (hex != null)
            {
                if (hex.MineableInHex != null)
                {
                    if (pManager.hexGridController.GetNumberOfEnemyOwnedPlanets(smOpponent) > 0)
                    {
                        if (hex.MineableInHex.Owned == smOpponent)
                        {
                            if (hex.MineableInHex.GetType() == typeof(Planet))
                            {
                                if (pManager.hexGridController.isClosestToHex(hex, selectedUnit, smPlayer) == true)
                                {
                                    if (closestHexWithMineable == null)
                                    {
                                        closestHexWithMineable = hex;
                                        closestMineableDistance = Mathf.Abs(pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerX - hex.centerX) + Mathf.Abs(pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerZ - hex.centerZ);
                                    }
                                    else
                                    {
                                        if (closestMineableDistance > Mathf.Abs(pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerX - hex.centerX) + Mathf.Abs(pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerZ - hex.centerZ))
                                        {
                                            closestHexWithMineable = hex;
                                            closestMineableDistance = Mathf.Abs(pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerX - hex.centerX) + Mathf.Abs(pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerZ - hex.centerZ);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (pManager.hexGridController.GetNumberOfEnemyOwnedMinables(smOpponent) > 0)
                        {
                            if (hex.MineableInHex.Owned == smOpponent)
                            {
                                if (pManager.hexGridController.isClosestToHex(hex, selectedUnit, smPlayer) == true)
                                {
                                    if (closestHexWithMineable == null)
                                    {
                                        closestHexWithMineable = hex;
                                        closestMineableDistance = Mathf.Abs(pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerX - hex.centerX) + Mathf.Abs(pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerZ - hex.centerZ);
                                    }
                                    else
                                    {
                                        if (closestMineableDistance > Mathf.Abs(pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerX - hex.centerX) + Mathf.Abs(pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerZ - hex.centerZ))
                                        {
                                            closestHexWithMineable = hex;
                                            closestMineableDistance = Mathf.Abs(pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerX - hex.centerX) + Mathf.Abs(pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerZ - hex.centerZ);
                                        }
                                    }
                                }
                            }
                        }
                        else if (pManager.hexGridController.GetNumberOfUnownedPlanets() > 0)
                        {
                            if (hex.MineableInHex.Owned == null)
                            {
                                if (hex.MineableInHex.GetType() == typeof(Planet))
                                {
                                    if (pManager.hexGridController.isClosestToHex(hex, selectedUnit, smPlayer) == true)
                                    {
                                        if (closestHexWithMineable == null)
                                        {
                                            closestHexWithMineable = hex;
                                            closestMineableDistance = Mathf.Abs(pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerX - hex.centerX) + Mathf.Abs(pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerZ - hex.centerZ);
                                        }
                                        else
                                        {
                                            if (closestMineableDistance > Mathf.Abs(pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerX - hex.centerX) + Mathf.Abs(pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerZ - hex.centerZ))
                                            {
                                                closestHexWithMineable = hex;
                                                closestMineableDistance = Mathf.Abs(pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerX - hex.centerX) + Mathf.Abs(pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerZ - hex.centerZ);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (hex.MineableInHex.Owned == null)
                            {
                                if (pManager.hexGridController.isClosestToHex(hex, selectedUnit, smPlayer) == true)
                                {
                                    if (closestHexWithMineable == null)
                                    {
                                        closestHexWithMineable = hex;
                                        closestMineableDistance = Mathf.Abs(pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerX - hex.centerX) + Mathf.Abs(pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerZ - hex.centerZ);
                                    }
                                    else
                                    {
                                        if (closestMineableDistance > Mathf.Abs(pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerX - hex.centerX) + Mathf.Abs(pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerZ - hex.centerZ))
                                        {
                                            closestHexWithMineable = hex;
                                            closestMineableDistance = Mathf.Abs(pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerX - hex.centerX) + Mathf.Abs(pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerZ - hex.centerZ);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        if (closestMineableDistance > 5 && selectedUnit != null)
        {
            if (hasAssignedPath == false)
            {
                getPath(closestHexWithMineable.HexagonObject);
                hasAssignedPath = true;
            }

            smMoveShip();
        }
        else
        {
            smEstablishMine();
        }
    }

    void sm4TempMinerLogic()
    {
        float closestMineableDistance = 0;
        Hexagon closestHexWithMineable = null;

        foreach (Hexagon hex in pManager.hexGridController.GridObjects)
        {
            if (hex != null)
            {
                if (hex.MineableInHex != null)
                {
                    //CHECKS TO SEE IF THERE IS A MINEABLE IN CLOSE PROXIMITY
                    if (pManager.hexGridController.closestMineableInRange(smPlayer, selectedUnit, 120) != null)
                    {
                        Debug.Log("CLOSE MIABLE");
                        //CHECKS TO SEE IF MINEABLE FALLS INTO THAT PROXIMITY
                        if (Mathf.Abs(pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerX - hex.centerX) + Mathf.Abs(pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerZ - hex.centerZ) < 120)
                        {
                            if (hex.MineableInHex.Owned != smPlayer)
                            {
                                if (closestHexWithMineable == null)
                                {
                                    closestHexWithMineable = hex;
                                    closestMineableDistance = Mathf.Abs(pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerX - hex.centerX) + Mathf.Abs(pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerZ - hex.centerZ);
                                }
                                else
                                {
                                    if (closestMineableDistance > Mathf.Abs(pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerX - hex.centerX) + Mathf.Abs(pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerZ - hex.centerZ))
                                    {
                                        closestHexWithMineable = hex;
                                        closestMineableDistance = Mathf.Abs(pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerX - hex.centerX) + Mathf.Abs(pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerZ - hex.centerZ);
                                    }
                                }
                            }
                        }
                    }
                    else if (pManager.hexGridController.GetNumberOfEnemyOwnedMinables(smOpponent) > 0)
                    {
                        if (hex.MineableInHex.Owned == smOpponent)
                        {
                            //PRIOROTIZES ENEMY PLANET OVER ASTEROID
                            if (pManager.hexGridController.GetNumberOfEnemyOwnedPlanets(smOpponent) > 0)
                            {
                                if (hex.MineableInHex.GetType() == typeof(Planet))
                                {
                                    if (closestHexWithMineable == null)
                                    {
                                        closestHexWithMineable = hex;
                                        closestMineableDistance = Mathf.Abs(pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerX - hex.centerX) + Mathf.Abs(pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerZ - hex.centerZ);
                                    }
                                    else
                                    {
                                        if (closestMineableDistance > Mathf.Abs(pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerX - hex.centerX) + Mathf.Abs(pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerZ - hex.centerZ))
                                        {
                                            closestHexWithMineable = hex;
                                            closestMineableDistance = Mathf.Abs(pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerX - hex.centerX) + Mathf.Abs(pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerZ - hex.centerZ);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (closestHexWithMineable == null)
                                {
                                    closestHexWithMineable = hex;
                                    closestMineableDistance = Mathf.Abs(pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerX - hex.centerX) + Mathf.Abs(pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerZ - hex.centerZ);
                                }
                                else
                                {
                                    if (closestMineableDistance > Mathf.Abs(pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerX - hex.centerX) + Mathf.Abs(pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerZ - hex.centerZ))
                                    {
                                        closestHexWithMineable = hex;
                                        closestMineableDistance = Mathf.Abs(pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerX - hex.centerX) + Mathf.Abs(pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerZ - hex.centerZ);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (hex.MineableInHex.Owned != smPlayer)
                        {
                            Debug.Log("OTHER");
                            if (closestHexWithMineable == null)
                            {
                                closestHexWithMineable = hex;
                                closestMineableDistance = Mathf.Abs(pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerX - hex.centerX) + Mathf.Abs(pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerZ - hex.centerZ);
                            }
                            else
                            {
                                if (closestMineableDistance > Mathf.Abs(pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerX - hex.centerX) + Mathf.Abs(pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerZ - hex.centerZ))
                                {
                                    closestHexWithMineable = hex;
                                    closestMineableDistance = Mathf.Abs(pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerX - hex.centerX) + Mathf.Abs(pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerZ - hex.centerZ);
                                }
                            }
                        }
                    }
                }
            }
        }

        if (closestMineableDistance > 5)
        {
            if (hasAssignedPath == false)
            {
                getPath(closestHexWithMineable.HexagonObject);
                hasAssignedPath = true;
            }

            smMoveShip();
        }
        else
        {
            smEstablishMine();
        }
    }

    //FOCUSES ON DESTROYING THE CLOSEST SHIP AROUND
    void sm1DestroyerLogic()
    {
        float closestEnemyDistance = 0;
        Hexagon closestHexWithEnemy = null;

        foreach (Ship_Base eShip in smOpponent.Fleet)
        {
            if (closestHexWithEnemy == null)
            {
                closestHexWithEnemy = pManager.hexGridController.GridObjects[eShip.hexX, eShip.hexZ];
                closestEnemyDistance = Mathf.Abs(closestHexWithEnemy.centerX - pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerX) + Mathf.Abs(closestHexWithEnemy.centerZ - pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerZ);
            }
            else
            {
                if (closestEnemyDistance > Mathf.Abs(pManager.hexGridController.GridObjects[eShip.hexX, eShip.hexZ].centerX - pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerX) + Mathf.Abs(pManager.hexGridController.GridObjects[eShip.hexX, eShip.hexZ].centerZ - pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerZ))
                {
                    closestHexWithEnemy = pManager.hexGridController.GridObjects[eShip.hexX, eShip.hexZ];
                    closestEnemyDistance = Mathf.Abs(pManager.hexGridController.GridObjects[eShip.hexX, eShip.hexZ].centerX - pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerX) + Mathf.Abs(pManager.hexGridController.GridObjects[eShip.hexX, eShip.hexZ].centerZ - pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerZ);
                }
            }
        }

        if (closestEnemyDistance > 40)
        {
            if (hasAssignedPath == false)
            {
                getPath(closestHexWithEnemy.HexagonObject);
                hasAssignedPath = true;
            }

            smMoveShip();
        }
        else
        {
            smAttack(closestHexWithEnemy.HexagonObject);
            selectedUnit.actionsLeft = 0;
        }
    }

    //FOCUSES ON DESTROYING THE CLOSEST SHIP AROUND
    void sm2DestroyerLogic()
    {
        float closestEnemyDistance = 0;
        Hexagon closestHexWithEnemy = null;

        foreach (Ship_Base eShip in smOpponent.Fleet)
        {
            if (closestHexWithEnemy == null)
            {
                closestHexWithEnemy = pManager.hexGridController.GridObjects[eShip.hexX, eShip.hexZ];
                closestEnemyDistance = Mathf.Abs(closestHexWithEnemy.centerX - pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerX) + Mathf.Abs(closestHexWithEnemy.centerZ - pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerZ);
            }
            else
            {
                if (closestEnemyDistance > Mathf.Abs(pManager.hexGridController.GridObjects[eShip.hexX, eShip.hexZ].centerX - pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerX) + Mathf.Abs(pManager.hexGridController.GridObjects[eShip.hexX, eShip.hexZ].centerZ - pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerZ))
                {
                    closestHexWithEnemy = pManager.hexGridController.GridObjects[eShip.hexX, eShip.hexZ];
                    closestEnemyDistance = Mathf.Abs(pManager.hexGridController.GridObjects[eShip.hexX, eShip.hexZ].centerX - pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerX) + Mathf.Abs(pManager.hexGridController.GridObjects[eShip.hexX, eShip.hexZ].centerZ - pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerZ);
                }
            }
        }

        if (closestEnemyDistance > 40)
        {
            if (hasAssignedPath == false)
            {
                getPath(closestHexWithEnemy.HexagonObject);
                hasAssignedPath = true;
            }

            smMoveShip();
        }
        else
        {
            smAttack(closestHexWithEnemy.HexagonObject);
            selectedUnit.actionsLeft = 0;
        }
    }

    //FOCUSES ON DESTROYING MINERS
    void sm3DestroyerLogic()
    {
        float closestEnemyDistance = 0;
        Hexagon closestHexWithEnemy = null;

        foreach (Ship_Base eShip in smOpponent.Fleet)
        {
            if (pManager.fleetController.GetEnemyMinerCount(smOpponent) > 0)
            {
                if (eShip.GetType() == typeof(MiningShip))
                {
                    if (closestHexWithEnemy == null)
                    {
                        closestHexWithEnemy = pManager.hexGridController.GridObjects[eShip.hexX, eShip.hexZ];
                        closestEnemyDistance = Mathf.Abs(closestHexWithEnemy.centerX - pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerX) + Mathf.Abs(closestHexWithEnemy.centerZ - pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerZ);
                    }
                    else
                    {
                        if (closestEnemyDistance > Mathf.Abs(pManager.hexGridController.GridObjects[eShip.hexX, eShip.hexZ].centerX - pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerX) + Mathf.Abs(pManager.hexGridController.GridObjects[eShip.hexX, eShip.hexZ].centerZ - pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerZ))
                        {
                            closestHexWithEnemy = pManager.hexGridController.GridObjects[eShip.hexX, eShip.hexZ];
                            closestEnemyDistance = Mathf.Abs(pManager.hexGridController.GridObjects[eShip.hexX, eShip.hexZ].centerX - pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerX) + Mathf.Abs(pManager.hexGridController.GridObjects[eShip.hexX, eShip.hexZ].centerZ - pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerZ);
                        }
                    }
                }
            }
            else
            {
                if (closestHexWithEnemy == null)
                {
                    closestHexWithEnemy = pManager.hexGridController.GridObjects[eShip.hexX, eShip.hexZ];
                    closestEnemyDistance = Mathf.Abs(closestHexWithEnemy.centerX - pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerX) + Mathf.Abs(closestHexWithEnemy.centerZ - pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerZ);
                }
                else
                {
                    if (closestEnemyDistance > Mathf.Abs(pManager.hexGridController.GridObjects[eShip.hexX, eShip.hexZ].centerX - pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerX) + Mathf.Abs(pManager.hexGridController.GridObjects[eShip.hexX, eShip.hexZ].centerZ - pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerZ))
                    {
                        closestHexWithEnemy = pManager.hexGridController.GridObjects[eShip.hexX, eShip.hexZ];
                        closestEnemyDistance = Mathf.Abs(pManager.hexGridController.GridObjects[eShip.hexX, eShip.hexZ].centerX - pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerX) + Mathf.Abs(pManager.hexGridController.GridObjects[eShip.hexX, eShip.hexZ].centerZ - pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerZ);
                    }
                }
            }
        }

        if (closestEnemyDistance > 40)
        {
            if (hasAssignedPath == false)
            {
                getPath(closestHexWithEnemy.HexagonObject);
                hasAssignedPath = true;
            }

            smMoveShip();
        }
        else
        {
            smAttack(closestHexWithEnemy.HexagonObject);
            selectedUnit.actionsLeft = 0;
        }
    }

    //FOCUSES ON DESTROYING MINERS
    void sm4DestroyerLogic()
    {
        float closestEnemyDistance = 0;
        Hexagon closestHexWithEnemy = null;

        foreach (Ship_Base eShip in smOpponent.Fleet)
        {
            if (pManager.fleetController.GetEnemyMinerCount(smOpponent) > 0)
            {
                if (eShip.GetType() == typeof(MiningShip))
                {
                    if (closestHexWithEnemy == null)
                    {
                        closestHexWithEnemy = pManager.hexGridController.GridObjects[eShip.hexX, eShip.hexZ];
                        closestEnemyDistance = Mathf.Abs(closestHexWithEnemy.centerX - pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerX) + Mathf.Abs(closestHexWithEnemy.centerZ - pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerZ);
                    }
                    else
                    {
                        if (closestEnemyDistance > Mathf.Abs(pManager.hexGridController.GridObjects[eShip.hexX, eShip.hexZ].centerX - pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerX) + Mathf.Abs(pManager.hexGridController.GridObjects[eShip.hexX, eShip.hexZ].centerZ - pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerZ))
                        {
                            closestHexWithEnemy = pManager.hexGridController.GridObjects[eShip.hexX, eShip.hexZ];
                            closestEnemyDistance = Mathf.Abs(pManager.hexGridController.GridObjects[eShip.hexX, eShip.hexZ].centerX - pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerX) + Mathf.Abs(pManager.hexGridController.GridObjects[eShip.hexX, eShip.hexZ].centerZ - pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerZ);
                        }
                    }
                }
            }
            else
            {
                if (closestHexWithEnemy == null)
                {
                    closestHexWithEnemy = pManager.hexGridController.GridObjects[eShip.hexX, eShip.hexZ];
                    closestEnemyDistance = Mathf.Abs(closestHexWithEnemy.centerX - pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerX) + Mathf.Abs(closestHexWithEnemy.centerZ - pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerZ);
                }
                else
                {
                    if (closestEnemyDistance > Mathf.Abs(pManager.hexGridController.GridObjects[eShip.hexX, eShip.hexZ].centerX - pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerX) + Mathf.Abs(pManager.hexGridController.GridObjects[eShip.hexX, eShip.hexZ].centerZ - pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerZ))
                    {
                        closestHexWithEnemy = pManager.hexGridController.GridObjects[eShip.hexX, eShip.hexZ];
                        closestEnemyDistance = Mathf.Abs(pManager.hexGridController.GridObjects[eShip.hexX, eShip.hexZ].centerX - pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerX) + Mathf.Abs(pManager.hexGridController.GridObjects[eShip.hexX, eShip.hexZ].centerZ - pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].centerZ);
                    }
                }
            }
        }

        if (closestEnemyDistance > 40)
        {
            if (hasAssignedPath == false)
            {
                getPath(closestHexWithEnemy.HexagonObject);
                hasAssignedPath = true;
            }

            smMoveShip();
        }
        else
        {
            smAttack(closestHexWithEnemy.HexagonObject);
            selectedUnit.actionsLeft = 0;
        }
    }

    //AI PLAYER LOGIC
    //FOCUSES ON CREATING 2 SHIPS PER A TURN
    void smS1PlayerLogic()
    {
        if (smPlayer.TotalMoney > 800)
        {
            for (int i = 0; i < 2; i++)
            {
                if (Random.Range(0, 10) >= 6)
                {
                    switch (Random.Range(0, 1))
                    {
                        case 0:
                            pManager.fleetController.CreateNewShip("Miner", "SM Miner", 1, 10, pManager.PlayersTurn);
                            break;
                        case 1:
                            pManager.fleetController.CreateNewShip("Destroyer", "SM Destroyer", 20, 20, pManager.PlayersTurn);
                            break;
                    }
                }
            }
        }

        preformedAIPlayerActions = true;
        preformedShipActions = false;
    }

    //FOCUSES ON SAVING RESOURCES BY ONLY CREATING ONE SHIP
    void smS2PlayerLogic()
    {
        if (smPlayer.TotalMoney > 400)
        {
            if (Random.Range(0, 10) >= 8)
            {
                switch (Random.Range(0, 1))
                {
                    case 0:
                        pManager.fleetController.CreateNewShip("Miner", "SM Miner", 1, 10, pManager.PlayersTurn);
                        break;
                    case 1:
                        pManager.fleetController.CreateNewShip("Destroyer", "SM Destroyer", 20, 20, pManager.PlayersTurn);
                        break;
                }
            }
        }

        preformedAIPlayerActions = true;
        preformedShipActions = false;
    }

    //DOESNT FOCUS ON CREATING ANY SHIPS
    void smS3PlayerLogic()
    {
        if (pManager.fleetController.GetAllyMinerCount(smPlayer) < 2)
        {
            for (int i = 0; i < 2 - pManager.fleetController.GetAllyMinerCount(smPlayer); i++)
            {
                if (smPlayer.TotalMoney > 200)
                {
                    pManager.fleetController.CreateNewShip("Miner", "SM Miner", 1, 10, pManager.PlayersTurn);
                }
            }
        }

        if (pManager.fleetController.GetAllyDestroyerCount(smPlayer) < 2)
        {
            for (int i = 0; i < 2 - pManager.fleetController.GetAllyDestroyerCount(smPlayer); i++)
            {
                if (smPlayer.TotalMoney > 400)
                {
                    pManager.fleetController.CreateNewShip("Destroyer", "SM Destroyer", 20, 20, pManager.PlayersTurn);
                }
            }
        }

        preformedAIPlayerActions = true;
        preformedShipActions = false;
    }

    //DOESNT FOCUS ON CREATING ANY SHIPS
    void smS4PlayerLogic()
    {
        if (pManager.fleetController.GetAllyMinerCount(smPlayer) < 1)
        {
            for (int i = 0; i < 2 - pManager.fleetController.GetAllyMinerCount(smPlayer); i++)
            {
                if (smPlayer.TotalMoney > 200)
                {
                    pManager.fleetController.CreateNewShip("Miner", "SM Miner", 1, 10, pManager.PlayersTurn);
                }
            }
        }

        if (pManager.fleetController.GetAllyDestroyerCount(smPlayer) < 1)
        {
            for (int i = 0; i < 2 - pManager.fleetController.GetAllyDestroyerCount(smPlayer); i++)
            {
                if (smPlayer.TotalMoney > 400)
                {
                    pManager.fleetController.CreateNewShip("Destroyer", "SM Destroyer", 20, 20, pManager.PlayersTurn);
                }
            }
        }

        preformedAIPlayerActions = true;
        preformedShipActions = false;
    }
}
