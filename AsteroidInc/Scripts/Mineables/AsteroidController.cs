using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : MonoBehaviour
{

    [Header("Asteroid Variables:")]
    public int resourcesAvailable;
    public int resourcesperTurn;

    public Asteroid baseClass;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string AsteroidNamingConvetion()
    {
        return "AST" + GameObject.FindGameObjectsWithTag("Asteroid").Length;
    }

    //public void enabledOwnership(int playerID)
    //{
    //    ownershipCircle.SetActive(true);

    //    ownershipCircle.GetComponent<MeshRenderer>().material = playerColors[playerID - 1];
    //}
}
