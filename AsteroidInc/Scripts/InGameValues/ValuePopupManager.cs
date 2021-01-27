using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValuePopupManager : MonoBehaviour
{
    PlayManager pManager;

    [Header("Value Prefabs:")]
    public GameObject ValueObject;

    // Start is called before the first frame update
    void Start()
    {
        pManager = GameObject.FindGameObjectWithTag("PlayManager").GetComponent<PlayManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateDamagePopUp(Vector3 position, int Value)
    {
        if (pManager.PlayersTurn == 1)
        {
            GameObject temp = Instantiate(ValueObject, new Vector3(position.x, position.y + 5, position.z + 15), Quaternion.Euler(55, 0, 0));
            ValuePopUpController pController = temp.GetComponent<ValuePopUpController>();
            pController.StartSetUp(Value, "damage");
        }
        else if (pManager.PlayersTurn == 2)
        {
            GameObject temp = Instantiate(ValueObject, new Vector3(position.x, position.y + 5, position.z - 15), Quaternion.Euler(55, 180, 0));
            ValuePopUpController pController = temp.GetComponent<ValuePopUpController>();
            pController.StartSetUp(Value, "damage");
        }
    }

    public void CreateResourcePopup(Vector3 position, int Value)
    {
        if (pManager.PlayersTurn == 1)
        {
            GameObject temp = Instantiate(ValueObject, new Vector3(position.x, position.y + 5, position.z + 15), Quaternion.Euler(55, 0, 0));
            ValuePopUpController pController = temp.GetComponent<ValuePopUpController>();
            pController.StartSetUp(Value, "resource");
        }
        else if (pManager.PlayersTurn == 2)
        {
            GameObject temp = Instantiate(ValueObject, new Vector3(position.x, position.y + 5, position.z - 15), Quaternion.Euler(55, 180, 0));
            ValuePopUpController pController = temp.GetComponent<ValuePopUpController>();
            pController.StartSetUp(Value, "resource");
        }
    }

    //PRESET POP_UPS
    public void DisplayResourcesEarned()
    {
        if (pManager.PlayersTurn == 1)
        {
            foreach (GameObject gO in GameObject.FindGameObjectsWithTag("Planet"))
            {
                PlanetController pController = gO.GetComponent<PlanetController>();
                if (pController.baseClass.Owned == pManager.p1Base)
                {
                    pManager.vpManager.CreateResourcePopup(gO.transform.position, pController.baseClass.ResourcePerTurn);
                }
            }

            foreach (GameObject gO in GameObject.FindGameObjectsWithTag("Asteroid"))
            {
                AsteroidController aController = gO.GetComponent<AsteroidController>();
                if (aController.baseClass.Owned == pManager.p1Base)
                {
                    pManager.vpManager.CreateResourcePopup(gO.transform.position, aController.baseClass.ResourcePerTurn);
                }
            }

            pManager.p1Base.GenerateIncomePerTurn();
        }
        else if (pManager.PlayersTurn == 2)
        {
            foreach (GameObject gO in GameObject.FindGameObjectsWithTag("Planet"))
            {
                PlanetController pController = gO.GetComponent<PlanetController>();
                if (pController.baseClass.Owned == pManager.p2Base)
                {
                    pManager.vpManager.CreateResourcePopup(gO.transform.position, pController.baseClass.ResourcePerTurn);
                }
            }

            foreach (GameObject gO in GameObject.FindGameObjectsWithTag("Asteroid"))
            {
                AsteroidController aController = gO.GetComponent<AsteroidController>();
                if (aController.baseClass.Owned == pManager.p2Base)
                {
                    pManager.vpManager.CreateResourcePopup(gO.transform.position, aController.baseClass.ResourcePerTurn);
                }
            }

            pManager.p2Base.GenerateIncomePerTurn();
        }
    }
}
