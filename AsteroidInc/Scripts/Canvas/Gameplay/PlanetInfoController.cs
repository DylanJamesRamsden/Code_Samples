using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlanetInfoController : MonoBehaviour
{

    [Header("UI Variables:")]
    public Canvas infoCanvas;

    public Text MineableName;
    public Text ResourceValue;
    public Text ResourcePerTurnValue;
    public Text OwnershipValue;

    public RawImage PlanetInfoWindow;

    PlayManager pManager;

    GameObject mineableToView = null;

    float secCounter = 0;

    PlanetController p;
    AsteroidController a;

    // Start is called before the first frame update
    void Start()
    {
        pManager = GameObject.FindGameObjectWithTag("PlayManager").GetComponent<PlayManager>();

        hidePlanetInfo();
    }

    // Update is called once per frame
    void Update()
    {
        //detectMineable();
        //showMineableInfo();
    }

    void detectMineable()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.gameObject.tag != "Board")
            {
                if (hit.transform.gameObject.tag == "Planet" || hit.transform.gameObject.tag == "Asteroid")
                {
                    if (mineableToView == null)
                    {
                        mineableToView = hit.transform.gameObject;
                    }
                    else
                    {
                        if (hit.transform.gameObject == mineableToView)
                        {
                            secCounter += Time.deltaTime;
                        }
                    }
                }
            }
            else
            {
                secCounter = 0;
                mineableToView = null;

                hidePlanetInfo();
            }
        }
    }

    void showMineableInfo()
    {
        if (secCounter > 1)
        {
            showPlanetInfoWindow();

            infoCanvas.transform.position = new Vector3(mineableToView.transform.position.x, 0, mineableToView.transform.position.z + 15);
        }
    }

    void showPlanetInfoWindow()
    {
        PlanetInfoWindow.gameObject.SetActive(true);

        if (mineableToView.tag == "Planet")
        {
            p = mineableToView.GetComponent<PlanetController>();
            MineableName.text = p.baseClass.Name;
            ResourceValue.text = p.baseClass.ResourcesAvailable.ToString();
            ResourcePerTurnValue.text = p.baseClass.ResourcePerTurn.ToString();

            if (p.baseClass.Owned != null)
            {
                if (p.baseClass.Owned.playerID == 1)
                {
                    OwnershipValue.text = "Player 1";
                }
                else if (p.baseClass.Owned.playerID == 2)
                {
                    OwnershipValue.text = "Player 2";
                }
            }
            else
            {
                OwnershipValue.text = "Unowned";
            }

        }
        else if (mineableToView.tag == "Asteroid")
        {
            a = mineableToView.GetComponent<AsteroidController>();
            MineableName.text = a.baseClass.Name;
            ResourceValue.text = a.baseClass.ResourcesAvailable.ToString();
            ResourcePerTurnValue.text = a.baseClass.ResourcePerTurn.ToString();

            if (a.baseClass.Owned != null)
            {
                if (a.baseClass.Owned.playerID == 1)
                {
                    OwnershipValue.text = "Player 1";
                }
                else if (a.baseClass.Owned.playerID == 2)
                {
                    OwnershipValue.text = "Player 2";
                }
            }
            else
            {
                OwnershipValue.text = "Unowned";
            }
        }
    }

    void hidePlanetInfo()
    {
        PlanetInfoWindow.gameObject.SetActive(false);
    }
}
