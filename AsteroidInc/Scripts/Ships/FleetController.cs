using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleetController : MonoBehaviour
{
    PlayManager pManager;

    [Header("Player 1:")]
    public GameObject[] USSR_ShipObjects;
    [Header("Player 2:")]
    public GameObject[] USA_ShipObjects;

    [Header("Player Colours:")]
    public Material p1Colour;
    public Material p2Colour;

    //UNIT CONTROLS
    public Ship_Base selectedUnit = null;
    List<Hexagon> shipPath = new List<Hexagon>();
    int shipPathIndex = 1;
    public bool canMoveSelected = false;
    public bool canAttack = false;

    // Start is called before the first frame update
    void Start()
    {
        pManager = GameObject.FindGameObjectWithTag("PlayManager").GetComponent<PlayManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (pManager.smController.isStateMachineTurn == false)
        {
            ClickedUnselect();

            selectUnit();
            getShipPath();

            shipButtonEnabler();

            AttackEnemyShip();
        }
    }

    private void FixedUpdate()
    {
        if (pManager.smController.isStateMachineTurn == false)
        {
            moveSelected();
        }
    }

    //FLEET CONTROLS
    void selectUnit()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && canAttack == false && canMoveSelected == false) //MAYBE MOVE THIS BOOLEAN
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.tag == "Ship")
                {
                    if (selectedUnit != null)
                    {
                        if (hit.transform.gameObject != selectedUnit.shipObject)
                        {
                            if (shipOwned(hit.transform.gameObject, pManager.PlayersTurn) != null)
                            {
                                unselectCurrentUnit();

                                selectedUnit = shipOwned(hit.transform.gameObject, pManager.PlayersTurn);
                                MeshRenderer mRenderer = selectedUnit.selectedObject.GetComponent<MeshRenderer>();
                                mRenderer.enabled = true;

                                pManager.sUIController.showShipUI(selectedUnit);
                            }
                        }
                        else
                        {
                            if (shipOwned(hit.transform.gameObject, pManager.PlayersTurn) != null)
                            {
                                selectedUnit = shipOwned(hit.transform.gameObject, pManager.PlayersTurn);
                                MeshRenderer mRenderer = selectedUnit.selectedObject.GetComponent<MeshRenderer>();
                                mRenderer.enabled = true;

                                pManager.sUIController.showShipUI(selectedUnit);
                            }
                        }
                    }
                    else
                    {
                        if (shipOwned(hit.transform.gameObject, pManager.PlayersTurn) != null)
                        {
                            selectedUnit = shipOwned(hit.transform.gameObject, pManager.PlayersTurn);
                            MeshRenderer mRenderer = selectedUnit.selectedObject.GetComponent<MeshRenderer>();
                            mRenderer.enabled = true;

                            pManager.sUIController.showShipUI(selectedUnit);
                        }
                    }

                    if (selectedUnit.actionsLeft != 0)
                    {
                        pManager.sUIController.enableSkipButton();
                    }
                    else pManager.sUIController.disableSkipButton();
                }
                else if (hit.transform.gameObject.tag == "Hexagon")
                {
                    if (selectedUnit != null)
                    {
                        if (hit.transform.gameObject != selectedUnit.shipObject)
                        {
                            if (shipOwned(pManager.hexGridController.getShipObjectInHexagon(hit.transform.gameObject, pManager.PlayersTurn), pManager.PlayersTurn) != null)
                            {
                                unselectCurrentUnit();

                                selectedUnit = shipOwned(pManager.hexGridController.getShipObjectInHexagon(hit.transform.gameObject, pManager.PlayersTurn), pManager.PlayersTurn);
                                MeshRenderer mRenderer = selectedUnit.selectedObject.GetComponent<MeshRenderer>();
                                mRenderer.enabled = true;

                                pManager.sUIController.showShipUI(selectedUnit);
                            }
                        }
                        else
                        {
                            if (shipOwned(pManager.hexGridController.getShipObjectInHexagon(hit.transform.gameObject, pManager.PlayersTurn), pManager.PlayersTurn) != null)
                            {
                                selectedUnit = shipOwned(pManager.hexGridController.getShipObjectInHexagon(hit.transform.gameObject, pManager.PlayersTurn), pManager.PlayersTurn);
                                MeshRenderer mRenderer = selectedUnit.selectedObject.GetComponent<MeshRenderer>();
                                mRenderer.enabled = true;

                                pManager.sUIController.showShipUI(selectedUnit);
                            }
                        }
                    }
                    else
                    {
                        if (shipOwned(pManager.hexGridController.getShipObjectInHexagon(hit.transform.gameObject, pManager.PlayersTurn), pManager.PlayersTurn) != null)
                        {
                            selectedUnit = shipOwned(pManager.hexGridController.getShipObjectInHexagon(hit.transform.gameObject, pManager.PlayersTurn), pManager.PlayersTurn);
                            MeshRenderer mRenderer = selectedUnit.selectedObject.GetComponent<MeshRenderer>();
                            mRenderer.enabled = true;

                            pManager.sUIController.showShipUI(selectedUnit);
                        }
                    }
                }
                else if (hit.transform.gameObject.tag == "Planet" || hit.transform.tag == "Asteroid")
                {
                   
                }
            }
        }
    }

    public void SelectUnitFromShipSelector(Ship_Base sBase)
    {
        if (pManager.PlayersTurn == 1)
        {
            foreach (Ship_Base sB in pManager.p1Base.Fleet)
            {
                if (sB == sBase)
                {
                    if (selectedUnit != null)
                    {
                        unselectCurrentUnit();
                    }

                    selectedUnit = sB;
                    MeshRenderer mRenderer = selectedUnit.selectedObject.GetComponent<MeshRenderer>();
                    mRenderer.enabled = true;

                    pManager.sUIController.showShipUI(selectedUnit);
                }
            }
        }
        else if (pManager.PlayersTurn == 2)
        {
            foreach (Ship_Base sB in pManager.p2Base.Fleet)
            {
                if (sB == sBase)
                {
                    if (selectedUnit != null)
                    {
                        unselectCurrentUnit();
                    }

                    selectedUnit = sB;
                    MeshRenderer mRenderer = selectedUnit.selectedObject.GetComponent<MeshRenderer>();
                    mRenderer.enabled = true;

                    pManager.sUIController.showShipUI(selectedUnit);
                }
            }
        }
    }

    void unselectCurrentUnit()
    {
        if (selectedUnit != null)
        {
            MeshRenderer mRenderer = selectedUnit.selectedObject.GetComponent<MeshRenderer>();
            mRenderer.enabled = false;
            selectedUnit = null;
        }

        pManager.hexGridController.resetBoard();
    }

    public Ship_Base shipOwned(GameObject ship, int playerTurn)
    {
        if (playerTurn == 1)
        {
            foreach(Ship_Base sBase in pManager.p1Base.Fleet)
            {
                if (ship == sBase.shipObject)
                {
                    return sBase;
                }
            }
        }
        else if (playerTurn == 2)
        {
            foreach (Ship_Base sBase in pManager.p2Base.Fleet)
            {
                if (ship == sBase.shipObject)
                {
                    return sBase;
                }
            }
        }

        return null;
    }

    void getShipPath()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            if (selectedUnit != null)
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform.gameObject.tag == "Hexagon" && canAttack == false)
                    {
                        pManager.hexGridController.resetBoard();

                        GameObject start = pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].HexagonObject;
                        GameObject destination = hit.transform.gameObject;

                        shipPath = pManager.hexGridController.getShortestPath(start, destination);
                        pManager.hexGridController.changeSelectedMaterial(shipPath, selectedUnit.actionsLeft);

                        //Debug.Log(shipPath.Count);

                        if (selectedUnit.actionsLeft != 0)
                        {
                            pManager.sUIController.enableMoveButton();
                        }
                        else pManager.sUIController.disableMoveButton();
                    }
                    else if (hit.transform.tag == "Planet")
                    {
                        pManager.hexGridController.resetBoard();

                        PlanetController pController = hit.transform.gameObject.GetComponent<PlanetController>();
                        GameObject destination = null;

                        foreach (Hexagon h in pManager.hexGridController.GridObjects)
                        {
                            if (h != null)
                            {
                                if (h.MineableInHex == pController.baseClass)
                                {
                                    destination = h.HexagonObject;
                                }
                            }
                        }

                        GameObject start = pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].HexagonObject;

                        shipPath = pManager.hexGridController.getShortestPath(start, destination);
                        pManager.hexGridController.changeSelectedMaterial(shipPath, selectedUnit.actionsLeft);

                        if (selectedUnit.actionsLeft != 0)
                        {
                            pManager.sUIController.enableMoveButton();
                        }
                        else pManager.sUIController.disableMoveButton();
                    }
                    else if (hit.transform.gameObject.tag == "Asteroid")
                    {
                        pManager.hexGridController.resetBoard();

                        AsteroidController pController = hit.transform.gameObject.GetComponent<AsteroidController>();
                        GameObject destination = null;

                        foreach (Hexagon h in pManager.hexGridController.GridObjects)
                        {
                            if (h != null)
                            {
                                if (h.MineableInHex == pController.baseClass)
                                {
                                    destination = h.HexagonObject;
                                }
                            }
                        }

                        GameObject start = pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].HexagonObject;

                        shipPath = pManager.hexGridController.getShortestPath(start, destination);
                        pManager.hexGridController.changeSelectedMaterial(shipPath, selectedUnit.actionsLeft);

                        if (selectedUnit.actionsLeft != 0)
                        {
                            pManager.sUIController.enableMoveButton();
                        }
                        else pManager.sUIController.disableMoveButton();
                    }
                }
            }
        }
    }

    void moveSelected()
    {
        if (canMoveSelected == true)
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
                        }
                    }

                    selectedUnit.shipObject.transform.LookAt(new Vector3(shipPath[shipPathIndex].HexagonObject.transform.position.x, selectedUnit.shipObject.transform.position.y, shipPath[shipPathIndex].HexagonObject.transform.position.z));
                }
            }
            else
            {
                selectedUnit.hexX = pManager.hexGridController.getHexIndex(shipPath[shipPathIndex - 1])[0];
                selectedUnit.hexZ = pManager.hexGridController.getHexIndex(shipPath[shipPathIndex - 1])[1];

                shipButtonEnabler();

                shipPathIndex = 1;
                canMoveSelected = false;
                pManager.hexGridController.resetBoard();

                if (selectedUnit.actionsLeft == 0)
                    unselectCurrentUnit();
            }
        }
    }

    public void ShipMine()
    {
        if (pManager.PlayersTurn == 1)
        {
            pManager.p1Base.MineablesOwned.Add(pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].MineableInHex);
            pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].MineableInHex.Owned = pManager.p1Base;
            selectedUnit.actionsLeft--;

            shipButtonEnabler();
        }
        else
        {
            pManager.p2Base.MineablesOwned.Add(pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].MineableInHex);
            pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].MineableInHex.Owned = pManager.p2Base;
            selectedUnit.actionsLeft--;

            shipButtonEnabler();
        }

        if (selectedUnit.actionsLeft == 0)
        {
            unselectCurrentUnit();
        }

            pManager.hexGridController.resetBoard();
    }

    void AttackEnemyShip()
    {
        if (canAttack == true)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform.gameObject.tag == "Hexagon")
                    {
                        if (pManager.hexGridController.inSurroundingHex(hit.transform.gameObject) == true) //IF CHOSEN HEX IS IN SURROUNDING RANGE
                        {
                            if (pManager.PlayersTurn == 1) //IF ITS PLAYER 1 TURN
                            {
                                if (pManager.hexGridController.getShipObjectInHexagon(hit.transform.gameObject, 2) != null) //CHECKS TO SEE IF THERE IS AN ENEMY SHIP IN HEX CHOSEN
                                {
                                    Ship_Base tempEnemyShip = shipOwned(pManager.hexGridController.getShipObjectInHexagon(hit.transform.gameObject.gameObject, 2), 2);

                                    if (tempEnemyShip != null)
                                    {
                                        tempEnemyShip.health -= selectedUnit.attack;
                                        pManager.gsManager.FightPlay();
                                        pManager.vpManager.CreateDamagePopUp(tempEnemyShip.shipObject.transform.position, selectedUnit.attack);
                                        if (tempEnemyShip.health <= 0)
                                        {
                                            Destroy(tempEnemyShip.shipObject);
                                            pManager.p2Base.Fleet.Remove(tempEnemyShip);
                                            pManager.gsManager.ExplosionPlay();
                                        }

                                        selectedUnit.health -= (selectedUnit.defence - tempEnemyShip.attack);
                                        pManager.vpManager.CreateDamagePopUp(selectedUnit.shipObject.transform.position, tempEnemyShip.attack - selectedUnit.defence);
                                        if (selectedUnit.health <= 0)
                                        {
                                            Destroy(selectedUnit.shipObject);
                                            pManager.p1Base.Fleet.Remove(selectedUnit);
                                            pManager.gsManager.ExplosionPlay();
                                            selectedUnit = null;
                                        }
                                        else
                                        {
                                            selectedUnit.actionsLeft = 0;
                                            unselectCurrentUnit();
                                        }

                                        canAttack = false;
                                        pManager.hexGridController.resetBoard();
                                    }
                                }
                            }
                            else if (pManager.PlayersTurn == 2)
                            {
                                if (pManager.hexGridController.getShipObjectInHexagon(hit.transform.gameObject, 1) != null) //CHECKS TO SEE IF THERE IS AN ENEMY SHIP IN HEX CHOSEN
                                {
                                    Ship_Base tempEnemyShip = shipOwned(pManager.hexGridController.getShipObjectInHexagon(hit.transform.gameObject.gameObject, 1), 1);

                                    if (tempEnemyShip != null)
                                    {
                                        tempEnemyShip.health -= selectedUnit.attack;
                                        pManager.gsManager.FightPlay();
                                        pManager.vpManager.CreateDamagePopUp(tempEnemyShip.shipObject.transform.position, selectedUnit.attack);
                                        if (tempEnemyShip.health <= 0)
                                        {
                                            Destroy(tempEnemyShip.shipObject);
                                            pManager.p1Base.Fleet.Remove(tempEnemyShip);
                                            pManager.gsManager.ExplosionPlay();
                                        }

                                        selectedUnit.health -= (selectedUnit.defence - tempEnemyShip.attack);
                                        pManager.vpManager.CreateDamagePopUp(selectedUnit.shipObject.transform.position, tempEnemyShip.attack - selectedUnit.defence);
                                        if (selectedUnit.health <= 0)
                                        {
                                            Destroy(selectedUnit.shipObject);
                                            pManager.p2Base.Fleet.Remove(selectedUnit);
                                            pManager.gsManager.ExplosionPlay();
                                            selectedUnit = null;
                                        }
                                        else
                                        {
                                            selectedUnit.actionsLeft = 0;
                                            unselectCurrentUnit();
                                        }

                                        canAttack = false;
                                        pManager.hexGridController.resetBoard();
                                    }
                                }
                            }
                        }
                    }
                    else if (hit.transform.gameObject.tag == "Ship")
                    {
                        Ship_Base shipHit = shipOwned(hit.transform.gameObject, 1);
                        if (shipHit == null)
                            shipHit = shipOwned(hit.transform.gameObject, 2);
                        //CHECKS TO SEE IF SHIP HIT IS IN SURROUNDING HEX
                        if (pManager.hexGridController.inSurroundingHex(pManager.hexGridController.GridObjects[shipHit.hexX, shipHit.hexZ].HexagonObject) == true) //IF CHOSEN HEX IS IN SURROUNDING RANGE
                        {
                            if (pManager.PlayersTurn == 1) //IF ITS PLAYER 1 TURN
                            {
                                Ship_Base tempEnemyShip = shipOwned(hit.transform.gameObject, 2);

                                if (tempEnemyShip != null)
                                {
                                    tempEnemyShip.health -= selectedUnit.attack;
                                    pManager.gsManager.FightPlay();
                                    pManager.vpManager.CreateDamagePopUp(tempEnemyShip.shipObject.transform.position, selectedUnit.attack);
                                    if (tempEnemyShip.health <= 0)
                                    {
                                        Destroy(tempEnemyShip.shipObject);
                                        pManager.p2Base.Fleet.Remove(tempEnemyShip);
                                        pManager.gsManager.ExplosionPlay();
                                    }

                                    selectedUnit.health -= (selectedUnit.defence - tempEnemyShip.attack);
                                    pManager.vpManager.CreateDamagePopUp(selectedUnit.shipObject.transform.position, tempEnemyShip.attack - selectedUnit.defence);
                                    if (selectedUnit.health <= 0)
                                    {
                                        Destroy(selectedUnit.shipObject);
                                        pManager.p1Base.Fleet.Remove(selectedUnit);
                                        pManager.gsManager.ExplosionPlay();
                                        selectedUnit = null;
                                    }
                                    else
                                    {
                                        selectedUnit.actionsLeft = 0;
                                        unselectCurrentUnit();
                                    }

                                    canAttack = false;
                                    pManager.hexGridController.resetBoard();
                                }
                            }
                            else if (pManager.PlayersTurn == 2)
                            {
                                Ship_Base tempEnemyShip = shipOwned(hit.transform.gameObject, 1);

                                if (tempEnemyShip != null)
                                {
                                    tempEnemyShip.health -= selectedUnit.attack;
                                    pManager.gsManager.FightPlay();
                                    pManager.vpManager.CreateDamagePopUp(tempEnemyShip.shipObject.transform.position, selectedUnit.attack);
                                    if (tempEnemyShip.health <= 0)
                                    {
                                        Destroy(tempEnemyShip.shipObject);
                                        pManager.p1Base.Fleet.Remove(tempEnemyShip);
                                        pManager.gsManager.ExplosionPlay();
                                    }

                                    selectedUnit.health -= (selectedUnit.defence - tempEnemyShip.attack);
                                    pManager.vpManager.CreateDamagePopUp(selectedUnit.shipObject.transform.position, tempEnemyShip.attack - selectedUnit.defence);
                                    if (selectedUnit.health <= 0)
                                    {
                                        Destroy(selectedUnit.shipObject);
                                        pManager.p2Base.Fleet.Remove(selectedUnit);
                                        pManager.gsManager.ExplosionPlay();
                                        selectedUnit = null;
                                    }
                                    else
                                    {
                                        selectedUnit.actionsLeft = 0;
                                        unselectCurrentUnit();
                                    }

                                    canAttack = false;
                                    pManager.hexGridController.resetBoard();
                                }
                            }
                        }
                    }
                    else if (hit.transform.gameObject.tag == "HQ")
                    {
                        HQShip tempHQ = null;
                        if (hit.transform.gameObject == pManager.p1Base.HQ.shipObject)
                        {
                            tempHQ = pManager.p1Base.HQ;
                        }
                        else tempHQ = pManager.p2Base.HQ;

                        if (pManager.hexGridController.inSurroundingHex(pManager.hexGridController.GridObjects[tempHQ.hexX, tempHQ.hexZ].HexagonObject) == true) //IF CHOSEN HEX IS IN SURROUNDING RANGE
                        {
                            if (pManager.PlayersTurn == 1)
                            {
                                if (tempHQ != pManager.p1Base.HQ)
                                {
                                    tempHQ.health -= selectedUnit.attack;
                                    pManager.gsManager.FightPlay();
                                    pManager.vpManager.CreateDamagePopUp(tempHQ.shipObject.transform.position, selectedUnit.attack);
                                    if (tempHQ.health <= 0)
                                    {
                                        //END GAME CONDITION
                                    }

                                    selectedUnit.health -= (tempHQ.attack - selectedUnit.defence);
                                    pManager.vpManager.CreateDamagePopUp(selectedUnit.shipObject.transform.position, tempHQ.attack - selectedUnit.defence);
                                    if (selectedUnit.health <= 0)
                                    {
                                        Destroy(selectedUnit.shipObject);
                                        pManager.p1Base.Fleet.Remove(selectedUnit);
                                        selectedUnit = null;
                                    }
                                    else
                                    {
                                        selectedUnit.actionsLeft = 0;
                                        unselectCurrentUnit();
                                    }

                                    canAttack = false;
                                    pManager.hexGridController.resetBoard();
                                }
                            }
                            else if (pManager.PlayersTurn == 2)
                            {
                                if (tempHQ != pManager.p2Base.HQ)
                                {
                                    tempHQ.health -= selectedUnit.attack;
                                    pManager.gsManager.FightPlay();
                                    pManager.vpManager.CreateDamagePopUp(tempHQ.shipObject.transform.position, selectedUnit.attack);
                                    if (tempHQ.health <= 0)
                                    {
                                        //END GAME CONDITION
                                    }

                                    selectedUnit.health -= (tempHQ.attack - selectedUnit.defence);
                                    pManager.vpManager.CreateDamagePopUp(selectedUnit.shipObject.transform.position, tempHQ.attack - selectedUnit.defence);
                                    if (selectedUnit.health <= 0)
                                    {
                                        Destroy(selectedUnit.shipObject);
                                        pManager.p2Base.Fleet.Remove(selectedUnit);
                                    }
                                    else
                                    {
                                        selectedUnit.actionsLeft = 0;
                                        unselectCurrentUnit();
                                    }

                                    canAttack = false;
                                    pManager.hexGridController.resetBoard();
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    public void SkipShipActions()
    {
        selectedUnit.actionsLeft = 0;
        shipButtonEnabler();
        unselectCurrentUnit();
    }

    void shipButtonEnabler()
    {
        if (selectedUnit != null)
        {
            if (selectedUnit.actionsLeft != 0)
            {
                pManager.sUIController.enableSkipButton();

                if (selectedUnit.GetType() == typeof(MiningShip))
                {
                    if (pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].MineableInHex != null)
                    {
                        if (pManager.PlayersTurn == 1)
                        {
                            if (pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].MineableInHex.Owned != pManager.p1Base)
                            {
                                pManager.sUIController.enableActionButton();
                            }
                            else pManager.sUIController.disableActionButton();
                        }
                        else if (pManager.PlayersTurn == 2)
                        {
                            if (pManager.hexGridController.GridObjects[selectedUnit.hexX, selectedUnit.hexZ].MineableInHex.Owned != pManager.p2Base)
                            {
                                pManager.sUIController.enableActionButton();
                            }
                            else pManager.sUIController.disableActionButton();
                        }
                    }
                }
                else if (selectedUnit.GetType() == typeof(DestroyerShip))
                {
                    if (canAttack == false)
                    {
                        pManager.sUIController.enableActionButton();
                    }
                    else pManager.sUIController.disableActionButton();
                }
            }
            else
            {
                pManager.sUIController.disableMoveButton();
                pManager.sUIController.disableSkipButton();
                pManager.sUIController.disableActionButton();

                pManager.sUIController.hideShipUI();
            }
        }
    }

    public void ResetShipActions()
    {
        if (pManager.PlayersTurn == 1)
        {
            foreach (Ship_Base s in pManager.p1Base.Fleet)
            {
                s.actionsLeft = s.numberOfActionsPerTurn;
            }
        }
        else
        {
            foreach (Ship_Base s in pManager.p2Base.Fleet)
            {
                s.actionsLeft = s.numberOfActionsPerTurn;
            }
        }
    }

    public void ClickedUnselect()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (selectedUnit != null)
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform.gameObject.tag == "Board" || hit.transform.gameObject.tag == "Hexagon")
                    {
                        if (canAttack == false)
                        {
                            unselectCurrentUnit();
                            pManager.hexGridController.resetBoard();
                            pManager.sUIController.hideShipUI();
                        }
                    }
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (selectedUnit != null)
            {
                if (canAttack == false)
                {
                    unselectCurrentUnit();
                    pManager.hexGridController.resetBoard();
                    pManager.sUIController.hideShipUI();
                }
                else
                {
                    //unselectCurrentUnit();
                    //pManager.hexGridController.resetBoard();
                    //pManager.sUIController.hideShipUI();

                    canAttack = false;
                    pManager.hexGridController.resetBoard();
                }
            }
        }
    }

    //FLEET INITIATION
    public void SpawnStartingFleet()
    {
        if (GameManager.instance.p1Team == "USA")
        {
            GameObject tempP1HQ = Instantiate(USA_ShipObjects[0], pManager.hexGridController.GridObjects[pManager.hexGridController.p1HQStartX, pManager.hexGridController.p1HQStartZ].HexagonObject.transform.position, Quaternion.Euler(-90, 0, 0));
            GameObject tempP1HQselected = tempP1HQ.transform.GetChild(0).gameObject;
            GameObject tempP1Miner = Instantiate(USA_ShipObjects[1], pManager.hexGridController.GridObjects[pManager.hexGridController.p1HQStartX, pManager.hexGridController.p1HQStartZ + 1].HexagonObject.transform.position, Quaternion.identity);
            GameObject tempP1Minerselected = tempP1Miner.transform.GetChild(0).transform.GetChild(9).gameObject;
            GameObject tempP1Destroyer = Instantiate(USA_ShipObjects[2], pManager.hexGridController.GridObjects[pManager.hexGridController.p1HQStartX, pManager.hexGridController.p1HQStartZ - 1].HexagonObject.transform.position, Quaternion.identity);
            GameObject tempP1Destroyerselected = tempP1Destroyer.transform.GetChild(5).gameObject;

            pManager.p1Base.HQ = new HQShip("USA HQ Ship", "HQ Ship", 100, 20, 20, 0, pManager.hexGridController.p1HQStartX, pManager.hexGridController.p1HQStartZ, tempP1HQ, tempP1HQselected);
            pManager.p1Base.Fleet.Add(new MiningShip("USA Miner 1", "Miner", 10, 1, 10, 2, pManager.hexGridController.p1HQStartX, pManager.hexGridController.p1HQStartZ + 1, tempP1Miner, tempP1Minerselected));
            pManager.p1Base.Fleet.Add(new DestroyerShip("USA LittleBoi", "Destroyer", 50, 20, 20, 2, pManager.hexGridController.p1HQStartX, pManager.hexGridController.p1HQStartZ - 1, tempP1Destroyer, tempP1Destroyerselected));

            GameObject tempP2HQ = Instantiate(USSR_ShipObjects[0], pManager.hexGridController.GridObjects[pManager.hexGridController.p2HQStartX, pManager.hexGridController.p2HQStartZ].HexagonObject.transform.position, Quaternion.Euler(-90, 0, 0));
            GameObject tempP2HQselected = tempP2HQ.transform.GetChild(0).gameObject;
            GameObject tempP2Miner = Instantiate(USSR_ShipObjects[1], pManager.hexGridController.GridObjects[pManager.hexGridController.p2HQStartX, pManager.hexGridController.p2HQStartZ - 1].HexagonObject.transform.position, Quaternion.identity);
            GameObject tempP2Minerselected = tempP2Miner.transform.GetChild(0).transform.GetChild(9).gameObject;
            GameObject tempP2Destroyer = Instantiate(USSR_ShipObjects[2], pManager.hexGridController.GridObjects[pManager.hexGridController.p2HQStartX, pManager.hexGridController.p2HQStartZ + 1].HexagonObject.transform.position, Quaternion.identity);
            GameObject tempP2Destroyerselected = tempP2Destroyer.transform.GetChild(5).gameObject;

            pManager.p2Base.HQ = new HQShip("USSR HQ Ship", "HQ Ship", 100, 20, 20, 0, pManager.hexGridController.p2HQStartX, pManager.hexGridController.p2HQStartZ, tempP2HQ, tempP2HQselected);
            pManager.p2Base.Fleet.Add(new MiningShip("USSR Miner 1", "Miner", 10, 1, 10, 2, pManager.hexGridController.p2HQStartX, pManager.hexGridController.p2HQStartZ - 1, tempP2Miner, tempP2Minerselected));
            pManager.p2Base.Fleet.Add(new DestroyerShip("USSR BigBoi", "Destroyer", 50, 20, 20, 2, pManager.hexGridController.p2HQStartX, pManager.hexGridController.p2HQStartZ + 1, tempP2Destroyer, tempP2Destroyerselected));
        }
        else if (GameManager.instance.p1Team == "USSR")
        {
            GameObject tempP1HQ = Instantiate(USSR_ShipObjects[0], pManager.hexGridController.GridObjects[pManager.hexGridController.p1HQStartX, pManager.hexGridController.p1HQStartZ].HexagonObject.transform.position, Quaternion.Euler(-90, 0, 0));
            GameObject tempP1HQselected = tempP1HQ.transform.GetChild(0).gameObject;
            GameObject tempP1Miner = Instantiate(USSR_ShipObjects[1], pManager.hexGridController.GridObjects[pManager.hexGridController.p1HQStartX, pManager.hexGridController.p1HQStartZ + 1].HexagonObject.transform.position, Quaternion.identity);
            GameObject tempP1Minerselected = tempP1Miner.transform.GetChild(0).transform.GetChild(9).gameObject;
            GameObject tempP1Destroyer = Instantiate(USSR_ShipObjects[2], pManager.hexGridController.GridObjects[pManager.hexGridController.p1HQStartX, pManager.hexGridController.p1HQStartZ - 1].HexagonObject.transform.position, Quaternion.identity);
            GameObject tempP1Destroyerselected = tempP1Destroyer.transform.GetChild(5).gameObject;

            pManager.p1Base.HQ = new HQShip("USSR HQ Ship", "HQ Ship", 100, 20, 20, 0, pManager.hexGridController.p1HQStartX, pManager.hexGridController.p1HQStartZ, tempP1HQ, tempP1HQselected);
            pManager.p1Base.Fleet.Add(new MiningShip("USSR Miner 1", "Miner", 10, 1, 10, 2, pManager.hexGridController.p1HQStartX, pManager.hexGridController.p1HQStartZ + 1, tempP1Miner, tempP1Minerselected));
            pManager.p1Base.Fleet.Add(new DestroyerShip("USSR LittleBoi", "Destroyer", 50, 20, 20, 2, pManager.hexGridController.p1HQStartX, pManager.hexGridController.p1HQStartZ - 1, tempP1Destroyer, tempP1Destroyerselected));

            GameObject tempP2HQ = Instantiate(USA_ShipObjects[0], pManager.hexGridController.GridObjects[pManager.hexGridController.p2HQStartX, pManager.hexGridController.p2HQStartZ].HexagonObject.transform.position, Quaternion.Euler(-90, 0, 0));
            GameObject tempP2HQselected = tempP2HQ.transform.GetChild(0).gameObject;
            GameObject tempP2Miner = Instantiate(USA_ShipObjects[1], pManager.hexGridController.GridObjects[pManager.hexGridController.p2HQStartX, pManager.hexGridController.p2HQStartZ - 1].HexagonObject.transform.position, Quaternion.identity);
            GameObject tempP2Minerselected = tempP2Miner.transform.GetChild(0).transform.GetChild(9).gameObject;
            GameObject tempP2Destroyer = Instantiate(USA_ShipObjects[2], pManager.hexGridController.GridObjects[pManager.hexGridController.p2HQStartX, pManager.hexGridController.p2HQStartZ + 1].HexagonObject.transform.position, Quaternion.identity);
            GameObject tempP2Destroyerselected = tempP2Destroyer.transform.GetChild(5).gameObject;

            pManager.p2Base.HQ = new HQShip("USA HQ Ship", "HQ Ship", 100, 20, 20, 0, pManager.hexGridController.p2HQStartX, pManager.hexGridController.p2HQStartZ, tempP2HQ, tempP2HQselected);
            pManager.p2Base.Fleet.Add(new MiningShip("USA Miner 1", "Miner", 10, 1, 10, 2, pManager.hexGridController.p2HQStartX, pManager.hexGridController.p2HQStartZ - 1, tempP2Miner, tempP2Minerselected));
            pManager.p2Base.Fleet.Add(new DestroyerShip("USA BigBoi", "Destroyer", 50, 20, 20, 2, pManager.hexGridController.p2HQStartX, pManager.hexGridController.p2HQStartZ + 1, tempP2Destroyer, tempP2Destroyerselected));
        }
    }

    public void CreateNewShip(string type, string name, int Attack, int Defence, int pTurn)
    {
        if (type == "Miner")
        {
            if (pTurn == 1)
            {
                if (GameManager.instance.p1Team == "USA")
                {
                    GameObject tempP1Miner = Instantiate(USA_ShipObjects[1], pManager.hexGridController.GridObjects[pManager.hexGridController.p1HQStartX, pManager.hexGridController.p1HQStartZ + 1].HexagonObject.transform.position, Quaternion.identity);
                    GameObject tempP1Minerselected = tempP1Miner.transform.GetChild(0).transform.GetChild(9).gameObject;
                    pManager.p1Base.Fleet.Add(new MiningShip("USA " + name, "Miner", 10, Attack, Defence, 2, pManager.hexGridController.p1HQStartX, pManager.hexGridController.p1HQStartZ + 1, tempP1Miner, tempP1Minerselected));
                }
                else if (GameManager.instance.p1Team == "USSR")
                {
                    GameObject tempP1Miner = Instantiate(USSR_ShipObjects[1], pManager.hexGridController.GridObjects[pManager.hexGridController.p1HQStartX, pManager.hexGridController.p1HQStartZ + 1].HexagonObject.transform.position, Quaternion.identity);
                    GameObject tempP1Minerselected = tempP1Miner.transform.GetChild(0).transform.GetChild(9).gameObject;
                    pManager.p1Base.Fleet.Add(new MiningShip("USSR " + name, "Miner", 10, Attack, Defence, 2, pManager.hexGridController.p1HQStartX, pManager.hexGridController.p1HQStartZ + 1, tempP1Miner, tempP1Minerselected));
                }
            }
            else if (pTurn == 2)
            {
                if (GameManager.instance.p2Team == "USA")
                {
                    GameObject tempP2Miner = Instantiate(USA_ShipObjects[1], pManager.hexGridController.GridObjects[pManager.hexGridController.p2HQStartX, pManager.hexGridController.p2HQStartZ - 1].HexagonObject.transform.position, Quaternion.identity);
                    GameObject tempP2Minerselected = tempP2Miner.transform.GetChild(0).transform.GetChild(9).gameObject;
                    pManager.p2Base.Fleet.Add(new MiningShip("USA " + name, "Miner", 10, Attack, Defence, 2, pManager.hexGridController.p2HQStartX, pManager.hexGridController.p2HQStartZ - 1, tempP2Miner, tempP2Minerselected));
                }
                else if (GameManager.instance.p2Team == "USSR")
                {
                    GameObject tempP2Miner = Instantiate(USSR_ShipObjects[1], pManager.hexGridController.GridObjects[pManager.hexGridController.p2HQStartX, pManager.hexGridController.p2HQStartZ - 1].HexagonObject.transform.position, Quaternion.identity);
                    GameObject tempP2Minerselected = tempP2Miner.transform.GetChild(0).transform.GetChild(9).gameObject;
                    pManager.p2Base.Fleet.Add(new MiningShip("USSR " + name, "Miner", 10, Attack, Defence, 2, pManager.hexGridController.p2HQStartX, pManager.hexGridController.p2HQStartZ - 1, tempP2Miner, tempP2Minerselected));
                }
            }
        }
        else if (type == "Destroyer")
        {
            if (pTurn == 1)
            {
                if (GameManager.instance.p1Team == "USA")
                {
                    GameObject tempP1Destroyer = Instantiate(USA_ShipObjects[2], pManager.hexGridController.GridObjects[pManager.hexGridController.p1HQStartX, pManager.hexGridController.p1HQStartZ - 1].HexagonObject.transform.position, Quaternion.identity);
                    GameObject tempP1DestroyerSelected = tempP1Destroyer.transform.GetChild(5).gameObject;
                    pManager.p1Base.Fleet.Add(new DestroyerShip("USA " + name, "Destroyer", 40, Attack, Defence, 2, pManager.hexGridController.p1HQStartX, pManager.hexGridController.p1HQStartZ - 1, tempP1Destroyer, tempP1DestroyerSelected));
                }
                else if (GameManager.instance.p1Team == "USSR")
                {
                    GameObject tempP1Destroyer = Instantiate(USSR_ShipObjects[2], pManager.hexGridController.GridObjects[pManager.hexGridController.p1HQStartX, pManager.hexGridController.p1HQStartZ - 1].HexagonObject.transform.position, Quaternion.identity);
                    GameObject tempP1DestroyerSelected = tempP1Destroyer.transform.GetChild(5).gameObject;
                    pManager.p1Base.Fleet.Add(new DestroyerShip("USSR " + name, "Destroyer", 40, Attack, Defence, 2, pManager.hexGridController.p1HQStartX, pManager.hexGridController.p1HQStartZ - 1, tempP1Destroyer, tempP1DestroyerSelected));
                }
            }
            else if (pTurn == 2)
            {
                if (GameManager.instance.p2Team == "USA")
                {
                    GameObject tempP2Destroyer = Instantiate(USA_ShipObjects[2], pManager.hexGridController.GridObjects[pManager.hexGridController.p2HQStartX, pManager.hexGridController.p2HQStartZ + 1].HexagonObject.transform.position, Quaternion.identity);
                    GameObject tempP2DestroyerSelected = tempP2Destroyer.transform.GetChild(5).gameObject;
                    pManager.p2Base.Fleet.Add(new DestroyerShip("USA " + name, "Destroyer", 40, Attack, Defence, 2, pManager.hexGridController.p2HQStartX, pManager.hexGridController.p2HQStartZ + 1, tempP2Destroyer, tempP2DestroyerSelected));
                }
                else if (GameManager.instance.p2Team == "USSR")
                {
                    GameObject tempP2Destroyer = Instantiate(USSR_ShipObjects[2], pManager.hexGridController.GridObjects[pManager.hexGridController.p2HQStartX, pManager.hexGridController.p2HQStartZ + 1].HexagonObject.transform.position, Quaternion.identity);
                    GameObject tempP2DestroyerSelected = tempP2Destroyer.transform.GetChild(5).gameObject;
                    pManager.p2Base.Fleet.Add(new DestroyerShip("USSR " + name, "Destroyer", 40, Attack, Defence, 2, pManager.hexGridController.p2HQStartX, pManager.hexGridController.p2HQStartZ + 1, tempP2Destroyer, tempP2DestroyerSelected));
                }
            }
        }
    }

    //FLEET INFORMATION
    public int GetEnemyMinerCount(PlayerBase opponent)
    {
        int enemyMinerCounter = 0;

        foreach (Ship_Base eShip in opponent.Fleet)
        {
            if (eShip.GetType() == typeof(MiningShip))
            {
                enemyMinerCounter++;
            }
        }

        return enemyMinerCounter;
    }

    public int GetAllyMinerCount(PlayerBase pPlayer)
    {
        int minerCounter = 0;

        foreach (Ship_Base eShip in pPlayer.Fleet)
        {
            if (eShip.GetType() == typeof(MiningShip))
            {
                minerCounter++;
            }
        }

        return minerCounter;
    }

    public int GetAllyDestroyerCount(PlayerBase pPlayer)
    {
        int destroyerCounter = 0;

        foreach (Ship_Base eShip in pPlayer.Fleet)
        {
            if (eShip.GetType() == typeof(DestroyerShip))
            {
                destroyerCounter++;
            }
        }

        return destroyerCounter;
    }
}
