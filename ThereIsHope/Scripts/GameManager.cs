using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public List<string> playerDecisionList;

    public PlayerDecisions pDecisions;

    public AudioSource menuMusic;

    public bool hasSword = false;

    public int level = 1;
    public bool isLevelDone = false;

    public string skeletonKeyCode = "74358902";
    public bool skeletonKeyEnabled = false;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void populatePlayerDecisions()
    {
        playerDecisionList = pDecisions.returnAllPlayerChoices();
    }

    public void resetPlayerDecisions()
    {
        //wpDecisions.
        playerDecisionList = new List<string>();
    }
}
