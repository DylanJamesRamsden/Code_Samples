using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlanateryTakeOverController : MonoBehaviour
{

    [Header("Assualt Variables")]
    public RawImage assualtWindow;

    public Text playerAttack;
    public Text playerDefence;
    public Text planetAttack;
    public Text planetDefence;

    public Text planetName;
    public Text planetResources;
    public Text planetResourcesPerTurn;

    PlayManager pManager;

    // Start is called before the first frame update
    void Start()
    {
        pManager = GameObject.FindGameObjectWithTag("PlayManager").GetComponent<PlayManager>();

        HideWindow();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void HideWindow()
    {
        assualtWindow.gameObject.SetActive(false);
    }

    //public void ShowWindow()
    //{
    //    assualtWindow.gameObject.SetActive(true);

    //    if (pManager.PlayersTurn == 1)
    //    {
    //        playerAttack.text = "Attack: " + pManager.p1Controller.pBase.Attack.ToString();
    //        playerDefence.text = "Defence: " + pManager.p1Controller.pBase.Shield.ToString();

    //        Planet pTemp = pManager.p1Controller.mineableObject.GetComponent<PlanetController>().baseClass;
    //        planetAttack.text = "Attack: " + pTemp.PlanetAttack.ToString();
    //        planetDefence.text = "Defence: " + pTemp.PlanetDefence.ToString();

    //        planetName.text = pTemp.Name;
    //        planetResources.text = "Resources: " + pTemp.ResourcesAvailable.ToString();
    //        planetResourcesPerTurn.text = "Resources/pt: " + pTemp.ResourcePerTurn.ToString();
    //    }
    //    else
    //    {
    //        playerAttack.text = "Attack: " + pManager.p2Controller.pBase.Attack.ToString();
    //        playerDefence.text = "Defence: " + pManager.p2Controller.pBase.Shield.ToString();

    //        Planet pTemp = pManager.p2Controller.mineableObject.GetComponent<PlanetController>().baseClass;
    //        planetAttack.text = "Attack: " + pTemp.PlanetAttack.ToString();
    //        planetDefence.text = "Defence: " + pTemp.PlanetDefence.ToString();

    //        planetName.text = pTemp.Name;
    //        planetResources.text = "Resources: " + pTemp.ResourcesAvailable.ToString();
    //        planetResourcesPerTurn.text = "Resources/pt: " + pTemp.ResourcePerTurn.ToString();
    //    }
    //}

    //public void TakeOverPlanet()
    //{
    //    if (pManager.PlayersTurn == 1)
    //    {
    //        pManager.p1Base.PlayerOvertake();
    //        HideWindow();

    //        pManager.icManager.OverthrowButton.interactable = false;
    //    }
    //    else if (pManager.PlayersTurn == 2)
    //    {
    //        pManager.p2Controller.PlayerOvertake();
    //        HideWindow();

    //        pManager.icManager.OverthrowButton.interactable = false;
    //    }
    //}

    public void CancelAction()
    {
        HideWindow();
    }
}
