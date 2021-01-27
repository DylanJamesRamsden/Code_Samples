using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractingCanvasManager : MonoBehaviour
{
    [Header("SideBar Variables:")]
    public GameObject icObject;
    public RawImage SideBar;
    bool isBarOpen = false;
    public Button BuyMenuButton;
    public Button CentreCameraButton;

    PlayManager pManager;

    PlanateryTakeOverController ptoController;

    // Start is called before the first frame update
    void Start()
    {
        pManager = GameObject.FindGameObjectWithTag("PlayManager").GetComponent<PlayManager>();

        ptoController = GameObject.FindGameObjectWithTag("ptoManager").GetComponent<PlanateryTakeOverController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        sideBarMove();
    }

    public void SideBarControls()
    {
        if (isBarOpen == false)
        {
            isBarOpen = true;
        }
        else
        {
            isBarOpen = false;
        }
    }

    void sideBarMove()
   {
        if (isBarOpen == true)
        {
            if (SideBar.transform.position.x < 38f)
            {
                SideBar.transform.Translate(transform.right * 400f * Time.deltaTime);
            }
        }
        else
        {
            if (SideBar.transform.position.x > -24)
            {
                SideBar.transform.Translate(-transform.right * 400f * Time.deltaTime);
            }
        }
   }

    public void GridButtonClick()
    {

    }

    public void HideUI()
    {
        icObject.SetActive(false);
    }
}
