using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{

    [Header("Grid Varaiables:")]
    public GameObject gridObject;

    public bool gridEnabled = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HideGrid()
    {
        gridObject.gameObject.SetActive(false);
        gridEnabled = false;
    }

    public void showGrid()
    {
        gridObject.gameObject.SetActive(true);
        gridEnabled = true;
    }
}
