using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetController : MonoBehaviour
{

    [Header("Planet Variables:")]
    public string planetName;
    public int resourcesAvailable;
    public int resourcesperTurn;

    [Header("Inhabitants Variables:")]
    public int inhabitanceAttack;
    public int inhabitanceDefence;

    public Planet baseClass;

    float rotationSpeed;

    // Start is called before the first frame update
    void Start()
    {
        //baseClass = new Planet(planetName, resourcesAvailable, resourcesperTurn, inhabitanceDefence, inhabitanceAttack);
        rotationSpeed = Random.Range(10, 25);
    }

    // Update is called once per frame
    void Update()
    {
        PlanetRotation();
    }

    void PlanetRotation()
    {
        gameObject.transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }

    //public void enabledOwnership(int playerID)
    //{
    //    ownershipCircle.SetActive(true);

    //    ownershipCircle.GetComponent<MeshRenderer>().material = playerColors[playerID - 1];
    //}
}
