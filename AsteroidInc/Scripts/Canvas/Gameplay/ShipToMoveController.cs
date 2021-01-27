using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipToMoveController : MonoBehaviour
{

    public GameObject ShipsToMoveObject;

    [Header ("UI Componenets:")]
    public List<Button> ShipButtons;

    PlayManager pManager;

    List<Ship_Base> shipsToMove;

    // Start is called before the first frame update
    void Start()
    {
        pManager = GameObject.FindGameObjectWithTag("PlayManager").GetComponent<PlayManager>();
    }

    // Update is called once per frame
    void Update()
    {
        AssignShips();
    }

    void ShowUI()
    {
        ShipsToMoveObject.SetActive(true);
    }

    void HideUI()
    {
        ShipsToMoveObject.SetActive(false);
    }

    void AssignShips()
    {
        if (pManager.PlayersTurn == 1)
        {
            if (pManager.p1Base.GetType() == typeof(H_Player))
            {
                ShowUI();

                shipsToMove = new List<Ship_Base>();

                foreach(Ship_Base sBase in pManager.p1Base.Fleet)
                {
                    if (sBase.actionsLeft != 0)
                    {
                        shipsToMove.Add(sBase);
                    }
                }

                for (int i = 0; i < shipsToMove.Count; i++)
                {
                    ShipButtons[i].gameObject.SetActive(true);

                    if (shipsToMove[i].GetType() == typeof(MiningShip))
                    {
                        ShipButtons[i].GetComponentInChildren<Text>().text = "Miner";
                    }
                    else if (shipsToMove[i].GetType() == typeof(DestroyerShip))
                    {
                        ShipButtons[i].GetComponentInChildren<Text>().text = "Destroyer";
                    }
                }

                for (int y = shipsToMove.Count; y < 10; y++)
                {
                    ShipButtons[y].gameObject.SetActive(false);
                }
            }
            else
            {
                HideUI();
            }
        }
        else if (pManager.PlayersTurn == 2)
        {
            if (pManager.p2Base.GetType() == typeof(H_Player))
            {
                ShowUI();

                shipsToMove = new List<Ship_Base>();

                foreach (Ship_Base sBase in pManager.p2Base.Fleet)
                {
                    if (sBase.actionsLeft > 0)
                    {
                        shipsToMove.Add(sBase);
                    }
                }

                for (int i = 0; i < shipsToMove.Count; i++)
                {
                    ShipButtons[i].gameObject.SetActive(true);

                    if (shipsToMove[i].GetType() == typeof(MiningShip))
                    {
                        ShipButtons[i].GetComponentInChildren<Text>().text = "Miner";
                    }
                    else if (shipsToMove[i].GetType() == typeof(MiningShip))
                    {
                        ShipButtons[i].GetComponentInChildren<Text>().text = "Destroyer";
                    }
                }

                for (int y = shipsToMove.Count; y < 10; y++)
                {
                    ShipButtons[y].gameObject.SetActive(false);
                }
            }
            else
            {
                HideUI();
            }
        }
    }

    public void shipButtonClick(int index)
    {
        pManager.fleetController.SelectUnitFromShipSelector(shipsToMove[index]);
    }
}
