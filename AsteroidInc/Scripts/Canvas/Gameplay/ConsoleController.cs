using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConsoleController : MonoBehaviour
{

    [Header("Console Variables:")]
    public GameObject consoleObject;
    public Text consoleInformationText;

    bool isShowing = true;

    PlayManager pManager;

    // Start is called before the first frame update
    void Start()
    {
        pManager = GameObject.FindGameObjectWithTag("PlayManager").GetComponent<PlayManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (pManager.gameOver == false)
        {
            showHideConsole();
        }

        showGameInfo();
    }

    void showHideConsole()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (isShowing == true)
            {
                isShowing = false;

                consoleObject.SetActive(false);
            }
            else if (isShowing == false)
            {
                isShowing = true;

                consoleObject.SetActive(true);

            }
        }
    }

    public void HideConsole()
    {
        consoleObject.SetActive(false);
    }

    void showGameInfo()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.gameObject.tag == "Hexagon")
            {
                Hexagon tempHex = pManager.hexGridController.FindHexagonBasedOnGameobject(hit.transform.gameObject);
                consoleInformationText.text = tempHex.HexagonName;
            }
            else if (hit.transform.gameObject.tag == "Planet")
            {
                PlanetController pController = hit.transform.gameObject.GetComponent<PlanetController>();
                consoleInformationText.text = pController.baseClass.getInfo();
            }
            else if (hit.transform.gameObject.tag == "Asteroid")
            {
                AsteroidController aController = hit.transform.gameObject.GetComponent<AsteroidController>();
                consoleInformationText.text = aController.baseClass.getInfo();
            }
            else if (hit.transform.gameObject.tag == "Ship")
            {
                Ship_Base tempShip = null;

                foreach(Ship_Base sBase in pManager.p1Base.Fleet)
                {
                    if (sBase.shipObject == hit.transform.gameObject)
                    {
                        tempShip = sBase;
                    }
                }

                if (tempShip == null)
                {
                    foreach (Ship_Base sBase in pManager.p2Base.Fleet)
                    {
                        if (sBase.shipObject == hit.transform.gameObject)
                        {
                            tempShip = sBase;
                        }
                    }
                }

                if (tempShip != null)
                {
                    consoleInformationText.text = tempShip.getInfo();
                }
            }
            else if (hit.transform.gameObject.tag == "HQ")
            {
                Ship_Base tempHQ = null;

                if (hit.transform.gameObject == pManager.p1Base.HQ.shipObject)
                {
                    tempHQ = pManager.p1Base.HQ;
                }
                else
                {
                    tempHQ = pManager.p2Base.HQ;
                }

                consoleInformationText.text = tempHQ.getInfo();
            }
        }
    }
}
