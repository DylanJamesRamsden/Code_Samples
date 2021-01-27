using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGameCanvasController : MonoBehaviour
{
    public GameObject egObject;
    PlayManager pManager;

    [Header("UI Componenets")]
    public Text winningText;

    // Start is called before the first frame update
    void Start()
    {
        pManager = GameObject.FindGameObjectWithTag("PlayManager").GetComponent<PlayManager>();
        hideUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void showUI()
    {
        egObject.SetActive(true);

        if (pManager.winner == 1)
        {
            winningText.text = GameManager.instance.p1Team + " HAS WON!";
        }
        else if (pManager.winner == 2)
        {
            winningText.text = GameManager.instance.p2Team + " HAS WON!";
        }
        else
        {
            winningText.text = "THERE IS NO WINNNER!";
        }
    }

    void hideUI()
    {
        egObject.SetActive(false);
    }
}
