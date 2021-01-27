using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexCollision : MonoBehaviour
{
    PlayManager pManager;
    bool hasIncremementedCount = false;
    public List<GameObject> surroundingHexagons;

    // Start is called before the first frame update
    void Awake()
    {
        pManager = GameObject.FindGameObjectWithTag("PlayManager").GetComponent<PlayManager>();
        surroundingHexagons = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Hexagon")
        {
            surroundingHexagons.Add(other.gameObject);

            if (hasIncremementedCount == false)
            {
                hasIncremementedCount = true;
                pManager.hexGridController.surroundingFoundCount++;
            }
        }
    }
}
